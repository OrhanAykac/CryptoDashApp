using Entities.Abstract;

namespace Entities.Concrete;

[Table("CryptoHistories")]
public class CryptoHistory : BaseEntity, IEntity
{
    public string CryptoCurrencyCode { get; set; } = "BTC";
    public string BaseCurrencyCode { get; set; }
    public decimal Rate { get; set; }
}
