using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Fashison_eCommerce.Models
{
    public class ProductsClient
    {
        private string Base_URL = "https://localhost:44320/api/";

        public IEnumerable<Product> findAll(int StoreID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Seller_Product/GetProducts/" + StoreID).Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<Product> filter(Find filter)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Seller_Product/FindProducts/filter?StoreID=" + filter.StoreID + "&Name=" + filter.Name + "&TypeID=" + filter.TypeID + "&QualityMin=" + filter.QualityMin + "&QualityMax=" + filter.QualityMax).Result;
                //HttpResponseMessage respone = client.GetAsync("Products/FindProducts/filter?"+ filter).Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Product find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Seller_Product/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<Product>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public int Storeid(int userID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Seller_Product/GetStoreid/" + userID).Result;

                if (response.IsSuccessStatusCode)
                    return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        public bool Create(Product product)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Seller_Product", product).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Product product)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("Seller_Product/" + product.Product_ID, product).Result;
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
                HttpResponseMessage response = client.DeleteAsync("Seller_Product/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}