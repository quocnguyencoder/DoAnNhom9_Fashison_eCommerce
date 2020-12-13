using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace Fashison_eCommerce.Models
{
    public class BuyerOrdersClient
    {
        private string Base_URL = "https://localhost:44320/api/";
        public IEnumerable<BuyerOrders> findAll(int user_id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Buyer_Orders/"+ user_id).Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<BuyerOrders>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public User find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Buyer_Orders/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<User>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(BuyerOrders buyerOrders)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Orders", buyerOrders).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
       
    }
}