using ItspTest.Api.Dtos;
using ItspTest.Api.Services.MovieCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItspTest.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly IMovieCollectionService _movieCollectionService;

        public CollectionController(IMovieCollectionService movieCollectionService)
        {
            _movieCollectionService = movieCollectionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieCollectionDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCollectionsAsync()
        {
            return Ok(await _movieCollectionService.GetUserCollectionsAsync());
        }
    }
}