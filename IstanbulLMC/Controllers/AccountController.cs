using IstanbulLMC.Models;
using IstanbulLMC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IstanbulLMC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userM;
        private readonly SignInManager<AppUser> _sinInM;
        private readonly lmcTourismContext _context;


        public AccountController(UserManager<AppUser> userM, SignInManager<AppUser> sinInM, lmcTourismContext context)
        {
            _userM = userM;
            _sinInM = sinInM;
            _context = context;
        }

        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)

        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userM.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var passwordCheck = await _userM.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var userRole = await _userM.IsInRoleAsync(user, "user");
                    if (userRole)
                    {
                        var result = await _sinInM.PasswordSignInAsync(user, loginVM.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                TempData["Error"] = "Email or Password is incorrect!";

                return View(loginVM);
            }
            TempData["Error"] = "Email or Password is incorrect!";

            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userM.FindByEmailAsync(registerVM.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new AppUser
            {
                firstName = registerVM.FirstName,
                lastName = registerVM.LastName,

                Email = registerVM.Email,
                UserName = registerVM.Email
            };

            var newUserResponse = await _userM.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                TempData["success"] = "Registration is Successful";
                await _userM.AddToRoleAsync(newUser, userRoles.user);

                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _sinInM.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
        public async Task<IActionResult> Profile()
        {
            return View("ProfileChangePassword");
        }



        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userM.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                var changePasswordResult = await _userM.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    await _sinInM.RefreshSignInAsync(user);
                    return RedirectToAction("ChangePasswordConfirmation");
                }

                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("ProfileChangePassword", model);
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View("AccessDenied");
        }
    }
}
