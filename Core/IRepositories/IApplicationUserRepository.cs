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
        Task<ApplicationUser> FindByEmailAsynch (string email);
        
        Task<bool> RegisterUserAsynch (ApplicationUser user , string password);
        Task<bool> CheckPasswordAsynch (ApplicationUser user , string password);
        Task<bool> SignInUserAsync(ApplicationUser user, string password);
      Task SignOutAsync();
    }
}




