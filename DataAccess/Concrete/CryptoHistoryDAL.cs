using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System.Data;

namespace DataAccess.Concrete;
public class CryptoHistoryDAL : ICryptoHistoryDAL
{
    private readonly IDbConnection _dbConnection;

    public CryptoHistoryDAL(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> InsertAsync(CryptoHistory model)
    {
        var addedEntityId = await _dbConnection.InsertAsync(model);
        return addedEntityId;
    }

    public async Task<IEnumerable<DailyCryptoHistoryDto>> GetDailyCryptoAssetHistoriesAsync(DateTime startDate, DateTime endDate)
    {
        string sqlQuery = "dbo.qry_GetCryptoAssetDailyHistory";

        var p = new
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await _dbConnection
            .QueryAsync<DailyCryptoHistoryDto>(
            sql: sqlQuery,
            param: p,
            commandType: CommandType.StoredProcedure);

        return result;
    }
    public async Task<IEnumerable<WeeklyCryptoHistoryDto>> GetWeeklyCryptoAssetHistoriesAsync(DateTime startDate, DateTime endDate)
    {
        string sqlQuery = "dbo.qry_GetCryptoAssetWeeklyHistory";

        var p = new
        {
            StartDate = startDate
            ,
            EndDate = endDate
        };

        var result = await _dbConnection
            .QueryAsync<WeeklyCryptoHistoryDto>(
            sql: sqlQuery,
            param: p,
            commandType: CommandType.StoredProcedure);

        return result;
    }
    public async Task<IEnumerable<MonthlyCryptoHistoryDto>> GetMonthlyCryptoAssetHistoriesAsync(DateTime startDate, DateTime endDate)
    {
        string sqlQuery = "dbo.qry_GetCryptoAssetMonthlyHistory";

        var p = new
        {
            StartDate = startDate
            ,
            EndDate = endDate
        };

        var result = await _dbConnection
            .QueryAsync<MonthlyCryptoHistoryDto>(
            sql: sqlQuery,
            param: p,
            commandType: CommandType.StoredProcedure);

        return result;
    }
}
