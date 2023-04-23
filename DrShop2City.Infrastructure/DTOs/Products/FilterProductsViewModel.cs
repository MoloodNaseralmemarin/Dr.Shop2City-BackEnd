using DrShop2City.DataLayer.Entities.Product;
using DrShop2City.Infrastructure.DTOs.Paging;

namespace DrShop2City.Infrastructure.DTOs.Products
{
    public class FilterProductsViewModel : BasePaging
    {
        public string? Title { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public List<Product>? Products { get; set; }

        public List<long>? Categories { get; set; }

        public ProductOrderBy OrderBy { get; set; }

        public FilterProductsViewModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            return this;
        }

        public FilterProductsViewModel SetProducts(List<Product> products)
        {
            this.Products = products;
            return this;
        }

        public enum ProductOrderBy
        {
            PriceAsc,
            PriceDec,
            CreateDateAsc,
            CreateDateDes,
            IsSpecial
        }
    }
}