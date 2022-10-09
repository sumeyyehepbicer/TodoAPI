using AutoMapper;
using Todo.Domain.Entities;
using Todo.Shared.DTOs.RoleDtos;

namespace Todo.API.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
