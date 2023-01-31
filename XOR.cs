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

        public static byte[] EncryptXOR(byte[] inputData, string keyword, string inputFile = null)
        {
            byte[] inputData = (inputFile == null) ? inputData : File.ReadAllBytes(inputFile);
            byte[] xorEncrypted = xorEncDec(inputData, keyword);
            if (inputFile != null) File.WriteAllBytes(inputFile + ".xor", xorEncrypted);
            return xorEncrypted;
        }

        public static byte[] DecryptXOR(byte[] inputData, string keyword, string inputFile = null)
        {
            byte[] inputData = (inputFile == null) ? inputData : File.ReadAllBytes(inputFile);
            byte[] xorDecrypted = xorEncDec(inputData, keyword);
            if (inputFile != null) File.WriteAllBytes(inputFile.Replace(".xor", ""), xorDecrypted);
            return xorDecrypted;
        }

        /*        public static byte[] EncryptXOR(byte[] inputData, string keyword)
                {
                    byte[] xorEncrypted = xorEncDec(inputData, keyword);
                    return xorEncrypted;

                }

                static public byte[] DecryptXOR(byte[] inputData, string keyword)
                {
                    // changed this if it breaks check here 
                    byte[] xorDecrypted = xorEncDec(inputData, keyword);
                    return xorDecrypted;

                }*/


    }
}
