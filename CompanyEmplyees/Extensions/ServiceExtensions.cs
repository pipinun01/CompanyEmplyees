using Contracts;
using LoggerService;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;

namespace CompanyEmplyees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        //ИДЕНТИЧЕН
        //public static void ConfigureCors(this IServiceCollection services)
        //{
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("AllowAllOrigins",
        //            builder =>
        //            {
        //                builder.AllowAnyOrigin()
        //                       .AllowAnyMethod()
        //                       .AllowAnyHeader();
        //            });
        //    });
        //}


        public static void ConfigureIISIntagration(this IServiceCollection services) => services.Configure<IISOptions>(options =>
        {
            
        });

        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerManager>();
        public static void ConfigureRepositoryManager(this IServiceCollection services)=>services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        public static void ConfigureAutoMapper(this IServiceCollection services) => services.AddAutoMapper(typeof(MappingProfile));
    }
}
