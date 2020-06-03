using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Marketplace.Models.AdTypes
{
    public class LaptopAd : Ad
    {
        [Column(TypeName = "decimal(18, 2)")]
        public double ScreenSize { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; }
        public string Videocard { get; set; }
        public int DriveVolume { get; set; }

        public LaptopAd()
        { }

        public LaptopAd(CreateVM vm) : base(vm)
        {
            ScreenSize = vm.ScreenSize;
            Processor = vm.Processor;
            RAM = vm.RAM;
            Videocard = vm.Videocard;
            DriveVolume = vm.DriveVolume;
        }

        public override Dictionary<string, string> GetAdditionalProperties()
        {
            Dictionary<string, string> additionalProperties = new Dictionary<string, string>();

            additionalProperties.Add("Screen size", ScreenSize.ToString());
            additionalProperties.Add("Processor", Processor);
            additionalProperties.Add("RAM", RAM.ToString());
            additionalProperties.Add("Videocard", Videocard);
            additionalProperties.Add("Drive volume", DriveVolume.ToString());

            return additionalProperties;
        }
    }
}
