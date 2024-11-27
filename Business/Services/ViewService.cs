using AutoMapper;
using Business.Repositories;
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
    public class ViewService : IViewService
    {
        private readonly IViewRepository _viewRepository;
        private readonly ISystemModuleRepository _systemModuleRepository;
        private readonly IMapper _mapper;

        public ViewService(IViewRepository viewRepository, ISystemModuleRepository systemModuleRepository, IMapper mapper)
        {
            _viewRepository = viewRepository;
            _systemModuleRepository = systemModuleRepository;
            _mapper = mapper;
        }

        public async Task<View> AddViewAsync(ViewViewModel viewViewModel)
        {
            var view = _mapper.Map<View>(viewViewModel);


            var systemModule = await _systemModuleRepository.GetByIdAsync((Guid)view.SystemId);
            if (systemModule == null)
            {
                throw new Exception("System Module not found.");
            }


            await _viewRepository.AddAsync(view);
            return view;
        }


        public async Task DeleteViewAsync(Guid id)
        {
            await _viewRepository.DeleteAsync(id);
        }



        public async Task<View> GetViewByIdAsync(Guid id)
        {
            return await _viewRepository.GetByIdAsync(id);
        }


    }
}
