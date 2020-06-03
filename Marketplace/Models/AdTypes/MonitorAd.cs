using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Marketplace.Models.AdTypes
{
    public class MonitorAd : Ad
    {
        [Column(TypeName = "decimal(18, 2)")]
        public double ScreenSize { get; set; }
        public string Resolution { get; set; }
        public string MatrixType { get; set; }
        public int RefreshRate { get; set; }
        public string AspectRatio { get; set; }

        public MonitorAd()
        { }

        public MonitorAd(CreateVM vm) : base(vm)
        {
            ScreenSize = vm.ScreenSize;
            Resolution = vm.Resolution;
            MatrixType = vm.MatrixType;
            RefreshRate = vm.RefreshRate;
            AspectRatio = vm.AspectRatio;
        }

        public override Dictionary<string, string> GetAdditionalProperties()
        {
            Dictionary<string, string> additionalProperties = new Dictionary<string, string>();

            additionalProperties.Add("Screen size", ScreenSize.ToString());
            additionalProperties.Add("Resolution", Resolution);
            additionalProperties.Add("Matrix type", MatrixType);
            additionalProperties.Add("Refresh rate", RefreshRate.ToString());
            additionalProperties.Add("Aspect ratio", AspectRatio);

            return additionalProperties;
        }
    }

}
