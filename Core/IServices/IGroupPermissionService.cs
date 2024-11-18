using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IGroupPermissionService
    {
        Task<GroupPermissionViewModel> GetByIdAsync(Guid groupId, Guid permissionId);
        Task<IEnumerable<GroupPermissionViewModel>> GetAllAsync();
        Task AddAsync(GroupPermissionViewModel groupPermissionViewModel);
        Task RemoveAsync(Guid groupId, Guid permissionId);
    }
}
