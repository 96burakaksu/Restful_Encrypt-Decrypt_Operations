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
        RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(1024);
        RSAParameters _publickey;
        RSAParameters _privatekey;
        public CriptoService()
        {
            _publickey = rsaCryptoServiceProvider.ExportParameters(false);
            _privatekey = rsaCryptoServiceProvider.ExportParameters(true);
            }
        public DataResult<string> Encrypt(CriptoDataRequest request)
        {
            byte[] encryptedData;
  
            var testData = request.Data;
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    rsa.ImportParameters(_publickey);
                     encryptedData = rsa.Encrypt(Encoding.Unicode.GetBytes(testData), true);
              
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return new DataResult<string> { Data =Convert.ToBase64String(encryptedData), Success = true };

        }

        public DataResult<string> Decrypt(CriptoDataRequest request)
        {

           string decryptedData;
        
           
        
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    

                                        
                    rsa.ImportParameters(_privatekey);

                    var resultBytes = Convert.FromBase64String(request.Data);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                     decryptedData = Encoding.Unicode.GetString(decryptedBytes);
                  

                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return new DataResult<string> { Data = decryptedData, Success = true };

        }

        
    }
}
