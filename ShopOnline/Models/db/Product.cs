using System;
using System.Collections.Generic;

#nullable disable

namespace ShopOnline.Models.db
{
    public partial class Product
    {
        public int PdId { get; set; }
        public string PdType { get; set; }
        public string PdName { get; set; }
        public string PdImage { get; set; }
        public decimal? PdPrice { get; set; }
        public int? PdItem { get; set; }
        public string PdDes { get; set; }
        public DateTime? PdUpdate { get; set; }
        public bool? PdStatus { get; set; }
    }
}
