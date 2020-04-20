using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.DBInteraction
{
    public class NotificationRepository
    {
        private MarketplaceContext ctx;
        private DbSet<Notification> notifications;

        public NotificationRepository(MarketplaceContext context)
        {
            ctx = context;
            notifications = context.Notifications;
        }

        public Notification GetById(int id)
        {
            return notifications.Find(id);
        }
        public IEnumerable<Notification> GetAllByUserId(int userId)
        {
            return notifications.Where(n => n.UserId == userId);
        }

        public void Create(Notification notification)
        {
            notifications.Add(notification);
            ctx.SaveChanges();
        }
    }
}
