using Dll4Xll;
using Org.BouncyCastle.Crypto;
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
            
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid arguments. Usage: filename <mode> [keyword] <inputFile> <outputFile>");
                Console.WriteLine("Available modes are '-aes', '-b64' and '-xor'");
                return;
            }
            string mode = args[0];
            string inputFile = "";
            string outputFile = "";
            string keyword = "";
            
            if (!mode.StartsWith("-"))
            {
                Console.WriteLine("Invalid encryption mode.");
                return;
            }
            mode = mode.Substring(1);

            if (args.Length == 3)
            {
                inputFile = args[1];
                outputFile = args[2];
            }
            else if (args.Length == 4)
            {
                keyword = args[1];
                inputFile = args[2];
                outputFile = args[3];
            }


/*            // Get the keyword and input/output file paths
            string keyword = args[1];
            string inputFile = args[2];
            string outputFile = args.Length > 3 ? args[3] : inputFile + ".enc";*/

            // Check if the -encode or -decode flag has been passed
            bool isEncode = args.Contains("-encode");
            bool isDecode = args.Contains("-decode");
            bool isCompress = args.Contains("-compress");
            bool isDecompress = args.Contains("-decompress");
            bool isDecrypt = args.Contains("-decrypt");

            // Implement the encryption logic
            switch (mode.ToLower())
            {
                /*                case "rc4":
                                    if (isEncode)
                                    {
                                        // This can prob be replaced wholesale with SkavenCode methods
                                        // Its also broken because the order is wrong. So now we know. Needs to be encrypt, compress, encode
                                        byte[] inputData = File.ReadAllBytes(inputFile);
                                        byte[] compressedData = Dll4Xll.SkavenCode.Compress(inputData);

                                        RC4.EncryptRC4(keyword, encodedData, outputFile);
                                        byte[] encodedData = Dll4Xll.SkavenCode.Encode(compressedData);
                                    }
                                    else if (isDecode)
                                    {
                                        // This too, needs decrypt, decompress, decode
                                        byte[] decryptedData = RC4.DecryptRC4(keyword, inputFile);
                                        byte[] decompressedData = SkavenCode.Decompress(decryptedData);
                                        byte[] decodedData = SkavenCode.Decode(decompressedData);
                                        File.WriteAllBytes(outputFile, decodedData);
                                    }
                                    else
                                    {
                                        RC4.EncryptRC4(keyword, inputFile, outputFile);
                                    }
                                    break;*/
                case "xor":
                    if (isEncode)
                    {
                        /*                        byte[] inputData = File.ReadAllBytes(inputFile);
                                                byte[] encryptedData = XOR.xorEncDec(inputData, keyword);
                                                byte[] compressedData = SkavenCode.CompressData(encryptedData);
                                                SkavenCode.EncodeToBase64Data(compressedData, outputFile);
                                                File.WriteAllBytes(outputFile, compressedData);*/

                        // Try it maybe with compress first?
                        {
                            byte[] inputData = File.ReadAllBytes(inputFile);
                            byte[] encryptedData = XOR.EncryptXOR(inputData, keyword);
                            byte[] compressedData = SkavenCode.CompressData(encryptedData);
                            SkavenCode.EncodeToBase64Data(compressedData, outputFile);
                        }

                    }
                    else if (isDecode)
                    {
                        // Decode the file
                        byte[] decodedBytes = Convert.FromBase64String(File.ReadAllText(inputFile));
                        byte[] inputData = SkavenCode.DecompressData(decodedBytes);
                        byte[] decryptedData = XOR.DecryptXOR(inputData, keyword);
                        File.WriteAllBytes(outputFile, decryptedData);
                    }
                    else if (isDecrypt)                   
                    {
                        // Old Way
                        /*                        byte[] inputBytes = File.ReadAllBytes(inputFile);
                                                byte[] decryptedBytes = XOR.DecryptXOR(inputBytes, keyword);
                                                File.WriteAllBytes(outputFile, decryptedBytes);*/
                        XOR.DecryptXORFile(inputFile, keyword, outputFile);

                    }
                    else
                    {
                        // Old Way
                        /*                        byte[] inputData = File.ReadAllBytes(inputFile);
                                                byte[] xorEncrypted = XOR.EncryptXOR(inputData, keyword);
                                                File.WriteAllBytes(outputFile, xorEncrypted);*/
                        XOR.EncryptXORFile(inputFile, keyword, outputFile);
                    }
                    break;
                case "aes":
                    if (isEncode)
                    {
                        // Encrypt the file
                        string encryptedFile = "encrypted_" + outputFile;
                        AES.EncryptAES(keyword, inputFile, encryptedFile);

                        // Compress and encode the encrypted file
                        SkavenCode.CompressAndEncodeFile(encryptedFile, outputFile);

                        // Delete the intermediate encrypted file
                        
                    }
                    else if (isDecode)
                    {
                        // Decode the file
                        byte[] decodedBytes = Convert.FromBase64String(File.ReadAllText(inputFile));
                        byte[] decodedData = SkavenCode.DecompressData(decodedBytes);
                        byte[] decryptedData = AES.DecryptAES(keyword, decodedData);
                        File.WriteAllBytes(outputFile, decryptedData);
                    }
                    else if (isDecrypt)
                    {
                        byte[] inputData = File.ReadAllBytes(inputFile);
                        byte[] decryptedData = AES.DecryptAES(keyword, inputData);
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
                        SkavenCode.EncodeToBase64File(inputFile, outputFile);
                    }
                    else if (isDecode)
                    {
                         // Decode the file from base64
                         SkavenCode.DecodeFromBase64File(inputFile, outputFile);
                    }
                    else if (isCompress)
                    {
                        SkavenCode.CompressFile(inputFile, outputFile);
                    }
                    else if (isDecompress)
                    {
                        SkavenCode.DecompressFile(inputFile, outputFile);
                    }
                    else if (isCompress && isEncode)
                    {
                        SkavenCode.CompressAndEncodeFile(inputFile, outputFile);
                    }
                    else if (isDecompress && isDecode)
                    {
                        SkavenCode.DecodeAndDecompressFile(inputFile, outputFile);
                        
                    }
                    else
                    {
                        Console.WriteLine("get yo life.");
                    }
                    break;
            }
        }
    }
}   
