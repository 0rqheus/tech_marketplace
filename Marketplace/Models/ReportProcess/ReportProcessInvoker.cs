using Marketplace.Models.ReportProcess.Handlers;

namespace Marketplace.Models.ReportProcess
{
    public class ReportProcessInvoker
    {
        private Handler _firstHandler;

        public void SetFirtHadler(Handler handler)
        {
            _firstHandler = handler;
        }

        public void StartReportProcessing(Report report, RepoFacade repo)
        {
            _firstHandler.Handle(report, repo);
        }
    }
}
