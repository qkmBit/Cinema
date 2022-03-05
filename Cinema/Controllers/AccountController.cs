using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Cinema.ViewModels; // ������������ ���� ������� RegisterModel � LoginModel
using Cinema.Models; // ������������ ���� UserContext � ������ User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Cinema.Controllers
{
    public class AccountController : Controller
    {
        private CinemaContext db;
        public AccountController(CinemaContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.User.FirstOrDefaultAsync(u => u.UserPhone == model.Phone && u.HashPassword == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Phone); // ��������������

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "������������ ����� �(���) ������");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.User.FirstOrDefaultAsync(u => u.UserPhone == model.Phone);
                if (user == null)
                {
                    // ��������� ������������ � ��
                    db.User.Add(new User { UserPhone = model.Phone, HashPassword = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Phone); // ��������������

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "������������ ����� �(���) ������");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // ������� ���� claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // ������� ������ ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // ��������� ������������������ ����
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}