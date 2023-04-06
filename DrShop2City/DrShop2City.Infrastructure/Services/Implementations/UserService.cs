using DrShop2City.DataLayer.Entities.Access;
using DrShop2City.DataLayer.Entities.Account;
using DrShop2City.DataLayer.Repository;
using DrShop2City.Infrastructure.DTOs.Account;
using DrShop2City.Infrastructure.Security;
using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Convertors;
using Microsoft.EntityFrameworkCore;

namespace DrShop2City.Infrastructure.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IGenericRepository<UserRole> _userRoleRepository;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, IGenericRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _userRoleRepository = userRoleRepository;
        }

        #endregion

        #region User Section

        public async Task<List<User?>> GetAllUsers()
        {
            return await _userRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterViewModel register)
        {
            if (IsUserExistsByEmail(register.Email))
                return RegisterUserResult.EmailExists;

            //TODO:Create User
            var user = new User
            {
                Email = register.Email.SanitizeText(),
                Address = register.Address.SanitizeText(),
                FirstName = register.FirstName.SanitizeText(),
                LastName = register.LastName.SanitizeText(),
                EmailActiveCode = Guid.NewGuid().ToString(),//تابع درست کن
                Password = _passwordHelper.EncodePasswordMd5(register.Password)
            };

            await _userRepository.AddEntity(user);

            await _userRepository.SaveChanges();

            //TODO:Send Email
            //var body = await _renderView.RenderToStringAsync("Email/ActivateAccount", user);

            //_mailSender.Send("mnaseri208@gmail.com", "test", body);

            return RegisterUserResult.Success;
        }

        public bool IsUserExistsByEmail(string email)
        {
            return _userRepository.GetEntitiesQuery().Any(s => s != null && s.Email == email.ToLower().Trim());
        }

        public async Task<LoginUserResult> LoginUser(LoginUserViewModel login, bool checkAdminRole = false)
        {
            var password = _passwordHelper.EncodePasswordMd5(login.Password);

            var user = await _userRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(s => s != null && s.Email == login.Email.ToLower().Trim() && s.Password == password);

            if (user == null) return LoginUserResult.IncorrectData;

            if (!user.IsActivated) return LoginUserResult.NotActivated;

            if (checkAdminRole)
            { 
                if (!await IsUserAdmin(user.Id))
                {
                    return LoginUserResult.NotAdmin;
                }
            }
            return LoginUserResult.Success;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(s => s.Email == email.ToLower().Trim());
        }

        public async Task<User?> GetUserByUserId(long userId)
        {
            return await _userRepository.GetEntityById(userId);
        }

        public async Task EditUserInfo(EditUserViewModel user, long userId)
        {
            var mainUser = await _userRepository.GetEntityById(userId);
            if (mainUser != null)
            {
                mainUser.FirstName = user.FirstName;
                mainUser.LastName = user.LastName;
                mainUser.Address = user.Address;
                _userRepository.UpdateEntity(mainUser);
                await _userRepository.SaveChanges();
            }
        }

        public async Task<bool> IsUserAdmin(long userId)
        {
            return await _userRoleRepository.GetEntitiesQuery()
                .Include(s => s.Role)
                .AsQueryable().AnyAsync(s => s.UserId == userId && s.Role.Name == "Admin");
        }
        #endregion

        #region dispose

        public void Dispose()
        {
            _userRepository?.Dispose();
        }

  
        #endregion
    }
}
