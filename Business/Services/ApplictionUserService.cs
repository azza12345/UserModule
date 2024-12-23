﻿using AutoMapper;
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
using Core;

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



                LoggerHelper.LogInfo("Login successful for user: " + loginViewModel?.Email);
                 return GenerateJwtToken(user);
                
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during login for user: " + loginViewModel?.Email, ex));
                return "An error occurred during login. Please try again later.";
            }
        }

        ///// <summary>
        ///// Registers a new user and generates a JWT token.
        ///// </summary>
        ///// <param name="registerViewModel">The registration information.</param>
        ///// <returns>A JWT token or an error message.</returns>
        //public async Task<string> RegisterAsync(RegisterViewModel registerViewModel)
        //{
        //    try
        //    {
        //        LoggerHelper.LogInfo("Registration attempt for user: " + registerViewModel?.Email);

        //        var user = _mapper.Map<User>(registerViewModel);
        //        var registrationSuccess = await _userRepository.RegisterUserAsynch(user, registerViewModel.Password);

        //        if (!registrationSuccess)
        //        {
        //            LoggerHelper.LogInfo("Registration failed for user: " + registerViewModel?.Email);
        //            return "Registration failed";
        //        }

        //        LoggerHelper.LogInfo("Registration successful for user: " + registerViewModel?.Email);
        //        return GenerateJwtToken(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerHelper.LogError(new Exception("An error occurred during registration for user: " + registerViewModel?.Email, ex));
        //        return "An error occurred during registration. Please try again later.";
        //    }
        //}


        public async Task<ApiResponse> RegisterAsync(RegisterViewModel registerViewModel)
        {
            try
            {
                LoggerHelper.LogInfo("Registration attempt for user: " + registerViewModel?.Email);

                var user = _mapper.Map<User>(registerViewModel);
                var registrationSuccess = await _userRepository.RegisterUserAsynch(user, registerViewModel.Password);

                if (!registrationSuccess)
                {
                    LoggerHelper.LogInfo("Registration failed for user: " + registerViewModel?.Email);
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Registration failed",
                        StatusCode = CustomStatusCode.BadRequest 
                    };
                }

                LoggerHelper.LogInfo("Registration successful for user: " + registerViewModel?.Email);
                return new ApiResponse
                {
                    Success = true,
                    Message = "Registration successful",
                    StatusCode = CustomStatusCode.Success
                };
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during registration for user: " + registerViewModel?.Email, ex));
                return new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred during registration. Please try again later.",
                    StatusCode = CustomStatusCode.InternalServerError
                };
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
                throw;
            }
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>A JWT token.</returns>
        //private string GenerateJwtToken(User user)
        //{
        //    try
        //    {
        //        LoggerHelper.LogInfo("Token generation initiated for user: " + user?.Email);

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        //      //  var defaultAction = "Read , Create, Delete , update"; 
        //     //   var defaultView = "User Dashboard";
        //        var metersActionPermissions = "Read , Create , Delete , Update";
        //        var metersViewPermissions = "AllMeters";


        //        var userPortalAction = "Read , Create , Delete , Update";
        //        var userPortalView = "UserDashboard";

        //        var claims = new[]
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //            new Claim(ClaimTypes.Name, user.UserName),
        //           // new Claim("Action", defaultAction), 
        //          //  new Claim("View", defaultView),
        //            new Claim("MetersAction",metersActionPermissions),
        //             new Claim("UserPortalAction",userPortalAction),
        //             new Claim("MetersView",metersViewPermissions),
        //             new Claim("UserPortalView",userPortalView)


        //        };

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(claims),
        //            Expires = DateTime.UtcNow.AddHours(24),
        //            Issuer = _configuration["Jwt:Issuer"],
        //            Audience = _configuration["Jwt:Audience"],
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        var tokenString = tokenHandler.WriteToken(token);

        //        LoggerHelper.LogInfo("Token generation successful for user: " + user?.Email);
        //        return tokenString;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerHelper.LogError(new Exception("An error occurred during token generation for user: " + user?.Email, ex));
        //        throw;
        //    }
        //}

        private string GenerateJwtToken(User user)
        {
            try
            {
                LoggerHelper.LogInfo("Token generation initiated for user: " + user?.Email);

                var tokenHandler = new JwtSecurityTokenHandler();
              //  var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var key = Convert.FromBase64String(_configuration["Jwt:Key"]);


                // Prepare Claims
                var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
        };

                
                var permissions = new Dictionary<string, HashSet<string>>
        {
            {"Meters", new HashSet<string> {"Read", "Create", "Delete", "Update"}},
            {"UserPortal", new HashSet<string> {"Read", "Create", "Delete", "Update"}},
            
        };

                
                foreach (var view in permissions)
                {
                    var permissionClaim = new Claim(view.Key + "_View", string.Join(", ", view.Value));
                    claims.Add(permissionClaim);

                    foreach (var action in view.Value)
                    {
                        var actionClaim = new Claim(view.Key + "_" + action, action);
                        claims.Add(actionClaim);
                    }
                }

                
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(24),
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
                LoggerHelper.LogError(new Exception($"An error occurred during token generation for user: {user?.Email}", ex));
                throw;
            }
        }



        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

      
        public async Task<List<User>> GetAllUsersBySystemIdAsync(Guid systemId)
        {
            return await _userRepository.GetAllUsersBySystemIdAsync(systemId);
        }
    }
}
