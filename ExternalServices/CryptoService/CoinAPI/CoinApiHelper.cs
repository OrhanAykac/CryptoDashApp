using ExternalServices.CryptoService.CoinAPI.Models;
using ExternalServices.Models;
using System.Net.Http.Json;

namespace ExternalServices.CryptoService.CoinAPI;
public class CoinApiHelper : ICryptoApiHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string _httpClientName = "CoinAPI";
    public CoinApiHelper(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<CryptoApiResponseModel> GetCryptoAssetRateAsync(string baseCurrencyCode = "USD", string cryptoCurrencyCode = "BTC")
    {
        string apiPath = $"v1/exchangerate/{cryptoCurrencyCode}/{baseCurrencyCode}";
        var http = _httpClientFactory.CreateClient(_httpClientName);

        var response = await http.GetFromJsonAsync<CoinApiCrytoAssetResponseModel>(apiPath);

        if (response is null)
            return default;

        return new CryptoApiResponseModel()
        {
            CryptoCurrencyCode = response.AssetIdBase,
            BaseCurrencyCode = response.AssetIdQuote,
            Rate = response.Rate,
        };
    }
}
