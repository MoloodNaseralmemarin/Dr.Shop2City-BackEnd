using System.Threading.Tasks;
using DrShop2City.Infrastructure.DTOs.Products;
using DrShop2City.Infrastructure.Services.Implementations;
using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Common;
using DrShop2City.WebApi.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DrShop2City.WebApi.Controllers
{
    public class AdminProductsController : SiteBaseController
    {
        #region constructor

        private readonly IProductService _productService;

        public AdminProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region edit product

        [PermissionChecker("ProductManager")]
        [HttpGet("get-product-for-edit/{id}")]
        public async Task<IActionResult> GetProductForEdit(long id)
        {
            var product = await _productService.GetProductForEdit(id);
            if (product == null) return JsonResponseStatus.NotFound();
            return JsonResponseStatus.Success(product);
        }

        #endregion

        #region edit product

        [HttpPost("edit-product")]
        public async Task<IActionResult> EditProduct([FromBody] EditProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                await _productService.EditProduct(product);
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.Error();
        }

        #endregion
    }
}
