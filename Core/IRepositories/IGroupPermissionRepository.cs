using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IGroupPermissionRepository
    {
        Task<GroupPermission> GetByIdAsync(Guid groupId, Guid permissionId);
        Task<IEnumerable<GroupPermission>> GetAllAsync();
        Task AddAsync(GroupPermission groupPermission);
        void Remove(GroupPermission groupPermission);
        Task SaveChangesAsync();
    }
}
