using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class User: AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<UserPermission> Permissions { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
