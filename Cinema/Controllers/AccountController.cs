using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Cinema.ViewModels;
using Cinema.Models; 
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
        [AllowAnonymous]
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
                    await Authenticate(model.Phone); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
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
                    // добавляем пользователя в бд
                    db.User.Add(new User { UserPhone = model.Phone, HashPassword = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Phone); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(User model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.User.FirstOrDefaultAsync(u => User.Identity.Name == model.UserPhone);
                if (user != null)
                {
                    return RedirectToAction("Profile", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}