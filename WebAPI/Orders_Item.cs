namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders_Item
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Recipt_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_ID { get; set; }

        public int? Quantity { get; set; }

        public float? Cost { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
