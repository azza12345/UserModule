using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface ISystemModuleRepository
    {
        Task<SystemModule> GetByIdAsync(Guid id);
        Task<IEnumerable<SystemModule>> GetAllAsync();
        Task AddAsync(SystemModule systemModule);
        Task UpdateAsync(SystemModule systemModule);
        Task DeleteAsync(Guid id);


    }
}
