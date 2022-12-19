using Core.GeneralResult;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CryptoService.Middlewares
{
    public class TokenRedirectMiddleware 
    {
        RequestDelegate _next;
        public TokenRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var token = httpContext.Request.Headers["token"].ToString();
            Console.WriteLine(token);
            if (token != null && token!=null)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                    var response = httpClient.GetAsync("https://localhost:44380/Auth/Validation").GetAwaiter().GetResult();
                    var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await _next.Invoke(httpContext);
                    }
                    else
                    {
                        httpContext.Response.Headers.Add("Message", "Session expired!");
                        httpContext.Response.StatusCode = 401;
             
                        await httpContext.Response.WriteAsync("Missing requeired keys !");

                        return;
                    }
                }
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Request.Headers.Add("result", "No tokens!");
                await httpContext.Response.WriteAsync("Token cannot be empty!");
                return;
            }

    
               
        }
        


    }
}
