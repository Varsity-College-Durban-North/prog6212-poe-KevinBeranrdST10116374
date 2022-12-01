using System.Text;
using XSystem.Security.Cryptography;

namespace ST10116374_PROG6212_POE
{
    public class PasswordHash
    {
        public static string Hash_SHA1(string input)
        {
            //Code to hash the password
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
