using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.ReportProcess.Receivers
{
    public class DefaultReceiver
    {
        public void ProcessReport(Report report, RepoFacade repo)
        {
            Ad ad = repo.GetAd(report.AdCategory, report.AdId);
            User owner = repo.GetUser(ad.UserId);

            string userPopover = $"<a data-toggle='popover'  data-container='body'  data-placement='top' data-html='true' data-content='<b>Email</b>: {owner.Email}<br><b>Phone</b>: {owner.Phone}'>{owner.Name}</a>";
            string reportPopover = $"<a data-toggle='popover'  data-container='body'  data-placement='top' data-html='true' data-content='<b>Type</b>: {report.Type}<br><b>Comment</b>: {report.Comment}'>reported</a>";
            string message = $"<a href='/Home/Ad/?id={ad.Id}&category={ad.Category}'>{ad.Title}</a> (owner: {userPopover}) was {reportPopover}! <span class='notification-date'>[{DateTime.Now}]<span>";

            repo.NotifyUser(message, NotificationType.Report, (repo.GetAdmin()).Id);
        }
    }
}
