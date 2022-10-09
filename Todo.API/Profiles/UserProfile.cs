using AutoMapper;
using Todo.Domain.Entities;
using Todo.Shared.DTOs.UserDtos;

namespace Todo.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateAdminRequest>().ReverseMap();
            CreateMap<User, CreateUserRequest>().ReverseMap();
        }
    }
}
