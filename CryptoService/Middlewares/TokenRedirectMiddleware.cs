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
                    dosyayaYaz(responseString);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await _next.Invoke(httpContext);
                    }
                    else
                    {
                        httpContext.Response.Headers.Add("Message", "OturumSüresiSonlanmıştır");
                        httpContext.Response.StatusCode = 401;
             
                                 await httpContext.Response.WriteAsync("Missing requeired keys !");
                        //await _next.Invoke(httpContext);
                        return;
                    }
                }
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Request.Headers.Add("result", "Token Yok!");
                await httpContext.Response.WriteAsync("Token Boş Olamaz!");
                //await _next.Invoke(httpContext);
                return;
            }

    
                //await _next.Invoke(httpContext);
        }
        
private static void dosyayaYaz(string yazi)
        {
            string dosya_yolu = @"C:\Users\burak\OneDrive\Masaüstü\metinbelgesi.txt";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            sw.WriteLine(yazi);
            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.
        }

    }
}
