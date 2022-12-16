using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            var key1 = httpContext.Request.Headers.Keys.Contains("Client-Key");
            var key2 = httpContext.Request.Headers.Keys.Contains("Device-Id");

            if (!key1 || !key2)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Missing requeired keys !");
                return;
            }
            else
            {
                //todo
            }
         

            //File.AppendAllText("almanca.txt", DateTime.Now.TimeOfDay.ToString() + Environment.NewLine);

            string dosya_yolu = @"C:\Users\burak\OneDrive\Masaüstü\metinbelgesi.txt";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            
            sw.WriteLine("lalalalla");
            sw.WriteLine("lalalalla2");
            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();
            await _next.Invoke(httpContext);
        }

      
    }
}
