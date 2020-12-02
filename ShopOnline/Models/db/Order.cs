using System;
using System.Collections.Generic;

#nullable disable

namespace ShopOnline.Models.db
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public bool? OrderStatus { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderPayment { get; set; }
    }
}
