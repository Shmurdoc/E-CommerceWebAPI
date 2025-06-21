using eCommerceWebAPI.Configurations;

namespace eCommerceWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        //Cors Policy
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        //IIS Integration
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                //IIS options here
            });

        //AutoMapper
        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(AutoMapperInitializer).Assembly);
    }
}