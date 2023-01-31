using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkavenCrypt
{
    public class XOR
    {
        public static byte[] xorEncDec(byte[] inputData, string keyword)
        {
            //byte[] keyBytes = Encoding.UTF8.GetBytes(keyPhrase);
            byte[] bufferBytes = new byte[inputData.Length];
            for (int i = 0; i < inputData.Length; i++)
            {
                bufferBytes[i] = (byte)(inputData[i] ^ Encoding.UTF8.GetBytes(keyword)[i % Encoding.UTF8.GetBytes(keyword).Length]);
            }
            return bufferBytes;
        }

        public static byte[] EncryptXOR(byte[] inputData, string keyword)
        {
            byte[] xorEncrypted = xorEncDec(inputData, keyword);
            return xorEncrypted;

        }

        static public byte[] DecryptXOR(byte[] inputData, string keyword)
        {
            // changed this if it breaks check here 
            byte[] xorDecrypted = xorEncDec(inputData, keyword);
            return xorDecrypted;

        }

        public static void EncryptXORFile(string inputFile, string keyword, string outputFile)
        {
            byte[] inputData = File.ReadAllBytes(inputFile);
            byte[] xorEncrypted = xorEncDec(inputData, keyword);
            File.WriteAllBytes(outputFile, xorEncrypted);
        }

        public static void DecryptXORFile(string inputFile, string keyword, string outputFile)
        {
            byte[] inputData = File.ReadAllBytes(inputFile);
            byte[] xorDecrypted = xorEncDec(inputData, keyword);
            File.WriteAllBytes(outputFile, xorDecrypted);
        }



    }
}
