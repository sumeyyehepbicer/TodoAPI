using Todo.Shared.DTOs.RoleDtos;
using Todo.Shared.DTOs.UserPermissionDtos;

namespace Todo.Shared.DTOs.UserDtos
{
    public class UserDto:BaseDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public RoleDto Role { get; set; }
        public virtual ICollection<UserPermissionDto> Permissions { get; set; }
    }
}
