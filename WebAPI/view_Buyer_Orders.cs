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
    
    public partial class view_Buyer_Orders
    {
        public string Order_ID { get; set; }
        public Nullable<int> User_ID { get; set; }
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