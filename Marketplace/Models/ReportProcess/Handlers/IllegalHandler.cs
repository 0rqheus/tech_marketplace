using System;

namespace Marketplace.Models.ReportProcess.Handlers
{
    public class IllegalHandler : Handler
    {
        public override void Handle(Report report, RepoFacade repo)
        {
            if (report.Type == ReportTypes.Illegal)
            {
                repo.FreezeAd(report.AdCategory, report.AdId);

                Ad ad = repo.GetAd(report.AdCategory, report.AdId);

                User admin = repo.GetAdmin();

                string adminPopover = $"<a data-toggle='popover' data-container='body' data-placement='top' data-html='true' data-content='Name: {admin.Name}<br>Email: {admin.Email}'>Admin</a>";
                string reportPopover = $"<a data-toggle='popover' data-container='body' data-placement='top' data-html='true' data-content='Type: {report.Type}<br>Comment: {report.Comment}'>reported</a>";
                string message = $"Your ad <a href='/Home/Ad/?id={ad.Id}&category={ad.Category}'>{ad.Title}</a> was {reportPopover} and now is freezed, contact {adminPopover} to solve this problem! <span class='notification-date'>[{DateTime.Now}]</span>";

                repo.NotifyUser(message, NotificationType.Report, ad.UserId);
            }

            Successor.Handle(report, repo);
        }
    }
}
