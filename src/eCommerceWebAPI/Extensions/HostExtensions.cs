using Autofac;
using Autofac.Extensions.DependencyInjection;
using eCommerceWebAPI.Configurations;

namespace eCommerceWebAPI.Extensions
{
    public static class HostExtensions
    {
        // Sets up Autofac as the dependency injection container for the application host.
        public static void ConfigureAutofac(this IHostBuilder host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    // Register application dependencies via Autofac modules
                    builder.RegisterModule(new AutofacDependencyInjection());
                });
        }
    }
}
