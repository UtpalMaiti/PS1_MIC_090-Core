using AutoMapper.Internal.Mappers;

using Microsoft.EntityFrameworkCore.ChangeTracking;

using Unity;
using Unity.Resolution;

namespace PS1_MIC_090_Core.DependencyResolver
{
    public class UnityDependencyResolver : IServiceProvider
    {
        private readonly IUnityContainer container;
        private readonly IServiceProvider provider;

        public UnityDependencyResolver(IUnityContainer _container, IServiceProvider _provider)
        {
            container = _container;
            provider = _provider;
        }

        public object? GetService(Type serviceType)
        {
            ICustomFileLogger logger = (ICustomFileLogger)provider.GetService(typeof(ICustomFileLogger));

            if (logger == null)
            {

            }
            return (object?)null;
        }
    }

    public interface ICustomFileLogger
    {
    }
    public class CustomFileLogger: ICustomFileLogger
    {
    }
    public class CustomFileLoggerX : ICustomFileLogger
    {
    }

    public class UnityDependencyRegister
    {
        IUnityContainer container = new UnityContainer();

    }
}

