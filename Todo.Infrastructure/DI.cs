using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Implementation.Services.CategoryServices;
using Todo.Infrastructure.Implementation.Services.PermissionServices;
using Todo.Infrastructure.Implementation.Services.StatusServices;
using Todo.Infrastructure.Implementation.Services.TagServices;
using Todo.Infrastructure.Implementation.Services.TaskServices;
using Todo.Infrastructure.Implementation.Services.UserServices;

namespace Todo.Infrastructure
{
    public static class DI
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IPermissionService, PermissionService>();


        }
    }
}
