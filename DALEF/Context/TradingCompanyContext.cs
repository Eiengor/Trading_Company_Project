using DALEF.Models;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Context
{
    public class TradingCompanyContext : TradingCompanyModel
    {
        private readonly string _connectionString;

        public TradingCompanyContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_connectionString);
    }
}
