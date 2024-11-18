using Core.IRepositories;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Core.Logging;

namespace Business.Repositories
{
    /// <summary>
    /// Repository for managing application user-related operations.
    /// </summary>
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRepository"/> class.
        /// </summary>
        /// <param name="userManager">The user manager dependency.</param>
        /// <param name="signInManager">The sign-in manager dependency.</param>
        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Checks if the provided password matches the user's password.
        /// </summary>
        /// <param name="user">The user to validate.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password is correct; otherwise, false.</returns>
        public async Task<bool> CheckPasswordAsynch(ApplicationUser user, string password)
        {
            try
            {
                LoggerHelper.LogInfo("Password validation initiated for user: " + user?.Email);
                var result = await _userManager.CheckPasswordAsync(user, password);
                LoggerHelper.LogInfo("Password validation completed for user: " + user?.Email);
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during password validation for user: " + user?.Email, ex));
                throw;
            }
        }

        /// <summary>
        /// Registers a new user with the specified password.
        /// </summary>
        /// <param name="user">The user to register.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>True if registration succeeds; otherwise, false.</returns>
        public async Task<bool> RegisterUserAsynch(ApplicationUser user, string password)
        {
            try
            {
                LoggerHelper.LogInfo("User registration initiated for: " + user?.Email);
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    LoggerHelper.LogInfo("User registration succeeded for: " + user?.Email);
                }
                else
                {
                    LoggerHelper.LogInfo("User registration failed for: " + user?.Email);
                }

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during user registration for: " + user?.Email, ex));
                throw;
            }
        }

        /// <summary>
        /// Attempts to sign in a user using the provided password.
        /// </summary>
        /// <param name="user">The user to sign in.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>True if sign-in succeeds; otherwise, false.</returns>
        public async Task<bool> SignInUserAsync(ApplicationUser user, string password)
        {
            try
            {
                LoggerHelper.LogInfo("Sign-in attempt initiated for user: " + user?.Email);
                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    LoggerHelper.LogInfo("Sign-in successful for user: " + user?.Email);
                }
                else
                {
                    LoggerHelper.LogInfo("Sign-in failed for user: " + user?.Email);
                }

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during sign-in for user: " + user?.Email, ex));
                throw;
            }
        }

        /// <summary>
        /// Signs out the currently authenticated user.
        /// </summary>
        public async Task SignOutAsync()
        {
            try
            {
                LoggerHelper.LogInfo("Sign-out initiated.");
                await _signInManager.SignOutAsync();
                LoggerHelper.LogInfo("Sign-out successful.");
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred during sign-out.", ex));
                throw;
            }
        }

        /// <summary>
        /// Finds a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        public async Task<ApplicationUser> FindByEmailAsynch(string email)
        {
            try
            {
                LoggerHelper.LogInfo("Finding user by email: " + email);
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    LoggerHelper.LogInfo("User found with email: " + email);
                }
                else
                {
                    LoggerHelper.LogInfo("No user found with email: " + email);
                }

                return user;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("An error occurred while finding user by email: " + email, ex));
                throw;
            }
        }
    }
}
