using Entities.Dto;

namespace RiseX.Entities.Dto;
public class CryptoHistoryGroupDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public object DailyHistories { get; set; }
    public object WeeklyHistories { get; set; }
    public object MonthlyHistories { get; set; }
}
