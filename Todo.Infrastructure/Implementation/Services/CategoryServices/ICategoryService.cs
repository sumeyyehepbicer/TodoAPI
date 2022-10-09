using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.DTOs.CategoryDtos;

namespace Todo.Infrastructure.Implementation.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDto> GetById(int id, CancellationToken cancellationToken);
        Task<CategoryDto> GetByName(string name, CancellationToken cancellationToken);
        Task<CategoryDto> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken);
        Task<CategoryDto> UpdateCategory(UpdateCategoryRequest request, CancellationToken cancellationToken);
    }

}
