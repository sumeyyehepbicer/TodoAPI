using AutoMapper;
using Todo.Domain.Entities;
using Todo.Shared.DTOs.UserPermissionDtos;

namespace Todo.API.Profiles
{
    public class UserPermissionProfile : Profile
    {
        public UserPermissionProfile()
        {
            CreateMap<UserPermission, UserPermissionDto>().ReverseMap();
        }
    }
}
