using DrShop2City.Const;
using System.ComponentModel.DataAnnotations;

namespace DrShop2City.Infrastructure.DTOs.Products
{
    public class EditProductViewModel
    {
        public long Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string ProductName { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        public string Description { get; set; }

        public string CurrentImage { get; set; }

        [Display(Name = "موجود / به اتمام رسیده")]
        public bool IsExists { get; set; }

        [Display(Name = "ویژه")]
        public bool IsSpecial { get; set; }

        public string Base64Image { get; set; }
    }
}
