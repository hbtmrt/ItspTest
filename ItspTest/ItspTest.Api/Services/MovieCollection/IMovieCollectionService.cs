using ItspTest.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.MovieCollection
{
    public interface IMovieCollectionService
    {
        Task<List<MovieCollectionDto>> GetUserCollectionsAsync();
    }
}