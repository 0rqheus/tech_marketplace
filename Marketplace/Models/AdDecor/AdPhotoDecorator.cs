using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public class AdPhotoDecorator : AdDecorator
    {    
        public AdPhotoDecorator(Ad ad) : base(ad)
        { }

        public override string GetImgSrc()
        {
            return "data:image/jpg;base64," + Convert.ToBase64String(this._ad.Photo, 0, this._ad.Photo.Length);
        }
    }
}
