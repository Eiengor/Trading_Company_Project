using DTO;
namespace DAL.Interface
{
    public interface IBankCardDal
    {
        List<BankCard> GetAll();
        BankCard GetById(int id);
    }
}
 