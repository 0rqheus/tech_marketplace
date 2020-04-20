using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;

namespace Marketplace.Models.SelectStrategy
{
    public class FilterVideocards<TEntity> : IFilterStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Select(IEnumerable<TEntity> ads, FilterViewModel select)
        {
            IEnumerable<VideocardAd> videocards = (IEnumerable<VideocardAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                videocards = IFilterStrategy<VideocardAd>.SelectPrice(videocards, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                videocards = IFilterStrategy<VideocardAd>.SelectBrand(videocards, select.Brand);
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
