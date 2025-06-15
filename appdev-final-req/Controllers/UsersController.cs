using System.Security.Principal;
using appdev_final_req.Models;
using appdev_final_req.Models.Entitiess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace appdev_final_req.Controllers
{
    public class UsersController : Controller
    {
        // dependency injections that give your controller access to ASP.NET Core Identity services — specifically for managing users and handling login.
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // constructor
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost] // handles register submission
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // create new User
            var user = new User { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);

            // If registration was successful, the user is automatically logged in then redirected to Home/ Index.
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // Error: show modal
            ViewData["RegisterError"] = string.Join("<br/>", result.Errors.Select(e => e.Description));
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // If the fields are invalid, it re-shows the form.
            if (!ModelState.IsValid) return View(model);

            // This checks the username and password against the database.
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            // Pass an error message using ViewData or TempData
            ViewData["LoginError"] = "Invalid username or password.";
            return View(model);
        }

        // Logs the user out and takes them back to the login screen.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }

}
