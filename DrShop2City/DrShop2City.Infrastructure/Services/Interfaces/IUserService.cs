using DrShop2City.DataLayer.Entities.Account;
using DrShop2City.Infrastructure.DTOs.Account;
using System.Threading.Tasks;

namespace DrShop2City.Infrastructure.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsers();
        Task<RegisterUserResult> RegisterUser(RegisterViewModel register);
        bool IsUserExistsByEmail(string email);
        Task<LoginUserResult> LoginUser(LoginUserViewModel login,bool checkAdminRole=false);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserId(long userId);
        Task EditUserInfo(EditUserViewModel user, long userId);
        Task<bool> IsUserAdmin(long userId);
    }
}