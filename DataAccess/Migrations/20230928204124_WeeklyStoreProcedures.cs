using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class WeeklyStoreProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var GetCryptoAssetWeeklyHistory = 
                @"CREATE PROCEDURE [dbo].[qry_GetCryptoAssetWeeklyHistory]
            @StartDate date,
            @EndDate date
            AS
            BEGIN

	            SET NOCOUNT ON;

		                SELECT 
			            BaseCurrencyCode
			            ,CryptoCurrencyCode
			            ,DATEPART(WEEK, CreatedAt) [Week]
			            ,AVG(Rate) as Rate
			            ,MAX(CreatedAt) as CreatedAt
			            FROM CryptoHistories WITH(NOLOCK)
		            WHERE CAST(CreatedAt as date) BETWEEN @StartDate AND @EndDate
		            GROUP BY 
			            BaseCurrencyCode,CryptoCurrencyCode,DATEPART(WEEK, CreatedAt)
            END";

            migrationBuilder.Sql(GetCryptoAssetWeeklyHistory);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
