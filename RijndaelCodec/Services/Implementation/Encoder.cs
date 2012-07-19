using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RijndaelCodec.Services.Contract;

namespace RijndaelCodec.Services.Implementation
{
    public class Encoder : ICoder, IDecoder
    {

        public byte[] Key { get; set; }


        public Encoder(byte[] key)
        {
            Key = key;
        }

        public string Decode(byte[] bytesToDecode)
        {
            Rijndael decoder = new Rijndael(Key);
            return decoder.Decrypt(bytesToDecode);
        }

        public byte[] Encode(string stringToEncode)
        {
            Rijndael coder = new Rijndael(Key);
            return coder.Encrypt(stringToEncode);
        }

        
    }
}
