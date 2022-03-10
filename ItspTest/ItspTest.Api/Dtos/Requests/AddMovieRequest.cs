using System.ComponentModel.DataAnnotations;

namespace ItspTest.Api.Dtos.Requests
{
    public class AddMovieRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }
    }
}