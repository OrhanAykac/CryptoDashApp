namespace RiseX.Entities.Dto;
public class DailyCryptoHistoryDto
{
    public string BaseCurrencyCode { get; set; }
    public string CryptoCurrencyCode { get; set; }
    public int Day { get; set; }
    public decimal Rate { get; set; }
    public DateTime CreatedAt { get; set; }

}
