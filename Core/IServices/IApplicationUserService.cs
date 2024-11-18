using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IApplicationUserService
    {
        Task<string> RegisterAsync(RegisterViewModel registerViewModel);
        Task<string> LoginAsync(LoginViewModel loginViewModel);

        Task LogoutAsync();
    }
}


 
