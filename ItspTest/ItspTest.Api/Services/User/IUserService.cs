using ItspTest.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string username, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    }
}