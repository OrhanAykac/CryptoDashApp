using Business.Abstract;
using Shared.Utilities.Services;

namespace WebUI.BackgroundServices;

public class CryptoWorker : BackgroundService
{
    private readonly ICryptoService _cryptoService;
    private readonly ILogger<CryptoWorker> _logger;
    public CryptoWorker(ILogger<CryptoWorker> logger)
    {
        _cryptoService = ServiceTool.ServiceProvider.GetService<ICryptoService>();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //It will wait until nex tick and then execute even at first run.
        PeriodicTimer periodicTimer = new(TimeSpan.FromMinutes(1));

        while (stoppingToken.IsCancellationRequested == false && await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                _logger.LogInformation("Crypto background service started.");
                await DoProcess();
                _logger.LogInformation("Crypto background service finished.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{message}", ex.Message);
            }
        }
    }

    private async Task DoProcess()
    {
        var response = await _cryptoService.GetCryptoRateFromApiAsync();

        if (response?.Success == true)
            _logger.LogInformation("{message}", response.Message);
        else
            _logger.LogError("{message}", response.Message);
    }
}
