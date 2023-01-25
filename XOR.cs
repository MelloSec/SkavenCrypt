using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkavenCrypt
{
    public class XOR
    {
        static byte[] xorEncDec(byte[] inputData, string keyPhrase)
        {
            //byte[] keyBytes = Encoding.UTF8.GetBytes(keyPhrase);
            byte[] bufferBytes = new byte[inputData.Length];
            for (int i = 0; i < inputData.Length; i++)
            {
                bufferBytes[i] = (byte)(inputData[i] ^ Encoding.UTF8.GetBytes(keyPhrase)[i % Encoding.UTF8.GetBytes(keyPhrase).Length]);
            }
            return bufferBytes;
        }

    }
}
