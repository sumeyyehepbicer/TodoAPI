using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared.DTOs.PermissionDtos
{
    public class PermissionDto:BaseDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PermissionId { get; set; }
    }
}
