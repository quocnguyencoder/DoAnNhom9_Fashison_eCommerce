using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Fashison_eCommerce.Models
{
    public class BuyerAddressClient
    {
        private string Base_URL = "https://localhost:44320/api/";
        public IEnumerable<BuyerAddress> find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Addresses/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<BuyerAddress>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
  
        public IEnumerable<BuyerAddress> findByAddressID(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("getAddressByID?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<BuyerAddress>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(Address address)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Addresses", address).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Address address)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("Addresses/" + address.Address_ID, address).Result;
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
                HttpResponseMessage response = client.DeleteAsync("Addresses/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}