using RiseX.ExternalServices.Models;

namespace RiseX.ExternalServices.CryptoService;
public interface ICryptoApiHelper
{
    Task<CryptoApiResponseModel> GetCryptoAssetRateAsync(string baseCurrencyCode = "USD", string cryptoCurrencyCode = "BTC");
}
