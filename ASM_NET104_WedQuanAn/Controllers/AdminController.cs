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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ASM_NET104_WedQuanAn.Controllers
{
    public class AdminController : Controller
    {
        private readonly ASMFINALContext _context;
        public const string sessionKey = "userLogin";
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
            if(UserLogin()!=null)
            {
                ViewData["Nhom"] = _context.Nhoms.ToList();
                ViewData["All"] = _context.ThucDons.ToList();
                ViewData["DATA"] = new SelectList(_context.Nhoms, "MaNhom", "TenNhom");
                ViewData["User"] = _context.Users.ToList();


                return View();
            } 
            else
            {
                ViewBag.Javascript = "Please login";
                return View("login");
            }    
          
            
        }
        public async Task<IActionResult> Create([Bind("MaTd,TenTd,MoTa,ImageFile,Nhom,Price")] ThucDon thucDon)
        {
            if (ModelState.IsValid)
            {
                if(thucDon.ImageFile is null)
                {
                    thucDon.Hinh = "unchose.jpg";
                }    
                else
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
        [Route("/edit/{id}")]
        [Route("/admin/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("MaTd,TenTd,MoTa,Hinh,ImageFile,Nhom,Price")] ThucDon thucDon)
        {
            if (id != thucDon.MaTd)
            {
                return NotFound();
            }

          
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
        User UserLogin()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(sessionKey);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<User>(jsoncart);
            }
            return null;
        }

        public IActionResult Sucess()
        {

            return View("Sucess");

        }
        [HttpPost]
        public IActionResult LoginWithUser(User user)
        {
            var u = _context.Users.Where(p => p.UserEmail.Equals(user.UserEmail) && p.Pass.Equals(user.Pass)).FirstOrDefault();

            if (u != null)
            {
                var session = HttpContext.Session;
                string jsoncart = JsonConvert.SerializeObject(u);
                session.SetString(sessionKey, jsoncart);
                return RedirectToAction("index", "Admin");
            }
            else
            {
                ViewBag.error = "invail account";
                return View("login");
            }
        }

        [HttpGet]
        public IActionResult LoginWithUser()
        {
            if (UserLogin() != null)
            {
                return Index();
            }
            else
            {
                ViewBag.Javascript = "request time out";
                return View("Index");
            }
        }


        public IActionResult Login()
        {
            if (UserLogin() != null)
            {
                return LoginWithUser(UserLogin());
            }
            else
            {
                return View("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userLogin");
            return RedirectToAction("login", "admin");
        }

        // GET: Users
        //public IActionResult Login()
        //{
        //    if (UserLogin() != null)
        //    {
        //        return LoginWithUser(UserLogin());
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //}

        // GET: Users/Details/5



    }




}
