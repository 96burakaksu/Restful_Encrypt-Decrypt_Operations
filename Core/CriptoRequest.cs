using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CriptoRequest:CriptoData
    {
        public bool IsEncrypt { get; set; }
    }
    public class CriptoData
    {
        public byte[] Data { get; set; }
    }
}
