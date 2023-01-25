using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkavenCrypt
{
    public class RC4
    {
        public static async void EncryptRC4(string keyword, string inputFile, string outputFile)
        {
            // You encryption/decryption key as a bytes array
            var key = Encoding.UTF8.GetBytes(keyword);
            var cipher = new RC4Engine();
            var keyParam = new KeyParameter(key);
            // for decrypting the file just switch the first param here to false
            cipher.Init(true, keyParam);

            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                // processing the file 4KB at a time.
                byte[] buffer = new byte[1024 * 4];
                long totalBytesRead = 0;
                long totalBytesToRead = inputStream.Length;
                while (totalBytesToRead > 0)
                {
                    // make sure that your method is marked as async
                    int read = await inputStream.ReadAsync(buffer, 0, buffer.Length);

                    // break the loop if we didn't read anything (EOF)
                    if (read == 0)
                    {
                        break;
                    }

                    totalBytesRead += read;
                    totalBytesToRead -= read;

                    byte[] outBuffer = new byte[1024 * 4];
                    cipher.ProcessBytes(buffer, 0, read, outBuffer, 0);
                    await outputStream.WriteAsync(outBuffer, 0, read);
                }
            }
        }
    }
}
