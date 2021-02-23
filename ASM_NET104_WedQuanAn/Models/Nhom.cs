using System;
using System.Collections.Generic;

#nullable disable

namespace ASM_NET104_WedQuanAn.Models
{
    public partial class Nhom
    {
        public Nhom()
        {
            ThucDons = new HashSet<ThucDon>();
        }

        public int? MaNhom { get; set; }
        public string TenNhom { get; set; }

        public virtual ICollection<ThucDon> ThucDons { get; set; }
    }
}
