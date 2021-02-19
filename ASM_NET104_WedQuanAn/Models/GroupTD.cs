using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_NET104_WedQuanAn.Models
{
    public class GroupTD
    {
       public string MaNhom { get; set; }
      
       public IEnumerable<ThucDon> ThucDons { get; set; }
    }
}
