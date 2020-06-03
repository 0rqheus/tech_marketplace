using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Models.FilterStrategy
{
    public class FilterDrives<TEntity> : IFilterAdsStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Filter(IEnumerable<TEntity> ads, FilterVM select)
        {
            IEnumerable<DriveAd> drives = (IEnumerable<DriveAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                drives = IFilterAdsStrategy<DriveAd>.FilterPrice(drives, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                drives = IFilterAdsStrategy<DriveAd>.FilterBrand(drives, select.Brand);
            }

            if (select.Capacity != 0)
            {
                drives = drives.Where(ad => ad.Capacity == select.Capacity);
            }
            if (select.RPM != 0)
            {
                drives = drives.Where(ad => ad.RPM == select.RPM);
            }

            return (IEnumerable<TEntity>)drives;
        }
    }
}
