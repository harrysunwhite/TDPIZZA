using System;
using System.Collections.Generic;

#nullable disable

namespace ASM_NET104_WedQuanAn.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int MaTd { get; set; }
        public int? SoLuong { get; set; }

        public virtual Cart IdNavigation { get; set; }
        public virtual ThucDon MaTdNavigation { get; set; }
    }
}
