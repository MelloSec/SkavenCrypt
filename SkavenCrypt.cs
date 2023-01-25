using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SkavenCrypt
{
    class SkavenCrypt
    {
        static void Main(string[] args)
        {
            // Check if the correct number of arguments have been provided
            if (args.Length < 3)
            {
                Console.WriteLine("Invalid number of arguments.");
                return;
            }

            // Determine the encryption mode
            string mode = args[0];
            if (!mode.StartsWith("-"))
            {
                Console.WriteLine("Invalid encryption mode.");
                return;
            }
            mode = mode.Substring(1);

            // Get the keyword and input/output file paths
            string keyword = args[1];
            string inputFile = args[2];
            string outputFile = args.Length > 3 ? args[3] : inputFile + ".enc";

            // Implement the encryption logic
            switch (mode.ToLower())
            {
                case "rc4":
                    RC4.EncryptRC4(keyword, inputFile, outputFile);
                    break;
                case "xor":
                    byte[] inputDataXOR = File.ReadAllBytes(inputFile);
                    byte[] outputData = XOR.xorEncDec(inputDataXOR, keyword);
                    File.WriteAllBytes(outputFile, outputData);
                    break;
                case "aes":
                    AES.EncryptAES(keyword, inputFile, outputFile);
                    break;
                case "des":
                    // DES encryption logic
                    break;
                case "dxor":
                    byte[] inputDataDXOR = File.ReadAllBytes(inputFile);
                    byte[] outputDataDXOR = XOR.xorDecryption(inputDataDXOR, keyword);
                    File.WriteAllBytes(outputFile, outputDataDXOR);
                    break;
                case "drc4":
                    // Decryption logic for RC4
                    break;
                case "daes":
                    AES.DecryptAES(keyword, inputFile, outputFile);
                    break;
                default:
                    Console.WriteLine("Invalid encryption mode.");
                    break;
            }
        }
    }
}

