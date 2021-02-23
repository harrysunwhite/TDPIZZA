using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASM_NET104_WedQuanAn.Models;

namespace ASM_NET104_WedQuanAn.Controllers
{
    public class AdminController : Controller
    {
        private readonly ASMFINALContext _context;
        public AdminController(ASMFINALContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Nhom"] = _context.Nhoms.ToList();
            ViewData["All"] = _context.ThucDons.ToList();

            ViewData["User"] = _context.Users.ToList();


            return View();
            
        }
      
    }
}
