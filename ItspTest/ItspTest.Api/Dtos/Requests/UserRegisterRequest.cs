using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItspTest.Api.Dtos.Requests
{
    public sealed class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RoleEnum Role { get; set; }

        [JsonIgnore]
        public string RoleValue
        {
            get
            {
                if (Role == RoleEnum.User)
                {
                    return Core.Authorization.Role.User;
                }

                return Core.Authorization.Role.Admin;
            }
        }
    }

    public enum RoleEnum
    {
        User,
        Admin
    }
}