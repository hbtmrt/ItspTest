using ItspTest.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.User
{
    public sealed class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserAsync(string username, string password)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }

            return null;
        }

        public async Task<bool> CreateAsync(ApplicationUser user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }
    }
}