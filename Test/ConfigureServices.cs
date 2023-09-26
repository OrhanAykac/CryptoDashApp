using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiseX.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseX.Test;
public static class ConfigureServices
{
    public static void Services()
    {
        IConfiguration configuration=new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        RegisterServices.ConfigureServices(new ServiceCollection(), configuration);
    }

}
