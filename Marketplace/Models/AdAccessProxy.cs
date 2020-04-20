using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public class AdAccessProxy
    {
        private AdDecorator _ad;

        public AdAccessProxy(AdDecorator ad)
        {
            _ad = ad;
        }

        public bool CheckOwner(User u)
        {
            if (_ad.GetOwnerId() == u.Id) return true;
            return false;
        }

        public bool CheckAdmin(User u)
        {
            if (u.RoleId == 1) return true;
            return false;
        }

        public string GetURLQuery()
        {
            return _ad.GetAdURLQuery();
        }
    }
}
