using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Infrastructure.Exceptions;
using Todo.Infrastructure.Implementation.Services.Common;
using Todo.Infrastructure.Implementation.Services.UserServices;
using Todo.Persistence.Contexts;
using Todo.Shared.DTOs.TaskDtos;

namespace Todo.Infrastructure.Implementation.Services.TaskServices
{
    public class TaskService : BaseService<Todo.Domain.Entities.Task, TaskDto>, ITaskService
    {
        private readonly TodoContext _context;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public TaskService(IMapper mapper, TodoContext context, IAuthenticatedUserService authenticatedUserService) : base(mapper)
        {
            _context = context;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<TaskDto> CreateTask(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            TaskDto taskDto = null;
            Todo.Domain.Entities.Task task = null;

            task = _mapper.Map<Todo.Domain.Entities.Task>(request);
            task.UserId = int.Parse(_authenticatedUserService.UserId);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return await this.GetById(task.Id, cancellationToken);
        }

        public async Task<List<TaskDto>> GetAll(CancellationToken cancellationToken)
        {
            List<TaskDto> taskDtos = new List<TaskDto>();
            List<Todo.Domain.Entities.Task> tasks = new List<Todo.Domain.Entities.Task>();
            tasks = await _context.Tasks
                .Include(op=>op.Tag)
                .Include(op=>op.User)
                .Include(op => op.Status)
                .Include(op => op.Category)
                .Include(op => op.Assigned)
                .ToListAsync();
            if (tasks.Count == 0)
                throw new AppException($"Görev bulunamadı.");

            taskDtos = ToDtos(tasks);
            return taskDtos;
        }

        public async Task<TaskDto> GetById(int id, CancellationToken cancellationToken)
        {
            TaskDto taskDto = null;
            Todo.Domain.Entities.Task task = null;
            task = await _context.Tasks
                .Include(op => op.Tag)
                .Include(op => op.User)
                .Include(op => op.Status)
                .Include(op => op.Category)
                 .Include(op => op.Assigned)
                .FirstOrDefaultAsync(op => op.Id == id);
            if (task is null)
                throw new AppException($"Görev bulunamadı.");

            taskDto = ToDto(task);
            return taskDto;
        }

        public async Task<TaskDto> GetByTitle(string title, CancellationToken cancellationToken)
        {
            TaskDto taskDto = null;
            Todo.Domain.Entities.Task task = null;
            task = await _context.Tasks
                .FirstOrDefaultAsync(op => op.Title == title);
            if (task is null)
                throw new AppException($"Görev bulunamadı.");

            taskDto = ToDto(task);
            return taskDto;
        }

        public async Task<List<TaskDto>> GetByUserId(CancellationToken cancellationToken)
        {
            List<TaskDto> taskDtos = null;
            List<Todo.Domain.Entities.Task> tasks = null;
            tasks = await _context.Tasks
                .Where(op => op.UserId == int.Parse(_authenticatedUserService.UserId)).ToListAsync();
            if (tasks.Count==0)
                throw new AppException($"Görev bulunamadı.");

            taskDtos = ToDtos(tasks);
            return taskDtos;
        }

        public async Task<TaskDto> UpdateTask(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            TaskDto taskDto = null;
            Todo.Domain.Entities.Task task = null;
            var oldTask = await this.GetById(request.Id,cancellationToken);     
            task = _mapper.Map<Todo.Domain.Entities.Task>(request);
            task.UserId = oldTask.UserId;
            ToUpdate(oldTask,task);
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return await this.GetById(task.Id, cancellationToken);
        }
    }
}
