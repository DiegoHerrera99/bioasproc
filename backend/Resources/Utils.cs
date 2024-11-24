using System.Security.Cryptography;
using System.Text;

namespace bioinsumos_asproc_backend.Resources
{
    public class Utils
    {
        public static string EncryptPassword(string password)
        {
            StringBuilder sb = new();
            Encoding enc = Encoding.UTF8;
            byte[] result = SHA256.HashData(enc.GetBytes(password));

            foreach (byte b in result)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}