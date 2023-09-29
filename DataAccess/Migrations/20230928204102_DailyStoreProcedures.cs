using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DailyStoreProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var GetCryptoAssetDailyHistory = 
                @"CREATE PROCEDURE [dbo].[qry_GetCryptoAssetDailyHistory]
            @StartDate date,
            @EndDate date
            AS
            BEGIN

	            SET NOCOUNT ON;

                SELECT 
		            BaseCurrencyCode
		            ,CryptoCurrencyCode
		            ,AVG(Rate) as Rate
		            ,DAY(CreatedAt) as [Day]
		            ,MAX(CreatedAt) as CreateAt
	            FROM CryptoHistories WITH(NOLOCK)
	            WHERE 
		            CAST(CreatedAt as date) BETWEEN @StartDate AND @EndDate
	            GROUP BY
		            BaseCurrencyCode,CryptoCurrencyCode,DAY(CreatedAt) 
            END";

            migrationBuilder.Sql(GetCryptoAssetDailyHistory);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
