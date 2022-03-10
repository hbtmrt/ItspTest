using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItspTest.Core.Models
{
    public class UserCollection
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public virtual List<UserMovieCollection> UserMovieCollections { get; set; }

        public UserCollection()
        {
            UserMovieCollections = new();
        }
    }
}