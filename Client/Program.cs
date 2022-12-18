
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
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
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
         
                tekraral:
                Console.WriteLine(" Encrpt için 1'e Decripty için 2'ye ");

                Console.Write("İşlem:");
                var islem = Console.ReadLine();
                switch (islem)
                {
                    case "1":
                        {
                            Console.WriteLine("Encripto edilecek metini girin :");
                           var encriptData= Console.ReadLine();
                            CriptoDataRequest encriptDataByte = new CriptoDataRequest { Data= ByteConverter.GetBytes(encriptData) };
                           var resultEnc= _apiConnet.GetEncription(encriptDataByte,token);
                           if(resultEnc.Success)
                            { Console.WriteLine(" EncripliData:");
                                Console.WriteLine(Convert.ToBase64String(resultEnc.Data));
                            }
                            else
                            {
                                Console.WriteLine(resultEnc.Message);
                            }
                            goto tekraral;


                        }
                    case "2":
                        {
                            Console.WriteLine("Decripto edilecek metini girin :");
                            var encriptData = Console.ReadLine();
                            CriptoDataRequest encriptDataByte = new CriptoDataRequest { Data = ByteConverter.GetBytes(encriptData) };
                            var resultEnc = _apiConnet.GetDescription(encriptDataByte, token);
                            if (resultEnc.Success)
                            {
                                Console.WriteLine(" DecriptoData:");
                                Console.WriteLine(Convert.ToBase64String(resultEnc.Data));
                                Console.WriteLine(Encoding.UTF8.GetString(resultEnc.Data));
                            }
                            else
                            {
                                Console.WriteLine(resultEnc.Message);
                            }
                            goto tekraral;
                        }
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
