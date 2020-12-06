using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashison_eCommerce.Models
{
    public class UserCart
    {
        public string Pictures { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int ItemID { get; set; }
        public int Store_ID { get; set; }
    }
}