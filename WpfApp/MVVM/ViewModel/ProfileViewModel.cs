using DAL.Concrete;
using DTO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.Core;
using WpfApp.MVVM.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WpfApp.MVVM.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public UserInfoDal _userDAL;
        public UserInfo _user;
        private DBConnect _dbConnect = new DBConnect();

        public ProfileViewModel()
        {
            _userDAL = _dbConnect.GetUsersDAL();
            if(UserSession.LoggedInUserId!=0)
            {
                LoadUserData(UserSession.LoggedInUserId);
            }
        }

        public void LoadUserData(int userId)
        {
            _user = _userDAL.GetById(userId);
            OnPropertyChanged(nameof(UserLogin));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(Phone));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Gender));
        }

        public string UserLogin => _user?.UserLogin ?? string.Empty;
        public string FirstName => _user?.FirstName ?? string.Empty;
        public string LastName => _user?.LastName ?? string.Empty;
        public string Address => _user.UserAddress ?? string.Empty;
        public string Phone => _user.PhoneNumber ?? string.Empty;
        public string Email => _user?.Email ?? string.Empty;
        public string Gender => _user?.Gender ?? string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
