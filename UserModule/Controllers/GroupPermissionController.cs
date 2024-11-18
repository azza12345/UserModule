using Core.IServices;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UserModule.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class GroupPermissionController : ControllerBase
        {
            private readonly IGroupPermissionService _service;

            public GroupPermissionController(IGroupPermissionService service)
            {
                _service = service;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var groupPermissions = await _service.GetAllAsync();
                return Ok(groupPermissions);
            }

            [HttpGet("{groupId}/{permissionId}")]
            public async Task<IActionResult> GetById(Guid groupId, Guid permissionId)
            {
                var groupPermission = await _service.GetByIdAsync(groupId, permissionId);
                if (groupPermission == null)
                    return NotFound("GroupPermission not found.");

                return Ok(groupPermission);
            }

            [HttpPost]
            public async Task<IActionResult> Add([FromBody] GroupPermissionViewModel groupPermissionViewModel)
            {
                await _service.AddAsync(groupPermissionViewModel);
                return CreatedAtAction(nameof(GetById), new { groupId = groupPermissionViewModel.GroupId, permissionId = groupPermissionViewModel.PermissionId }, groupPermissionViewModel);
            }

            [HttpDelete("{groupId}/{permissionId}")]
            public async Task<IActionResult> Remove(Guid groupId, Guid permissionId)
            {
                try
                {
                    await _service.RemoveAsync(groupId, permissionId);
                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }
    }


