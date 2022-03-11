using ItspTest.Api.Dtos;
using ItspTest.Api.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.MovieCollection
{
    public interface IMovieCollectionService
    {
        Task<List<MovieCollectionDto>> GetUserCollectionsAsync();
        Task<MovieCollectionDto> AddCollectionAsync(string currentUserId, AddMovieCollectionRequest request);
        Task<List<MovieDto>> SearchCollection(int id, string searchText);
        Task<MovieDto> AddMovieAsync(int id, AddMovieRequest request, string currentUserId);
        Task DeleteMovieAsync(int collectionId, int movieId, string currentUserId);
        Task<MovieCollectionDto> GetCollectionAsync(int id);
        Task<List<MovieDto>> GetAvailableMovies(int collectionId, string currentUserId);
        Task<MovieCollectionDto> AddMoviesAsync(int id, string currentUserId, List<int> movieIds);
    }
}