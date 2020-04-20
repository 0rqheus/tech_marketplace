using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;

namespace Marketplace.Models.SelectStrategy
{
    public class FilterSmartphones<TEntity> : IFilterStrategy<TEntity> where TEntity : Ad
    { 
        public IEnumerable<TEntity> Select(IEnumerable<TEntity> ads, FilterViewModel select)
        {
            IEnumerable<SmartphoneAd> smartphones = (IEnumerable<SmartphoneAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                smartphones = IFilterStrategy<SmartphoneAd>.SelectPrice(smartphones, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                smartphones = IFilterStrategy<SmartphoneAd>.SelectBrand(smartphones, select.Brand);
            }

            if(select.ScreenSize != 0)
            {
                smartphones = smartphones.Where(ad => ad.ScreenSize == select.ScreenSize);
            }
            if (select.RAM != 0)
            {
                smartphones = smartphones.Where(ad => ad.RAM == select.RAM);
            }
            if (select.BatteryCapacity != 0)
            {
                smartphones = smartphones.Where(ad => ad.BatteryCapacity == select.BatteryCapacity);
            }

            return (IEnumerable<TEntity>)smartphones;
        }
    }
}
