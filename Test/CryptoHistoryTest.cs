using Microsoft.Extensions.DependencyInjection;
using Business.Abstract;
using Entities.Dto;
using Shared.Results;
using Shared.Utilities.Services;

namespace Test;
public class CryptoHistoryTest
{
    private readonly ICryptoService _cryptoService;

    public CryptoHistoryTest()
    {
        ConfigureServices.Services();
        _cryptoService = ServiceTool.ServiceProvider.GetRequiredService<ICryptoService>();
    }

    [Fact]
    public async Task GetCryptoRateFromApiAsyncTest()
    {
        var result = await _cryptoService.GetCryptoRateFromApiAsync();

        Assert.IsType<BaseResponse>(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetCryptoAssetHistoryTest()
    {
        var result = await _cryptoService.GetCryptoAssetHistoryAsync(DateTime.Now.AddDays(-7), DateTime.Now);

        Assert.IsType<BaseDataResponse<CryptoHistoryGroupDto>>(result);
        Assert.True(result.Success);
    }
}
