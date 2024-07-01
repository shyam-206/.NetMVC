using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShyamDhokiya_557.Common
{
    public class WebHelper
    {
        public static async Task<string> HttpRequestResponse(string url)
        {
            using(HttpClient client = new HttpClient())
            {
                var token = HttpContext.Current.Request.Cookies["jwt"]?.Value;
                client.BaseAddress = new Uri("http://localhost:60880/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                }

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                
                return null;
            }
        }

        public static async Task<string> HttpRequestResponsePost(string url,string content)
        {
            using (HttpClient client = new HttpClient())
            {
                var token = HttpContext.Current.Request.Cookies["jwt"]?.Value;
                client.BaseAddress = new Uri("http://localhost:60880/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                }


                HttpResponseMessage response = await client.PostAsync(url,new StringContent(content,Encoding.UTF8,"application/json"));

                response.EnsureSuccessStatusCode();

                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return null;
            }
        }
    }
}