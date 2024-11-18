using Core.IRepositories;
using Core.IServices;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;

        public PermissionService(IPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Permission> GetPermissionByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreatePermissionAsync(Permission permission)
        {
            await _repository.AddAsync(permission);
        }

        public async Task UpdatePermissionAsync(Permission permission)
        {
            await _repository.UpdateAsync(permission);
        }

        public async Task DeletePermissionAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
