using System.ComponentModel.DataAnnotations;

namespace ItspTest.Api.Dtos.Requests
{
    public class AddMovieCollectionRequest
    {
        [Required]
        public string Name { get; set; }
    }
}