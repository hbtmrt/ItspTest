using ItspTest.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string username, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password, string role);
        Task<List<string>> GetUserRoles(ApplicationUser user);
        string GetUserId(ClaimsPrincipal user);
        string GetTest();
    }
}