using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SkavenCrypt
{
    public class AES
    {
        public static void EncryptAES(string keyword, string inputFile, string outputFile)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyword);
            byte[] iv = Encoding.UTF8.GetBytes(keyword);

            // Read the input file into a byte array
            byte[] inputData = File.ReadAllBytes(inputFile);

            // Encrypt the input data using AES
            byte[] outputData = AESEncryption(inputData, key, iv);

            // Write the encrypted data to the output file
            File.WriteAllBytes(outputFile, outputData);
        }

        public static void DecryptAES(string keyword, string inputFile, string outputFile)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyword);
            byte[] iv = Encoding.UTF8.GetBytes(keyword);

            // Read the input file into a byte array
            byte[] inputData = File.ReadAllBytes(inputFile);

            // Decrypt the input data using AES
            byte[] outputData = AESDecryption(inputData, key, iv);

            // Write the decrypted data to the output file
            File.WriteAllBytes(outputFile, outputData);
        }

        private static byte[] AESEncryption(byte[] inputData, byte[] key, byte[] iv)
        {
            // Create a new AES instance
            using (Aes aes = Aes.Create())
            {
                // Set the encryption key and IV
                aes.Key = key;
                aes.IV = iv;

                // Create a memory stream to hold the encrypted data
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create a crypto stream to encrypt the data
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Write the input data to the crypto stream
                        cs.Write(inputData, 0, inputData.Length);
                        cs.FlushFinalBlock();
                    }

                    // Return the encrypted data
                    return ms.ToArray();
                }
            }
        }

        private static byte[] AESDecryption(byte[] inputData, byte[] key, byte[] iv)
        {
            // Create a new AES instance
            using (Aes aes = Aes.Create())
            {
                // Set the encryption key and IV
                aes.Key = key;
                aes.IV = iv;

                // Create a memory stream to hold the decrypted data
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create a crypto stream to decrypt the data
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        // Write the input data to the crypto stream
                        cs.Write(inputData, 0, inputData.Length);
                        cs.FlushFinalBlock();
                    }

                    // Return the decrypted data
                    return ms.ToArray();
                }
            }
        }
    }
}

