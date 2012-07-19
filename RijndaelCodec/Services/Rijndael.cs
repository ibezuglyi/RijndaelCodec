using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace RijndaelCodec.Services
{
    public class Rijndael
    {
        public Rijndael(byte[] key)
        {
            Key = key;
        }

        public byte[] Encrypt(string data)
        {

            var dataBytes = Encoding.Unicode.GetBytes(data);
            byte[] resultBytes;
            MemoryStream sout = new MemoryStream();

            var rm = new RijndaelManaged();
            rm.Mode = CipherMode.ECB;
            rm.KeySize = 256;
            var enc = rm.CreateEncryptor(Key, null);
            using (var cs = new CryptoStream(sout, enc, CryptoStreamMode.Write))
            {
                cs.Write(dataBytes, 0, dataBytes.Length);
                sout.Seek(0, SeekOrigin.Begin);
                resultBytes = new byte[sout.Length];
                sout.Read(resultBytes, 0, (int)sout.Length);
            }
            return resultBytes;
        }
        public string Decrypt(byte[] data)
        {
            byte[] resultBuffer = new byte[data.Length];
            int resultBufferLength = 0;
            using (var sin = new MemoryStream(data))
            {
                var rm = new RijndaelManaged();
                rm.Mode = CipherMode.ECB;
                rm.KeySize = 256;
                var buf = new byte[256];
                var dec = rm.CreateDecryptor(Key, null);
                using (var cs = new CryptoStream(sin, dec, CryptoStreamMode.Read))
                {
                    int len;
                    while ((len = cs.Read(buf, 0, buf.Length)) > 0)
                    {
                        Buffer.BlockCopy(buf, 0, resultBuffer, resultBufferLength, len);
                        resultBufferLength += buf.Length;
                    }
                }
                return Encoding.Unicode.GetString(resultBuffer);
            }






        }


        private byte[] Key { get; set; }
    }
}
