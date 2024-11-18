using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IGroupService
    {
        Task<GroupViewModel> GetGroupByIdAsync(Guid id);
        Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync();
        Task AddGroupAsync(GroupViewModel groupViewModel);
        Task UpdateGroupAsync(GroupViewModel groupViewModel);
        Task DeleteGroupAsync(Guid id);
    }

}
