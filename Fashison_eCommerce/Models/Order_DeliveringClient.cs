using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class Order_DeliveringClient
    {

        private string Base_URL = "https://localhost:44320/api/";
        public IEnumerable<Order_Delivering> findAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Order_Delivering").Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<Order_Delivering>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Order_Delivering find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Order_Delivering/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<Order_Delivering>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(Order_Delivering Order_Delivering)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Order_Delivering", Order_Delivering).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Order_Delivering Order_Delivering)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("Order_Delivering/" + Order_Delivering.Order_ID, Order_Delivering).Result;
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
                HttpResponseMessage response = client.DeleteAsync("Order_Delivering/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}