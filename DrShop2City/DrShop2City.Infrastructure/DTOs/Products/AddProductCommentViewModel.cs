using DrShop2City.Const;
using System.ComponentModel.DataAnnotations;

namespace DrShop2City.Infrastructure.DTOs.Products
{
    public class AddProductCommentViewModel
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredMessage)]
        [MaxLength(1000, ErrorMessage = ErrorMessage.MaxLengthMessage)]
        public string Text { get; set; }
    }
}
