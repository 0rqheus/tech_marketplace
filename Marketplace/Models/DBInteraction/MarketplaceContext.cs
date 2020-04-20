using Marketplace.Models.AdTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Models
{
    public class MarketplaceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<SmartphoneAd> Smartphones { get; set; }
        public DbSet<LaptopAd> Laptops { get; set; }
        public DbSet<VideocardAd> Videocards { get; set; }
        public DbSet<MonitorAd> Monitors { get; set; }
        public DbSet<ProcessorAd> Processors { get; set; }
        public DbSet<RAMAd> RAMs { get; set; }
        public DbSet<DriveAd> Drives { get; set; }

        public DbSet<Report> Reports { get; set; }

        public MarketplaceContext(DbContextOptions<MarketplaceContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Roles
            Role adminRole = new Role { Id = 1, Name = "Admin" };
            Role userRole = new Role { Id = 2, Name = "User" };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            base.OnModelCreating(modelBuilder);

            // Subscribers
            var valueComparer = new ValueComparer<List<int>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => (List<int>)c.ToList());


            var converter = new ValueConverter<List<int>, string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val)).ToList());

            modelBuilder.Entity<SmartphoneAd>()
                .Property(ad => ad.Subscribers)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<LaptopAd>()
                .Property(ad => ad.Subscribers)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<VideocardAd>()
                .Property(ad => ad.Subscribers)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<MonitorAd>()
                .Property(ad => ad.Subscribers)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<ProcessorAd>()
                .Property(ad => ad.Subscribers)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<RAMAd>()
                .Property(ad => ad.Subscribers)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<DriveAd>()
                .Property(ad => ad.Subscribers)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);
        }
    }
}
