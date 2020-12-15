using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Find
    {
        public int StoreID { get; set; }
        public string Name { get; set; }
        public int QualityMin { get; set; }
        public int QualityMax { get; set; }
        public int TypeID { get; set; }

    }
}