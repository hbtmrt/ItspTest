using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItspTest.Core.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Year { get; set; }

        public virtual List<UserMovieCollection> UserMovieCollections { get; set; }

        public Movie()
        {
            UserMovieCollections = new();
        }
    }
}