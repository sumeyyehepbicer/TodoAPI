using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Implementation.Services.TagServices;
using Todo.Shared.DTOs.TagDtos;

namespace Todo.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tags.Read")]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _tagService.GetAll(cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tags.Read")]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<TagDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _tagService.GetById(id, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tags.Read")]
        [HttpGet("get-by-name")]
        public async Task<ActionResult<TagDto>> GetByName(string name, CancellationToken cancellationToken)
        {
            return Ok(await _tagService.GetByName(name, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Tags.Create")]
        [HttpPost("create-tag")]
        public async Task<ActionResult<TagDto>> CreateTag(CreateTagRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _tagService.CreateTag(request, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Tags.Update")]
        [HttpPost("update-tag")]
        public async Task<ActionResult<TagDto>> UpdateTag(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _tagService.UpdateTag(request, cancellationToken));
        }
    }
}
