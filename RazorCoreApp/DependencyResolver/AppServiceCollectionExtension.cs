using Microsoft.Extensions.DependencyInjection;

namespace PS1_MIC_090_Core.DependencyResolver
{
    public static class AppServiceCollectionExtension
    {

        
        public static IServiceCollection AddDI<T>(this IServiceCollection services)
        {
            //return services.AddMediatR(typeof(T));

            return services;
        }
    }
}
