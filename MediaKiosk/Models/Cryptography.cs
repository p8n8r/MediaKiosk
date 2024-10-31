using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/*
* Credit for encryption/decrytion algorithm goes to 
* https://www.c-sharpcorner.com/article/encryption-and-decryption-using-a-symmetric-key-in-c-sharp/
*/

namespace MediaKiosk.Models
{
    public static class Cryptography
    {
        private const int SIZE_IV = 16;
        private const string KEY = "uJ4ERcCWIjAqN2hTDDG38Q=="; //Hard-coded, for simplicity
        private static readonly byte[] KEY_BYTES = Encoding.UTF8.GetBytes(KEY);

        public static string EncryptString(string text)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = KEY_BYTES;
                aes.IV = new byte[SIZE_IV];
                
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, 
                        encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string DecryptString(string encryptedText)
        {
            byte[] bytesEncrypted = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = KEY_BYTES;
                aes.IV = new byte[SIZE_IV];
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(bytesEncrypted))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, 
                        decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
