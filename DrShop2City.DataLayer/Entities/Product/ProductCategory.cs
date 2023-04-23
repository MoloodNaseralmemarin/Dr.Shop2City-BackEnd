using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DrShop2City.Const;
using DrShop2City.DataLayer.Entities.Common;

namespace DrShop2City.DataLayer.Entities.Product
{
    public class ProductCategory : BaseEntity
    {
        #region Properties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Title { get; set; }

        [Display(Name = "عنوان لینک")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string UrlTitle { get; set; }

        public long? ParentId { get; set; }


        #endregion

        #region Relations

        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        #endregion
    }
}
