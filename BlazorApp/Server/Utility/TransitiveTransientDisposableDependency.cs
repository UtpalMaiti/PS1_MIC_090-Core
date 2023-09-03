namespace BlazorApp.Server.Utility
{
    public class TransitiveTransientDisposableDependency
    : ITransitiveTransientDisposableDependency, IDisposable
    {
        public void Dispose() { }
    }

    public interface ITransitiveTransientDisposableDependency
    {
    }

    public class TransientDependency
    {
        private readonly ITransitiveTransientDisposableDependency
            transitiveTransientDisposableDependency;

        public TransientDependency(ITransitiveTransientDisposableDependency
            transitiveTransientDisposableDependency)
        {
            this.transitiveTransientDisposableDependency =
                transitiveTransientDisposableDependency;
        }
    }
}
