using System.Text.Json.Serialization;

namespace RiseX.ExternalServices.CryptoService.CoinAPI.Models;
public class CoinApiCrytoAssetResponseModel
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }
    [JsonPropertyName("asset_id_base")]
    public string AssetIdBase { get; set; }

    [JsonPropertyName("asset_id_quote")]
    public string AssetIdQuote { get; set; }
    [JsonPropertyName("rate")]
    public decimal Rate { get; set; }
}
