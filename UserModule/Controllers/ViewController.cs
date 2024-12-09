using Core.IServices;
using Core.Logging;
using Core.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
namespace UserModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly IViewService _viewService;

        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }
        [HttpPost("View/add")]
        public async Task<IActionResult> AddViewAsync([FromBody] ViewViewModel viewViewModel)
        {
            if (viewViewModel == null)
            {
                LoggerHelper.LogInfo("Add View : you entered invaild input");
                return BadRequest("invaild input ");
            }
            try
            {
                LoggerHelper.LogInfo("Add View : Trying to add the View "+viewViewModel.Id);
                var view = await _viewService.AddViewAsync(viewViewModel);
                LoggerHelper.LogInfo("Add View : You added the view sucessfully" + viewViewModel.Id);
                return Ok(view);
                
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception("Add View : an error during adding the view "+viewViewModel.Id));
                return BadRequest("$Error :  {ex.Message}");
            }
        }
        [HttpGet("View/GetById{id}")]
        public async Task<IActionResult> GetViewByIdAsync(Guid id)
        { 
            LoggerHelper.LogInfo("Get View By Id : Trying to Get View "+id);
            var view = await _viewService.GetViewByIdAsync(id);

            if (view == null)
            {
                return NotFound();
            }
            LoggerHelper.LogInfo("Get View By Id: You Got the view sucessfully" +id);

            return Ok(view);
        }
        [HttpDelete("View/Delete{id}")]
        public async Task<IActionResult> DeleteViewAsync(Guid id)
        {
            try
            {
                LoggerHelper.LogInfo("Delete View : Trying Delete View " + id);
                await _viewService.DeleteViewAsync(id);
                LoggerHelper.LogInfo($"Delet View : The View{id} Deleted Successfully ");
                return Ok();
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(new Exception($"Delete View : There's an error during delete View{id}"));
                return BadRequest("$Error : { ex.Message}");
            }
        }
    }
}
