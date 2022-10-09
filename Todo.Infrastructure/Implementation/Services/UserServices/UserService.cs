using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.Domain.Entities;
using Todo.Domain.Settings;
using Todo.Infrastructure.Exceptions;
using Todo.Infrastructure.Implementation.Services.Common;
using Todo.Persistence.Contexts;
using Todo.Shared.DTOs.UserDtos;
using Todo.Shared.Services.CacheServices;

namespace Todo.Infrastructure.Implementation.Services.UserServices
{
    public class UserService: BaseService<User, UserDto>,IUserService
    {
        private readonly TodoContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly ICacheService _cacheService;
        public const string Cache_Users = "USERS";
        public UserService(IMapper mapper, TodoContext context, IOptions<JwtSettings> jwtSettings, IAuthenticatedUserService authenticatedUserService, ICacheService cacheService) : base(mapper)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _authenticatedUserService = authenticatedUserService;
            _cacheService = cacheService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            AuthenticateResponse authenticateResponse = new();
            var user = await _context.Users
                .Include(op=>op.Tasks)
                .Include(op => op.Permissions)
                .ThenInclude(op=>op.Permission)
                .Include(op => op.Role)
                .FirstOrDefaultAsync(op=>op.Username==request.Username);

            if (user is null)
                throw new AppException("Kullanıcı bulunamadı.");

            if (user.Password != request.Password)
                throw new AppException("Parola hatalı.");

            var userDto = ToDto(user);

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Title),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.GivenName,user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            List<AbilityDto> abilities = new();
            List<Permission> userAllPermissions = new();
            List<Permission> userPermissions = new();

            foreach (var permission in user.Permissions)
                userPermissions.Add(permission.Permission);

            userAllPermissions.AddRange(userPermissions);



            for (int i = 0; i < userAllPermissions.Count; i++)
                claims.Add(new Claim("permission", userAllPermissions[i].Key));

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var expDate = DateTime.Now.AddYears(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expDate,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            foreach (var userPermission in userAllPermissions)
            {
                var permission = userPermission.Key.Split('.');
                abilities.Add(new()
                {
                    Action = permission[2].ToLower(),
                    Subject = permission[1]
                });
            }

            return new AuthenticateResponse(userDto, abilities, jwtToken);
        }
              
        public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            List<UserDto> userDtos = new List<UserDto>();
            List<User> users = new List<User>();
            var auth = await this.GetById(int.Parse(_authenticatedUserService.UserId),cancellationToken);
            if (auth.Role.Title=="SuperAdmin")
            {
                if (_cacheService.GetCache(Cache_Users, out userDtos))
                    return userDtos;

                users = await _context.Users
                .Include(op => op.Role)
                .Include(op => op.Permissions)
                .ToListAsync();
                userDtos = ToDtos(users);

                _cacheService.CreateCache(Cache_Users, userDtos);
            }
            else
            {
                if (_cacheService.GetCache(Cache_Users, out userDtos))
                    return userDtos.Where(op=>op.Role.Title!="SuperAdmin").ToList();

                users = await _context.Users
                .Include(op => op.Role)
                .Include(op => op.Permissions)
                .ToListAsync();
                userDtos = ToDtos(users);

                _cacheService.CreateCache(Cache_Users, userDtos);
            }

            
            return userDtos;
        }

        public async Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
            UserDto userDto = null;
            User user = null;
            user = await _context.Users
                .Include(op => op.Role)
                .Include(op => op.Permissions)
                .FirstOrDefaultAsync(op=>op.Id==id);
            userDto = ToDto(user);
            return userDto;
        }

        public async Task<UserDto> GetByUsername(string username, CancellationToken cancellationToken)
        {
            UserDto userDto = null;
            User user = null;
            user = await _context.Users
                .Include(op => op.Role)
                .Include(op => op.Permissions)
                .FirstOrDefaultAsync(op => op.Username == username);
            userDto = ToDto(user);
            return userDto;
        }

        public async Task<UserDto> CreateAdmin(CreateAdminRequest request, CancellationToken cancellationToken)
        {
            UserDto userDto = null;
            User user = null;

            var role = await _context.Roles.FirstOrDefaultAsync(op => op.Title == "Admin", cancellationToken);
            if (request.RoleId != role.Id)
                throw new AppException($"Lütfen verdiğiniz yetkiyi kontrol ediniz.");

            user = await _context.Users.FirstOrDefaultAsync(op => op.Username == request.Username);
            if (user != null)
                throw new AppException($"{request.Username} adlı kullanıcı sistemde mevcuttur.");


            user = _mapper.Map<User>(request);
            user.RoleId = role.Id;

            await _context.Users.AddAsync(user);
            _cacheService.Delete(Cache_Users);
            await _context.SaveChangesAsync();

            return await this.GetById(user.Id, cancellationToken);
        }
        public async Task<UserDto> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            UserDto userDto = null;
            User user = null;

            var role = await _context.Roles.FirstOrDefaultAsync(op=>op.Title=="User",cancellationToken);
            if (request.RoleId != role.Id)
                throw new AppException($"Lütfen verdiğiniz yetkiyi kontrol ediniz.");

            user = await _context.Users.FirstOrDefaultAsync(op=>op.Username==request.Username);
            if (user != null)
                throw new AppException($"{request.Username} adlı kullanıcı sistemde mevcuttur.");

            user = _mapper.Map<User>(request);
            user.RoleId = role.Id;
            user.ParentId = int.Parse(_authenticatedUserService.UserId);

            await _context.Users.AddAsync(user);
            _cacheService.Delete(Cache_Users);
            await _context.SaveChangesAsync();

            return await this.GetById(user.Id,cancellationToken);
        }

    }
}
