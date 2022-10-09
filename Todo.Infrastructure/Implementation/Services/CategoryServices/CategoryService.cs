using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Infrastructure.Exceptions;
using Todo.Infrastructure.Implementation.Services.Common;
using Todo.Persistence.Contexts;
using Todo.Shared.DTOs.CategoryDtos;

namespace Todo.Infrastructure.Implementation.Services.CategoryServices
{
    public class CategoryService : BaseService<Category, CategoryDto>, ICategoryService
    {
        private readonly TodoContext _context;
        public CategoryService(IMapper mapper, TodoContext context) : base(mapper)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            List<Category> categories = new List<Category>();
            categories = await _context.Categories
                .ToListAsync();
            if (categories.Count==0)
                throw new AppException($"Kategori bulunamadı.");

            categoryDtos = ToDtos(categories);
            return categoryDtos;
        }

        public async Task<CategoryDto> GetById(int id, CancellationToken cancellationToken)
        {
            CategoryDto categoryDto = null;
            Category category = null;
            category = await _context.Categories
                .FirstOrDefaultAsync(op => op.Id == id);
            if (category is null)
                throw new AppException($"Kategori bulunamadı.");

            categoryDto = ToDto(category);
            return categoryDto;
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            CategoryDto categoryDto = null;
            Category category = null;
            category = await _context.Categories.FirstOrDefaultAsync(op=>op.Name==request.Name);
            if (category is not null)
                throw new AppException($"Kategori sistemde mevcuttur.");

            category = _mapper.Map<Category>(request);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return await this.GetById(category.Id, cancellationToken);
        }

        public async Task<CategoryDto> GetByName(string name, CancellationToken cancellationToken)
        {
            CategoryDto categoryDto = null;
            Category category = null;
            category = await _context.Categories
                .FirstOrDefaultAsync(op => op.Name == name);
            if (category is null)
                throw new AppException($"Kategori bulunamadı.");

            categoryDto = ToDto(category);
            return categoryDto;
        }

        public async Task<CategoryDto> UpdateCategory(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            CategoryDto categoryDto = null;
            Category category = null;

            var oldCategory = await this.GetById(request.Id,cancellationToken);    
            category = _mapper.Map<Category>(request);
            ToUpdate(oldCategory, category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return await this.GetById(category.Id, cancellationToken);
        }
    }
}
