using Entities.Dto;
using Shared.Results;

namespace Business.Abstract;
public interface ICryptoService
{
    Task<BaseResponse> GetCryptoRateFromApiAsync();
    Task<BaseDataResponse<CryptoHistoryGroupDto>> GetCryptoAssetHistoryAsync(DateTime startDate, DateTime endDate);
}
