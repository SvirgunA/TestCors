using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCors.Common.Settings;
using TestCors.Data.EF;

namespace TestCors.API.Configure
{
    public static class InitialSeeding
    {
        public static void SeedDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TestCorsContext>();
                var autoMigrationDatabase =
                    scope.ServiceProvider.GetService<IOptions<DatabaseSettings>>().Value.AUTO_MIGRATIONS;

                if (autoMigrationDatabase)
                {
                    context.Database.Migrate();
                }
                context.SeedPhonesData();
            }
        }
    }
}
