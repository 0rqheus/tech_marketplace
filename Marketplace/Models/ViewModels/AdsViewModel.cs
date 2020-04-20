using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.ViewModels
{
    public class AdsViewModel
    {
        public IEnumerable<AdPhotoDecorator> Ads { get; set; }
        public string Category { get; set; }
        public int PageNumber { get; set; }
        public int PageAmount { get; set; }

        public AdsViewModel(IEnumerable<AdPhotoDecorator> ads, string category, int pageNumber, int pageAmount)
        {
            Ads = ads;
            Category = category;
            PageNumber = pageNumber;
            PageAmount = pageAmount;
        }
    }
}
