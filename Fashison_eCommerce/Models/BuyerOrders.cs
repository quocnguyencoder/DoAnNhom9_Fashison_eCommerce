using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class BuyerOrders
    {
        public string Order_ID { get; set; }
        public int User_ID { get; set; }
        public Nullable<int> Store_ID { get; set; }
        public Nullable<double> delivery { get; set; }
        public string payment { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> status { get; set; }
        public string decription { get; set; }
        public Nullable<int> Address_ID { get; set; }
        public Nullable<double> Total_Order { get; set; }
    }
}