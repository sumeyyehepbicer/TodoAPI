using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Infrastructure.Implementation.Services.UserServices;
using Todo.Shared.DTOs.UserDtos;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;


        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model, CancellationToken cancellationToken)
        {
            return Ok(await _userService.Authenticate(model, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Users.Read")]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetAll(cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Users.Read")]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<UserDto>> GetById(int id,CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetById(id,cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Users.Read")]
        [HttpGet("get-by-username")]
        public async Task<ActionResult<UserDto>> GetByUsername(string username,CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetByUsername(username,cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin", Policy = "Permissions.Users.Create")]
        [HttpPost("create-admin")]
        public async Task<ActionResult<UserDto>> CreateAdmin(CreateAdminRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _userService.CreateAdmin(request, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Users.Create")]
        [HttpPost("create-user")]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _userService.CreateUser(request, cancellationToken));
        }
    }
}
