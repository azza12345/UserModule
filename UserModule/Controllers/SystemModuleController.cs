using Core.IServices;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UserModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemModuleController : ControllerBase
    {
        private readonly ISystemModuleService _service;

        public SystemModuleController(ISystemModuleService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var module = await _service.GetByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var modules = await _service.GetAllAsync();
            return Ok(modules);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SystemModuleViewModel model)
        {
            await _service.AddAsync(model);
            return Ok("SystemModule created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update(SystemModuleViewModel model)
        {
            await _service.UpdateAsync(model);
            return Ok("SystemModule updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok("SystemModule deleted successfully.");
        }
    }

}
