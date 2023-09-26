namespace RiseX.ExternalServices.Models;
public class CryptoApiResponseModel
{
    public string CryptoCurrencyCode { get; set; }
    public string BaseCurrencyCode { get; set; }//USD,USDT
    public decimal Rate { get; set; }
}
