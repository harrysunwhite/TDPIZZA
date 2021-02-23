using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_NET104_WedQuanAn.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;



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

        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
        }

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.ThucDons
                .Where(p => p.MaTd == productid)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.thucdon.MaTd == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, thucdon = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }

        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.thucdon.MaTd == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.thucdon.MaTd == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

    }
}
