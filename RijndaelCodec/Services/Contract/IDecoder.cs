using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RijndaelCodec.Services.Contract
{
    public interface IDecoder
    {
        string Decode(byte[] bytesToDecode);



    }
}
