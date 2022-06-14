using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTicketing.Data;
using eTicketing.Data.Static;
using eTicketing.Data.ViewModels;
using eTicketing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTicketing.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: /<controller>/
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user!=null)
            {
                var passwordCheck = _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordCheck.Result!=false)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,loginVM.Password,false,false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index","Movies");
                    }
                }
                TempData["Error"] = "Wrong Credentials. Please, try again!";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong Credentials. Please, try again!";
            return View(loginVM);

        }

        public IActionResult Register() => View(new RegisterVM());


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(user!=null)
            {
                TempData["Error"] = "This Email Address is already in use";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                EmailConfirmed = true
            };
            var newUserResponse = await _userManager.CreateAsync(newUser,registerVM.Password);
            TempData["Error"] = "";
            if (!newUserResponse.Succeeded && newUserResponse.Errors.Count() > 0)
            {
                foreach (var error in newUserResponse.Errors)
                {
                    TempData["Error"] = error.Description + " " + TempData["Error"];
                }
                return View(registerVM);
            }
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Movies");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View(returnUrl);
        }
    }
}

