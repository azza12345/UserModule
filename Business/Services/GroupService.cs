using AutoMapper;
using Core.IRepositories;
using Core.IServices;
using Core.ViewModels;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<GroupViewModel> GetGroupByIdAsync(Guid id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            return _mapper.Map<GroupViewModel>(group);
        }

        public async Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GroupViewModel>>(groups);
        }

        public async Task AddGroupAsync(GroupViewModel groupViewModel)
        {
            var group = _mapper.Map<Group>(groupViewModel);
            await _groupRepository.AddAsync(group);
        }

        public async Task UpdateGroupAsync(GroupViewModel groupViewModel)
        {
            var group = _mapper.Map<Group>(groupViewModel);
            await _groupRepository.UpdateAsync(group);
        }

        public async Task DeleteGroupAsync(Guid id)
        {
            await _groupRepository.DeleteAsync(id);
        }
    }

}
