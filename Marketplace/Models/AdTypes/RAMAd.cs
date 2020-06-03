using System.Collections.Generic;

namespace Marketplace.Models.AdTypes
{
    public class RAMAd : Ad
    {
        public int TotalCapacity { get; set; }
        public int ModulesAmount { get; set; }
        public string MemoryType { get; set; }

        public RAMAd()
        { }

        public RAMAd(CreateVM vm) : base(vm)
        {
            TotalCapacity = vm.TotalCapacity;
            ModulesAmount = vm.ModulesAmount;
            MemoryType = vm.MemoryType;
        }

        public override Dictionary<string, string> GetAdditionalProperties()
        {
            Dictionary<string, string> additionalProperties = new Dictionary<string, string>();

            additionalProperties.Add("Total capacity", TotalCapacity.ToString());
            additionalProperties.Add("Modules amount", ModulesAmount.ToString());
            additionalProperties.Add("Memory type", MemoryType);

            return additionalProperties;
        }
    }
}
