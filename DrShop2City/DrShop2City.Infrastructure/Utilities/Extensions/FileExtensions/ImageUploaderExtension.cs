 using System.Drawing;
using System.Drawing.Imaging;

namespace DrShop2City.Infrastructure.Utilities.Extensions.FileExtensions
{
    public static class ImageUploaderExtension
    {
        public static void AddImageToServer(this Image image, string fileName, string originalPath, string deleteFileName = null)
        {
            if (image != null)
            {
                if (!Directory.Exists(originalPath)) Directory.CreateDirectory(originalPath);

                if (!string.IsNullOrEmpty(deleteFileName)) File.Delete(originalPath + deleteFileName);

                var imageName = originalPath + fileName;

                using (var stream = new FileStream(imageName, FileMode.Create))
                {
                    if (!Directory.Exists(imageName)) image.Save(stream, ImageFormat.Jpeg);
                }
            }
        }

        public static byte[] DecodeUrlBase64(string s)
        {
            return Convert.FromBase64String(s.Substring(s.LastIndexOf(',') + 1));
        }

        public static Image Base64ToImage(string base64string)
        {
            var res = DecodeUrlBase64(base64string);
            var ms = new MemoryStream(res, 0, res.Length);
            ms.Write(res, 0, res.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }
    }
}
