using DrShop2City.DataLayer.Entities.Common;

namespace DrShop2City.DataLayer.Entities.Orders
{
    public class OrderDetail : BaseEntity
    {
        #region properties

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }

        #endregion

        #region relations

        public Order Order { get; set; }

        public Product.Product Product { get; set; }

        #endregion
    }
}
