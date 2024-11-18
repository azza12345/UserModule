using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface ISystemModuleService
    {
        Task<SystemModuleViewModel> GetByIdAsync(Guid id);
        Task<IEnumerable<SystemModuleViewModel>> GetAllAsync();
        Task AddAsync(SystemModuleViewModel model);
        Task UpdateAsync(SystemModuleViewModel model);
        Task DeleteAsync(Guid id);

    }
}
