namespace Entities.Dto;

public class MonthlyCryptoHistoryDto
{
    public string BaseCurrencyCode { get; set; }
    public string CryptoCurrencyCode { get; set; }
    public int Month { get; set; }
    public decimal Rate { get; set; }
    public DateTime CreatedAt { get; set; }

}
