using System.Security.Cryptography;
using System.Text;

public static class HashHelper
{
    public static string Hash(string value)
    {

        byte[] bytes = Encoding.UTF8.GetBytes(value);
        byte[] hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }

}