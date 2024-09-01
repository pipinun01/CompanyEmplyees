using CompanyEmplyees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NLog;
using Service;
namespace CompanyEmplyees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            LogManager.LoadConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config"));
            var df = Directory.GetCurrentDirectory();
            // Add services to the container.
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters()
            .AddCustomCSVFormatter().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntagration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureAutoMapper();    
            builder.Services.ConfigureRepositoryManager();
            //builder.Services.AddControllers().AddApplicationPart(typeof(CompanyEmplyees.Presentation.AssemblyReference).Assembly);

            var app = builder.Build();
            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);
            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment()) 
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllerRoute(name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllers();

            app.Run();
        }
    }
}
