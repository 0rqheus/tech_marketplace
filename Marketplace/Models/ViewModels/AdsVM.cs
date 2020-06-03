using System.Collections.Generic;

namespace Marketplace.Models.ViewModels
{
    public class AdsVM
    {
        public IEnumerable<Ad> Ads { get; set; }
        public string Category { get; set; }
        public int PageNumber { get; set; }
        public int PageAmount { get; set; }

        public AdsVM(IEnumerable<Ad> ads, string category, int pageNumber, int pageAmount)
        {
            Ads = ads;
            Category = category;
            PageNumber = pageNumber;
            PageAmount = pageAmount;
        }
    }
}
