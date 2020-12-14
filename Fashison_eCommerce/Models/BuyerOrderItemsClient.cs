using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace Fashison_eCommerce.Models
{
    public class BuyerOrderItemsClient
    {
        private string Base_URL = "https://localhost:44320/api/";
        public IEnumerable<BuyerOrderItems> findAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Order_Items").Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<BuyerOrderItems>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<BuyerOrderDetail> find(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Order_Items/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<BuyerOrderDetail>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(BuyerOrderItems orderItems)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Order_Items", orderItems).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
       
    }
}