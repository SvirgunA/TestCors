using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCors.Common.Constants;
using TestCors.Common.Settings;

namespace TestCors.API.Configure
{
    public static class Options
    {
        internal static void RegisterApplicationConfiguration(this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.AddOptions();

            services.Configure<BaseSettings>(configuration);

            //services.Configure<UrlsSettings>(
            //    configuration.GetSection(ConfigurationFilesKeys.ApplicationSettings.URLS_SETTINGS_SECTION_KEY));

            services.Configure<AuthSettings>(
                configuration.GetSection(ConfigurationFilesKeys.ApplicationSettings.AUTH_SETTINGS_SECTION_KEY));

            //services.Configure<MailSettings>(
            //    configuration.GetSection(ConfigurationFilesKeys.ApplicationSettings.MAIL_SETTINGS_SECTION_KEY));

            services.Configure<DatabaseSettings>(
                configuration.GetSection(ConfigurationFilesKeys.ConnectionSettings.DATABASE_SECTION_NAME));

            //services.Configure<SmsSettings>(
            //    configuration.GetSection(ConfigurationFilesKeys.ApplicationSettings.SMS_SETTINGS_SECTION_KEY));

            //services.Configure<S3Settings>(
            //    configuration.GetSection(ConfigurationFilesKeys.ApplicationSettings.S3_SETTINGS_SECTION_KEY));
        }
    }
}
