namespace BlazorApp.Client.Services
{
    public class TransientDisposable : IDisposable
    {
        public void Dispose() => throw new NotImplementedException();
    }
}
