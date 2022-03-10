using AutoMapper;
using ItspTest.Api.Dtos;
using ItspTest.Core.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.MovieCollection
{
    public sealed class MovieCollectionService : IMovieCollectionService
    {
        private readonly CollectionContext _collectionContext;
        private readonly IMapper _mapper;

        public MovieCollectionService(CollectionContext context, IMapper mapper)
        {
            _collectionContext = context;
            _mapper = mapper;
        }

        public async Task<List<MovieCollectionDto>> GetUserCollectionsAsync()
        {
            var collections = _mapper.Map<List<MovieCollectionDto>>(await _collectionContext.UserCollections.ToListAsync());
            return collections;
        }
    }
}