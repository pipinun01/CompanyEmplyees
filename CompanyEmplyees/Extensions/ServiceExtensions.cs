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
    }
}
