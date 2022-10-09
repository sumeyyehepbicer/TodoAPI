using AutoMapper;
using Todo.Domain.Entities;
using Todo.Shared.DTOs.TagDtos;

namespace Todo.API.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Tag, CreateTagRequest>().ReverseMap();
            CreateMap<Tag, UpdateTagRequest>().ReverseMap();
        }
    }
}
