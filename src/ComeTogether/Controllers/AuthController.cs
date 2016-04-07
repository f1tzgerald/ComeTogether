using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using ComeTogether.Models;
using ComeTogether.ViewModels;
using ComeTogether.DAL.Entities;

namespace ComeTogether.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<Person> _signInManager;

        public AuthController(SignInManager<Person> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Main");
            }                

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password, true, false);

                if (signInResult.Succeeded)
                {
                    // If redirect to login page from other
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "Main");
                    }                        
                    else
                    {
                        return RedirectToAction(returnUrl);
                    }                                            
                }
            }
            else
            {
                #warning Не отображается ошибка Add more extend error
                ModelState.AddModelError("", "Username or password incorrect");
            }

            return View();
         }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Main");
        }
    }
}
