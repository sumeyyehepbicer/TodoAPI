using Microsoft.AspNetCore.Authorization;

namespace Todo.API.Handlers
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IConfiguration _configuration;

        public PermissionAuthorizationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (context.User == null)
                return;

            var permissions = context.User.Claims.Where(x => x.Type == "permission" &&
                                                    x.Value == requirement.Permission &&
                                                    x.Issuer == _configuration.GetValue<string>("JwtSettings:Issuer"))
                                        .Select(x => x.Value);

            if (permissions.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
