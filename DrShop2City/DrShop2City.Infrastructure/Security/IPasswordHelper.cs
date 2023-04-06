namespace DrShop2City.Infrastructure.Security
{
    public interface IPasswordHelper
    {
        string EncodePasswordMd5(string password);
    }
}
