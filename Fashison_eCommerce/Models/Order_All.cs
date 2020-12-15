using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class Order_All
    {
        public string Name { get; set; }
        public string Order_ID { get; set; }
        public double delivery { get; set; }
        public System.DateTime created_date { get; set; }
        public int status { get; set; }
        public string decription { get; set; }
        public string address { get; set; }
        public double Total_Order { get; set; }
    }
}