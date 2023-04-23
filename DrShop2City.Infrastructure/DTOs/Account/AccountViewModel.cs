
using DrShop2City.Const;
using System.ComponentModel.DataAnnotations;

namespace DrShop2City.Infrastructure.DTOs.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        [Compare("Password", ErrorMessage =ErrorMessage.CompareMessage)]
        public string ConfirmPassword { get; set; }

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
        [MaxLength(500, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Address { get; set; }
    }

    public enum RegisterUserResult
    {
        Success,
        EmailExists
    }

    public class LoginUserViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Password { get; set; }
    }

    public enum LoginUserResult
    {
        Success,
        IncorrectData,
        NotActivated,
        NotAdmin
    }
}
