using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string key, string pass);
    }
}
