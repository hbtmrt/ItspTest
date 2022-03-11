using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItspTest.Api.Dtos.Requests
{
    public class AddMoviesRequest
    {
        public List<int> MovieIds { get; set; }
    }
}
