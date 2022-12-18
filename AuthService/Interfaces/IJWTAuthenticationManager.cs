
using Core;
using Core.GeneralResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public interface IJWTAuthenticationManager
    {
        DataResult<string> Authenticate(Users user);

    }
}
