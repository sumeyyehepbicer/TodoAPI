using Todo.Shared.DTOs.PermissionDtos;

namespace Todo.Infrastructure.Implementation.Services.PermissionServices
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAll(CancellationToken cancellationToken);
    }
}
