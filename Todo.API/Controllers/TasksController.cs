using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Implementation.Services.TaskServices;
using Todo.Shared.DTOs.TaskDtos;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tasks.Read")]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _taskService.GetAll(cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tasks.Read")]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<TaskDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _taskService.GetById(id, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tasks.Read")]
        [HttpGet("get-by-userId")]
        public async Task<ActionResult<TaskDto>> GetByUserId(CancellationToken cancellationToken)
        {
            return Ok(await _taskService.GetByUserId(cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tasks.Read")]
        [HttpGet("get-by-title")]
        public async Task<ActionResult<TaskDto>> GetByTitle(string title, CancellationToken cancellationToken)
        {
            return Ok(await _taskService.GetByTitle(title, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tasks.Create")]
        [HttpPost("create-task")]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _taskService.CreateTask(request, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Tasks.Update")]
        [HttpPost("update-task")]
        public async Task<ActionResult<TaskDto>> UpdateTask(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _taskService.UpdateTask(request, cancellationToken));
        }
    }
}
