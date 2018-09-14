using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TestCors.Data.UoW;
using TestCors.Data.UoW.Implementation;

namespace TestCors.Data.IoC
{
    public static class DataAccessDependencies
    {
        public static IServiceCollection RegisterDataAccessDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
