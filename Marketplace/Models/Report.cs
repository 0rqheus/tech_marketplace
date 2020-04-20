using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public enum ReportTypes
    {
        Illegal = 1,
        Scam,
        Other
    }

    public class Report
    {
        public int Id { get; set; }
        public int AdId { get; set; }
        public string AdCategory { get; set; }
        public string Comment { get; set; }
        public ReportTypes Type { get; set; }
        public int SenderId { get; set; } // for spam checking
    }
}
