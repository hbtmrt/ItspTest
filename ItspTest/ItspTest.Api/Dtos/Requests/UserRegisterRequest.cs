using System.ComponentModel.DataAnnotations;

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
    }
}