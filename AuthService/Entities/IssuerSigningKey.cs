using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Datas
{
    public class IssuerSigningKey
    {
        public   byte[] MySecret = Encoding.ASCII.GetBytes("It is a secret created for Procenne");

    }
}
