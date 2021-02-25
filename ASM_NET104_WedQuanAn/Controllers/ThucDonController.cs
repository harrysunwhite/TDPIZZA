using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASM_NET104_WedQuanAn.Models;

namespace ASM_NET104_WedQuanAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucDonController : ControllerBase
    {
        private readonly ASMFINALContext _context;

        public ThucDonController(ASMFINALContext context)
        {
            _context = context;
        }

        // GET: api/ThucDon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThucDon>>> GetThucDons()
        {
            return await _context.ThucDons.ToListAsync();
        }

        // GET: api/ThucDon/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var thucdon = await _context.Set<ThucDon>().FirstOrDefaultAsync(x => x.MaTd == id);
            //var Name = thucdon.Nhom;
            //var n = await _context.Set<Nhom>().FirstOrDefaultAsync(x => x.MaNhom == Name);
            //thucdon.TenNhom = n.TenNhom;
            
          
     
            if (thucdon == null)
            {
                return NotFound();
            }
            return Ok(thucdon);
        }

        // PUT: api/ThucDon/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThucDon(int id, ThucDon thucDon)
        {
            if (id != thucDon.MaTd)
            {
                return BadRequest();
            }

            _context.Entry(thucDon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThucDonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ThucDon
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ThucDon>> PostThucDon(ThucDon thucDon)
        {
            _context.ThucDons.Add(thucDon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThucDon", new { id = thucDon.MaTd }, thucDon);
        }

        // DELETE: api/ThucDon/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ThucDon>> DeleteThucDon(int id)
        {
            var thucDon = await _context.ThucDons.FindAsync(id);
            if (thucDon == null)
            {
                return NotFound();
            }

            _context.ThucDons.Remove(thucDon);
            await _context.SaveChangesAsync();

            return thucDon;
        }

        private bool ThucDonExists(int id)
        {
            return _context.ThucDons.Any(e => e.MaTd == id);
        }
    }
}
