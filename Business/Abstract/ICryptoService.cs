using RiseX.Entities.Dto;
using RiseX.Shared.Results;

namespace RiseX.Business.Abstract;
public interface ICryptoService
{
    Task<BaseResponse> GetCryptoRateFromApiAsync();
    Task<BaseDataResponse<CryptoHistoryGroupDto>> GetCryptoAssetHistoryAsync(DateTime startDate, DateTime endDate);
}
