
using Core;
using Core.GeneralResult;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using System.Threading.Tasks;

namespace Client
{
    public class ApiConnect
    {
       
        Users _user;
     public   ApiConnect(Users user)
        {
            _user = user;
        }

       public DataResult<string> GetApiToken()
        {
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri("https://localhost:44380/Auth/Authenticate");
                var loginPostJson = JsonConvert.SerializeObject(_user);
                HttpContent httpContent = new StringContent(loginPostJson, Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.PostAsync(endpoint, httpContent).GetAwaiter().GetResult();
                string webResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //return new DataResult<byte[]>();
              return JsonConvert.DeserializeObject<DataResult<string>>(webResult);
            }
           
        }
        public  DataResult<byte[]> GetDescription(CriptoDataRequest request,string token)
        {
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri("https://localhost:44370/Cryption/Decripto");
                var paramater = JsonConvert.SerializeObject(request);
                HttpContent httpContent = new StringContent(paramater, Encoding.UTF8, "application/json");
                httpContent.Headers.Add("token", token);
                //httpContent.Headers.Add("request", paramater);
                
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
                var response = httpClient.PostAsync(endpoint, httpContent).GetAwaiter().GetResult();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var webResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    return JsonConvert.DeserializeObject<DataResult<byte[]>>(webResult);
                  
                }
                else
                {
                    return new DataResult<byte[]> { Success = false, Message = response.RequestMessage.ToString(), Data = null };
                }
            }

        }
        public DataResult<byte[]> GetEncription(CriptoDataRequest request,string token)
        {
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri("https://localhost:44370/Cryption/Encripto");
                var paramater = JsonConvert.SerializeObject(request);
                HttpContent httpContent = new StringContent(paramater, Encoding.UTF8, "application/json");
                httpContent.Headers.Add("token", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response =   httpClient.PostAsync(endpoint, httpContent).GetAwaiter().GetResult();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var webResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    return JsonConvert.DeserializeObject<DataResult<byte[]>>(webResult);
                    //var a = JsonSerializer.Deserialize<DataResult<byte[]>>(webResult);
            
                    
                }
                else
                {
                    return new DataResult<byte[]> { Success = false, Message = response.RequestMessage.ToString(), Data = null };
                }
            }

        }
    }
}
