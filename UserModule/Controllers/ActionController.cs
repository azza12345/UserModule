using Core.IServices;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UserModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionService _actionService;

        public ActionController(IActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAction([FromBody] ActionViewModel actionViewModel)
        {
            if (actionViewModel == null)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                var action = await _actionService.AddActionAsync(actionViewModel);
                return Ok(action);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAction(Guid id)
        {
            var action = await _actionService.GetActionByIdAsync(id);
            if (action == null)
            {
                return NotFound();
            }

            return Ok(action);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAction(Guid id)
        {
            try
            {
                await _actionService.DeleteActionAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

       
    }
}
