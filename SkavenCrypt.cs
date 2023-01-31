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

            // Check if the -encode or -decode flag has been passed
            bool isEncode = args.Contains("-encode");
            bool isDecode = args.Contains("-decode");

            // Implement the encryption logic
            switch (mode.ToLower())
            {
                case "rc4":
                    if (isEncode)
                    {
                        byte[] inputData = File.ReadAllBytes(inputFile);
                        byte[] compressedData = SkavenCode.Compress(inputData);
                        byte[] encodedData = SkavenCode.Encode(compressedData);
                        RC4.EncryptRC4(keyword, encodedData, outputFile);
                    }
                    else if (isDecode)
                    {
                        byte[] decryptedData = RC4.DecryptRC4(keyword, inputFile);
                        byte[] decompressedData = SkavenCode.Decompress(decryptedData);
                        byte[] decodedData = SkavenCode.Decode(decompressedData);
                        File.WriteAllBytes(outputFile, decodedData);
                    }
                    else
                    {
                        RC4.EncryptRC4(keyword, inputFile, outputFile);
                    }
                    break;
                case "xor":
                    if (isEncode)
                    {
                        byte[] inputData = File.ReadAllBytes(inputFile);
                        byte[] encryptedData = XOR.xorEncDec(inputData, keyword);
                        byte[] compressedData = SkavenCode.Compress(encryptedData);
                        byte[] encodedData = SkavenCode.Encode(compressedData);
                        File.WriteAllBytes(outputFile, encodedData);

                    }
                    else if (isDecode)
                    {
                        // Decode the file
                        byte[] decodedBytes = Convert.FromBase64String(File.ReadAllText(inputFile));
                        byte[] decompressedData = SkavenCode.Decompress(decodedBytes);
                        byte[] decryptedData = XOR.xorEncDec(decompressedData, keyword);
                        File.WriteAllBytes(outputFile, decryptedData);
                    }
                    else
                    {
                        XOR.EncryptXOR(keyword, inputFile, outputFile);
                    }
                    break;
                case "aes":
                    if (isEncode)
                    {
                        // Encrypt the file
                        byte[] inputData = File.ReadAllBytes(inputFile);
                        byte[] encryptedData = AES.EncryptAES(keyword, inputData);
                        byte[] compressedData = SkavenCode.Compress(encryptedData);
                        byte[] encodedData = SkavenCode.Encode(compressedData);
                        File.WriteAllBytes(outputFile, encodedData);
                    }
                    else if (isDecode)
                    {
                        // Decode the file
                        byte[] decodedBytes = Convert.FromBase64String(File.ReadAllText(inputFile));
                        byte[] decodedData = SkavenCode.Decompress(decodedBytes);
                        byte[] decryptedData = AES.DecryptAES(keyword, decodedData);
                        File.WriteAllBytes(outputFile, decryptedData);
                    }
                    else
                    {
                        AES.EncryptAES(keyword, inputFile, outputFile);
                    }
                    break;
                case "-b64":
                    if (isEncode)
                    {
                        // Encode the file to base64
                        string base64Encoded = SkavenCode.EncodeToBase64(inputFile);
                        File.WriteAllText(outputFile, base64Encoded);
                    }
                    else if (isDecode)
                    {
                        // Decode the file from base64
                        byte[] decodedBytes = SkavenCode.DecodeFromBase64(inputFile);
                        File.WriteAllBytes(outputFile, decodedBytes);
                    }
                    break;
