using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASM_NET104_WedQuanAn.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace ASM_NET104_WedQuanAn.Controllers
{
    public class AdminController : Controller
    {
        private readonly ASMFINALContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private bool ThucDonExists(int id)
        {
            return _context.ThucDons.Any(e => e.MaTd == id);
        }

        public AdminController(ASMFINALContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            ViewData["Nhom"] = _context.Nhoms.ToList();
            ViewData["All"] = _context.ThucDons.ToList();
            ViewData["DATA"] = new SelectList(_context.Nhoms, "MaNhom", "TenNhom");
            ViewData["User"] = _context.Users.ToList();


            return View();
            
        }
        public async Task<IActionResult> Create([Bind("MaTd,TenTd,MoTa,ImageFile,Nhom,Price")] ThucDon thucDon)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(thucDon.ImageFile.FileName);
                string ext = Path.GetExtension(thucDon.ImageFile.FileName);
                thucDon.Hinh = fileName = thucDon.MaTd + DateTime.Now.ToString("ddmmyyyy") + ext;
                string path = Path.Combine(rootpath + "/img/", fileName);
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await thucDon.ImageFile.CopyToAsync(fs);
                }
                _context.Add(thucDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Nhom"] = new SelectList(_context.Nhoms, "MaNhom", "TenNhom", thucDon.Nhom);
            return View(thucDon);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDons.FindAsync(id);
            if (thucDon == null)
            {
                return NotFound();
            }
            ViewData["Nhom"] = new SelectList(_context.Nhoms, "MaNhom", "TenNhom", thucDon.Nhom);
            return View(thucDon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTd,TenTd,MoTa,Hinh,ImageFile,Nhom,Price")] ThucDon thucDon)
        {
            if (id != thucDon.MaTd)
            {
                return NotFound();
            }

            Console.WriteLine(thucDon.Hinh);
            if (ModelState.IsValid)
            {
                try
                {
                    string rootpath = _hostEnvironment.WebRootPath;
                    if (thucDon.ImageFile is null)
                    {
                        
                    }
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(thucDon.ImageFile.FileName);
                        string ext = Path.GetExtension(thucDon.ImageFile.FileName);
                        thucDon.Hinh = fileName = thucDon.MaTd + DateTime.Now.ToString("ddmmyyyy") + ext;
                        string path = Path.Combine(rootpath + "/img/", fileName);
                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            await thucDon.ImageFile.CopyToAsync(fs);
                        }
                    }    
                   
                    _context.Update(thucDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThucDonExists(thucDon.MaTd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Nhom"] = new SelectList(_context.Nhoms, "MaNhom", "TenNhom", thucDon.Nhom);
            return View(thucDon);
        }


    }
}
