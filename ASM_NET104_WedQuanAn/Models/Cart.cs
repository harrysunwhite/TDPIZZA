using System;
using System.Collections.Generic;

#nullable disable

namespace ASM_NET104_WedQuanAn.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public string Sdtkh { get; set; }
        public string DiaChiKh { get; set; }
        public string Tenkh { get; set; }
        public string EmailKh { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
