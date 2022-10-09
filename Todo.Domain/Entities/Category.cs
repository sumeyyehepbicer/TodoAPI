using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class Category:AuditableEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
