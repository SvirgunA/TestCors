using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using TestCors.API.Configure;
using TestCors.Common.Constants;
using TestCors.Common.IoC;
using TestCors.Common.Logging;
using TestCors.Common.Settings;
using TestCors.Data.EF;
using TestCors.Data.IoC;
using TestCors.Services.IoC;

namespace TestCors.API
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ApiConstants.FilePath.APPLICATION_SETTINGS_FILE_NAME, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {ConfigurationFilesKeys.ApplicationSettings.APPLICATION_ROOT_KEY, Directory.GetCurrentDirectory() }
                });

            builder.AddJsonFile(ApiConstants.FilePath.CONNECTED_SERVICES_SETTINGS_FILE_NAME, optional: false, reloadOnChange: true);

            _configuration = builder.Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            Formatting formatting;
#if DEBUG
            formatting = Formatting.Indented;
#else
            formatting = Formatting.None;
#endif
            services
                .AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                    options.SerializerSettings.Formatting = formatting;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new JsonStringTrimConverter());
                });

            services
                .RegisterCommonDependencies()
                .RegisterDataAccessDependencies()
                .RegisterServiceDependencies()
                .RegisterApiDependencies()
                .RegisterApplicationConfiguration(_configuration);

            services.AddDbContext<TestCorsContext>();
            var dependencyResolver = services.RegisterDependencyResolver();
            services.AddJwtAuth(dependencyResolver.GetService<IOptions<AuthSettings>>().Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<DatabaseSettings> settings)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddDbLogging(settings);

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v0.0.1/swagger.json", "Camps API"); });

            app.UseMvc();
        }
    }
}
