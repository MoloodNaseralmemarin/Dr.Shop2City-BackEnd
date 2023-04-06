using DrShop2City.Infrastructure.DTOs.Products;
using DrShop2City.Infrastructure.Services.Implementations;
using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Common;
using DrShop2City.Infrastructure.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DrShop2City.WebApi.Controllers
{
    public class ProductsController : SiteBaseController
    {
        #region constructor

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region products

        [HttpGet("filter-products")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsViewModel filter)
        {
            var products = await _productService.FilterProducts(filter);

            // await Task.Delay(4000);

            return JsonResponseStatus.Success(products);

        }

        #endregion

        #region get products categories

        [HttpGet("product-active-categories")]
        public async Task<IActionResult> GetProductsCategories()
        {
            return JsonResponseStatus.Success(await _productService.GetAllActiveProductCategories());
        }

        #endregion

        #region get single product

        [HttpGet("single-product/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await _productService.GetProductById(id);
            var productGalleries = await _productService.GetProductActiveGalleries(id);

            //به خاطر لودینگ در کلاینت
            await Task.Delay(3000);
            if (product != null)
                return JsonResponseStatus.Success(new { product = product, galleries = productGalleries });
            return JsonResponseStatus.NotFound(new { message = "محصولی بااین مشخصات وارد شده یافت نشد" });
        }


        #endregion

        #region related products
        [HttpGet("related-products/{id}")]
        public async Task<IActionResult> GetRelatedProducts(long id)
        {
            var relatedProducts = await _productService.GetRelatedProducts(id);

            return JsonResponseStatus.Success(relatedProducts);
        }
        #endregion

        #region product comments

        #region list

        [HttpGet("product-comments/{id}")]
        public async Task<IActionResult> GetProductComments(long id)
        {
            var comments = await _productService.GetActiveProductComments(id);

            return JsonResponseStatus.Success(comments);
        }

        #endregion

        #region add

        [HttpPost("add-product-comment")]
        public async Task<IActionResult> AddProductComponent([FromBody] AddProductCommentViewModel comment)
        {
            if (!User.Identity.IsAuthenticated)
                return JsonResponseStatus.Error(new { message = "لطفا ابتدا وارد سایت شوید" });

            if (!await _productService.IsExistsProductById(comment.ProductId))
                return JsonResponseStatus.NotFound();

            var userId = User.GetUserId();

            var res = await _productService.AddProductComment(comment, userId);

            return JsonResponseStatus.Success(res);
        }

        #endregion

        #endregion
    }
}