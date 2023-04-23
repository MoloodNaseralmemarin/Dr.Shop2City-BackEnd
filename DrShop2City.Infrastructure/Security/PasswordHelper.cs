using System.Security.Cryptography;
using System.Text;

namespace DrShop2City.Infrastructure.Security;

public class PasswordHelper: IPasswordHelper
{
    [Obsolete("Obsolete")]
    public string EncodePasswordMd5(string password)   
    { 
        MD5 md5 = new MD5CryptoServiceProvider();
        var originalBytes = Encoding.Default.GetBytes(password);
        var encodedBytes = md5.ComputeHash(originalBytes);
        return BitConverter.ToString(encodedBytes);
    }
}