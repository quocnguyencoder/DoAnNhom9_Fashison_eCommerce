using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class view_Product
    {
        public int Product_ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public int TypeID { get; set; }
        public int Store_ID { get; set; }
        public string Pictures { get; set; }
        public string Decription { get; set; }
        public Nullable<int> BrandID { get; set; }

    }
}