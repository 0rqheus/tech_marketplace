using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Models.AdTypes;
using Marketplace.Models.DBInteraction;
using Marketplace.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models
{
    public class RepoFacade
    {
        UserRepository userRepo;
        NotificationRepository notificationRepo;

        ReportRepository reportRepo;

        GenericAdRepository<SmartphoneAd> smartphoneRepo;
        GenericAdRepository<LaptopAd> laptopRepo;
        GenericAdRepository<VideocardAd> videocardRepo;
        GenericAdRepository<MonitorAd> monitorRepo;
        GenericAdRepository<ProcessorAd> processorRepo;
        GenericAdRepository<RAMAd> ramRepo;
        GenericAdRepository<DriveAd> driveRepo;
        public RepoFacade(MarketplaceContext db)
        {
            userRepo = new UserRepository(db);
            notificationRepo = new NotificationRepository(db);

            reportRepo = new ReportRepository(db);

            smartphoneRepo = new GenericAdRepository<SmartphoneAd>(db);
            laptopRepo = new GenericAdRepository<LaptopAd>(db);
            videocardRepo = new GenericAdRepository<VideocardAd>(db);
            monitorRepo = new GenericAdRepository<MonitorAd>(db);
            processorRepo = new GenericAdRepository<ProcessorAd>(db);
            ramRepo = new GenericAdRepository<RAMAd>(db);
            driveRepo = new GenericAdRepository<DriveAd>(db);
        }

        // Ads
        public Ad GetAd(string category, int id)
        {
            if (category == "smartphone")
            {
                return smartphoneRepo.GetById(id);
            }
            else if (category == "laptop")
            {
                return laptopRepo.GetById(id);
            }
            else if (category == "videocard")
            {
                return videocardRepo.GetById(id);
            }
            else if (category == "monitor")
            {
                return monitorRepo.GetById(id);
            }
            else if (category == "processor")
            {
                return processorRepo.GetById(id);
            }
            else if (category == "ram")
            {
                return ramRepo.GetById(id);
            }
            else if (category == "drive")
            {
                return driveRepo.GetById(id);
            }

            return null;
        }

        public PageContainer GetPage(string category, int page = 1, string search="", string sort = "recent", FilterVM filter = null)
        {
            PageContainer container = new PageContainer();

            if (category == "smartphone")
            {
                container = smartphoneRepo.GetPage(category, page, search, sort, filter);
            }
            else if (category == "laptop")
            {
                container = laptopRepo.GetPage(category, page, search, sort, filter);
            }
            else if (category == "videocard")
            {
                container = videocardRepo.GetPage(category, page, search, sort, filter);
            }
            else if (category == "monitor")
            {
                container = monitorRepo.GetPage(category, page, search, sort, filter);
            }
            else if (category == "processor")
            {
                container = processorRepo.GetPage(category, page, search, sort, filter);
            }
            else if (category == "ram")
            {
                container = ramRepo.GetPage(category, page, search, sort, filter);
            }
            else if (category == "drive")
            {
                container = driveRepo.GetPage(category, page, search, sort, filter);
            }

            return container;
        }

        public int CreateAd(CreateVM createVM, int userId)
        {
            if (createVM.Category == "smartphone")
            {
                SmartphoneAd categoryAd = new SmartphoneAd(createVM) { UserId = userId };
                smartphoneRepo.Create(categoryAd);
                return categoryAd.Id;
            }
            else if (createVM.Category == "laptop")
            {
                LaptopAd categoryAd = new LaptopAd(createVM) { UserId = userId };
                laptopRepo.Create(categoryAd);
                return categoryAd.Id;
            }
            else if (createVM.Category == "videocard")
            {
                VideocardAd categoryAd = new VideocardAd(createVM) { UserId = userId };
                videocardRepo.Create(categoryAd);
                return categoryAd.Id;
            }
            else if (createVM.Category == "monitor")
            {
                MonitorAd categoryAd = new MonitorAd(createVM) { UserId = userId };
                monitorRepo.Create(categoryAd);
                return categoryAd.Id;
            }
            else if (createVM.Category == "processor")
            {
                ProcessorAd categoryAd = new ProcessorAd(createVM) { UserId = userId };
                processorRepo.Create(categoryAd);
                return categoryAd.Id;
            }
            else if (createVM.Category == "ram")
            {
                RAMAd categoryAd = new RAMAd(createVM) { UserId = userId };
                ramRepo.Create(categoryAd);
                return categoryAd.Id;
            }
            else if (createVM.Category == "drive")
            {
                DriveAd categoryAd = new DriveAd(createVM) { UserId = userId };
                driveRepo.Create(categoryAd);
                return categoryAd.Id;
            }

            return 0;
        }

        public void UpdateAd(CreateVM vm, Func<int, string, Task> notify)
        {
            if (vm.Category == "smartphone") smartphoneRepo.Update(vm, notificationRepo, notify);
            else if (vm.Category == "laptop") laptopRepo.Update(vm, notificationRepo, notify);
            else if (vm.Category == "videocard") videocardRepo.Update(vm, notificationRepo, notify);
            else if (vm.Category == "monitor") monitorRepo.Update(vm, notificationRepo, notify);
            else if (vm.Category == "processor") processorRepo.Update(vm, notificationRepo, notify);
            else if (vm.Category == "ram") ramRepo.Update(vm, notificationRepo, notify);
            else if (vm.Category == "drive") driveRepo.Update(vm, notificationRepo, notify);
        }

        public void DeleteAd(string category, int id)
        {
            if (category == "smartphone") smartphoneRepo.Delete(id);
            else if (category == "laptop") laptopRepo.Delete(id);
            else if (category == "videocard") videocardRepo.Delete(id);
            else if (category == "monitor") monitorRepo.Delete(id);
            else if (category == "processor") processorRepo.Delete(id);
            else if (category == "ram") ramRepo.Delete(id);
            else if (category == "drive") driveRepo.Delete(id);
        }

        public void SubscribeOnAd(string category, int id, int subscriberId)
        {
            if (category == "smartphone") smartphoneRepo.Subscribe(id, subscriberId);
            else if (category == "laptop") laptopRepo.Subscribe(id, subscriberId);
            else if (category == "videocard") videocardRepo.Subscribe(id, subscriberId);
            else if (category == "monitor") monitorRepo.Subscribe(id, subscriberId);
            else if (category == "processor") processorRepo.Subscribe(id, subscriberId);
            else if (category == "ram") ramRepo.Subscribe(id, subscriberId);
            else if (category == "drive") driveRepo.Subscribe(id, subscriberId);
        }

        public void UnsubscribeFromAd(string category, int id, int subscriberId)
        {
            if (category == "smartphone") smartphoneRepo.Unsubscribe(id, subscriberId);
            else if (category == "laptop") laptopRepo.Unsubscribe(id, subscriberId);
            else if (category == "videocard") videocardRepo.Unsubscribe(id, subscriberId);
            else if (category == "monitor") monitorRepo.Unsubscribe(id, subscriberId);
            else if (category == "processor") processorRepo.Unsubscribe(id, subscriberId);
            else if (category == "ram") ramRepo.Unsubscribe(id, subscriberId);
            else if (category == "drive") driveRepo.Unsubscribe(id, subscriberId);
        }

        public void FreezeAd(string category, int id)
        {
            if (category == "smartphone") smartphoneRepo.Freeze(id);
            else if (category == "laptop") laptopRepo.Freeze(id);
            else if (category == "videocard") videocardRepo.Freeze(id);
            else if (category == "monitor") monitorRepo.Freeze(id);
            else if (category == "processor") processorRepo.Freeze(id);
            else if (category == "ram") ramRepo.Freeze(id);
            else if (category == "drive") driveRepo.Freeze(id);
        }

        public void DefreezeAd(string category, int id)
        {
            if (category == "smartphone") smartphoneRepo.Defreeze(id);
            else if (category == "laptop") laptopRepo.Defreeze(id);
            else if (category == "videocard") videocardRepo.Defreeze(id);
            else if (category == "monitor") monitorRepo.Defreeze(id);
            else if (category == "processor") processorRepo.Defreeze(id);
            else if (category == "ram") ramRepo.Defreeze(id);
            else if (category == "drive") driveRepo.Defreeze(id);
        }


        // Users
        public User GetUser(int id)
        {
            return userRepo.GetById(id);
        }

        public User GetUser(string email)
        {
            return userRepo.GetByEmail(email);
        }

        public User GetAdmin()
        {
            return userRepo.GetAdmin();
        }

        public void NotifyUser(string message, NotificationType type, int userId)
        {
            notificationRepo.Create(new Notification(message, type, userId));
        }

        public void BanUser(int id)
        {
            userRepo.Ban(id);
        }

        public void UnbanUser(int id)
        {
            userRepo.Unban(id);
        }


        // Reports
        public Report GetReport(int id)
        {
            return reportRepo.GetById(id);
        }
        
        public Report GetReport(int adId, int senderId)
        {
            return reportRepo.GetByAdAndSender(adId, senderId);
        }

        public void CreateReport(Report report)
        {
            reportRepo.Create(report);
        }
    }
}
