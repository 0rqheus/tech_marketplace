using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.ReportProcess
{
    public class ReportProcessInvoker
    {
        private ReportHandler _firstHandler;

        public void SetFirtHadler(ReportHandler handler)
        {
            _firstHandler = handler;
        }

        public void StartReportProcessing()
        {
            _firstHandler.Handle();
        }
    }
}
