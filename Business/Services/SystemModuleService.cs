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
    public class SystemModuleService : ISystemModuleService
    {
        private readonly ISystemModuleRepository _repository;

        public SystemModuleService(ISystemModuleRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(SystemModuleViewModel model)
        {
            var module = new SystemModule
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description
            };
            await _repository.AddAsync(module);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SystemModuleViewModel>> GetAllAsync()
        {
            var modules = await _repository.GetAllAsync();
            return modules.Select(m => new SystemModuleViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description
            });
        }

        public async Task<SystemModuleViewModel> GetByIdAsync(Guid id)
        {
            var module = await _repository.GetByIdAsync(id);
            return module == null ? null : new SystemModuleViewModel
            {
                Id = module.Id,
                Name = module.Name,
                Description = module.Description
            };
        }

        public async Task UpdateAsync(SystemModuleViewModel model)
        {
            var module = await _repository.GetByIdAsync(model.Id);
            if (module != null)
            {
                module.Name = model.Name;
                module.Description = model.Description;
                await _repository.UpdateAsync(module);
            }
        }
    }
}
