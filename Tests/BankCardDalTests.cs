using DAL.Concrete;
using DTO;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    public class BankCardDalTests
    {
        private readonly BankCardDal _bankCardDal;
        public BankCardDalTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("configTest.json")
                .Build();

            string connectionString = configuration.GetConnectionString("TradingCompanyTest") ?? "";
            _bankCardDal = new BankCardDal(connectionString);
        }
        [Fact]
        public void GetAll_ReturnsAllBankCards()
        {
            // Act
            var bankCards = _bankCardDal.GetAll();

            // Assert
            Assert.NotNull(bankCards);
            Assert.True(bankCards.Count > 0);  // Ensure there is at least one bank card
        }

        [Fact]
        public void GetById_ReturnsBankCard()
        {
            var bankCardId = 1;
            var bankCard = _bankCardDal.GetById(bankCardId);
            Assert.NotNull(bankCard);
            Assert.Equal(bankCardId, bankCard.BankCardId);
        }

        [Fact]
        public void InsertBankCard_InsertsSuccessfully()
        {
            var newBankCard = new BankCard
            {
                OwnerId = 2,
                Number = "1234567890123456",
                CVV = 123,
                PIN = 1234
            };
            var insertedBankCard = _bankCardDal.InsertBankCard(newBankCard);
            Assert.NotNull(insertedBankCard);
            Assert.True(insertedBankCard.BankCardId > 0);
            Assert.Equal(newBankCard.OwnerId, insertedBankCard.OwnerId);
        }

        [Fact]
        public void DeleteBankCard_DeletedSuccessfully()
        {
            var bankCardId = 1;
            _bankCardDal.DeleteBankCard(bankCardId);
            Assert.Throws<NullReferenceException>(() => _bankCardDal.GetById(bankCardId));
        }

        [Fact]
        public void UpdateBankCard_UpdatesSuccessfully()
        {
            var bankCardId = 1;
            var newNumber = "1111222233334444";
            _bankCardDal.UpdateBankCard(bankCardId, "number", newNumber);
            var updatedCard = _bankCardDal.GetById(bankCardId);
            Assert.Equal(newNumber, updatedCard.Number);
        }
        [Fact]
        public void GetById_InvalidId_ThrowsException()
        {
            var invalidBankCardId = -1; 
            Assert.Throws<NullReferenceException>(() => _bankCardDal.GetById(invalidBankCardId));
        }
    }
}