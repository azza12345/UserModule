using AutoMapper;
using Business.Services;
using Core;
using Core.IRepositories;
using Core.Logging;
using Core.ViewModels;
using Data;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModuleTests
{
    public class ApplicationUserServiceTests
    {
        private readonly Mock<IApplicationUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ApplicationUserService _service;

        public ApplicationUserServiceTests()
        {
            _userRepositoryMock = new Mock<IApplicationUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("testKey");
            _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("testIssuer");
            _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("testAudience");


            var mockLogger = new Mock<Core.Logging.ILogger>();
            LoggerHelper.Initialize(mockLogger.Object);

                _service = new ApplicationUserService(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task LoginAsync_UserNotFound_ReturnsErrorMessage()
        {
            // Arrange
            var loginViewModel = new LoginViewModel { Email = "test@example.com", Password = "password123" };
            _userRepositoryMock.Setup(repo => repo.FindByEmailAsynch(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _service.LoginAsync(loginViewModel);

            // Assert
            Assert.Equal("User not found", result);
            _userRepositoryMock.Verify(repo => repo.FindByEmailAsynch(loginViewModel.Email), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_InvalidPassword_ReturnsErrorMessage()
        {
            // Arrange
            var user = new User { Email = "test@example.com" };
            var loginViewModel = new LoginViewModel { Email = "test@example.com", Password = "wrong" };
            _userRepositoryMock.Setup(repo => repo.FindByEmailAsynch(It.IsAny<string>())).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.CheckPasswordAsynch(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _service.LoginAsync(loginViewModel);

            // Assert
            Assert.Equal("Invalid password", result);
        }

        [Fact]
        public async Task LoginAsync_SuccessfulLogin_ReturnsJwtToken()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", UserName = "TestUser" };
            var loginViewModel = new LoginViewModel { Email = "test@example.com", Password = "correctpassword" };
            _userRepositoryMock.Setup(repo => repo.FindByEmailAsynch(It.IsAny<string>())).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.CheckPasswordAsynch(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _service.LoginAsync(loginViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(".", result); // JWT format contains periods (found between them
        }

        [Fact]
        public async Task LoginAsync_ExceptionThrown_ReturnsErrorMessage()
        {
            // Arrange
            var loginViewModel = new LoginViewModel { Email = "test@example.com", Password = "password123" };
            _userRepositoryMock.Setup(repo => repo.FindByEmailAsynch(It.IsAny<string>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _service.LoginAsync(loginViewModel);

            // Assert
            Assert.Equal("An error occurred during login. Please try again later.", result);
        }

        [Fact]
        public async Task RegisterAsync_RegistrationFailed_ReturnsApiResponseWithFailure()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Email = "test@example.com",
                Password = "password123"
            };
            var user = new User { Email = registerViewModel.Email };

            _mapperMock.Setup(mapper => mapper.Map<User>(registerViewModel)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.RegisterUserAsynch(user, registerViewModel.Password))
                               .ReturnsAsync(false);

            // Act
            var response = await _service.RegisterAsync(registerViewModel);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Registration failed", response.Message);
            Assert.Equal(CustomStatusCode.BadRequest, response.StatusCode);
            _userRepositoryMock.Verify(repo => repo.RegisterUserAsynch(user, registerViewModel.Password), Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_RegistrationSuccessful_ReturnsApiResponseWithSuccess()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Email = "test@example.com",
                Password = "Password123"
            };
            var user = new User { Email = registerViewModel.Email };

            _mapperMock.Setup(mapper => mapper.Map<User>(registerViewModel)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.RegisterUserAsynch(user, registerViewModel.Password))
                               .ReturnsAsync(true);


            // Act
            var response = await _service.RegisterAsync(registerViewModel);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Registration successful", response.Message);
            Assert.Equal(CustomStatusCode.Success, response.StatusCode);
            _userRepositoryMock.Verify(repo => repo.RegisterUserAsynch(user, registerViewModel.Password), Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_ExceptionThrown_ReturnsApiResponseWithError()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Email = "test@example.com",
                Password = "password123"
            };

            _mapperMock.Setup(mapper => mapper.Map<User>(registerViewModel))
                       .Throws(new Exception("Mapping error"));

            // Act
            var response = await _service.RegisterAsync(registerViewModel);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("An error occurred during registration. Please try again later.", response.Message);
            Assert.Equal(CustomStatusCode.InternalServerError, response.StatusCode);
            _userRepositoryMock.Verify(repo => repo.RegisterUserAsynch(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }


    }
}
