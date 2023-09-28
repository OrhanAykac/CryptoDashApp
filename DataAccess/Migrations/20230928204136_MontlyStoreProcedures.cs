using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MontlyStoreProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var GetCryptoAssetMonthlyHistory = 
                @"CREATE PROCEDURE [dbo].[qry_GetCryptoAssetMonthlyHistory]
        @StartDate date,
        @EndDate date
        AS
        BEGIN

	        SET NOCOUNT ON;

		            SELECT 
			        BaseCurrencyCode
			        ,CryptoCurrencyCode
			        ,DATEPART(MONTH, CreatedAt) [Month]
			        ,AVG(Rate) as Rate
			        ,MAX(CreatedAt) as CreatedAt
			        FROM CryptoHistories WITH(NOLOCK)
		        WHERE CAST(CreatedAt as date) BETWEEN @StartDate AND @EndDate
		        GROUP BY 
			        BaseCurrencyCode,CryptoCurrencyCode,DATEPART(MONTH, CreatedAt)
        END";

            migrationBuilder.Sql(GetCryptoAssetMonthlyHistory);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
