
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
            bool inputLoop = true;
            bool transactionLoop = true;
            

            while (inputLoop)
            {
                Users loginPost = new Users();
                Console.Write("Key: ");
                loginPost.Key = Console.ReadLine();
                Console.Write("Pass: ");
                loginPost.Pass = Console.ReadLine();
                Console.WriteLine();

                ApiConnect _apiConnet = new ApiConnect(loginPost);
                var result = _apiConnet.GetApiToken();
                if (result.Success)
                {
                    Console.WriteLine("Login successful");
                    Console.WriteLine();
                    token = result.Data;

                    
                    while (transactionLoop)
                    {
                        Console.WriteLine("Press '1' for Encrypt and '2' for Decrypt ");

                        Console.Write("Process: ");
                        var process = Console.ReadLine();
                        switch (process)
                        {
                            case "1":
                                {
                                    Console.WriteLine("Enter the text to be encrypted:");
                                    var encryptData = Console.ReadLine();
                                    CriptoDataRequest encryptDataByte = new CriptoDataRequest { Data = encryptData };
                                    var resultEnc = _apiConnet.GetEncription(encryptDataByte, token);
                                    if (resultEnc.Success)
                                    {
                                        Console.WriteLine("Encrypted data:");
                                        Console.WriteLine(resultEnc.Data);
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        if (resultEnc.StatusCode == 401)
                                        {
                                            Console.WriteLine("Your Session Period has expired");
                                            Console.WriteLine();
                                            transactionLoop = false;
                                            break;
                                        }
                                        else if (resultEnc.StatusCode == 400)
                                        {
                                            Console.WriteLine("Token Not Sent");
                                            Console.WriteLine();
                                            transactionLoop = false;
                                            break;
                                        }

                                    }
                                    break;

                                }
                            case "2":
                                {
                                    Console.WriteLine("Enter the text to be decrypted:");
                                    var encryptData = Console.ReadLine();
                                    CriptoDataRequest encryptDataByte = new CriptoDataRequest { Data = encryptData };
                                    var resultEnc = _apiConnet.GetDescription(encryptDataByte, token);
                                    if (resultEnc.Success)
                                    {
                                        Console.WriteLine("Decrypted Data:");
                                        Console.WriteLine(resultEnc.Data);
                                        Console.WriteLine();

                                    }
                                    else
                                    {
                                        if (resultEnc.StatusCode == 401)
                                        {
                                            Console.WriteLine("Your Session Period has expired");
                                            Console.WriteLine();
                                            transactionLoop = false;
                                            break;

                                        }
                                        else if (resultEnc.StatusCode == 400)
                                        {
                                            Console.WriteLine("Token Not Sent");
                                            Console.WriteLine();
                                            transactionLoop = false;
                                            break;

                                        }
                                    }
                                    break;
                                }
                            default:
                                Console.WriteLine("You entered an incorrect operator. Try again.");
                                Console.WriteLine();
                                break;
                        }
                    }

                    transactionLoop = true;

                }
                else
                {
                    Console.WriteLine(result.Message);
                    Console.WriteLine("Re-enter");
                    Console.WriteLine();
                }
            }
            


        }
    }


}
