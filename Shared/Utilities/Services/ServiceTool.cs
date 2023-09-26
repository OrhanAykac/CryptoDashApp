using Microsoft.Extensions.DependencyInjection;

namespace Shared.Utilities.Services;
public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; set; }

    public static void Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
    }
}
