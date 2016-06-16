using System.Security.Cryptography;
using System.Text;

namespace BookLibrary.ApiTest
{
    class PasswordHasher
    {
        public static string GetMd5Hash(string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();

            }
        }
    }
}
