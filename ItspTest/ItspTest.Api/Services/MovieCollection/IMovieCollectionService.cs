using ItspTest.Api.Dtos;
using ItspTest.Api.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.MovieCollection
{
    public interface IMovieCollectionService
    {
        Task<List<MovieCollectionDto>> GetUserCollectionsAsync();
        Task<MovieCollectionDto> AddCollectionAsync(AddMovieCollectionRequest request);
        Task<List<MovieDto>> SearchCollection(string userId, string searchText);
        Task<MovieDto> AddMovieAsync(int id, AddMovieRequest request, string currentUserId);
    }
}