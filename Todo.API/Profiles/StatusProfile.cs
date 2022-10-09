using AutoMapper;
using Todo.Domain.Entities;
using Todo.Shared.DTOs.StatusDtos;

namespace Todo.API.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>().ReverseMap();
            CreateMap<Status, CreateStatusRequest>().ReverseMap();
            CreateMap<Status, UpdateStatusRequest>().ReverseMap();
        }
    }
}
