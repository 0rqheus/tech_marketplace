using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;

namespace Marketplace.Models.FilterStrategy
{
    public class FilterVideocards<TEntity> : IFilterAdsStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Filter(IEnumerable<TEntity> ads, FilterVM select)
        {
            IEnumerable<VideocardAd> videocards = (IEnumerable<VideocardAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                videocards = IFilterAdsStrategy<VideocardAd>.FilterPrice(videocards, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                videocards = IFilterAdsStrategy<VideocardAd>.FilterBrand(videocards, select.Brand);
            }

            if (select.MemorySize != 0)
            {
                videocards = videocards.Where(ad => ad.MemorySize == select.MemorySize);
            }
            if (select.MemoryType?.Trim() != "" && select.MemoryType != null)
            {
                videocards = videocards.Where(ad => ad.MemoryType == select.MemoryType);
            }

            return (IEnumerable<TEntity>)videocards;
        }
    }
}
