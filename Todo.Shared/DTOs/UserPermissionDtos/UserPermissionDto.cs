using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared.DTOs.UserPermissionDtos
{
    public class UserPermissionDto
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int UserId { get; set; }
    }
}
