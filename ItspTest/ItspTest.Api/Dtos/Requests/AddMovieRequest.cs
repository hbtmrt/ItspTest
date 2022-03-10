using System.ComponentModel.DataAnnotations;

namespace ItspTest.Api.Dtos.Requests
{
    public class AddMovieRequest
    {
        [Required]
        public int MovieId { get; set; }
    }
}