using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RijndaelCodec.Services.Contract
{
    public interface ICoder
    {
        byte[] Encode(string stringToEncode);
    }
}
