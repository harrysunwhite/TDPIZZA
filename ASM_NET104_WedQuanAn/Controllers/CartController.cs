using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASM_NET104_WedQuanAn.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ASM_NET104_WedQuanAn.Controllers
{
    public class CartController : Controller
    {
        public const string CARTKEY = "cart";
        private ILogger<CartController> _logger;
        private readonly ASMFINALContext _context;

        public CartController(ILogger<CartController> logger, ASMFINALContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [HttpGet]
        [Route("/Listcart")]
        public List<CartIteamModel> ListCart()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartIteamModel>>(jsoncart);
            }
            return new List<CartIteamModel>();
        }

        [HttpGet]
        [Route("/countcart")]
        public int countQuantiy()
        {
            var cart = ListCart();
            int count = 0;
            foreach(var item in cart)
            {
                count += item.Quantity;
            }
            return count;
        }

        void SaveCartSession(List<CartIteamModel> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        [HttpGet]
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var thucDon = _context.ThucDons
                .Where(p => p.MaTd == productid)
                .FirstOrDefault();


            // Xử lý đưa vào Cart ...
            var cart = ListCart();
            var cartitem = cart.Find(p => p.thucDon.MaTd == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartIteamModel() { Quantity = 1, thucDon = thucDon });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart

            return PartialView("_CartPartial");
        }

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = ListCart();
            var cartitem = cart.Find(p => p.thucDon.MaTd == productid);
            if (cartitem != null)
            {

                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);

            return Ok();
        }

        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = ListCart();
            var cartitem = cart.Find(p => p.thucDon.MaTd == productid);
            if (cartitem != null)
            {

                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return PartialView("_CartPartial");
        }


        
        public IActionResult Cart()
        {
            return PartialView("_CartPartial", ListCart());
        }

        public IActionResult CartFinal()
        {
            return View("Cart", ListCart());
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.ThucDons.ToListAsync());
        }



    }
}

