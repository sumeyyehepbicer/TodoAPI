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
using Todo.Shared.DTOs.StatusDtos;

namespace Todo.Infrastructure.Implementation.Services.StatusServices
{
    public class StatusService : BaseService<Status, StatusDto>, IStatusService
    {
        private readonly TodoContext _context;
        public StatusService(IMapper mapper, TodoContext context) : base(mapper)
        {
            _context = context;
        }

        public async Task<StatusDto> CreateStatus(CreateStatusRequest request, CancellationToken cancellationToken)
        {
            StatusDto statusDto = null;
            Status status = null;
            status = await _context.Statuses.FirstOrDefaultAsync(op => op.Name == request.Name);
            if (status is not null)
                throw new AppException($"Durum sistemde mevcuttur.");

            status = _mapper.Map<Status>(request);
            await _context.Statuses.AddAsync(status);
            await _context.SaveChangesAsync();

            return await this.GetById(status.Id, cancellationToken);
        }

        public async Task<List<StatusDto>> GetAll(CancellationToken cancellationToken)
        {
            List<StatusDto> statusDtos = new List<StatusDto>();
            List<Status> statuses = new List<Status>();
            statuses = await _context.Statuses
                .ToListAsync();
            if (statuses.Count == 0)
                throw new AppException($"Durum bulunamadı.");

            statusDtos = ToDtos(statuses);
            return statusDtos;
        }

        public async Task<StatusDto> GetById(int id, CancellationToken cancellationToken)
        {
            StatusDto statusDto = null;
            Status status = null;
            status = await _context.Statuses
                .FirstOrDefaultAsync(op => op.Id == id);
            if (status is null)
                throw new AppException($"Durum bulunamadı.");

            statusDto = ToDto(status);
            return statusDto;
        }

        public async Task<StatusDto> GetByName(string name, CancellationToken cancellationToken)
        {
            StatusDto statusDto = null;
            Status status = null;
            status = await _context.Statuses
                .FirstOrDefaultAsync(op => op.Name == name);
            if (status is null)
                throw new AppException($"Durum bulunamadı.");

            statusDto = ToDto(status);
            return statusDto;
        }

        public async Task<StatusDto> UpdateStatus(UpdateStatusRequest request, CancellationToken cancellationToken)
        {
            StatusDto statusDto = null;
            Status status = null;
            var oldStatus = await this.GetById(request.Id,cancellationToken);
            
            status = _mapper.Map<Status>(request);
            ToUpdate(oldStatus, status);
            _context.Statuses.Update(status);
            await _context.SaveChangesAsync();

            return await this.GetById(status.Id, cancellationToken);
        }
    }
}
