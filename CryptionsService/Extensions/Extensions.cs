using CryptionsService.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptionsService.Extensions
{
    static public class Extensions
    {
        static public IApplicationBuilder UseTokenRedirect(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<TokenRedirectMiddleware>();
        }
    }
}
