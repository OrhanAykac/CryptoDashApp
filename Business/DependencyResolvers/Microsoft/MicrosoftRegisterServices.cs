using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using ExternalServices.CryptoService;
using ExternalServices.CryptoService.CoinAPI;
using Shared.Utilities.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Data;

namespace Business.DependencyResolvers.Microsoft;

public static class MicrosoftRegisterServices
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IDbConnection>(sql => new SqlConnection(configuration.GetConnectionString("DefaultConnStr")));
        services.AddHttpClient("CoinAPI", config =>
        {
            config.BaseAddress = new Uri(configuration["CoinApi:ApiUrl"]);
            config.DefaultRequestHeaders.Add("X-CoinAPI-Key", configuration["CoinApi:ApiKey"]);
        }).AddPolicyHandler(GetRetryPolicy());

        services.AddHttpContextAccessor();

        ConfigureIoC(services);
        LoggerConfiguration();
        ServiceTool.Create(services);
    }

    private static void LoggerConfiguration()
    {
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Console(theme: SystemConsoleTheme.Colored
            , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}");

        Log.Logger = loggerConfig.CreateLogger();
    }

    private static void ConfigureIoC(IServiceCollection services)
    {
        //DataAccess
        services.AddScoped<IUserDAL, UserDAL>();
        services.AddScoped<ICryptoHistoryDAL, CryptoHistoryDAL>();

        //Business
        services.AddScoped<ICryptoService, CryptoManager>();
        services.AddScoped<IAuthService, AuthManager>();

        //ExternalServices
        services.AddScoped<ICryptoApiHelper, CoinApiHelper>();
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound || msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests || msg.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
