namespace RiseX.Entities.Dto;

public class WeeklyCryptoHistoryDto
{
    public string BaseCurrencyCode { get; set; }
    public string CryptoCurrencyCode { get; set; }
    public int Week { get; set; }
    public decimal Rate { get; set; }
    public DateTime CreatedAt { get; set; }

}
