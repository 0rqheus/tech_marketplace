using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Marketplace.Models.DBInteraction;
using Marketplace.Models.ViewModels;
using Marketplace.Models.SelectStrategy;

namespace Marketplace.Models
{
    public class GenericAdRepository<TEntity> where TEntity : Ad
    {
        private DbContext ctx;
        private DbSet<TEntity> dbSet;

        public GenericAdRepository(DbContext context)
        {
            ctx = context;
            dbSet = context.Set<TEntity>();
        }

        public PageContainer GetPage(string category, int page, int entitiesPerPage, string sort, FilterViewModel select)
        {
            PageContainer container = new PageContainer();
            IEnumerable<TEntity> ads = dbSet.Where(ad => ad.IsFreezed == false);

            //select
            IFilterStrategy<TEntity> selectAds = null;

            if (select != null)
            {
                if (category == "smartphone")
                {
                    selectAds = new FilterSmartphones<TEntity>();
                }
                else if (category == "laptop")
                {
                    selectAds = new FilterLaptops<TEntity>();
                }
                else if (category == "monitor")
                {
                    selectAds = new FilterMonitors<TEntity>();
                }
                else if (category == "videocard")
                {
                    selectAds = new FilterVideocards<TEntity>();
                }
                else if (category == "processor")
                {
                    selectAds = new FilterProcessors<TEntity>();
                }
                else if (category == "RAM")
                {
                    selectAds = new FilterRAMs<TEntity>();
                }
                else if (category == "drive")
                {
                    selectAds = new FilterDrives<TEntity>();
                }
            }

            if (selectAds != null) 
                ads = selectAds.Select(ads, select);
            //

            // sort
            if (sort == "recent") ads = ads.OrderByDescending(ad => ad.Id);
            else if (sort == "decrease-price") ads = ads.OrderByDescending(ad => ad.Price);
            else if (sort == "increase-price") ads = ads.OrderBy(ad => ad.Price);
            else if (sort == "own") ads = ads.Where(ad => ad.UserId == select.UserId);
            //

            container.Ads = ads.Skip((page - 1) * entitiesPerPage).Take(entitiesPerPage);
            container.PageAmount = (int)Math.Ceiling(ads.Count() / (double)entitiesPerPage);

            return container;
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
            ctx.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(CreateViewModel vm, NotificationRepository notificationRepo)
        {
            TEntity currAd = GetById(vm.Id);

            if (vm.Title != null && vm.Title.Trim() != "" && vm.Title != currAd.Title)
            {
                currAd.Title = vm.Title;
            }
            else if (vm.Description != null && vm.Description.Trim() != "" && vm.Description != currAd.Description)
            {
                currAd.Description = vm.Description;
            }
            else if (vm.Price != 0 && vm.Price != currAd.Price)
            {
                currAd.Price = vm.Price;

                for (int i = 0; i < currAd.Subscribers.Count; i++)
                {
                    notificationRepo.Create(new Notification($"<a href='/Home/Ad/?id={vm.Id}&category={vm.Category}'>{currAd.Title}`s</a> new price is {currAd.Price} <span class='notification-date'>[${DateTime.Now}]</span>", NotificationType.PriceTrack, currAd.Subscribers[i]));
                }
            }
            else if (vm.PhotoFile != null)
            {
                using (var binaryReader = new BinaryReader(vm.PhotoFile.OpenReadStream()))
                {
                    byte[] photoBytes = binaryReader.ReadBytes((int)vm.PhotoFile.Length);
                    if (currAd.Photo != photoBytes) currAd.Photo = photoBytes;
                }
            }

            ctx.SaveChanges();
        }
    
        public void Delete(int id)
        {
            TEntity ad = GetById(id);
            dbSet.Remove(ad);
            ctx.SaveChanges();
        }

        public void Subscribe(int id, int subscriberId)
        {
            TEntity currAd = GetById(id);
            currAd.Subscribers.Add(subscriberId);
            ctx.SaveChanges();
        }

        public void Unsubscribe(int id, int subscriberId)
        {
            TEntity currAd = GetById(id);
            currAd.Subscribers.Remove(subscriberId);
            ctx.SaveChanges();
        }

        public void Freeze(int id)
        {
            TEntity currAd = GetById(id);
            currAd.IsFreezed = true;
            ctx.SaveChanges();
        }

        public void Defreeze(int id)
        {
            TEntity currAd = GetById(id);
            currAd.IsFreezed = false;
            ctx.SaveChanges();
        }
    }
}
