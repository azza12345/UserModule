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
   
        public class GroupPermissionService : IGroupPermissionService
        {
            private readonly IGroupPermissionRepository _repository;
            private readonly IMapper _mapper;

            public GroupPermissionService(IGroupPermissionRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GroupPermissionViewModel> GetByIdAsync(Guid groupId, Guid permissionId)
            {
                var groupPermission = await _repository.GetByIdAsync(groupId, permissionId);
                if (groupPermission == null)
                    return null;

                return _mapper.Map<GroupPermissionViewModel>(groupPermission);
            }

            public async Task<IEnumerable<GroupPermissionViewModel>> GetAllAsync()
            {
                var groupPermissions = await _repository.GetAllAsync();
                return _mapper.Map<IEnumerable<GroupPermissionViewModel>>(groupPermissions);
            }

            public async Task AddAsync(GroupPermissionViewModel groupPermissionViewModel)
            {
                var groupPermission = _mapper.Map<GroupPermission>(groupPermissionViewModel);
                await _repository.AddAsync(groupPermission);
                await _repository.SaveChangesAsync();
            }

            public async Task RemoveAsync(Guid groupId, Guid permissionId)
            {
                var groupPermission = await _repository.GetByIdAsync(groupId, permissionId);
                if (groupPermission == null)
                    throw new KeyNotFoundException("GroupPermission not found.");

                _repository.Remove(groupPermission);
                await _repository.SaveChangesAsync();
            }
        }
    }


