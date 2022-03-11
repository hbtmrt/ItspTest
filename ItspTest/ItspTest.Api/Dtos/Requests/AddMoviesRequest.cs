using System.Collections.Generic;

namespace ItspTest.Api.Dtos.Requests
{
    public class AddMoviesRequest
    {
        public List<int> MovieIds { get; set; }
    }
}