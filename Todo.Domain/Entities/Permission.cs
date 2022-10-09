using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class Permission : AuditableEntity
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
