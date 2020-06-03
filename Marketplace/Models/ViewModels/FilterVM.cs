using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.ViewModels
{
    public class FilterVM
    {
        public int UserId { get; set; }
        public int FromPrice { get; set; }
        public int ToPrice { get; set; }
        public string Brand { get; set; }
        public double ScreenSize { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; }
        public int BatteryCapacity { get; set; }
        public int DriveVolume { get; set; }
        public string Resolution { get; set; }
        public int RefreshRate { get; set; }
        public int MemorySize { get; set; }
        public string MemoryType { get; set; }
        public int CoresAmount { get; set; }
        public int ClockSpeed { get; set; }
        public int TotalCapacity { get; set; }
        public int ModulesAmount { get; set; }
        public int Capacity { get; set; }
        public int RPM { get; set; }
    }
}
