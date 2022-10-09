using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.DTOs.TaskDtos;

namespace Todo.Infrastructure.Implementation.Services.TaskServices
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAll(CancellationToken cancellationToken);
        Task<TaskDto> GetById(int id, CancellationToken cancellationToken);
        Task<List<TaskDto>> GetByUserId(CancellationToken cancellationToken);
        Task<TaskDto> GetByTitle(string title, CancellationToken cancellationToken);
        Task<TaskDto> CreateTask(CreateTaskRequest request, CancellationToken cancellationToken);
        Task<TaskDto> UpdateTask(UpdateTaskRequest request, CancellationToken cancellationToken);
    }
}
