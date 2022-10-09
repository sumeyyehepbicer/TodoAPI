using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.DTOs.CategoryDtos;
using Todo.Shared.DTOs.StatusDtos;
using Todo.Shared.DTOs.TagDtos;
using Todo.Shared.DTOs.UserDtos;

namespace Todo.Shared.DTOs.TaskDtos
{
    public class TaskDto:BaseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Point { get; set; }
        public int StatusId { get; set; }
        public StatusDto Status { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int AssignedId { get; set; }
        public UserDto Assigned { get; set; }
        public int TagId { get; set; }
        public TagDto Tag { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
