using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using Marketplace.Models.ReportProcess.Commands;
using Marketplace.Models.ReportProcess.Receivers;
using Marketplace.Models.ReportProcess;
using Marketplace.Models.ViewModels;

namespace Marketplace.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class HomeController : Controller
    {
        private RepoFacade repo;

        public HomeController(MarketplaceContext ctx)
        {
            repo = new RepoFacade(ctx);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        // Ad actions
        [HttpPost]
        public void Buy(int adId, string category)
        {
            User sender = repo.GetUser(User.Identity.Name);
            Ad ad = repo.GetAd(category, adId);

            if (ad == null)
            {
                return;
            }

            string userPopover = $"<a data-toggle='popover'  data-container='body'  data-placement='top' data-html='true' data-content='<b>Email</b>: {sender.Email}<br><b>Phone</b>: {sender.Phone}'>{sender.Name}</a>";
            string adLink = $"<a href='/Home/Ad/?id={ad.Id}&category={ad.Category}'>{ad.Title}</a>";

            repo.NotifyUser($"{userPopover} bought your '{adLink}' <span class='notification-date'>[{DateTime.Now}]</span>", NotificationType.Purchase, ad.UserId);
            repo.NotifyUser($"You ordered '{adLink}' <span class='notification-date'>[{DateTime.Now}]</span>", NotificationType.Sale, sender.Id);
        }

        [HttpPost]
        public void SubscribeOnPriceToggle(int id, string category)
        {
            Ad targetAd = repo.GetAd(category, id);
            User u = repo.GetUser(User.Identity.Name);

            if (targetAd == null)
            {
                return;
            }

            if (targetAd.Subscribers.Contains(u.Id)) repo.Unsubscribe(category, id, u.Id);
            else repo.Subscribe(category, id, u.Id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void FreezeAdToggle(int id, string category)
        {
            Ad ad = repo.GetAd(category, id);

            if(ad == null)
            {
                return;
            }

            if (ad.IsFreezed) repo.FreezeAd(category, id);
            else repo.DefreezeAd(category, id);
        }

        [HttpPost]
        public string ReportAd(int adId, string adCategory, string comment, string reportType)
        {
            User u = repo.GetUser(User.Identity.Name);

            Report r = repo.GetReport(adId, u.Id);

            if (r != null) return "You already reported this!";

            ReportTypes type = reportType == "Illegal"
                                                ? ReportTypes.Illegal
                                                : reportType == "Scam"
                                                    ? ReportTypes.Scam
                                                    : ReportTypes.Other;

            Report report = new Report() { AdId = adId, AdCategory = adCategory, Comment = comment, Type = type, SenderId = u.Id };
            repo.CreateReport(report);

            DefaultHandler defaultHandler = new DefaultHandler(new DefaultReceiver(), report, repo);
            ScamHandler scamHandler = new ScamHandler(new ScamReceiver(), report, repo) { Successor = defaultHandler};
            IllegalHandler illegalHandler = new IllegalHandler(new IllegalReceiver(), report, repo) { Successor = scamHandler };

            ReportProcessInvoker invoker = new ReportProcessInvoker();
            invoker.SetFirtHadler(illegalHandler);
            invoker.StartReportProcessing();

            return "Thanks for report!";
        }

        // CRUD
        [HttpGet]
        public IActionResult Create()
        {
            User u = repo.GetUser(User.Identity.Name);
            if (u.IsBanned) return RedirectToAction("Banned", "Error");

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel createVM)
        {
            User u = repo.GetUser(User.Identity.Name);

            if (u.IsBanned) if (u.IsBanned) return RedirectToAction("Banned", "Error");

            int Id = repo.Create(createVM, u.Id);

            return RedirectToAction("Ad", "Home", new { id = Id, category  = createVM.Category});
        }

        public IActionResult CreatesPartial(string category)
        {
            return PartialView(new Category(category));
        }

        public IActionResult Ads(string category)
        {
            int enititesPerPage = 10;
            PageContainer container = repo.GetPage(category, 1, enititesPerPage);
            AdsViewModel vm = new AdsViewModel(container.Ads.Select(ad => new AdPhotoDecorator(ad)), category, 1, container.PageAmount);

            return View(vm);
        }

        public IActionResult AdsPartial(int page, string category, string sort, FilterViewModel select)
        {
            int enititesPerPage = 10;
            PageContainer container = repo.GetPage(category, page, enititesPerPage, sort, select);

            if (sort == "own") select.UserId = (repo.GetUser(User.Identity.Name)).Id;

            AdsViewModel vm = new AdsViewModel(container.Ads.Select(ad => new AdPhotoDecorator(ad)), category, page, container.PageAmount);

            return PartialView(vm);
        }

        public IActionResult Ad(int id, string category)
        {
            Ad targetAd = repo.GetAd(category, id);

            if (targetAd == null) return RedirectToAction("PageNotFound", "Error");
            if (targetAd.IsFreezed == true && targetAd.UserId != (repo.GetUser(User.Identity.Name)).Id) return Content("Sorry, but this Ad is freezed");

            AdDecorator decor = new AdPhotoDecorator(targetAd);

            ViewBag.AdDecor = decor;
            ViewBag.Ad = targetAd;
            ViewBag.AdAccess = new AdAccessProxy(decor);
            ViewBag.User = repo.GetUser(User.Identity.Name);

            return View();
        }

        [HttpGet]
        public IActionResult Update(int id, string category)
        {
            Ad ad = repo.GetAd(category, id);
            User u = repo.GetUser(User.Identity.Name);

            if (ad == null) return RedirectToAction("PageNotFound", "Error");
            if (ad.UserId != u.Id) return RedirectToAction("Forbidden", "Error");

            return View(ad);
        }

        [HttpPost]
        public IActionResult Update(CreateViewModel vm)
        {
            User u = repo.GetUser(User.Identity.Name);
            Ad ad = repo.GetAd(vm.Category, vm.Id);

            if (ad == null) return RedirectToAction("PageNotFound", "Error");
            if (ad.UserId != u.Id) return RedirectToAction("Forbidden", "Error");

            repo.Update(vm);

            return RedirectToAction("Ad", "Home", new { id = vm.Id, category = vm.Category });
        }

        [HttpPost]
        public IActionResult Delete(int id, string category)
        {
            User u = repo.GetUser(User.Identity.Name);
            Ad ad = repo.GetAd(category, id);

            if (ad == null) return RedirectToAction("PageNotFound", "Error");
            if (ad.UserId != u.Id && u.RoleId != 1) return RedirectToAction("Forbidden", "Error");

            repo.Delete(category, id);

            return RedirectToAction("Index", "Home");
        }

        // Errors
        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
