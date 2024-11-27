using Core.ViewModels;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IViewService
    {
        Task<View> AddViewAsync(ViewViewModel viewViewModel);
        Task<View> GetViewByIdAsync(Guid id);
        Task DeleteViewAsync(Guid id);
      
    }
}
