using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Marketplace.Models;
using Marketplace.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Marketplace.Models.DBInteraction;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Marketplace.Controllers
{
    public class AccountController : Controller
    {
        private static MarketplaceContext db;
        private UserRepository userRepo;
        private NotificationRepository notificationRepo;

        public AccountController(MarketplaceContext ctx)
        {
            db = ctx;
            userRepo = new UserRepository(ctx);
            notificationRepo = new NotificationRepository(ctx);
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                User user = userRepo.GetByEmailAndPassword(model.Email, HashPassword(model.Password));

                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                model.isIncorrect = true;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (userRepo.GetByEmail(user.Email) == null)
                {
                    user.Password = HashPassword(user.Password);
                    userRepo.Create(user);

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect data");
                    Console.WriteLine("Registration failed!");
                }
            }
            return View(user);
        }

        private async Task Authenticate(User user)
        {
            Role role = db.Roles.Find(user.RoleId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role?.Name)
            };

            ClaimsIdentity ident = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ident));
        }

        private string HashPassword(string password)
        {
            using(SHA256 algorithm = SHA256.Create())
            {
                byte[] passwordInBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hash = algorithm.ComputeHash(passwordInBytes );
                return System.Text.Encoding.ASCII.GetString(hash);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult Notifications()
        {
            int entitiesPerPage = 10;
            User owner = userRepo.GetByEmail(User.Identity.Name);

            IEnumerable<Notification> notifications = notificationRepo.GetAllByUserId(owner.Id);

            List<Notification> list = notifications?.Reverse().Take(entitiesPerPage).ToList();
            int pageAmount = (int)Math.Ceiling(list.Count() / (double)entitiesPerPage);

            NotificationsVM vm = new NotificationsVM(list, 1, pageAmount);

            return View(vm);
        }

        public IActionResult NotificationsPartial(string sort, int page)
        {
            int entitiesPerPage = 10;
            User owner = userRepo.GetByEmail(User.Identity.Name);

            IEnumerable<Notification> notifications = notificationRepo.GetAllByUserId(owner.Id);

            if (sort == "track") notifications = notifications.Where(n => n.Type == NotificationType.PriceTrack);
            else if (sort == "purchase") notifications = notifications.Where(n => n.Type == NotificationType.Purchase);
            else if (sort == "order") notifications = notifications.Where(n => n.Type == NotificationType.Sale);
            else if (sort == "report") notifications = notifications.Where(n => n.Type == NotificationType.Report);

            List<Notification> list = notifications?.Reverse().Skip((page - 1) * entitiesPerPage).Take(entitiesPerPage).ToList();
            int pageAmount = (int)Math.Ceiling(list.Count() / (double)entitiesPerPage);

            Console.WriteLine(pageAmount);

            NotificationsVM vm = new NotificationsVM(list, page, pageAmount);

            return PartialView(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UserAccount(int id)
        {
            User user = userRepo.GetById(id);

            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult BanToggle(int id)
        {
            User user = userRepo.GetById(id);

            if (user.IsBanned) userRepo.Unban(id);
            else userRepo.Ban(id);

            return RedirectToAction("UserAccount", "Account", new { id = id });
        }
    }
}