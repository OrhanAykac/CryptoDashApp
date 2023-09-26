using Microsoft.Extensions.DependencyInjection;
using RiseX.Business.Abstract;
using RiseX.Entities.Dto;
using RiseX.Shared.Results;
using RiseX.Shared.Utilities.Services;

namespace RiseX.Test;
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
