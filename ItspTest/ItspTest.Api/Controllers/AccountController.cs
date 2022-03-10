using ItspTest.Api.Dtos.Requests;
using ItspTest.Api.Helpers;
using ItspTest.Api.Services.User;
using ItspTest.Core.Models;
using ItspTest.Core.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace ItspTest.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(
            IUserService userService,
            IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Constants.ResponseMessages.Error.UsernameOrPasswordRequired);
            }

            ApplicationUser user = await _userService.GetUserAsync(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            JwtSecurityToken token = new JwtHelper(_configuration).GetToken(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Constants.ResponseMessages.Error.UsernameOrPasswordRequired);
            }

            ApplicationUser userExists = await _userService.GetUserAsync(model.Username, model.Password);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.UserExist);

            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            bool succeeded = await _userService.CreateAsync(user, model.Password);
            if (!succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.UserCreationFailed);

            return Ok(Constants.ResponseMessages.Success.UserCreated);
        }
    }
}