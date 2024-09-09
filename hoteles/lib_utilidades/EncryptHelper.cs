using System.Security.Cryptography;
using System.Text;

namespace lib_utilidades
{
    public class EncryptHelper
    {
        private static string key = "v00ca210da4eed65bbce2ea235ca85d4";
        private static Aes? aes = null;

        public static void Create()
        {
            if (aes != null)
                return;
            aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];
        }

        public static string Encrypt(string value)
        {
            Create();
            ICryptoTransform encryptor = aes!.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cs))
                    {
                        writer.Write(value);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

    }
}
