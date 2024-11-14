using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Core;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private DBConnect dbConnect = new DBConnect();
        public UserInfo _currentUser;

        private UserInfoDal _userDAL;
        public RelayCommand RegistrateCommand => new RelayCommand(execute => RegistrateUser(), canExecute => UserLogin != null || FirstName != null ||
        LastName != null ||
        Password != null ||
        PasswordKeyWord != null ||
        Address != null ||
        Email != null ||
        Phone != null);
        
        public RegistrationViewModel()
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
        
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
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

        private string _passwordKeyWord;
        public string PasswordKeyWord
        {
            get => _passwordKeyWord;
            set
            {
                _passwordKeyWord = value;
                OnPropertyChanged(nameof(PasswordKeyWord));
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _gender;
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        
        private List<string> _genderList = new List<string>
        {
            "Male", "Female", "Helicopter"
        };
        public List<string> GenderList
        {
            get { return  _genderList; }
            set { }

        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void RegistrateUser()
        {
            if (string.IsNullOrEmpty(UserLogin) ||
                string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(PasswordKeyWord) ||
                string.IsNullOrEmpty(Address) ||
                string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Phone))
            {
                MessageBox.Show("Missing data!", "Missing data!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UserInfo newUser = new UserInfo
                {
                    UserLogin = UserLogin,
                    FirstName = FirstName,
                    LastName = LastName,
                    HashPassword = HashingPassword.HashPassword(Password),
                    PasswordKeyword = PasswordKeyWord,
                    UserAddress = Address,
                    Gender = "Male",
                    Email = Email,
                    PhoneNumber = Phone
                };
                _userDAL.InsertUser(newUser);
                MessageBox.Show("Successfully registered!", "Wow!", MessageBoxButton.OK, MessageBoxImage.Information);
                _currentUser = _userDAL.GetByUserLogin(UserLogin);
                UserSession.SetCurrentUser(_currentUser.UserId);
                new MainWindowModel().ProductsViewCommand.Execute(this);
            }
        }

    }
}
