using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Core.IRepositories
{
    public interface IActionRepository
    {
        Task<Data.Action> GetByIdAsync(Guid id);
        Task<IEnumerable<Data.Action>> GetAllAsync();
        Task AddAsync(Data.Action action);
        Task UpdateAsync(Data.Action action);
        Task DeleteAsync(Guid id);
    }
}
