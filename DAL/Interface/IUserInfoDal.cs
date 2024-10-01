using DTO;
namespace DAL.Interface
{
    public interface IUserInfoDal
    {
        List<UserInfo> GetAll();
        UserInfo GetByID(int id);
    }
}
