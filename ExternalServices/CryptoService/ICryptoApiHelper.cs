using ExternalServices.Models;

namespace ExternalServices.CryptoService;
public interface ICryptoApiHelper
{
    Task<CryptoApiResponseModel> GetCryptoAssetRateAsync(string baseCurrencyCode = "USD", string cryptoCurrencyCode = "BTC");
}
