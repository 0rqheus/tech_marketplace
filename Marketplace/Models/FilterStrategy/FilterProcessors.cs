using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Models.SelectStrategy
{
    public class FilterProcessors<TEntity> : IFilterStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Select(IEnumerable<TEntity> ads, FilterViewModel select)
        {
            IEnumerable<ProcessorAd> processors = (IEnumerable<ProcessorAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                processors = IFilterStrategy<ProcessorAd>.SelectPrice(processors, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                processors = IFilterStrategy<ProcessorAd>.SelectBrand(processors, select.Brand);
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
