using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared.DTOs.CategoryDtos
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
