using ItspTest.Api.Dtos;
using ItspTest.Api.Dtos.Requests;
using ItspTest.Api.Services.MovieCollection;
using ItspTest.Api.Services.User;
using ItspTest.Core.Authorization;
using ItspTest.Core.CustomExceptions;
using ItspTest.Core.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItspTest.Api.Controllers
{
    [EnableCors("OpenCORSPolicy")]
    [Authorize(Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        #region Delcarations

        private readonly IMovieCollectionService _movieCollectionService;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        #endregion Delcarations

        #region Constructor

        public CollectionController(
            IMovieCollectionService movieCollectionService,
            IUserService userService,
            ILogger<AccountController> logger)
        {
            _movieCollectionService = movieCollectionService;
            _userService = userService;
            _logger = logger;
        }

        #endregion Constructor

        #region Methods

        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<string> TestAsync()
        {
            return "It works!!!";
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieCollectionDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCollectionsAsync()
        {
            _logger.LogInformation(Constants.Log.Info.GetCollectionsRequestReceived);
            return Ok(await _movieCollectionService.GetUserCollectionsAsync());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieCollectionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCollectionAsync([FromBody] AddMovieCollectionRequest request)
        {
            _logger.LogInformation(Constants.Log.Info.AddCollectionRequestReceived);

            if (!ModelState.IsValid)
            {
                _logger.LogError(Constants.Log.Error.InvalidRequest, JsonConvert.SerializeObject(request));
                return BadRequest(JsonConvert.SerializeObject(ModelState.Values.SelectMany(v => v.Errors)));
            }

            string currentUserId = _userService.GetUserId(User);
            try
            {
                MovieCollectionDto collection = await _movieCollectionService.AddCollectionAsync(currentUserId, request);
                _logger.LogError(Constants.Log.Info.CollectionCreated, JsonConvert.SerializeObject(request));
                return Ok(collection);
            }
            catch (CollectionExistException)
            {
                _logger.LogError(Constants.Log.Error.CollectionExist, currentUserId);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.CollectionAlreadyExist);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Log.Error.AddCollectionFailed, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieCollectionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCollectionAsync(int id)
        {
            _logger.LogInformation(Constants.Log.Info.GetCollectionsRequestReceived);
            return Ok(await _movieCollectionService.GetCollectionAsync(id));
        }

        [HttpGet("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchMyMovies(int id, string searchText)
        {
            _logger.LogInformation(Constants.Log.Info.SearchCollectionRequestReceived);

            try
            {
                return Ok(await _movieCollectionService.SearchCollection(id, searchText));
            }
            catch (CollectionNotExistException)
            {
                _logger.LogError(Constants.Log.Error.CollectionNotExist, id);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.CollectionNotExist);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Log.Error.SearchCollectionFailed, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}/availableMovies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvailableMovies(int id)
        {
            _logger.LogInformation(Constants.Log.Info.GetCollectionsRequestReceived);
            string currentUserId = _userService.GetUserId(User);
            return Ok(await _movieCollectionService.GetAvailableMovies(id, currentUserId));
        }

        [HttpPost("{id}/movies/add-range")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMoviesAsync(int id, [FromBody] AddMoviesRequest request)
        {
            _logger.LogInformation(Constants.Log.Info.GetCollectionsRequestReceived);
            string currentUserId = _userService.GetUserId(User);
            return Ok(await _movieCollectionService.AddMoviesAsync(id, currentUserId, request.MovieIds));
        }

        [HttpPost("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovieAsync(int id, [FromBody] AddMovieRequest request)
        {
            _logger.LogInformation(Constants.Log.Info.AddMovieRequestReceived);

            if (!ModelState.IsValid)
            {
                _logger.LogError(Constants.Log.Error.InvalidRequest, JsonConvert.SerializeObject(request));
                return BadRequest(JsonConvert.SerializeObject(ModelState.Values.SelectMany(v => v.Errors)));
            }

            try
            {
                string currentUserId = _userService.GetUserId(User);
                return Ok(await _movieCollectionService.AddMovieAsync(id, request, currentUserId));
            }
            catch (CollectionNotExistException)
            {
                _logger.LogError(Constants.Log.Error.CollectionNotExist, id);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.CollectionNotExist);
            }
            catch (NotAllowedActionException)
            {
                _logger.LogError(Constants.Log.Error.NotAllowed);
                return StatusCode(StatusCodes.Status403Forbidden, Constants.ResponseMessages.Error.Forbidden);
            }
            catch (MovieNotFoundException)
            {
                _logger.LogError(Constants.Log.Error.MovieNotFoundInCollection, request.MovieId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.MovieNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Log.Error.AddMovieFailed, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{collectionId}/movies/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovieAsync(int collectionId, int id)
        {
            _logger.LogInformation(Constants.Log.Info.DeleteMovieRequestReceived);

            try
            {
                string currentUserId = _userService.GetUserId(User);
                await _movieCollectionService.DeleteMovieAsync(collectionId, id, currentUserId);
                _logger.LogInformation(Constants.Log.Info.MovieDeleted, id, collectionId);
                return Ok();
            }
            catch (CollectionNotExistException)
            {
                _logger.LogError(Constants.Log.Error.CollectionNotExist, collectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.CollectionNotExist);
            }
            catch (NotAllowedActionException)
            {
                _logger.LogError(Constants.Log.Error.NotAllowed);
                return StatusCode(StatusCodes.Status403Forbidden, Constants.ResponseMessages.Error.Forbidden);
            }
            catch (MovieNotFoundException)
            {
                _logger.LogError(Constants.Log.Error.MovieNotFoundInCollection, id, collectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.MovieNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Log.Error.DeleteMovieFailed, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion Methods
    }
}