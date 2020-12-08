using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class BuyerOrderItems
    {
        public string Order_ID { get; set; }
        public int Item_ID { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<double> discount { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<int> status { get; set; }
    }
}