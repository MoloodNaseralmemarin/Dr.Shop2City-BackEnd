using System;
using System.Threading.Tasks;
using DrShop2City.DataLayer.Entities.Orders;
using DrShop2City.Infrastructure.DTOs.Orders;

namespace DrShop2City.Infrastructure.Services.Interfaces
{
    public interface IOrderService : IDisposable
    {
        #region order

        Task<Order> CreateUserOrder(long userId);
        Task<Order> GetUserOpenOrder(long userId);

        #endregion

        #region order detail

        Task AddProductToOrder(long userId, long productId, int count);
        Task<List<OrderDetail>> GetOrderDetails(long orderId);
        Task<List<OrderBasketDetail>> GetUserBasketDetails(long userId);
        Task DeleteOrderDetail(OrderDetail detail);
        #endregion
    }
}