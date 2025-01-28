using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarhammerPaintCenter.Data;
using WarhammerPaintCenter.Models;
using WarhammerPaintCenter.Models.Entities;

namespace WarhammerPaintCenter.Controllers
{
    public class AccountConttroller : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public AccountConttroller(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View(dbContext.UserAccounts.ToList());
        }


        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.Email = model.Email;    
                account.Password = model.Password;
                account.Nick = model.Nick;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;


            try { 
                dbContext.UserAccounts.Add(account);
                dbContext.SaveChanges();


                ModelState.Clear();
                ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully";

            }
            catch(DbUpdateException ex){
                    ModelState.AddModelError("", "Please enter unique Email or Password");
                    return View(model);
                }

                return RedirectToAction("Login", "AccountConttroller");

                
            }
            return View(model);
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await dbContext.UserAccounts
                    .FirstOrDefaultAsync(x => x.Nick == model.Nick && x.Password == model.Password);
                if (user != null)
                {
                    // Tworzenie listy roszczeń
                    var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, user.Nick),
    new Claim("Name", user.FirstName),
    new Claim(ClaimTypes.Role, "User"),
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Dodaj to oświadczenie
};


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is not correct");
                }
            }
            return View(model);
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
