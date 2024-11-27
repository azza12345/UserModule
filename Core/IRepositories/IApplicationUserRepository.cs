using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Identity;
namespace Core.IRepositories
{
    public interface IApplicationUserRepository
    {
        Task<User> FindByEmailAsynch (string email);
        
        Task<bool> RegisterUserAsynch (User user , string password);
        Task<bool> CheckPasswordAsynch (User user , string password);
        Task<bool> SignInUserAsync(User user, string password);
        Task SignOutAsync();
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllUsersBySystemIdAsync(Guid systemId);

    }
}




