using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestCors.API.Configure
{
    public static class ApiDependencies
    {
        public static IServiceCollection RegisterApiDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.0.1", new Info { Title = "Camps API", Version = "0.0.1-alpha" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    In = "header",
                    Description = "Please insert JWT token with Bearer prefix",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();
                string filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "testCors_api.xml");
                c.IncludeXmlComments(filePath);
            });

            return services;
        }
    }
}
