using DrShop2City.DataLayer.Entities.Access;
using DrShop2City.DataLayer.Entities.Account;
using DrShop2City.DataLayer.Entities.Orders;
using DrShop2City.DataLayer.Entities.Product;
using DrShop2City.DataLayer.Entities.Site;
using Microsoft.EntityFrameworkCore;

namespace DrShop2City.DataLayer.Context
{
    public class DrShop2CityDBContext : DbContext
    {
        #region constructor

        public DrShop2CityDBContext(DbContextOptions<DrShop2CityDBContext> options) : base(options)
        {
        }

        #endregion


        #region Db Sets

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }

        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        public DbSet<ProductVisit> ProductVisits { get; set; }

        public DbSet<ProductComment> ProductComments { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion

        #region disable cascading delete in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}