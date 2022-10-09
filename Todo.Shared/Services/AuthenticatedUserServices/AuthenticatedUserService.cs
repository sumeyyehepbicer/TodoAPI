using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Todo.Infrastructure.Implementation.Services.UserServices
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Username = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);
            ParentId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.SerialNumber);
        }

        public string UserId { get; }

        public string Username { get; }

        public string ParentId { get; }
    }
}