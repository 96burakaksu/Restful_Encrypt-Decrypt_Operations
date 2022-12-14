using Client.DTOs;
using Core;
using Core.GeneralResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
                var endpoint = new Uri("https://localhost:44380/Auth/authenticate");
                var loginPostJson = JsonSerializer.Serialize(_user);
                HttpContent httpContent = new StringContent(loginPostJson, Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.PostAsync(endpoint, httpContent).GetAwaiter().GetResult();
                var webResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
              return JsonSerializer.Deserialize<DataResult<string>>(webResult);
            }
           
        }

    }
}
