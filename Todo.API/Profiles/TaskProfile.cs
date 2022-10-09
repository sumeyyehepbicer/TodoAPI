using AutoMapper;
using Todo.Shared.DTOs.TaskDtos;

namespace Todo.API.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Todo.Domain.Entities.Task, TaskDto>().ReverseMap();
            CreateMap<Todo.Domain.Entities.Task, CreateTaskRequest>().ReverseMap();
            CreateMap<Todo.Domain.Entities.Task, UpdateTaskRequest>().ReverseMap();
        }
    }
}
