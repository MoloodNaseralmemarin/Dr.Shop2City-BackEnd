namespace DrShop2City.Infrastructure.DTOs.Products
{
    public class ProductCommentViewModel
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string UserFullName { get; set; }

        public string CreateDate { get; set; }

        public string Text { get; set; }
    }
}
