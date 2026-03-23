using System.IO;
using System.Text;

namespace DrawTower.Logic
{
    public class AesProcess
    {
        public static byte[] EncryptStringToBytes(string plainText)
        {
            // Portfolio build: keep the persistence flow but do not ship embedded secrets.
            return Encoding.UTF8.GetBytes(plainText ?? string.Empty);
        }

        public static string DecryptStringFromBytes(byte[] cipherText)
        {
            if (cipherText == null || cipherText.Length == 0)
            {
                return string.Empty;
            }

            using (var msDecrypt = new MemoryStream(cipherText))
            using (var srDecrypt = new StreamReader(msDecrypt, Encoding.UTF8))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}
