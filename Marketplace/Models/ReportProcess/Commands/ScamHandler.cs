using Marketplace.Models.ReportProcess.Receivers;

namespace Marketplace.Models.ReportProcess.Commands
{
    public class ScamHandler : ReportHandler
    {
        private ScamReceiver _receiver;
        private Report _report;
        private RepoFacade _repo;

        public ScamHandler(ScamReceiver receiver, Report report, RepoFacade repo)
        {
            _receiver = receiver;
            _report = report;
            _repo = repo;
        }

        public override void Handle()
        {
            if (_report.Type == ReportTypes.Scam)
            {
                _receiver.ProcessReport(_report, _repo);
            }
            Successor.Handle();
        }
    }
}
