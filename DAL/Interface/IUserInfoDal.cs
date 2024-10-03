using DTO;
namespace DAL.Interface
{
    public interface IUserInfoDal
    {
        List<UserInfo> GetAll();
        UserInfo GetByID(int id);
        UserInfo InsertUser(UserInfo userInfo);
        void DeleteUser(int id);
    }
}
