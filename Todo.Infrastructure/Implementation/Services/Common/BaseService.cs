using AutoMapper;
using Todo.Domain.Common;
using Todo.Shared.DTOs;

namespace Todo.Infrastructure.Implementation.Services.Common
{
    
    public abstract class BaseService<TEntity, TDto> where TEntity : AuditableEntity where TDto : BaseDto
    {
        public readonly IMapper _mapper;
        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDto ToDto(TEntity entity)
        {
            return _mapper.Map<TDto>(entity);
        }

        public TEntity ToEntity(TDto dto)
        {
            return _mapper.Map<TEntity>(dto);
        }

        public List<TEntity> ToEntities(List<TDto> dtos)
        {
            return _mapper.Map<List<TEntity>>(dtos);
        }
        public List<TDto> ToDtos(List<TEntity> entities)
        {
            return _mapper.Map<List<TDto>>(entities);
        }

        public void ToUpdate(TDto oldEntity, TEntity newEntity)
        {

            newEntity.DateCreated = (DateTime)oldEntity.DateCreated;
            newEntity.CreatedBy = oldEntity.CreatedBy;
        }
    }
}
