using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.ViewModels
{
    public class NotificationsVM
    {
        public List<Notification> Notifications { get; set; }
        public int PageNumber { get; set; }
        public int PageAmount { get; set; }

        public NotificationsVM(List<Notification> notifications, int pageNumber, int pageAmount)
        {
            Notifications = notifications;
            PageNumber = pageNumber;
            PageAmount = pageAmount;
        }
    }
}
