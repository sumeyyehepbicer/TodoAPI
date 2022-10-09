using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.DTOs.RoleDtos;

namespace Todo.Shared.DTOs.UserDtos
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse()
        {

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public RoleDto RoleItem { get; set; }
        public string Role { get; set; }
        public List<AbilityDto> Ability { get; set; }
        

        public AuthenticateResponse(UserDto user, List<AbilityDto> abilities, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Role = user.Role.Title;
            RoleItem = user.Role;
            AccessToken = token;
            Ability = abilities;
            RefreshToken = token;
        }

    }

}
