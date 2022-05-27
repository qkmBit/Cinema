using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Cinema.ViewModels;
using Cinema.Models; 
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private CinemaContext db;
        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, CinemaContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl=null)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string phone = model.Phone.Replace(" ", "");
                var result = await _signInManager.PasswordSignInAsync(phone, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    Users user = await db.AspNetUsers.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
                    if(user.Email!=null)
                        await _userManager.AddClaimAsync(user, new Claim("Email", user.Email ));
                    if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль.");
                }
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
                string phone = model.Phone.Replace(" ", "");
                Users user = new Users { PhoneNumber = phone, UserName = phone };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}