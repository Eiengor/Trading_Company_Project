using DTO;
namespace DAL.Interface
{
    public interface IBankCardDal
    {
        List<BankCard> GetAll();
        BankCard GetById(int id);
        BankCard InsertBankCard(BankCard bankCard);
        void DeleteBankCard(int id);
        void UpdateBankCard(int id, string bankCardProperty, string propertyValue);
    }
}
 