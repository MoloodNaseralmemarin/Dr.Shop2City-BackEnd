using System;
using System.Collections.Generic;
using DrShop2City.DataLayer.Entities.Account;
using DrShop2City.DataLayer.Entities.Common;

namespace DrShop2City.DataLayer.Entities.Orders
{
    public class Order : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public bool IsPay { get; set; }

        public DateTime? PaymentDate { get; set; }

        #endregion

        #region relations

        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
