using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Datas
{
    public class DefinedUsers
    {
        public readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"key1", "pass1" },
            {"key3", "pass3" }
        };
    }
}
