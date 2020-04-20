using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Models.SelectStrategy
{
    public class FilterLaptops<TEntity> : IFilterStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Select(IEnumerable<TEntity> ads, FilterViewModel select)
        {
            IEnumerable<LaptopAd> laptops = (IEnumerable<LaptopAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                laptops = IFilterStrategy<LaptopAd>.SelectPrice(laptops, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                laptops = IFilterStrategy<LaptopAd>.SelectBrand(laptops, select.Brand);
            }

            if (select.ScreenSize != 0)
            {
                laptops = laptops.Where(ad => ad.ScreenSize == select.ScreenSize);
            }
            if (select.RAM != 0)
            {
                laptops = laptops.Where(ad => ad.RAM == select.RAM);
            }
            if (select.DriveVolume != 0)
            {
                laptops = laptops.Where(ad => ad.DriveVolume == select.BatteryCapacity);
            }

            return (IEnumerable<TEntity>)laptops;
        }
    }
}
