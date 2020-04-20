using Marketplace.Models.AdTypes;
using Marketplace.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Models.SelectStrategy
{
    public class FilterRAMs<TEntity> : IFilterStrategy<TEntity> where TEntity : Ad
    {
        public IEnumerable<TEntity> Select(IEnumerable<TEntity> ads, FilterViewModel select)
        {
            IEnumerable<RAMAd> drives = (IEnumerable<RAMAd>)ads;

            if (select.FromPrice != 0 || select.ToPrice != 0)
            {
                drives = IFilterStrategy<RAMAd>.SelectPrice(drives, select.FromPrice, select.ToPrice);
            }
            if (select.Brand?.Trim() != "" && select.Brand != null)
            {
                drives = IFilterStrategy<RAMAd>.SelectBrand(drives, select.Brand);
            }

            if (select.TotalCapacity != 0)
            {
                drives = drives.Where(ad => ad.TotalCapacity == select.TotalCapacity);
            }
            if (select.ModulesAmount != 0)
            {
                drives = drives.Where(ad => ad.ModulesAmount == select.ModulesAmount);
            }
            if (select.MemoryType?.Trim() != "" && select.MemoryType != null)
            {
                drives = drives.Where(ad => ad.MemoryType == select.MemoryType);
            }

            return (IEnumerable<TEntity>)drives;
        }
    }
}
