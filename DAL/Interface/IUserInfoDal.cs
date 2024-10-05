using DTO;
namespace DAL.Interface
{
    public interface IUserInfoDal
    {
        List<UserInfo> GetAll();
        UserInfo GetById(int id);
        UserInfo InsertUser(UserInfo userInfo);
        void DeleteUser(int id);
        void UpdateUser(int id, string userProperty, string propertyValue);
    }
}
