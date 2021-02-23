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
    public class ModelController : Controller
    {
        private readonly ASMFINALContext _context;

        public ModelController(ASMFINALContext context)
        {
            _context = context;
        }

        // GET: Model
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nhoms.ToListAsync());
        }

        // GET: Model/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhom = await _context.Nhoms
                .FirstOrDefaultAsync(m => m.MaNhom == id);
            if (nhom == null)
            {
                return NotFound();
            }

            return View(nhom);
        }

        // GET: Model/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Model/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhom,TenNhom")] Nhom nhom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhom);
        }

        // GET: Model/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhom = await _context.Nhoms.FindAsync(id);
            if (nhom == null)
            {
                return NotFound();
            }
            return View(nhom);
        }

        // POST: Model/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("MaNhom,TenNhom")] Nhom nhom)
        {
            if (id != nhom.MaNhom)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhomExists(nhom.MaNhom))
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
            return View(nhom);
        }

        // GET: Model/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhom = await _context.Nhoms
                .FirstOrDefaultAsync(m => m.MaNhom == id);
            if (nhom == null)
            {
                return NotFound();
            }

            return View(nhom);
        }

        // POST: Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var nhom = await _context.Nhoms.FindAsync(id);
            _context.Nhoms.Remove(nhom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhomExists(int? id)
        {
            return _context.Nhoms.Any(e => e.MaNhom == id);
        }
    }
}
