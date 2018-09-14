using Microsoft.Extensions.DependencyInjection;
using TestCors.Services.Core;
using TestCors.Services.Core.Impl;

namespace TestCors.Services.IoC
{
    public static class ServiceDependencies
    {
        public static IServiceCollection RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IPhoneService, PhoneService>();
            services.AddTransient<IAuthService, AuthService>();
            return services;
        }
    }
}
