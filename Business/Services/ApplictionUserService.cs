using AutoMapper;
using Core.Interfaces;
using Core.IRepositories;
using Core.ViewModels;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Logging;

namespace Business.Services
{
    /// <summary>
    /// Service responsible for user-related business logic, including login, registration, and token generation.
    /// </summary>
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository dependency.</param>
        /// <param name="mapper">The mapper dependency for object transformations.</param>
        /// <param name="configuration">The application configuration dependency.</param>
        public ApplicationUserService(IApplicationUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// Logs in a user by verifying credentials and generating a JWT token.
        /// </summary>
        /// <param name="loginViewModel">The login information.</param>
        /// <returns>A JWT token or an error message.</returns>
        public async Task<string> LoginAsync(LoginViewModel loginViewModel)
        {
            try
            {
                LoggerHelper.LogInfo("Login attempt for user: " + loginViewModel?.Email);

                var user = await _userRepository.FindByEmailAsynch(loginViewModel.Email);
                if (user == null)
                {
                    LoggerHelper.LogInfo("User not found for email: " + loginViewModel?.Email);
                    return "User not found";
                }

                var passwordValid = await _userRepository.CheckPasswordAsynch(user, loginViewModel.Password);
                if (!passwordValid)
                {
                    LoggerHelper.LogInfo("Invalid password for user: " + loginViewModel?.Email);
                    return "Invalid password";
                }

                var signInResult = await _userRepository.SignInUserAsync(user, loginViewModel.Password);
                if (!signInResult)
                {
                    LoggerHelper.LogInfo("Login failed for user: " + loginViewModel?.Email);
                    return "Login failed";
                }

                LoggerHelper.LogInfo("Login successful for user: " + loginViewModel?.Email);
                return GenerateJwtToken(user);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during login for user: " + loginViewModel?.Email, ex));
                return "An error occurred during login. Please try again later.";
            }
        }

        /// <summary>
        /// Registers a new user and generates a JWT token.
        /// </summary>
        /// <param name="registerViewModel">The registration information.</param>
        /// <returns>A JWT token or an error message.</returns>
        public async Task<string> RegisterAsync(RegisterViewModel registerViewModel)
        {
            try
            {
                LoggerHelper.LogInfo("Registration attempt for user: " + registerViewModel?.Email);

                var user = _mapper.Map<ApplicationUser>(registerViewModel);
                var registrationSuccess = await _userRepository.RegisterUserAsynch(user, registerViewModel.Password);

                if (!registrationSuccess)
                {
                    LoggerHelper.LogInfo("Registration failed for user: " + registerViewModel?.Email);
                    return "Registration failed";
                }

                LoggerHelper.LogInfo("Registration successful for user: " + registerViewModel?.Email);
                return GenerateJwtToken(user);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during registration for user: " + registerViewModel?.Email, ex));
                return "An error occurred during registration. Please try again later.";
            }
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        public async Task LogoutAsync()
        {
            try
            {
                LoggerHelper.LogInfo("Logout attempt initiated.");
                await _userRepository.SignOutAsync();
                LoggerHelper.LogInfo("Logout successful.");
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during logout.", ex));
                throw; // Re-throw the exception for higher-level handling if needed
            }
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>A JWT token.</returns>
        private string GenerateJwtToken(ApplicationUser user)
        {
            try
            {
                LoggerHelper.LogInfo("Token generation initiated for user: " + user?.Email);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                LoggerHelper.LogInfo("Token generation successful for user: " + user?.Email);
                return tokenString;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during token generation for user: " + user?.Email, ex));
                throw;
            }
        }
    }
}
