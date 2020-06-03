using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Marketplace.Models.AdTypes
{
    public class ProcessorAd : Ad
    {
        public int CoresAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public double ClockSpeed { get; set; }

        public ProcessorAd()
        { }

        public ProcessorAd(CreateVM vm) : base(vm)
        {
            CoresAmount = vm.CoresAmount;
            ClockSpeed = vm.ClockSpeed;
        }

        public override Dictionary<string, string> GetAdditionalProperties()
        {
            Dictionary<string, string> additionalProperties = new Dictionary<string, string>();

            additionalProperties.Add("Cores amount", CoresAmount.ToString());
            additionalProperties.Add("Clock speed", ClockSpeed.ToString());

            return additionalProperties;
        }
    }
}
