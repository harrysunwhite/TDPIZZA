using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASM_NET104_WedQuanAn.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM_NET104_WedQuanAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomController : ControllerBase
    {
        private readonly ASMFINALContext _context;

        public NhomController(ASMFINALContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var nhoms = await _context.Set<Nhom>().AsQueryable().ToListAsync();

            return Ok(nhoms);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NhomModel model)
        {
            model.MaNhom = _context.Nhoms.Count() + 1;
            
            if (!string.IsNullOrEmpty(model.MaNhom.ToString()))
            {
                _context.Set<Nhom>().Add(new Nhom { MaNhom = model.MaNhom, TenNhom = model.TenNhom });
                await _context.SaveChangesAsync();
                return Ok("Added");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _context.Set<Nhom>().FirstOrDefaultAsync(x => x.MaNhom == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }



        [HttpPut]
        public async Task<IActionResult> Put(NhomModel model)
        {
            if (!string.IsNullOrEmpty(model.TenNhom))
            {
                var find = await _context.Set<Nhom>().FirstOrDefaultAsync(x => x.MaNhom == model.MaNhom);
                if (find != null)
                {
                    find.MaNhom = model.MaNhom;
                    find.TenNhom = model.TenNhom;

                    _context.Set<Nhom>().Attach(find);
                    await _context.SaveChangesAsync();
                    return Ok("Saved");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Set<Nhom>().FirstOrDefaultAsync(x => x.MaNhom == id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Set<Nhom>().Remove(student);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
