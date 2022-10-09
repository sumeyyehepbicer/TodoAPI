using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.DTOs.TagDtos;

namespace Todo.Infrastructure.Implementation.Services.TagServices
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAll(CancellationToken cancellationToken);
        Task<TagDto> GetById(int id, CancellationToken cancellationToken);
        Task<TagDto> GetByName(string name, CancellationToken cancellationToken);
        Task<TagDto> CreateTag(CreateTagRequest request, CancellationToken cancellationToken);
        Task<TagDto> UpdateTag(UpdateTagRequest request, CancellationToken cancellationToken);
    }
}
