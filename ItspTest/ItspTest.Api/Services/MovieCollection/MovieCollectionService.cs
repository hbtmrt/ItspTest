using AutoMapper;
using ItspTest.Api.Dtos;
using ItspTest.Api.Dtos.Requests;
using ItspTest.Core.Contexts;
using ItspTest.Core.CustomExceptions;
using ItspTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return _mapper.Map<List<MovieCollectionDto>>(await _collectionContext.UserCollections.ToListAsync());
        }

        public async Task<MovieCollectionDto> AddCollectionAsync(AddMovieCollectionRequest request)
        {
            UserCollection collectionExists = await _collectionContext.UserCollections.SingleOrDefaultAsync(c => c.UserId.Equals(request.UserId));

            if (collectionExists != null)
            {
                throw new CollectionExistException();
            }

            UserCollection userCollection = new UserCollection
            {
                UserId = request.UserId,
                Name = request.Name
            };

            _collectionContext.UserCollections.Add(userCollection);
            await _collectionContext.SaveChangesAsync();

            return _mapper.Map<MovieCollectionDto>(userCollection);
        }

        public async Task<List<MovieDto>> SearchCollection(string userId, string searchText)
        {
            UserCollection userCollection = await _collectionContext.UserCollections.SingleOrDefaultAsync(c => c.UserId.Equals(userId));

            if (userCollection == null)
            {
                throw new CollectionNotExistException();
            }

            _collectionContext.Entry(userCollection).Collection(b => b.UserMovieCollections).Load();

            var query = userCollection.UserMovieCollections.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(umc => umc.Movie.Name.Contains(searchText));
            }

            List<Movie> movieList = query.Select(umc => umc.Movie).ToList();
            return _mapper.Map<List<MovieDto>>(movieList);
        }
    }
}