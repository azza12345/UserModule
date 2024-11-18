using Core.IServices;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UserModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _groupService.AddGroupAsync(groupViewModel);
            return Ok("Group added successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _groupService.UpdateGroupAsync(groupViewModel);
            return Ok("Group updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            await _groupService.DeleteGroupAsync(id);
            return Ok("Group deleted successfully");
        }
    }

}
