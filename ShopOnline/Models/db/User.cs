using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

#nullable disable

namespace ShopOnline.Models.db
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserFname { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public IFormFile UserImage { get; set; }
        public string UserPathimg { get; set; }
 
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }
        public DateTime? UserBirthday { get; set; }
        public DateTime? UserUpdate { get; set; }
    }
}
