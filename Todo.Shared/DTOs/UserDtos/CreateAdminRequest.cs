using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.DTOs.UserPermissionDtos;

namespace Todo.Shared.DTOs.UserDtos
{
    public class CreateAdminRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual ICollection<UserPermissionDto> Permissions { get; set; }
    }
}

