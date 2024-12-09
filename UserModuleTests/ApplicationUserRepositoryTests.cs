using Business.Repositories;
using Core.Logging;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
namespace UserModuleTests
{
    public class ApplicationUserRepositoryTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly ApplicationUserRepository _repository;

        public ApplicationUserRepositoryTests()
        {
            // Mock UserManager
            _mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            // Mock SignInManager
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var userClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(o => o.Value).Returns(new IdentityOptions());
            var logger = new Mock<ILogger<SignInManager<User>>>();
            var schemeProvider = new Mock<IAuthenticationSchemeProvider>();
            var userConfirmation = new Mock<IUserConfirmation<User>>();

            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                httpContextAccessor.Object,
                userClaimsPrincipalFactory.Object,
                options.Object,
                logger.Object,
                schemeProvider.Object,
                userConfirmation.Object);


            var mockLogger = new Mock<Core.Logging.ILogger>();
            LoggerHelper.Initialize(mockLogger.Object);



            _repository = new ApplicationUserRepository(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task RegisterUserAsynch_ShouldReturnTrue_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var user = new User { Email = "azza5@gmail.com" };
            var password = "ValidPassword";

            // Mock UserManager to return success
            _mockUserManager.Setup(um => um.CreateAsync(user, password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _repository.RegisterUserAsynch(user, password);

            // Assert
            Assert.True(result);
            _mockUserManager.Verify(um => um.CreateAsync(user, password), Times.Once);
        }

        [Fact]
        public async Task RegisterUserAsynch_ShouldReturnFalse_WhenRegistrationFails()
        {
            // Arrange
            var user = new User { Email = "azza5@gmail.com" };
            var password = "WeakPassword";

            // Mock UserManager to return failure
            _mockUserManager.Setup(um => um.CreateAsync(user, password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password too weak" }));

            // Act
            var result = await _repository.RegisterUserAsynch(user, password);

            // Assert
            Assert.False(result);
            _mockUserManager.Verify(um => um.CreateAsync(user, password), Times.Once);
        }
    }
}