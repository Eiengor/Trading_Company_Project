using DAL.Concrete;
using DTO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Core;
using WpfApp.MVVM.Model;
using WpfApp.MVVM.View;

namespace WpfApp.MVVM.ViewModel
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        private DBConnect dbConnect = new DBConnect();
        private UserInfo _currentUser;

        private UserInfoDal _userDAL;
        public RelayCommand LogInCommand => new RelayCommand(execute => LogInUser(), canExecute => UserLogin != null);
        public LogInViewModel()
        {
           _userDAL = dbConnect.GetUsersDAL();
        }
        
        private string _userLogin;
        public string UserLogin
        {
            get => _userLogin;
            set
            {
                _userLogin = value;
                OnPropertyChanged(nameof(UserLogin));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private void LogInUser()
        {
            if (string.IsNullOrEmpty(UserLogin))
            {
                MessageBox.Show("Please enter a user name!", "Wrong user name!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please enter a password!", "Wrong password!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (UserExists(UserLogin) && CorrectPassword(UserLogin, Password))
                {
                    _currentUser = _userDAL.GetByUserLogin(UserLogin);
                    UserSession.SetCurrentUser(_currentUser.UserId);
                    MessageBox.Show("You logged in!", "Succesfull!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //new MainWindowModel().CurrentView = new MainWindowModel().ProductsVM;
                }
                else
                {
                    MessageBox.Show("Wrong name or password!", "Invalid Username or password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool UserExists(string userLogin)
        {
            var userList = _userDAL.GetAll();
            foreach (var user in userList)
            {
                if(user.UserLogin == userLogin)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CorrectPassword(string userLogin, string password)
        {
            var user = _userDAL.GetByUserLogin(userLogin);
            var checkPassword = HashingPassword.HashPassword(password);
            if (user.HashPassword.Replace(" ", "") == checkPassword)
            {
                return true;
            }
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
