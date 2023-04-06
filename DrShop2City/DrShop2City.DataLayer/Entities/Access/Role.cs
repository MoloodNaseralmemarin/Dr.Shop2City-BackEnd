using System.ComponentModel.DataAnnotations;
using DrShop2City.Const;
using DrShop2City.DataLayer.Entities.Common;

namespace DrShop2City.DataLayer.Entities.Access
{
    public class Role : BaseEntity
    {
        #region Properties

        [Display(Name = "نام سیستمی")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Name { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Title { get; set; }

        #endregion

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}
