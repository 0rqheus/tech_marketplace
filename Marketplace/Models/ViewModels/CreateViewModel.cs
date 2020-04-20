using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public IFormFile PhotoFile { get; set; }
        public string Brand { get; set; }
        public double ScreenSize { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; }
        public int BatteryCapacity { get; set; }
        public string Videocard { get; set; }
        public int DriveVolume { get; set; }
        public string Resolution { get; set; }
        public string MatrixType { get; set; }
        public int RefreshRate { get; set; }
        public string AspectRatio { get; set; }
        public int MemorySize { get; set; }
        public string MemoryType { get; set; }
        public int MemoryBusWidth { get; set; }
        public int CoresAmount { get; set; }
        public int ClockSpeed { get; set; }
        public int TotalCapacity { get; set; }
        public int ModulesAmount { get; set; }
        public int Capacity { get; set; }
        public int RPM { get; set; }
        public int FormFactor { get; set; }

    }
}
