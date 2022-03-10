using System.ComponentModel.DataAnnotations;

namespace ItspTest.Api.Dtos.Requests
{
    public sealed class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}