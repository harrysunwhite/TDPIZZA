using System;
using System.Collections.Generic;

#nullable disable

namespace ASM_NET104_WedQuanAn.Models
{
    public partial class ThucDon
    {
        public int MaTd { get; set; }
        public string TenTd { get; set; }
        public string MoTa { get; set; }
        public string Hinh { get; set; }
        public int Nhom { get; set; }
        public decimal? Price { get; set; }

        public virtual Nhom NhomNavigation { get; set; }
    }
}
