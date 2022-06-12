using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTicketing.Data;
using eTicketing.Data.ViewModels;
using eTicketing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}

