namespace Marketplace.Models.ReportProcess.Handlers
{
    public abstract class Handler
    {
        public Handler Successor { get; set; }

        public abstract void Handle(Report report, RepoFacade repo);
    }
}
