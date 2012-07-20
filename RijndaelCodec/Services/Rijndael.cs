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
        private const byte FillByte = 255;
        public Rijndael(byte[] key)
        {
            Key = key;
        }

        public byte[] Encrypt(string data)
        {

            var dataBytes = GetArrangedData(data);
            byte[] resultBytes;
            MemoryStream sout = new MemoryStream();

            var rm = GetRijndael();
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

        private static byte[] GetArrangedData(string data)
        {
            var dataBytes = Encoding.Unicode.GetBytes(data);
            int mod = dataBytes.Length % 16;
            if (mod == 0)
                return dataBytes;

            var arrangedBytes = new byte[dataBytes.Length + (16 - mod)];
            for (int i = 0; i < arrangedBytes.Length; i++)
                arrangedBytes[i] = FillByte;

            Buffer.BlockCopy(dataBytes, 0, arrangedBytes, 0, dataBytes.Length);

            return arrangedBytes;

        }

        private static RijndaelManaged GetRijndael()
        {
            var rm = new RijndaelManaged();
            rm.Mode = CipherMode.ECB;
            rm.KeySize = 256;
            rm.Padding = PaddingMode.None;
            return rm;
        }
        public string Decrypt(byte[] data)
        {
            byte[] resultBuffer = new byte[data.Length];
            int resultBufferLength = 0;
            using (var sin = new MemoryStream(data))
            {
                var rm = GetRijndael();
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
                string result = Encoding.Unicode.GetString(resultBuffer);
                string fillString = Encoding.Unicode.GetString(new byte[] { FillByte});


                return result.Replace(fillString, string.Empty);
            }






        }


        private byte[] Key { get; set; }
    }
}
