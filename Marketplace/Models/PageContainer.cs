using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public class PageContainer
    {
        public int PageAmount { get; set; }
        public IEnumerable<Ad> Ads {get; set;}
    }
}
