//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
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
    
        public virtual Brand Brand { get; set; }
        public virtual Store Store { get; set; }
        public virtual Product_Type Product_Type { get; set; }
        public virtual Cart_Item Cart_Item { get; set; }
    }
}