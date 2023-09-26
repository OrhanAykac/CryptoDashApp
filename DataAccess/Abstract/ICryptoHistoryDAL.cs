using RiseX.Entities.Concrete;
using RiseX.Entities.Dto;

namespace RiseX.DataAccess.Abstract;
public interface ICryptoHistoryDAL
{
    Task<int> InsertAsync(CryptoHistory model);
    Task<IEnumerable<DailyCryptoHistoryDto>> GetDailyCryptoAssetHistoriesAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<WeeklyCryptoHistoryDto>> GetWeeklyCryptoAssetHistoriesAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<MonthlyCryptoHistoryDto>> GetMonthlyCryptoAssetHistoriesAsync(DateTime startDate, DateTime endDate);
}