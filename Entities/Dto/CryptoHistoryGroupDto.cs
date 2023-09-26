namespace RiseX.Entities.Dto;
public class CryptoHistoryGroupDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IEnumerable<DailyCryptoHistoryDto> DailyHistories { get; set; }
    public IEnumerable<WeeklyCryptoHistoryDto> WeeklyHistories { get; set; }
    public IEnumerable<MonthlyCryptoHistoryDto> MonthlyHistories { get; set; }
}
