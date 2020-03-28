using Eyon.Utilities.Extensions;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Eyon.Utilities.Security
{
    public static class Encryption
    {
        public static string Decrypt( this byte[] cipherText, byte[] Key, byte[] iv )
        {

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using ( Aes aesAlg = Aes.Create() )
            {
                aesAlg.Key = Key.GetSHA256();
                aesAlg.IV = iv.GetMD5();

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using ( MemoryStream msDecrypt = new MemoryStream(cipherText) )
                {
                    using ( CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read) )
                    {
                        using ( StreamReader srDecrypt = new StreamReader(csDecrypt) )
                        {
                            // Read the decrypted bytes from the decrypting  stream
                            // and place them in a string. 
                            //return srDecrypt.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
        public static byte[] Encrypt( this string plainText, byte[] Key, byte[] IV )
        {
            // Check arguments.
            if ( plainText == null || plainText.Length <= 0 )
                throw new ArgumentNullException("plainText");
            if ( Key == null || Key.Length <= 0 )
                throw new ArgumentNullException("Key");
            if ( IV == null || IV.Length <= 0 )
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object with the specified key and IV.
            using ( Aes aesAlg = Aes.Create() )
            {
                aesAlg.Key = Key.GetSHA256();
                aesAlg.IV = IV.GetMD5();
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using ( MemoryStream msEncrypt = new MemoryStream() )
                {
                    using ( CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write) )
                    {
                        using ( StreamWriter swEncrypt = new StreamWriter(csEncrypt) )
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
    }
}