using AutoMapper;
using Core.IRepositories;
using Core.IServices;
using Core.ViewModels;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Data.Action;

namespace Business.Services
{
   
        public class ActionService : IActionService
        {
            private readonly IActionRepository _actionRepository;
            private readonly ISystemModuleRepository _systemModuleRepository;
            private readonly IMapper _mapper;

            public ActionService(IActionRepository actionRepository, ISystemModuleRepository systemModuleRepository, IMapper mapper)
            {
                _actionRepository = actionRepository;
                _systemModuleRepository = systemModuleRepository;
                _mapper = mapper;
            }

            public async Task<Action> AddActionAsync(ActionViewModel actionViewModel)
            {
                var action = _mapper.Map<Action>(actionViewModel);

                
                var systemModule = await _systemModuleRepository.GetByIdAsync(action.SystemId);
                if (systemModule == null)
                {
                    throw new Exception("System Module not found.");
                }

                
                await _actionRepository.AddAsync(action);
                return action;
            }

            public async Task<Action> GetActionByIdAsync(Guid id)
            {
                return await _actionRepository.GetByIdAsync(id);
            }

            public async Task<IEnumerable<Action>> GetAllActionsAsync()
            {
                return await _actionRepository.GetAllAsync();
            }

            public async Task DeleteActionAsync(Guid id)
            {
                await _actionRepository.DeleteAsync(id);
            }
        }
    }



