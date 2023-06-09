﻿using System.ComponentModel.DataAnnotations;
using DrShop2City.Const;
using DrShop2City.DataLayer.Entities.Common;
using DrShop2City.DataLayer.Entities.Orders;

namespace DrShop2City.DataLayer.Entities.Product
{
    public class Product : BaseEntity
    {
        #region properties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string ProductName { get; set; }

        [Display(Name = "قیمت")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public int Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        public string Description { get; set; }

        [Display(Name = "نام تصویر")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string ImageName { get; set; }

        [Display(Name = "موجود / به اتمام رسیده")]
        public bool IsExists { get; set; }

        [Display(Name = "ویژه")]
        public bool IsSpecial { get; set; }


        #endregion

        #region relations

        public ICollection<ProductGallery> ProductGalleries { get; set; }

        public ICollection<ProductVisit> ProductVisits { get; set; }

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        public ICollection<ProductComment> ProductComments { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
