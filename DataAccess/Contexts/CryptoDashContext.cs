using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts;
public class CryptoDashContext : DbContext
{
    public CryptoDashContext(DbContextOptions<CryptoDashContext> contextOptions) : base(contextOptions)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<CryptoHistory> CryptoHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CryptoHistory>().HasData(new CryptoHistory()
        {
            Id = 1,
            BaseCurrencyCode = "USD",
            CryptoCurrencyCode = "BTC",
            CreatedAt = DateTime.Now,
            CreatedBy = 0,
            IsActive = true,
            IsDeleted = false,
            Rate = 15000,
            UpdatedAt = DateTime.Now,
            UpdatedBy = 0
        });

        modelBuilder.Entity<CryptoHistory>().HasData(new CryptoHistory()
        {
            Id = 2,
            BaseCurrencyCode = "USD",
            CryptoCurrencyCode = "BTC",
            CreatedAt = DateTime.Now.AddDays(-1),
            CreatedBy = 0,
            IsActive = true,
            IsDeleted = false,
            Rate = 10000,
            UpdatedAt = DateTime.Now.AddDays(-1),
            UpdatedBy = 0
        });

        modelBuilder.Entity<CryptoHistory>().HasData(new CryptoHistory()
        {
            Id = 3,
            BaseCurrencyCode = "USD",
            CryptoCurrencyCode = "BTC",
            CreatedAt = DateTime.Now.AddDays(-7),
            CreatedBy = 0,
            IsActive = true,
            IsDeleted = false,
            Rate = 25000,
            UpdatedAt = DateTime.Now.AddDays(-7),
            UpdatedBy = 0
        });

        modelBuilder.Entity<CryptoHistory>().HasData(new CryptoHistory()
        {
            Id = 4,
            BaseCurrencyCode = "USD",
            CryptoCurrencyCode = "BTC",
            CreatedAt = DateTime.Now.AddMonths(-1),
            CreatedBy = 0,
            IsActive = true,
            IsDeleted = false,
            Rate = 5000,
            UpdatedAt = DateTime.Now.AddMonths(-1),
            UpdatedBy = 0
        });

        modelBuilder.Entity<CryptoHistory>().HasData(new CryptoHistory()
        {
            Id = 5,
            BaseCurrencyCode = "USD",
            CryptoCurrencyCode = "BTC",
            CreatedAt = DateTime.Now.AddMonths(-2),
            CreatedBy = 0,
            IsActive = true,
            IsDeleted = false,
            Rate = 7500,
            UpdatedAt = DateTime.Now.AddMonths(-2),
            UpdatedBy = 0
        });

    }

}
