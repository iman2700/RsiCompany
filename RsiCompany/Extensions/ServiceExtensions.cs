using Contracts;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;
using LoggerService;

namespace RsiCompany.Extensions
{
    public static class ServiceExtensions
    {
        // Configures the repository manager service in the dependency injection container
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        // Configures the logger service in the dependency injection container
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        // Configures the SQL context for the repository in the dependency injection container
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        // Configures Cross-Origin Resource Sharing (CORS) for the application
        public static void ConfigureCors(this IServiceCollection services) =>
          services.AddCors(options =>
          {
             options.AddPolicy("CorsPolicy", builder =>
             builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
          });

        // Configures integration with Internet Information Services (IIS)
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        // Configures the service manager service in the dependency injection container
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();


        // Extension method to add a custom CSV formatter to the MVC builder.
        // It adds a CsvOutputFormatter instance to the MvcOptions' OutputFormatters collection.

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
        }
    }



}
