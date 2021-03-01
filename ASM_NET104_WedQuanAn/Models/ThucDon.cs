using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ASM_NET104_WedQuanAn.Models
{
    public partial class ThucDon
    {
        public ThucDon()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int MaTd { get; set; }
        public string TenTd { get; set; }
        public string MoTa { get; set; }
        public string Hinh { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int Nhom { get; set; }
        public decimal? Price { get; set; }

        public virtual Nhom NhomNavigation { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
