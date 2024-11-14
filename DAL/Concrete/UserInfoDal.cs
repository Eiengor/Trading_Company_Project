using DTO;
using DAL.Interface;
using System.Data.SqlClient;

namespace DAL.Concrete
{
    public class UserInfoDal : IUserInfoDal
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
                cmd.CommandText = "SELECT user__id, user_login, first_name, last_name, gender, user_address, email, phone_number FROM user_info WHERE user__id = @userId";
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
                        LastName = reader["last_name"].ToString(),
                        Email = reader["email"].ToString(),
                        UserAddress = reader["user_address"].ToString(),
                        PhoneNumber = reader["phone_number"].ToString(),
                        Gender = reader["gender"].ToString()
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
        public UserInfo InsertUser(UserInfo userInfo)
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO user_info (user_login, hashpassword, first_name, last_name, password_keyword, gender, user_address, email, phone_number) OUTPUT inserted.user__id " +
                    "VALUES (@userLogin, @hashPassword, @firstName, @lastName, @passwordKeyword, @Gender, @userAddress, @Email, @phoneNumber)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("userLogin", userInfo.UserLogin);
                cmd.Parameters.AddWithValue("hashPassword", userInfo.HashPassword);
                cmd.Parameters.AddWithValue("firstName", userInfo.FirstName);
                cmd.Parameters.AddWithValue("lastName", userInfo.LastName);
                cmd.Parameters.AddWithValue("passwordKeyword", userInfo.PasswordKeyword);
                cmd.Parameters.AddWithValue("Gender", userInfo.Gender);
                cmd.Parameters.AddWithValue("userAddress", userInfo.UserAddress);
                cmd.Parameters.AddWithValue("Email", userInfo.Email);
                cmd.Parameters.AddWithValue("phoneNumber", userInfo.PhoneNumber);

                _connection.Open();
                userInfo.UserId = Convert.ToInt32(cmd.ExecuteScalar());
                _connection.Close();
                return userInfo;
            }
        }
        public void DeleteUser(int userID)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM user_info WHERE user__id = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", userID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public void UpdateUser(int userID, string userProperty, string propertyValue)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = $"UPDATE user_info SET {userProperty} = @value WHERE user__id = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("value",  propertyValue);
                cmd.Parameters.AddWithValue("id", userID);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public UserInfo GetByUserLogin(string userLogin)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT user__id, user_login, first_name, last_name, hashpassword FROM user_info WHERE user_login = @UserLogin";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("UserLogin", userLogin.ToString());
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                UserInfo? userInfo = null;
                if (reader.Read())
                {
                    userInfo = new UserInfo
                    {
                        UserId = Convert.ToInt32(reader["user__id"]),
                        UserLogin = reader["user_login"].ToString(),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        HashPassword = reader["hashpassword"].ToString()
                    };
                }
                _connection.Close();
                if (userInfo == null)
                {
                    throw new NullReferenceException("Wrong User ID");
                }
                return userInfo;
            }
        }
    }
}
