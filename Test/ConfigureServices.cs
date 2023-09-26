using Business.DependencyResolvers.Microsoft;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test;
public static class ConfigureServices
{
    public static void Services()
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        MicrosoftRegisterServices.ConfigureServices(new ServiceCollection(), configuration);
    }

}
