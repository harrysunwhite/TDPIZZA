using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_NET104_WedQuanAn.Models;

namespace ASM_NET104_WedQuanAn.Controllers
{
    public class ThucDonsController : Controller
    {
        private readonly ASMFINALContext _context;

        public ThucDonsController(ASMFINALContext context)
        {
            _context = context;
        }

        // GET: ThucDons
        public async Task<IActionResult> Index()
        {
            var aSMFINALContext = _context.ThucDons.Include(t => t.NhomNavigation);
            return View(await aSMFINALContext.ToListAsync());
        }

        public IActionResult Category()
        {
            ViewData["Nhom"] = _context.Nhoms;
            ViewData["All"] = _context.ThucDons.ToList();

        
               

            return View();
              
        }
      


        public ActionResult MenucateGory()
        {
            var navbar = _context.Nhoms;
        return View(navbar.ToListAsync());
        }
       

        // GET: ThucDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDons
                .Include(t => t.NhomNavigation)
                .FirstOrDefaultAsync(m => m.MaTd == id);
            if (thucDon == null)
            {
                return NotFound();
            }

            return View(thucDon);
        }

        // GET: ThucDons/Create
        public IActionResult Create()
        {
            ViewData["Nhom"] = new SelectList(_context.Nhoms, "MaNhom", "TenNhom");
            return View();
        }

        // POST: ThucDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTd,TenTd,MoTa,Hinh,Nhom,Price")] ThucDon thucDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thucDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Nhom"] = new SelectList(_context.Nhoms, "MaNhom", "TenNhom", thucDon.Nhom);
            return View(thucDon);
        }

        // GET: ThucDons/Edit/5
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

        // POST: ThucDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTd,TenTd,MoTa,Hinh,Nhom,Price")] ThucDon thucDon)
        {
            if (id != thucDon.MaTd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: ThucDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDons
                .Include(t => t.NhomNavigation)
                .FirstOrDefaultAsync(m => m.MaTd == id);
            if (thucDon == null)
            {
                return NotFound();
            }

            return View(thucDon);
        }

        // POST: ThucDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thucDon = await _context.ThucDons.FindAsync(id);
            _context.ThucDons.Remove(thucDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThucDonExists(int id)
        {
            return _context.ThucDons.Any(e => e.MaTd == id);
        }
    }
}
