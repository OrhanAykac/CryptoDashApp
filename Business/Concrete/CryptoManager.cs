using DataAccess.Abstract;
using Entities.Dto;
using ExternalServices.CryptoService;
using Microsoft.Extensions.Logging;
using Shared.Results;
using System.Data.SqlTypes;

namespace Business.Concrete;
public class CryptoManager : ICryptoService
{
    private readonly ICryptoApiHelper _cryptoApiHelper;
    private readonly ILogger<CryptoManager> _logger;
    private readonly ICryptoHistoryDAL _cryptoHistoryDAL;
    public CryptoManager(ICryptoApiHelper cryptoApiHelper, ILogger<CryptoManager> logger, ICryptoHistoryDAL cryptoHistoryDAL)
    {
        _cryptoApiHelper = cryptoApiHelper;
        _logger = logger;
        _cryptoHistoryDAL = cryptoHistoryDAL;
    }


    public async Task<BaseResponse> GetCryptoRateFromApiAsync()
    {
        try
        {
            var result = await _cryptoApiHelper.GetCryptoAssetRateAsync();
            if (result is not null)
            {
                await _cryptoHistoryDAL.InsertAsync(new Entities.Concrete.CryptoHistory()
                {
                    BaseCurrencyCode = result.BaseCurrencyCode,
                    CryptoCurrencyCode = result.CryptoCurrencyCode,
                    Rate = result.Rate,
                    CreatedBy = 0,//System
                });
            }

            return new BaseResponse(true, "Crypto data succesfuly added.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse(false, "An error occoured when getting data");
        }
    }


    public async Task<BaseDataResponse<CryptoHistoryGroupDto>> GetCryptoAssetHistoryAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            //startDate sql'in min date'inden küçükse değer ataması yapılmamıştır. 7 gün öncesini atıyoruz.
            if (startDate < (DateTime)SqlDateTime.MinValue)
                startDate = DateTime.Now.AddDays(-7);

            //endDate bugünden büyükse yada değer atanmamışsa bugünü atıyoruz.
            if (endDate.Date > DateTime.UtcNow.Date || endDate < (DateTime)SqlDateTime.MinValue)
                endDate = DateTime.UtcNow.Date;


            var dailyHistories = await _cryptoHistoryDAL.GetDailyCryptoAssetHistoriesAsync(startDate, endDate);
            var weeklyHistories = await _cryptoHistoryDAL.GetWeeklyCryptoAssetHistoriesAsync(startDate, endDate);
            var monthlyHistories = await _cryptoHistoryDAL.GetMonthlyCryptoAssetHistoriesAsync(startDate, endDate);

            var cryptoHistoryGroup = new CryptoHistoryGroupDto
            {
                StartDate = startDate,
                EndDate = endDate,
                DailyHistories = dailyHistories.OrderBy(o => o.CreatedAt).Select(x => new[] { x.Day, x.Rate }).ToArray(),
                WeeklyHistories = weeklyHistories.OrderBy(o => o.CreatedAt).Select(x => new[] { x.Week, x.Rate }).ToArray(),
                MonthlyHistories = monthlyHistories.OrderBy(o => o.CreatedAt).Select(x => new[] { x.Month, x.Rate }).ToArray(),
            };

            return new BaseDataResponse<CryptoHistoryGroupDto>(cryptoHistoryGroup, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseDataResponse<CryptoHistoryGroupDto>(default, false, "İşlenemeyen bir hata oluştu.");
        }
    }

}
