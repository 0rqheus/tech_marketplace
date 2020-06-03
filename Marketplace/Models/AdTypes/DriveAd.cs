using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Marketplace.Models.AdTypes
{
    public class DriveAd : Ad
    {
        public int Capacity { get; set; }
        public int RPM { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public double FormFactor { get; set; }

        public DriveAd()
        { }

        public DriveAd(CreateVM vm) : base(vm)
        {
            Capacity = vm.Capacity;
            RPM = vm.RPM;
            FormFactor = vm.FormFactor;
        }

        public override Dictionary<string, string> GetAdditionalProperties()
        {
            Dictionary<string, string> additionalProperties = new Dictionary<string, string>();

            additionalProperties.Add("Capacity", Capacity.ToString());
            additionalProperties.Add("RPM", RPM.ToString());
            additionalProperties.Add("Form factor", FormFactor.ToString());

            return additionalProperties;
        }
    }
}
