using System;
using System.Threading.Tasks;

namespace DrShop2City.Infrastructure.Services.Interfaces
{
    public interface IAccessService : IDisposable
    {
        #region user role

        Task<bool> CheckUserRole(long userId, string role);

        #endregion
    }
}