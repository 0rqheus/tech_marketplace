using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.ReportProcess
{
    public abstract class ReportHandler
    {
        public ReportHandler Successor { get; set; }

        public abstract void Handle();
    }
}
