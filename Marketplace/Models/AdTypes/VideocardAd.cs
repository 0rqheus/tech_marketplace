using System.Collections.Generic;

namespace Marketplace.Models.AdTypes
{
    public class VideocardAd : Ad
    {
        public int MemorySize { get; set; }
        public string MemoryType { get; set; }
        public int MemoryBusWidth { get; set; }

        public VideocardAd()
        { }

        public VideocardAd(CreateVM vm) : base(vm)
        {
            MemorySize = vm.MemorySize;
            MemoryType = vm.MemoryType;
            MemoryBusWidth = vm.MemoryBusWidth;
        }

        public override Dictionary<string, string> GetAdditionalProperties()
        {
            Dictionary<string, string> additionalProperties = new Dictionary<string, string>();

            additionalProperties.Add("Memory size", MemorySize.ToString());
            additionalProperties.Add("Memory type", MemoryType);
            additionalProperties.Add("Memory busWidth", MemoryBusWidth.ToString());

            return additionalProperties;
        }
    }
}
