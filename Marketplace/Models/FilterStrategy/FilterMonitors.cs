using System.Collections.Generic;
using System.Linq;
using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;

namespace Marketplace.Models.FilterStrategy
{
    public class FilterMonitors<TEntity> : IFilterAdsStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Filter(IEnumerable<TEntity> ads, FilterVM select)
        {
            IEnumerable<MonitorAd> monitors = (IEnumerable<MonitorAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                monitors = IFilterAdsStrategy<MonitorAd>.FilterPrice(monitors, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                monitors = IFilterAdsStrategy<MonitorAd>.FilterBrand(monitors, select.Brand);
            }

            if (select.ScreenSize != 0)
            {
                monitors = monitors.Where(ad => ad.ScreenSize == select.ScreenSize);
            }
            if (select.Resolution?.Trim() != "" && select.Resolution != null)
            {
                monitors = monitors.Where(ad => ad.Resolution == select.MemoryType);
            }
            if (select.RefreshRate != 0)
            {
                monitors = monitors.Where(ad => ad.RefreshRate == select.RefreshRate);
            }

            return (IEnumerable<TEntity>)monitors;
        }
    }
}
