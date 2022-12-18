using Core;
using Core.GeneralResult;
using CryptoService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoService.Classes
{
    public class CriptoService : ICryptoService
    {
        public DataResult<string> Encrypt(CriptoDataRequest request)
        {
            byte[] encryptedData;
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            //Import the RSA Key information. This only needs
            //toinclude the public key information.
            RSA.ImportParameters(RSA.ExportParameters(false));


            //Encrypt the passed byte array and specify OAEP padding.  
            //OAEP padding is only available on Microsoft Windows XP or
            //later.  
            encryptedData = RSA.Encrypt(Encoding.UTF8.GetBytes(request.Data), false);
            return new DataResult<string> { Data = Convert.ToBase64String(encryptedData), Success = true };

        }

        public DataResult<string> Decrypt(CriptoDataRequest request)
        {

            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSA.ExportParameters(false));


                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                decryptedData = RSA.Decrypt(Encoding.UTF8.GetBytes(request.Data), false);
            }
            return new DataResult<string> { Data = Convert.ToBase64String(decryptedData), Success = true };

        }

        
    }
}
