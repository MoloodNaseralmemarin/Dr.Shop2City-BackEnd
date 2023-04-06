namespace DrShop2City.Infrastructure.Utilities.Common
{
    public static class PathTools
    {
        #region domain

        public static string Domain = "https://localhost:44309";

        #endregion

        #region product

        public static string ProductImagePath = "/images/products/origin/";
        public static string ProductImageServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products/origin/");

        #endregion


    }
}
