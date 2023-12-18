using IstanbulLMC.Models;
using IstanbulLMC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    //[Area("Admin")]

    public class AdminController : Controller
    {

        private readonly UserManager<AppUser> _userM;
        private readonly SignInManager<AppUser> _sinInM;
        private readonly lmcTourismContext _context;


        public AdminController(UserManager<AppUser> userM, SignInManager<AppUser> sinInM, lmcTourismContext context)
        {
            _userM = userM;
            _sinInM = sinInM;
            _context = context;
        }
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [AllowAnonymous]
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userM.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var userRole = await _userM.IsInRoleAsync(user, "admin");
                if (userRole)
                {
                    var passwordCheck = await _userM.CheckPasswordAsync(user, loginVM.Password);
                    if (passwordCheck)
                    {
                        var result = await _sinInM.PasswordSignInAsync(user, loginVM.Password, false, false);
                        if (result.Succeeded)
                        {
                            return View("Index");
                        }
                    }
                }


                TempData["Error"] = "Email or Password is incorrect!";

                return View(loginVM);
            }
            TempData["Error"] = "Email or Password is incorrect!";

            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _sinInM.SignOutAsync();
            return RedirectToAction("Login");
        }

        [CustomAuthorize]
        public IActionResult Index()
        {
            if (_sinInM.IsSignedIn(User) && User.IsInRole("user"))
            {
                _sinInM.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
