using System;
using System.Collections.Generic;

#nullable disable

namespace ShopOnline.Models.db
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? CartQty { get; set; }
        public decimal? CartTotal { get; set; }
    }
}
