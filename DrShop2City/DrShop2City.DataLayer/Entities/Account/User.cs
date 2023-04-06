using System.ComponentModel.DataAnnotations;
using DrShop2City.Const;
using DrShop2City.DataLayer.Entities.Access;
using DrShop2City.DataLayer.Entities.Common;
using DrShop2City.DataLayer.Entities.Product;

namespace DrShop2City.DataLayer.Entities.Account
{
    public class User : BaseEntity
    {
        #region properties

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage =ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage =ErrorMessage.MaxLengthMessage)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string LastName { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string EmailActiveCode { get; set; }

        public bool IsActivated { get; set; }

        #endregion

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }

        #endregion
    }
}
