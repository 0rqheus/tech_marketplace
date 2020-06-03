using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Models.FilterStrategy
{
    public class FilterProcessors<TEntity> : IFilterAdsStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Filter(IEnumerable<TEntity> ads, FilterVM select)
        {
            IEnumerable<ProcessorAd> processors = (IEnumerable<ProcessorAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                processors = IFilterAdsStrategy<ProcessorAd>.FilterPrice(processors, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                processors = IFilterAdsStrategy<ProcessorAd>.FilterBrand(processors, select.Brand);
            }

            if (select.CoresAmount != 0)
            {
                processors = processors.Where(ad => ad.CoresAmount == select.CoresAmount);
            }
            if (select.ClockSpeed != 0)
            {
                processors = processors.Where(ad => ad.ClockSpeed == select.ClockSpeed);
            }

            return (IEnumerable<TEntity>)processors;
        }
    }
}
