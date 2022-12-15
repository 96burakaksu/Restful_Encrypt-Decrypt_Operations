
using Core;
using Core.GeneralResult;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Client
{
    class Program
    {

        static void Main(string[] args)
        {
            string token = "";
        BasaDon:
            Users loginPost = new Users();
            Console.Write("Key: ");
            loginPost.Key = Console.ReadLine();
            Console.Write("Pass: ");
            loginPost.Pass = Console.ReadLine();

            ApiConnect _apiConnet = new ApiConnect(loginPost);
            var result = _apiConnet.GetApiToken();
            if (result.Success)
            {
                token = result.Data;
             var   a= _apiConnet.GetAll(token);
                tekraral:
                Console.WriteLine(" Encrpt için 1'e Decripty için 2'ye ");

                Console.Write("İşlem:");
                var islem = Console.ReadLine();
                switch (islem)
                {
                    case "1":
                        {
                            Console.WriteLine("Encripto");
                            _apiConnet.GetAll(token);
                            goto tekraral;


                        }
                    case "2":
                        Console.WriteLine("Decripto");
                        _apiConnet.GetAll(token);
                        goto tekraral;
                       
                    default:
                        Console.WriteLine("Yanlış bir operatör girdiniz. Tekrar deneyin.");
                        goto tekraral;
                }


            }
            else
            {
                Console.WriteLine(result.Message);
                Console.WriteLine("Yeniden Griniz");
                goto BasaDon;
            }


        }
    }


}
