using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class BuyerAddress
    {
        public int Address_ID { get; set; }
        public Nullable<int> User_ID { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public Nullable<int> default_address { get; set; }
    }
}