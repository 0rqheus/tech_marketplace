using Marketplace.Models.ReportProcess.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models.ReportProcess.Commands
{
    public class IllegalHandler : ReportHandler
    {
        private IllegalReceiver _receiver;
        private Report _report;
        private RepoFacade _repo;

        public IllegalHandler(IllegalReceiver receiver, Report report, RepoFacade repo)
        {
            _receiver = receiver;
            _report = report;
            _repo = repo;
        }

        public override void Handle()
        {
            if (_report.Type == ReportTypes.Illegal)
            {
                _receiver.ProcessReport(_report, _repo);
            }
            Successor.Handle();
        }
    }
}
