using System.ComponentModel.DataAnnotations;
using DrShop2City.Const;
using DrShop2City.DataLayer.Entities.Common;

namespace DrShop2City.DataLayer.Entities.Product
{
    public class ProductVisit : BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        [Display(Name = "IP")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string UserIp { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        #endregion
    }
}
