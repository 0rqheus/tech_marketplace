using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using Marketplace.Models.ReportProcess.Handlers;
using Marketplace.Models.ReportProcess;
using Marketplace.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Marketplace.Hubs;
using System.Threading.Tasks;

namespace Marketplace.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class HomeController : Controller
    {
        private RepoFacade repo;
        private IHubContext<NotificationHub> hubContext;

        public HomeController(MarketplaceContext ctx, IHubContext<NotificationHub> hubCtx)
        {
            repo = new RepoFacade(ctx);
            hubContext = hubCtx;
        }

        [AllowAnonymous]
        private async Task Notify(int userId, string title, string text)
        {
            User user = repo.GetUser(userId);
            await hubContext.Clients.User(user.Email).SendAsync("Notify", title, text);
        }

        [AllowAnonymous]
        private async Task Notify(string userEmail, string title, string text)
        {
            await hubContext.Clients.User(userEmail).SendAsync("Notify", title, text);
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
        public async void Buy(int adId, string category)
        {
            User sender = repo.GetUser(User.Identity.Name);
            Ad ad = repo.GetAd(category, adId);

            if (ad == null)
            {
                return;
            }

            string userPopover = $"<a data-toggle='popover'  data-container='body'  data-placement='top' data-html='true' data-content='<b>Email</b>: {sender.Email}<br><b>Phone</b>: {sender.Phone}'>{sender.Name}</a>";
            string adLink = $"<a href='/Home/Ad/?id={ad.Id}&category={ad.Category}'>{ad.Title}</a>";

            string saleText = $"{userPopover} bought your '{adLink}' <span class='notification-date'>[{DateTime.Now}]</span>";
            string orderedText = $"You ordered '{adLink}' <span class='notification-date'>[{DateTime.Now}]</span>";

            repo.NotifyUser(saleText, NotificationType.Purchase, ad.UserId);
            repo.NotifyUser(orderedText, NotificationType.Sale, sender.Id);

            await Notify(ad.UserId, "New purchase", saleText);
            await Notify(sender.Email, "New order", orderedText);
        }

        [HttpPost]
        public void SubscribeOnPriceToggle(int adId, string category)
        {
            Ad targetAd = repo.GetAd(category, adId);
            User u = repo.GetUser(User.Identity.Name);

            if (targetAd == null)
            {
                return;
            }

            if (targetAd.Subscribers.Contains(u.Id)) repo.UnsubscribeFromAd(category, adId, u.Id);
            else repo.SubscribeOnAd(category, adId, u.Id);
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
        public async Task<string> ReportAd(int adId, string adCategory, string comment, string reportType)
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

            DefaultHandler defaultHandler = new DefaultHandler();
            ScamHandler scamHandler = new ScamHandler() { Successor = defaultHandler};
            IllegalHandler illegalHandler = new IllegalHandler() { Successor = scamHandler };

            ReportProcessInvoker invoker = new ReportProcessInvoker();
            invoker.SetFirtHadler(illegalHandler);

            invoker.StartReportProcessing(report, repo);

            await Notify(u.Email, "Your ad was reported", comment);

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
        public IActionResult Create(CreateVM createVM)
        {
            User u = repo.GetUser(User.Identity.Name);

            if (u.IsBanned) if (u.IsBanned) return RedirectToAction("Banned", "Error");

            int Id = repo.CreateAd(createVM, u.Id);

            return RedirectToAction("Ad", "Home", new { id = Id, category  = createVM.Category});
        }

        public IActionResult CreatesPartial(string category)
        {
            return PartialView(new Category(category));
        }

        public IActionResult Ads(string category)
        {
            PageContainer container = repo.GetPage(category, 1);
            AdsVM vm = new AdsVM(container.Ads, category, 1, container.PageAmount);

            return View(vm);
        }

        public IActionResult AdsPartial(int page, string category, string search, string sort, FilterVM filter)
        {
            if (sort == "own") filter.UserId = (repo.GetUser(User.Identity.Name)).Id;
            PageContainer container = repo.GetPage(category, page, search, sort, filter);

            AdsVM vm = new AdsVM(container.Ads, category, page, container.PageAmount);

            return PartialView(vm);
        }

        public IActionResult Ad(int id, string category)
        {
            Ad targetAd = repo.GetAd(category, id);
            User user = repo.GetUser(User.Identity.Name);

            if (targetAd == null) return RedirectToAction("PageNotFound", "Error");
            if (targetAd.IsFreezed == true && targetAd.UserId != user.Id) return RedirectToAction("Frozen", "Error");

            
            ViewBag.User = user;

            return View(targetAd);
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
        public async Task<IActionResult> Update(CreateVM vm)
        {
            User u = repo.GetUser(User.Identity.Name);
            Ad ad = repo.GetAd(vm.Category, vm.Id);

            if (ad == null) return RedirectToAction("PageNotFound", "Error");
            if (ad.UserId != u.Id) return RedirectToAction("Forbidden", "Error");

            repo.UpdateAd(vm, (userId, message) => Notify(userId, "Price of observed item changed", message));
            await Notify(ad.UserId, "Price of observed item changed", "testing...");

            return RedirectToAction("Ad", "Home", new { id = vm.Id, category = vm.Category });
        }

        [HttpPost]
        public IActionResult Delete(int id, string category)
        {
            User u = repo.GetUser(User.Identity.Name);
            Ad ad = repo.GetAd(category, id);

            if (ad == null) return RedirectToAction("PageNotFound", "Error");
            if (ad.UserId != u.Id && u.RoleId != 1) return RedirectToAction("Forbidden", "Error");

            repo.DeleteAd(category, id);

            return RedirectToAction("Index", "Home");
        }

    }
}
