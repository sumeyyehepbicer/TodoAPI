using AutoMapper;
using Todo.Domain.Entities;
using Todo.Shared.DTOs.PermissionDtos;

namespace Todo.API.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDto>().ReverseMap();
        }
    }
}
