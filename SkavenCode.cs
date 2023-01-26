using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace SkavenCrypt
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

        public static byte[] Compress(byte[] inputBytes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream compressionStream = new GZipStream(ms, CompressionMode.Compress))
                {
                    compressionStream.Write(inputBytes, 0, inputBytes.Length);
                }
                return ms.ToArray();
            }
        }

        public static byte[] Decompress(byte[] inputBytes)
        {
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (GZipStream decompressionStream = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(outputStream);
                        return outputStream.ToArray();
                    }
                }
            }
        }
    }
}


