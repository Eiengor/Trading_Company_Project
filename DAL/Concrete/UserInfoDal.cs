using DTO;
using DAL.Interface;
using System.Data.SqlClient;

namespace DAL.Concrete
{
    public class UserInfoDal
    {
        private readonly SqlConnection _connection;
        public UserInfoDal(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }
        public List<UserInfo> GetAll()
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT user__id, user_login, first_name, last_name FROM user_info";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<UserInfo> userInfoList = new List<UserInfo>();
                while (reader.Read())
                {
                    userInfoList.Add(new UserInfo
                    {
                        UserId = Convert.ToInt32(reader["user__id"]),
                        UserLogin = reader["user_login"].ToString(),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString()
                    });
                }
                _connection.Close();
                return userInfoList;
            }
        }
        public UserInfo GetById(int id)
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT user__id, user_login, first_name, last_name FROM user_info WHERE user__id = @userId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("userId", id);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                UserInfo? userInfo = null;
                if(reader.Read())
                {
                    userInfo = new UserInfo
                    {
                        UserId = Convert.ToInt32(reader["user__id"]),
                        UserLogin = reader["user_login"].ToString(),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString()
                    };
                }
                _connection.Close();
                if(userInfo == null)
                {
                    throw new NullReferenceException("Wrong User ID");
                }
                return userInfo;
            }
        }
    }
}
