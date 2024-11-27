using Business;
using Business.Services;
using Core;
using Core.Interfaces;
using Core.Logging;

using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication and registration.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service dependency.</param>
        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="loginViewModel">The login information.</param>
        /// <returns>An IActionResult containing the JWT token or an error message.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                LoggerHelper.LogInfo("Invalid model state for login: " + ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                LoggerHelper.LogInfo("Login attempt for user: " + loginViewModel?.Email);

                var token = await _userService.LoginAsync(loginViewModel);

                if (string.IsNullOrEmpty(token))
                {
                    LoggerHelper.LogInfo("Unauthorized login attempt for user: " + loginViewModel?.Email);
                    return Unauthorized(new { message = "Invalid login attempt." });
                }

                LoggerHelper.LogInfo("Login successful for user: " + loginViewModel?.Email);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during login for user: " + loginViewModel?.Email, ex));
                return StatusCode(500, new { message = "An error occurred while processing the login request." });
            }
        }

        ///// <summary>
        ///// Registers a new user and generates a JWT token.
        ///// </summary>
        ///// <param name="registerViewModel">The registration information.</param>
        ///// <returns>An IActionResult containing the JWT token or an error message.</returns>
        //[HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        LoggerHelper.LogInfo("Invalid model state for registration: " + ModelState);
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        LoggerHelper.LogInfo("Registration attempt for user: " + registerViewModel?.Email);

        //        var token = await _userService.RegisterAsync(registerViewModel);

        //        if (token == "Registration failed")
        //        {
        //            LoggerHelper.LogInfo("Registration failed for user: " + registerViewModel?.Email);
        //            return BadRequest(new { message = "Registration failed. Please check the provided information." });
        //        }

        //        LoggerHelper.LogInfo("Registration successful for user: " + registerViewModel?.Email);
        //        return Ok(new { Token = token });
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerHelper.LogError(new Exception("An error occurred during registration for user: " + registerViewModel?.Email, ex));
        //        return StatusCode(500, new { message = "An error occurred while processing the registration request." });
        //    }
        //}


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data provided.",
                    StatusCode = CustomStatusCode.BadRequest
                });
            }

            var response = await _userService.RegisterAsync(registerViewModel);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("allUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("usersBySystem/{systemId}")]
        public async Task<IActionResult> GetUsersBySystemId(Guid systemId)
        {
            var users = await _userService.GetAllUsersBySystemIdAsync(systemId);
            if (users == null || !users.Any())
            {
                return NotFound($"No users found for SystemId: {systemId}");
            }
            return Ok(users);
        }

    }
}
