using ItspTest.Core.Models;
using System.Threading.Tasks;

namespace ItspTest.Api.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string username, string password);
        Task<bool> CreateAsync(ApplicationUser user, string password);
    }
}