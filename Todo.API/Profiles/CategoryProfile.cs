using AutoMapper;
using Todo.Domain.Entities;
using Todo.Shared.DTOs.CategoryDtos;

namespace Todo.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
        }
    }
}
