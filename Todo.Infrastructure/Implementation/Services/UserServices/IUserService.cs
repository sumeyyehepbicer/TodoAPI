using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.DTOs.UserDtos;

namespace Todo.Infrastructure.Implementation.Services.UserServices
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken);
        Task<List<UserDto>> GetAll(CancellationToken cancellationToken);
        Task<UserDto> GetById(int id,CancellationToken cancellationToken);
        Task<UserDto> GetByUsername(string username,CancellationToken cancellationToken);
        Task<UserDto> CreateAdmin(CreateAdminRequest request, CancellationToken cancellationToken);
        Task<UserDto> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
    }
}
