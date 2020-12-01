using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace Fashison_eCommerce.Models
{
    public class UserCartClient
    {
        private string Base_URL = "https://localhost:44320/api/";
        public IEnumerable<UserCart> LoadCart(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respone = client.GetAsync("Cart/" + id ).Result;
                if (respone.IsSuccessStatusCode)
                    return respone.Content.ReadAsAsync<IEnumerable<UserCart>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}