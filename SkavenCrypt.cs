using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SkavenCrypt;

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
                    // XOR encryption logic
                    break;
                case "aes":
                    // AES encryption logic
                    break;
                case "des":
                    // DES encryption logic
                    break;
                default:
                    Console.WriteLine("Invalid encryption mode.");
                    break;
            }
        }
    }
}