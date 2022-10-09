using Todo.Shared.DTOs.StatusDtos;

namespace Todo.Infrastructure.Implementation.Services.StatusServices
{
    public interface IStatusService
    {
        Task<List<StatusDto>> GetAll(CancellationToken cancellationToken);
        Task<StatusDto> GetById(int id, CancellationToken cancellationToken);
        Task<StatusDto> GetByName(string name, CancellationToken cancellationToken);
        Task<StatusDto> CreateStatus(CreateStatusRequest request, CancellationToken cancellationToken);
        Task<StatusDto> UpdateStatus(UpdateStatusRequest request, CancellationToken cancellationToken);
    }

}
