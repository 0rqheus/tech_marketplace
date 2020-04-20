using Marketplace.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.SelectStrategy
{
    interface IFilterStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Select(IEnumerable<TEntity> ads, FilterViewModel select);

        public static IEnumerable<TEntity> SelectPrice(IEnumerable<TEntity> ads, int priceFrom, int priceTo)
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

        public static IEnumerable<TEntity> SelectBrand(IEnumerable<TEntity> ads, string brand)
        {
            return ads.Where(ad => ad.Brand == brand);
        }
    }
}
