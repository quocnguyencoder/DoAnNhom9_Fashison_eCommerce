using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class BuyerOrderDetail
    {
        public string Order_ID { get; set; }
        public Nullable<int> Store_ID { get; set; }
        public string ShopName { get; set; }
        public Nullable<double> delivery { get; set; }
        public string payment { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> Address_ID { get; set; }
        public Nullable<double> Total_Order { get; set; }
        public int Item_ID { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<double> discount { get; set; }
        public Nullable<double> total { get; set; }
        public string Pictures { get; set; }
        public string Name { get; set; }
    }
}