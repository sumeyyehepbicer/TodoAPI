using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared.DTOs.TaskDtos
{
    public class UpdateTaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? Point { get; set; }
        public int? StatusId { get; set; }
        public int? AssignedId { get; set; }
        public int? TagId { get; set; }
        public int? CategoryId { get; set; }
    }
}
