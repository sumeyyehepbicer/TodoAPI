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
using Todo.Shared.DTOs.TagDtos;

namespace Todo.Infrastructure.Implementation.Services.TagServices
{
    public class TagService : BaseService<Tag, TagDto>, ITagService
    {
        private readonly TodoContext _context;
        public TagService(IMapper mapper, TodoContext context) : base(mapper)
        {
            _context = context;
        }

        public async Task<TagDto> CreateTag(CreateTagRequest request, CancellationToken cancellationToken)
        {
            TagDto tagDto = null;
            Tag tag = null;
            tag = await _context.Tags.FirstOrDefaultAsync(op => op.Name == request.Name);
            if (tag is not null)
                throw new AppException($"Etiket sistemde mevcuttur.");

            tag = _mapper.Map<Tag>(request);
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return await this.GetById(tag.Id, cancellationToken);
        }

        public async Task<List<TagDto>> GetAll(CancellationToken cancellationToken)
        {
            List<TagDto> tagDtos = new List<TagDto>();
            List<Tag> tags = new List<Tag>();
            tags = await _context.Tags
                .ToListAsync();
            if (tags.Count == 0)
                throw new AppException($"Etiket bulunamadı.");

            tagDtos = ToDtos(tags);
            return tagDtos;
        }

        public async Task<TagDto> GetById(int id, CancellationToken cancellationToken)
        {
            TagDto tagDto = null;
            Tag tag = null;
            tag = await _context.Tags
                .FirstOrDefaultAsync(op => op.Id == id);
            if (tag is null)
                throw new AppException($"Etiket bulunamadı.");

            tagDto = ToDto(tag);
            return tagDto;
        }

        public async Task<TagDto> GetByName(string name, CancellationToken cancellationToken)
        {
            TagDto tagDto = null;
            Tag tag = null;
            tag = await _context.Tags
                .FirstOrDefaultAsync(op => op.Name == name);
            if (tag is null)
                throw new AppException($"Etiket bulunamadı.");

            tagDto = ToDto(tag);
            return tagDto;
        }

        public async Task<TagDto> UpdateTag(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            TagDto tagDto = null;
            Tag tag = null;
            var oldTag = await this.GetById(request.Id,cancellationToken);

            tag = _mapper.Map<Tag>(request);
            ToUpdate(oldTag, tag);
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();

            return await this.GetById(tag.Id, cancellationToken);
        }
    }
}
