﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TaskManagement.Session;
using TaskManagement_Model.ViewModel;

namespace TaskManagement.Common
{
    public class WebHelper
    {
        
        /*public static async Task<List<AssignmentModelList>> CompleteTaskList()
        {
            List<AssignmentModelList> _list = new List<AssignmentModelList>();
            int teacherId = SessionHelper.UserId;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync($"api/TeacherAPI/CompleteTaskList?teacherId={teacherId}");

                if (response.IsSuccessStatusCode)
                {
                    var resData = await response.Content.ReadAsStringAsync();
                    _list = JsonConvert.DeserializeObject<List<AssignmentModelList>>(resData);
                }
            }

            return _list;
        }*/

        public static async Task<string> HttpRequestResponce(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57800/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static async Task<string> HttpClientPostRequest(string url, string jsonContent)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57800/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return null;
            }
        }
    }
}