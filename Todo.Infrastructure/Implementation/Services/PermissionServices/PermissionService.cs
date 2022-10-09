using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Infrastructure.Exceptions;
using Todo.Infrastructure.Implementation.Services.Common;
using Todo.Persistence.Contexts;
using Todo.Shared.DTOs.PermissionDtos;

namespace Todo.Infrastructure.Implementation.Services.PermissionServices
{
    public class PermissionService : BaseService<Permission, PermissionDto>, IPermissionService
    {
        private readonly TodoContext _context;
        public PermissionService(IMapper mapper, TodoContext context) : base(mapper)
        {
            _context = context;
        }

        public async Task<List<PermissionDto>> GetAll(CancellationToken cancellationToken)
        {
            List<PermissionDto> permissionDtos = new List<PermissionDto>();
            List<Permission> permissions = new List<Permission>();
            permissions = await _context.Permissions
                .ToListAsync();
            if (permissions.Count == 0)
                throw new AppException($"İzin bulunamadı.");

            permissionDtos = ToDtos(permissions);
            return permissionDtos;
        }

    }
}
