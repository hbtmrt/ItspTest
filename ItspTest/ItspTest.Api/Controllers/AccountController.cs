using ItspTest.Api.Dtos.Requests;
using ItspTest.Api.Helpers;
using ItspTest.Api.Services.User;
using ItspTest.Core.Models;
using ItspTest.Core.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace ItspTest.Api.Controllers
{
    [EnableCors("OpenCORSPolicy")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IUserService userService,
            IConfiguration configuration,
            ILogger<AccountController> logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequest model)
        {
            _logger.LogInformation(
                Constants.Log.Info.UserAuthenticateRequestReceived,
                JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogError(Constants.Log.Error.InvalidRequest, JsonConvert.SerializeObject(model));

                return BadRequest(Constants.ResponseMessages.Error.UsernameOrPasswordRequired);
            }

            ApplicationUser user = await _userService.GetUserAsync(model.Username, model.Password);

            if (user == null)
            {
                _logger.LogError(Constants.Log.Error.UserNotExist, model.Username);

                return Unauthorized();
            }

            List<string> userRoles = await _userService.GetUserRoles(user);

            JwtSecurityToken token = new JwtHelper(_configuration).GetToken(user, userRoles);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation(Constants.Log.Info.TokenCreated, tokenString);

            return Ok(new
            {
                token = tokenString,
                expiration = token.ValidTo,
                userId = user.Id
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest model)
        {
            _logger.LogInformation(
                    Constants.Log.Info.UserRegisterRequestReceived,
                    JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogError(Constants.Log.Error.InvalidRequest, JsonConvert.SerializeObject(model));
                return BadRequest(Constants.ResponseMessages.Error.UsernameOrPasswordRequired);
            }

            ApplicationUser userExists = await _userService.GetUserAsync(model.Username, model.Password);
            if (userExists != null)
            {
                _logger.LogError(Constants.Log.Error.UserAlreadyExist, model.Username);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.UserExist);
            }

            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            IdentityResult result = await _userService.CreateAsync(user, model.Password, model.RoleValue);
            if (!result.Succeeded)
            {
                _logger.LogError(Constants.Log.Error.UserCreationFailed, JsonConvert.SerializeObject(result.Errors));
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.ResponseMessages.Error.UserCreationFailed);
            }

            return Ok(Constants.ResponseMessages.Success.UserCreated);
        }
    }
}