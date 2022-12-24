using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace TinyMemFS

{

    public static class TextEncryptor
    {

        public static Tuple<byte[], Tuple<byte[], byte[]>> 
            Encrypt(byte[] plainText, string EncryptionKey, byte[] saltStringBytes = null, byte[] ivStringBytes = null)
        {
         
            Tuple<byte[], Tuple<byte[], byte[]>> tuple_data;
            using (Aes encryptor = Aes.Create())
            {
                if (saltStringBytes == null)
                {
                    saltStringBytes = new Byte[16];

                    //RNGCryptoServiceProvider is an implementation of a random number generator.
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetBytes(saltStringBytes); // The array is now filled with cryptographically strong random bytes.
                }
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, saltStringBytes);
                encryptor.Key = pdb.GetBytes(32);
                if(ivStringBytes == null)
                    encryptor.IV = pdb.GetBytes(16);
                else
                    encryptor.IV = ivStringBytes;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainText, 0, plainText.Length);
                        cs.Close();
                    }
                    plainText = ms.ToArray();
                    tuple_data = Tuple.Create(plainText, Tuple.Create(saltStringBytes, encryptor.IV));
                }
            }
            return tuple_data;
        }
        public static byte[] Decrypt(byte[] plainText, string EncryptionKey, byte[] saltStringBytes = null, byte[] ivStringBytes = null)
        {

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, saltStringBytes);
                encryptor.Key = pdb.GetBytes(32);
                if (ivStringBytes == null)
                    encryptor.IV = pdb.GetBytes(16);
                else
                    encryptor.IV = ivStringBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainText, 0, plainText.Length);
                        cs.Close();
                    }
                    plainText = ms.ToArray();
                }
            }
            return plainText;
        }
    }
}