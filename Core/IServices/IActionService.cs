using Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Data.Action;


namespace Core.IServices
{
    public interface IActionService
    {
        Task<Action> AddActionAsync(ActionViewModel actionViewModel);
        Task<Action> GetActionByIdAsync(Guid id);
        Task DeleteActionAsync(Guid id);
    }
}
