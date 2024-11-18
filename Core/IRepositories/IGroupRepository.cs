using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IGroupRepository
    {
        Task<Group> GetByIdAsync(Guid id);
        Task<IEnumerable<Group>> GetAllAsync();
        Task AddAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(Guid id);
    }
}
