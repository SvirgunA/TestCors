using Microsoft.Extensions.DependencyInjection;
using TestCors.Common.Services;

namespace TestCors.Common.IoC
{
    public static class CommonDependencies
    {
        public static IServiceCollection RegisterCommonDependencies(this IServiceCollection services)
        {
            return services;
        }

        public static IDependencyResolver RegisterDependencyResolver(this IServiceCollection services)
        {
            var dependencyResolver =
                new DependencyResolver().RegisterServiceProvider(services.BuildServiceProvider);

            services.AddSingleton(typeof(IDependencyResolver), dependencyResolver);

            return dependencyResolver;
        }
    }
}
