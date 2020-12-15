using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class Order_AllClient
    {
        private string Base_URL = "https://localhost:44320/api/";
        public IEnumerable<Order_All> findAll(int userID, int  status)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Order_All?userID="+ userID + "&status="+status).Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<Order_All>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Order_All find(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Order_All/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<Order_All>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(Order_All Order_All)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Order_All", Order_All).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Order_All Order_All)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("Order_All/" + Order_All.Order_ID, Order_All).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("Order_All/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}