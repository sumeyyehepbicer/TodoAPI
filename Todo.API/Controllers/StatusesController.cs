using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Implementation.Services.StatusServices;
using Todo.Shared.DTOs.StatusDtos;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private IStatusService _statusService;
        public StatusesController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Statuses.Read")]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _statusService.GetAll(cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Statuses.Read")]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<StatusDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _statusService.GetById(id, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Statuses.Read")]
        [HttpGet("get-by-name")]
        public async Task<ActionResult<StatusDto>> GetByName(string name, CancellationToken cancellationToken)
        {
            return Ok(await _statusService.GetByName(name, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Statuses.Create")]
        [HttpPost("create-status")]
        public async Task<ActionResult<StatusDto>> CreateStatus(CreateStatusRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _statusService.CreateStatus(request, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Statuses.Update")]
        [HttpPost("update-status")]
        public async Task<ActionResult<StatusDto>> UpdateStatus(UpdateStatusRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _statusService.UpdateStatus(request, cancellationToken));
        }
    }
}
