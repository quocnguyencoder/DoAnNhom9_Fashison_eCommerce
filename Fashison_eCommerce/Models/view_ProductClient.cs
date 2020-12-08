using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class view_ProductClient
    {
        private string Base_URL = "https://localhost:44320/api/";
        public IEnumerable<view_Product> findAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Products").Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<view_Product>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public view_Product find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("view_Products/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<view_Product>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<view_Product> findByType(int typeID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("view_Products/Type/" + typeID).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<view_Product>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}