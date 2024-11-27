using Core.ViewModels;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IApplicationUserService
    {
        Task<ApiResponse> RegisterAsync(RegisterViewModel registerViewModel);
        Task<string> LoginAsync(LoginViewModel loginViewModel);

        Task LogoutAsync();
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllUsersBySystemIdAsync(Guid systemId);
    }
}


 
