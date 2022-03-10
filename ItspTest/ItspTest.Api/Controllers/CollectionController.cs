using ItspTest.Api.Dtos;
using ItspTest.Api.Dtos.Requests;
using ItspTest.Api.Services.MovieCollection;
using ItspTest.Core.Authorization;
using ItspTest.Core.CustomExceptions;
using ItspTest.Core.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItspTest.Api.Controllers
{
    [Authorize(Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly IMovieCollectionService _movieCollectionService;
        private readonly ILogger<AccountController> _logger;

        public CollectionController(
            IMovieCollectionService movieCollectionService,
            ILogger<AccountController> logger)
        {
            _movieCollectionService = movieCollectionService;
            _logger = logger;
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
                return BadRequest(Constants.ResponseMessages.Error.UsernameOrPasswordRequired);
            }

            try
            {
                MovieCollectionDto collection = await _movieCollectionService.AddCollectionAsync(request);
                _logger.LogError(Constants.Log.Info.CollectionCreated, JsonConvert.SerializeObject(request));
                return Ok(collection);
            }
            catch (CollectionExistException)
            {
                _logger.LogError(Constants.Log.Error.CollectionExist, request.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.CollectionAlreadyExist);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Log.Error.AddCollectionFailed, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchMyMovies(string id, string searchText)
        {
            _logger.LogInformation(Constants.Log.Info.SearchCollectionRequest);

            try
            {
                return Ok(await _movieCollectionService.SearchCollection(id, searchText));
            }
            catch (CollectionNotExistException)
            {
                _logger.LogError(Constants.Log.Error.CollectionExist, id);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.CollectionNotExist);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Log.Error.SearchCollectionFailed, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}