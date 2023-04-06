using System.ComponentModel.DataAnnotations;
using DrShop2City.Const;
using DrShop2City.DataLayer.Entities.Account;
using DrShop2City.DataLayer.Entities.Common;

namespace DrShop2City.DataLayer.Entities.Product
{
    public class ProductComment : BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        public long UserId { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(1000, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Text { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        public User User { get; set; }

        #endregion

    }
}
