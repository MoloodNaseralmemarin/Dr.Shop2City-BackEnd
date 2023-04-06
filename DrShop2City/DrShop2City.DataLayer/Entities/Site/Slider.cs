using System.ComponentModel.DataAnnotations;
using DrShop2City.Const;
using DrShop2City.DataLayer.Entities.Common;

namespace DrShop2City.DataLayer.Entities.Site
{
    public class Slider : BaseEntity
    {
        #region Properties

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(150, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string ImageName { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(1000, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Description { get; set; }

        [Display(Name = "لینک")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Link { get; set; }
        #endregion
    }
}
