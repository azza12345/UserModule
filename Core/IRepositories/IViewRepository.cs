using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IViewRepository
    {
        Task<View> GetByIdAsync(Guid id);

        Task AddAsync(View view);
        Task UpdateAsync(View view);
        Task DeleteAsync(Guid id);
    }
}
