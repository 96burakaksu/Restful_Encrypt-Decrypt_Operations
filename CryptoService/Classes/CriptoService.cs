using Core;
using Core.GeneralResult;
using CryptoService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CryptoService.Classes
{
    public class CriptoService : ICryptoService
    {
        public DataResult<byte[]> Encrypt(CriptoData request)
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSA.ExportParameters(false));


                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                encryptedData = RSA.Encrypt(request.Data, false);
            }
            return new DataResult<byte[]> { Data = encryptedData };

        }

        public DataResult<byte[]> Decrypt(CriptoData request)
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
                decryptedData = RSA.Decrypt(request.Data, false);
            }
            return new DataResult<byte[]> { Data = decryptedData };

        }

        
    }
}
