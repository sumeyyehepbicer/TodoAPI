using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Implementation.Services.PermissionServices;
using Todo.Shared.DTOs.PermissionDtos;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

      

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Permissions.Read")]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _permissionService.GetAll(cancellationToken));
        }
                
    }
}
