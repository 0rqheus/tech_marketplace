using Marketplace.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.FilterStrategy
{
    interface IFilterAdsStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Filter(IEnumerable<TEntity> ads, FilterVM select);

        public static IEnumerable<TEntity> FilterPrice(IEnumerable<TEntity> ads, int priceFrom, int priceTo)
        {
            if (priceFrom != 0 && priceTo != 0)
            {
                return ads.Where(ad => ad.Price >= priceFrom && ad.Price <= priceTo);
            }
            else if (priceFrom != 0)
            {
                return ads.Where(ad => ad.Price >= priceFrom);
            }
            else
            {
                return ads.Where(ad => ad.Price <= priceTo);
            }
        }

        public static IEnumerable<TEntity> FilterBrand(IEnumerable<TEntity> ads, string brand)
        {
            return ads.Where(ad => ad.Brand == brand);
        }
    }
}
