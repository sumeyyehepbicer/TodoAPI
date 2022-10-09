using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class Task:AuditableEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? Point { get; set; }
        public int? StatusId { get; set; }
        public Status? Status { get; set; }       
        public int UserId { get; set; }
        public User User { get; set; }
        public int? AssignedId { get; set; }
        public User? Assigned { get; set; }
        public int? TagId { get; set; }
        public Tag? Tag { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
