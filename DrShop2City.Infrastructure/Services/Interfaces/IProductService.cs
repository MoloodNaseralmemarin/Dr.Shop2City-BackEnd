using DrShop2City.DataLayer.Entities.Product;
using DrShop2City.Infrastructure.DTOs.Products;

namespace DrShop2City.Infrastructure.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        #region product

        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<FilterProductsViewModel> FilterProducts(FilterProductsViewModel filter);
        Task<Product> GetProductById(long productId);
        Task<List<Product>> GetRelatedProducts(long productId);
        Task<bool> IsExistsProductById(long productId);
        Task<Product> GetProductForUserOrder(long productId);
        Task<EditProductViewModel> GetProductForEdit(long productId);

        Task EditProduct(EditProductViewModel product);
        #endregion
        #region product categories

        Task<List<ProductCategory?>> GetAllActiveProductCategories();

        #endregion
        #region proudct gallery

        Task<List<ProductGallery>> GetProductActiveGalleries(long productId);

        #endregion
        #region product comments
        Task AddCommentToProduct(ProductComment comment);
        Task<List<ProductCommentViewModel>> GetActiveProductComments(long productId);
        Task<ProductCommentViewModel> AddProductComment(AddProductCommentViewModel comment, long userId);
        #endregion
        #region latest products
        
        #endregion
    }

}