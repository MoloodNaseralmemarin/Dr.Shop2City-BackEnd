using DrShop2City.DataLayer.Entities.Access;
using DrShop2City.DataLayer.Entities.Account;
using DrShop2City.DataLayer.Repository;
using DrShop2City.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrShop2City.Infrastructure.Services.Implementations
{
    public class AccessService : IAccessService
    {
        #region constructor

        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IGenericRepository<User> _userRepository;

        public AccessService(IGenericRepository<Role> roleRepository, IGenericRepository<UserRole> userRoleRepository, IGenericRepository<User> userRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region user role

        public async Task<bool> CheckUserRole(long userId, string role)
        {
            var result = await _userRoleRepository.GetEntitiesQuery().AsQueryable()
                .AnyAsync(s => s.UserId == userId && s.Role.Name == role);
            return result;
        }

        #endregion

        #region dispose

        public void Dispose()
        {
            _userRepository?.Dispose();
            _userRoleRepository?.Dispose();
            _roleRepository?.Dispose();
        }

        #endregion
    }
}
