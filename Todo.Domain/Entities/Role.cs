using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class Role : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
