using DAL.Concrete;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WpfApp.Core
{
    public class DBConnect
    {
        private string connectionString;
        public DBConnect()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            connectionString = configuration.GetConnectionString("TradingCompany") ?? "";
        }
        public UserInfoDal GetUsersDAL()
        {
            var users = new UserInfoDal(connectionString);
            return users;
        }
        public BankCardDal GetBankCardDAL()
        {
            var bankCards = new BankCardDal(connectionString);
            return bankCards;
        }
        public ProductDal GetProductDAL()
        {
            var products = new ProductDal(connectionString);
            return products;
        }
    }
}
