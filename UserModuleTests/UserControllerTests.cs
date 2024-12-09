using Xunit;
using Moq;
using UserModule;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using Core;
using Data;
using Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
namespace UserModuleTests
{
    public class UserControllerTests
    {
        private readonly Mock<IApplicationUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IApplicationUserService>();
            _controller = new UserController(_userServiceMock.Object);

            var mockLogger = new Mock<Core.Logging.ILogger>();
            LoggerHelper.Initialize(mockLogger.Object);


        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "Password123"
            };
            var token = "mock-jwt-token";

            _userServiceMock.Setup(service => service.LoginAsync(loginViewModel))
                .ReturnsAsync(token);

            // Act
            var result = await _controller.Login(loginViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var response = okResult.Value; // Access the actual response value

           
            Assert.NotNull(response);
            Assert.True(response.GetType().GetProperty("Token") != null); // Check that "Token" exists
            Assert.Equal(token, response.GetType().GetProperty("Token")?.GetValue(response, null)?.ToString());
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "invalid",
                Password = "wrongpassword"
            };

            _userServiceMock.Setup(service => service.LoginAsync(loginViewModel))
                .ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.Login(loginViewModel);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);

            var response = unauthorizedResult.Value; // Access the actual response value

            // Explicitly check the response structure dynamically
            Assert.NotNull(response);
            Assert.True(response.GetType().GetProperty("message") != null); // Check that "message" exists
            Assert.Equal("Invalid login attempt.", response.GetType().GetProperty("message")?.GetValue(response, null)?.ToString());
        }

        [Fact]
        public async Task Register_ValidData_ReturnsSuccessResponse()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var apiResponse = new ApiResponse
            {
                Success = true,
                Message = "Registration successful",
                StatusCode = CustomStatusCode.Success
            };

            _userServiceMock.Setup(service => service.RegisterAsync(registerViewModel))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.Register(registerViewModel);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)CustomStatusCode.Success, okResult.StatusCode);

            var response = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.True(response.Success);
            Assert.Equal("Registration successful", response.Message);
        }


        [Fact]
        public async Task Register_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Email = "", // Invalid data
                Password = ""
            };

            _controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = await _controller.Register(registerViewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Invalid data provided.", response.Message);
        }
        [Fact]
        public async Task GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = Guid.NewGuid(), Email = "user1@example.com" },
        new User { Id = Guid.NewGuid(), Email = "user2@example.com" }
    };

            _userServiceMock.Setup(service => service.GetAllUsersAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var response = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, response.Count);
        }

    }
}
