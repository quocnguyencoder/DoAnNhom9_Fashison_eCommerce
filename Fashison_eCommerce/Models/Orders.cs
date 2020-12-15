using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class Orders
    {
        public string Order_ID { get; set; }
        public int User_ID { get; set; }
        public int Store_ID { get; set; }
        public double delivery { get; set; }
        public string payment { get; set; }
        public System.DateTime created_date { get; set; }
        public int status { get; set; }
        public string decription { get; set; }
        public int Address_ID { get; set; }
        public double Total_Order { get; set; }
    }
}