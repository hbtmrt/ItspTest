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
        #region Declarations

        private readonly CollectionContext _collectionContext;
        private readonly IMapper _mapper;

        #endregion Declarations

        #region Constructor

        public MovieCollectionService(CollectionContext context, IMapper mapper)
        {
            _collectionContext = context;
            _mapper = mapper;
        }

        #endregion Constructor

        #region Methods

        public async Task<List<MovieCollectionDto>> GetUserCollectionsAsync()
        {
            return _mapper.Map<List<MovieCollectionDto>>(await _collectionContext.UserCollections.ToListAsync());
        }

        public async Task<MovieCollectionDto> AddCollectionAsync(string currentUserId, AddMovieCollectionRequest request)
        {
            UserCollection collectionExists = await _collectionContext.UserCollections.SingleOrDefaultAsync(c => c.UserId.Equals(currentUserId));

            if (collectionExists != null)
            {
                throw new CollectionExistException();
            }

            UserCollection userCollection = new()
            {
                UserId = currentUserId,
                Name = request.Name
            };

            _collectionContext.UserCollections.Add(userCollection);
            await _collectionContext.SaveChangesAsync();

            return _mapper.Map<MovieCollectionDto>(userCollection);
        }

        public async Task<List<MovieDto>> SearchCollection(int id, string searchText)
        {
            UserCollection userCollection = await _collectionContext.UserCollections
                .FindAsync(id);

            if (userCollection == null)
            {
                throw new CollectionNotExistException();
            }

            var query = _collectionContext.UserMovieCollections.Include(umc => umc.Movie)
                .Where(umc => umc.UserCollectionId == id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(q => q.Movie.Name.Contains(searchText));
            }

            List<Movie> movieList = query.Select(umc => umc.Movie).ToList();
            return _mapper.Map<List<MovieDto>>(movieList);
        }

        public async Task<MovieDto> AddMovieAsync(int collectionId, AddMovieRequest request, string currentUserId)
        {
            UserCollection collectionExists = await _collectionContext.UserCollections.FindAsync(collectionId);

            if (collectionExists == null)
            {
                throw new CollectionNotExistException();
            }

            if (!collectionExists.UserId.Equals(currentUserId))
            {
                throw new NotAllowedActionException();
            }

            Movie movie = await _collectionContext.Movies.FindAsync(request.MovieId);

            if (movie == null)
            {
                throw new MovieNotFoundException();
            }

            await _collectionContext.UserMovieCollections.AddAsync(new UserMovieCollection
            {
                MovieId = request.MovieId,
                UserCollectionId = collectionExists.Id
            });

            await _collectionContext.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task DeleteMovieAsync(int collectionId, int movieId, string currentUserId)
        {
            UserCollection collectionExists = await _collectionContext.UserCollections.FindAsync(collectionId);

            if (collectionExists == null)
            {
                throw new CollectionNotExistException();
            }

            if (!collectionExists.UserId.Equals(currentUserId))
            {
                throw new NotAllowedActionException();
            }

            var movie = _collectionContext.UserMovieCollections
                .FirstOrDefault(umc => umc.UserCollectionId == collectionId && umc.MovieId == movieId);

            if (movie == null)
            {
                throw new MovieNotFoundException();
            }

            _collectionContext.UserMovieCollections.Remove(movie);
            _collectionContext.SaveChanges();
        }

        public async Task<MovieCollectionDto> GetCollectionAsync(int id)
        {
            UserCollection collection = await _collectionContext.UserCollections.FindAsync(id);

            if (collection == null)
            {
                throw new CollectionNotExistException();
            }

            return _mapper.Map<MovieCollectionDto>(collection);
        }

        public async Task<List<MovieDto>> GetAvailableMovies(int collectionId, string currentUserId)
        {
            UserCollection collection = await _collectionContext.UserCollections.FindAsync(collectionId);

            if (collection == null)
            {
                throw new CollectionNotExistException();
            }

            if (!collection.UserId.Equals(currentUserId))
            {
                throw new NotAllowedActionException();
            }

            List<Movie> moviesInCollection = await _collectionContext.UserMovieCollections.Where(umc => umc.UserCollectionId == collectionId)
                .Select(umc => umc.Movie).ToListAsync();

            List<Movie> allMovies = await _collectionContext.Movies.ToListAsync();

            List<Movie> availableMovies = allMovies.Except(moviesInCollection).ToList();

            return _mapper.Map<List<MovieDto>>(availableMovies);
        }

        public async Task<MovieCollectionDto> AddMoviesAsync(int id, string currentUserId, List<int> movieIds)
        {
            UserCollection collection = await _collectionContext.UserCollections.FindAsync(id);

            if (collection == null)
            {
                throw new CollectionNotExistException();
            }

            if (!collection.UserId.Equals(currentUserId))
            {
                throw new NotAllowedActionException();
            }

            List<UserMovieCollection> newMoviesInCollection = new();
            movieIds.ForEach(movieId =>
            {
                newMoviesInCollection.Add(new UserMovieCollection
                {
                    MovieId = movieId,
                    UserCollectionId = id
                });
            });

            _collectionContext.UserMovieCollections.AddRange(newMoviesInCollection);
            await _collectionContext.SaveChangesAsync();

            return _mapper.Map<MovieCollectionDto>(collection);
        }

        #endregion Methods
    }
}