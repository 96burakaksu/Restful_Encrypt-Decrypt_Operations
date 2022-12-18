
using Core;
using Core.GeneralResult;
using CryptoService.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoService.Classes
{
    public class CriptoService : ICryptoService
    {
        byte[] encrypetData;
        public DataResult<byte[]> Encrypt(CriptoDataRequest request)
        {
            //byte[] encryptedData;

            //var publicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            //var testData = request.Data;
            //using (var rsa = new RSACryptoServiceProvider(1024))
            //{
            //    try
            //    {
            //        rsa.FromXmlString(publicKey);
            //         encryptedData = rsa.Encrypt(testData, true);

            //    }
            //    finally
            //    {
            //        rsa.PersistKeyInCsp = false;
            //    }
            //}
            //return new DataResult<byte[]> { Data = encryptedData, Success = true };
            encrypetData = Encryption(request.Data, RSA.ExportParameters(false), false);
            var a = ByteConverter.GetString(encrypetData);
            return new DataResult<byte[]> { Data = encrypetData, Success = true };
        }

        public DataResult<byte[]> Decrypt(CriptoDataRequest request)
        {

            //byte[] decryptedData;
            //var privateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";
            //// buraya gizli key'inizi yazmalısınız.
            //var testData = request.Data;
            //using (var rsa = new RSACryptoServiceProvider(1024))
            //{
            //    try
            //    {
            //        var base64Encrypted = Convert.ToBase64String(request.Data);
            //        rsa.FromXmlString(privateKey);
            //        var resultBytes = Convert.FromBase64String(base64Encrypted);
            //        var decryptedBytes = rsa.Decrypt(resultBytes, true);
            //         decryptedData = decryptedBytes;

            //    }
            //    finally
            //    {
            //        rsa.PersistKeyInCsp = false;
            //    }
            //}
            var decrypetData = Decryption(encrypetData, RSA.ExportParameters(true), false);
            var a = ByteConverter.GetString(decrypetData);
            return new DataResult<byte[]> { Data = decrypetData, Success = true };

        }

        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;

    
        
        //Method for Encryption
        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //Method for Decryption
        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
