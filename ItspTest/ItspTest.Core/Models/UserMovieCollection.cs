namespace ItspTest.Core.Models
{
    public class UserMovieCollection
    {
        public int UserCollectionId { get; set; }
        public UserCollection UserCollection { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}