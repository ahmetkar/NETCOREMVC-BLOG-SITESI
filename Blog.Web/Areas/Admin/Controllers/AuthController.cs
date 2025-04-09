using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<TUser> userManager;
        private readonly SignInManager<TUser> signInManager;

        public AuthController(UserManager<TUser> userManager,SignInManager<TUser> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginData)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(userLoginData.Email);
                if (user != null) {
                    var result = await signInManager.PasswordSignInAsync(user,userLoginData.Password,userLoginData.RememberMe,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home",new {Area = "Admin"});
                    }else
                    {
                        ModelState.AddModelError("","Giriş başarısız.Eposta veya şifreyi kontrol edin.");
                        return View();
                    }
                }else
                {
                    ModelState.AddModelError("", "Giriş başarısız.Eposta veya şifreyi kontrol edin.");
                    return View();
                }
            }
                
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home",new {Area = ""});
           
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
