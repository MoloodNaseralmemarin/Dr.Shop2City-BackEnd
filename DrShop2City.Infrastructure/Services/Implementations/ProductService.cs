using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.DataLayer.Entities.Product;
using DrShop2City.DataLayer.Repository;
using DrShop2City.Infrastructure.DTOs.Products;
using DrShop2City.Infrastructure.DTOs.Paging;
using DrShop2City.Infrastructure.Utilities.Extensions.FileExtensions;
using DrShop2City.Infrastructure.Utilities.Extensions.Paging;
using Microsoft.EntityFrameworkCore;
using static DrShop2City.Infrastructure.DTOs.Products.FilterProductsViewModel;
using DrShop2City.Infrastructure.Utilities.Common;

namespace DrShop2City.Infrastructure.Services.Implementations
{
    public class ProductService : IProductService
    {

        #region constructor

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<ProductGallery> _productGalleryRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;
        private readonly IGenericRepository<ProductVisit> _productVisitRepository;
        private readonly IGenericRepository<ProductComment> _productCommentRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository, IGenericRepository<ProductVisit> productVisitRepository, IGenericRepository<ProductComment> productCommentRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productGalleryRepository = productGalleryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _productVisitRepository = productVisitRepository;
            _productCommentRepository = productCommentRepository;
        }

        #endregion

        #region product

        public async Task AddProduct(Product product)
        {
            await _productRepository.AddEntity(product);
            await _productRepository.SaveChanges();
        }

        public async Task UpdateProduct(Product product)
        {
            _productRepository.UpdateEntity(product);
            await _productRepository.SaveChanges();
        }

        public async Task<FilterProductsViewModel> FilterProducts(FilterProductsViewModel filter)
        {
            var productsQuery = _productRepository.GetEntitiesQuery().AsQueryable();

            switch (filter.OrderBy)
            {
                case ProductOrderBy.PriceAsc:
                    productsQuery = productsQuery.OrderBy(s => s.Price);
                    break;
                case ProductOrderBy.PriceDec:
                    productsQuery = productsQuery.OrderByDescending(s => s.Price);
                    break;
                case ProductOrderBy.CreateDateAsc:
                    productsQuery = productsQuery.OrderBy(s => s.CreateDate);
                    break;
                case ProductOrderBy.CreateDateDes:
                    productsQuery = productsQuery.OrderByDescending(s => s.CreateDate);
                    break;
                case ProductOrderBy.IsSpecial:
                    productsQuery = productsQuery.OrderByDescending(s => s.IsSpecial);
                    break;

            }

            if (!string.IsNullOrEmpty(filter.Title))
                productsQuery = productsQuery.Where(s => s.ProductName.Contains(filter.Title));

            if (filter.StartPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice);

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);

            productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice);

            if (filter.Categories != null && filter.Categories.Any())
                productsQuery = productsQuery.SelectMany(s =>
                    s.ProductSelectedCategories.Where(f => filter.Categories.Contains(f.ProductCategoryId)).Select(t => t.Product));

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
        }

        public async Task<Product> GetProductById(long productId)
        {
            return await _productRepository
                .GetEntitiesQuery()
                .AsQueryable()
                .SingleOrDefaultAsync(s => !s.IsDelete && s.Id == productId);
        }

        public async Task<List<Product>> GetRelatedProducts(long productId)
        {
            var product = await _productRepository.GetEntityById(productId);

            if (product == null) return null;

            var productCategoriesList = await _productSelectedCategoryRepository.GetEntitiesQuery()
                .Where(s => s.ProductId == productId).Select(f => f.ProductCategoryId).ToListAsync();

            var relatedProducts = await _productRepository
                .GetEntitiesQuery()
                .SelectMany(s => s.ProductSelectedCategories.Where(f => productCategoriesList.Contains(f.ProductCategoryId)).Select(t => t.Product))
                .Where(s => s.Id != productId)
                .OrderByDescending(s => s.CreateDate)
                .Take(4).ToListAsync();

            return relatedProducts;
        }
        public async Task<bool> IsExistsProductById(long productId)
        {
            return await _productRepository.GetEntitiesQuery().AnyAsync(s => s.Id == productId);
        }
        public async Task<Product> GetProductForUserOrder(long productId)
        {
            return await _productRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(s => s.Id == productId && !s.IsDelete);
        }

        public async Task<EditProductViewModel> GetProductForEdit(long productId)
        {
            var product = await _productRepository.GetEntitiesQuery().AsQueryable()
                    .SingleOrDefaultAsync(s => s.Id == productId);
                if (product == null) return null;

                return new EditProductViewModel
                {
                    Id = product.Id,
                    CurrentImage = product.ImageName,
                    Description = product.Description,
                    IsExists = product.IsExists,
                    IsSpecial = product.IsSpecial,
                    Price = product.Price,
                    ProductName = product.ProductName,
                    ShortDescription = product.ShortDescription
                };
            }

        public async Task EditProduct(EditProductViewModel product)
        {
            var mainProduct = await _productRepository.GetEntityById(product.Id);
            if (mainProduct != null)
            {
                mainProduct.ProductName = product.ProductName;
                mainProduct.Description = product.Description;
                mainProduct.IsExists = product.IsExists;
                mainProduct.IsSpecial = product.IsSpecial;
                mainProduct.Price = product.Price;
                mainProduct.Description = product.Description;
                mainProduct.ShortDescription = product.ShortDescription;
                if (!string.IsNullOrEmpty(product.Base64Image))
                {
                    var imageFile = ImageUploaderExtension.Base64ToImage(product.Base64Image);
                    var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
                    imageFile.AddImageToServer(imageName, PathTools.ProductImageServerPath, mainProduct.ImageName);
                    mainProduct.ImageName = imageName;
                }

                _productRepository.UpdateEntity(mainProduct);
                await _productRepository.SaveChanges();
            }
        }

        #endregion

        #region product categories

        public async Task<List<ProductCategory?>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetEntitiesQuery()
                .Where(s => !s.IsDelete).ToListAsync();
        }

        #endregion

        #region products gallery

        public async Task<List<ProductGallery>> GetProductActiveGalleries(long productId)
        {
            return await _productGalleryRepository
                .GetEntitiesQuery()
                .Where(s => s.ProductId == productId && !s.IsDelete)

                //به خاطر اینکه productنداره
                //no traking امتحان کن
                .Select(s => new ProductGallery
                {
                    ProductId = s.ProductId,
                    Id = s.Id,
                    ImageName = s.ImageName,
                    CreateDate = s.CreateDate
                }).ToListAsync();
        }

        #endregion

        #region product comments

        public async Task AddCommentToProduct(ProductComment comment)
        {
            await _productCommentRepository.AddEntity(comment);
            await _productCommentRepository.SaveChanges();
        }

        public async Task<List<ProductCommentViewModel>> GetActiveProductComments(long productId)
        {
            return await _productCommentRepository
                .GetEntitiesQuery()
                .Include(s => s.User)
                .Where(c => c.ProductId == productId && !c.IsDelete)
                .OrderByDescending(s => s.CreateDate)
                .Select(s => new ProductCommentViewModel
                {
                    Id = s.Id,
                    Text = s.Text,
                    UserId = s.UserId,
                    UserFullName = s.User.FirstName + " " + s.User.LastName,
                    CreateDate = s.CreateDate.ToString("yyyy/MM/dd HH:mm")
                }).ToListAsync();
        }

        public async Task<ProductCommentViewModel> AddProductComment(AddProductCommentViewModel comment, long userId)
        {
            var commentData = new ProductComment
            {
                ProductId = comment.ProductId,
                Text = comment.Text,
                UserId = userId
            };

            await _productCommentRepository.AddEntity(commentData);

            await _productCommentRepository.SaveChanges();

            return new ProductCommentViewModel
            {
                Id = commentData.Id,
                CreateDate = commentData.CreateDate.ToString("yyyy/MM/dd HH:mm"),
                Text = commentData.Text,
                UserId = userId,
                UserFullName = ""
            };
        }

        #endregion
        #region dispose

        public void Dispose()
        {
            _productRepository?.Dispose();
            _productCategoryRepository?.Dispose();
            _productGalleryRepository?.Dispose();
            _productSelectedCategoryRepository?.Dispose();
            _productVisitRepository?.Dispose();
        }
        #endregion
    }
}
