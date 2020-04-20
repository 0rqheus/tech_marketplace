using System.Collections.Generic;

namespace Marketplace.Models.AdTypes
{
    public class SmartphoneAd : Ad
    {
        public double ScreenSize { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; }
        public int BatteryCapacity { get; set; }

        public SmartphoneAd()
        { }

        public SmartphoneAd(CreateViewModel vm) : base(vm)
        {
            ScreenSize = vm.ScreenSize;
            Processor = vm.Processor;
            RAM = vm.RAM;
            BatteryCapacity = vm.BatteryCapacity;
        }

        public override Dictionary<string, string> GetAdditionalProperties()
        {
            Dictionary<string, string> additionalProperties = new Dictionary<string, string>();

            additionalProperties.Add("Screen size", ScreenSize.ToString());
            additionalProperties.Add("Processor", Processor);
            additionalProperties.Add("RAM", RAM.ToString());
            additionalProperties.Add("Battery capacity", BatteryCapacity.ToString());

            return additionalProperties;
        }
    }
}
