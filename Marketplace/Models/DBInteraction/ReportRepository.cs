using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.DBInteraction
{
    public class ReportRepository
    {
        private MarketplaceContext ctx;
        private DbSet<Report> reports;

        public ReportRepository(MarketplaceContext context)
        {
            ctx = context;
            reports = context.Reports;
        }

        public Report GetById(int id)
        {
            return reports.Find(id);
        }
        public Report GetByAdAndSender(int adId, int senderId)
        {
            return reports.FirstOrDefault(r => r.AdId == adId && r.SenderId == senderId);
        }

        public void Create(Report report)
        {
            reports.Add(report);
            ctx.SaveChanges();
        }

    }
}
