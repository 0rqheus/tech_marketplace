using Marketplace.Models.ReportProcess.Receivers;

namespace Marketplace.Models.ReportProcess.Commands
{
    public class DefaultHandler : ReportHandler
    {
        private DefaultReceiver _receiver;
        private Report _report;
        private RepoFacade _repo;

        public DefaultHandler(DefaultReceiver receiver, Report report, RepoFacade repo)
        {
            _receiver = receiver;
            _report = report;
            _repo = repo;
        }

        public override void Handle()
        {
            _receiver.ProcessReport(_report, _repo);
        }
    }
}
