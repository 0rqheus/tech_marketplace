using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public abstract class AdDecorator : Component
    {
        protected Ad _ad;

        public readonly string Title;
        public readonly string Description;
        public readonly decimal Price;

        public AdDecorator(Ad ad)
        {
            _ad = ad;
            Title = ad.Title;
            Description = ad.Description;
            Price = ad.Price;
        }

        public int GetOwnerId()
        {
            return _ad.UserId;
        }

        public override string GetImgSrc()
        {
            if (_ad != null) return _ad.GetImgSrc();

            return "";
        }

        public string GetAdURLQuery()
        {
            return $"?id={_ad.Id}&category={_ad.Category}";
        }
    }
}
