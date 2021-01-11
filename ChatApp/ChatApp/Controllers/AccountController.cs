using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Login", "Account");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new User
            {
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Register));
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

    }
}