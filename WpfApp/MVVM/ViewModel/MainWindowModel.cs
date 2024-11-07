using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    public class MainWindowModel : ObservableObject
    {
        public RelayCommand LogInViewCommand { get; set; }
        public RelayCommand RegistrationViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }

        public LogInViewModel LogInVM { get; set; }
        public RegistrationViewModel RegistrationVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainWindowModel()
        {
            LogInVM = new LogInViewModel();
            RegistrationVM = new RegistrationViewModel();
            ProductsVM = new ProductsViewModel();
            CurrentView = ProductsVM;

            LogInViewCommand = new RelayCommand(o => { CurrentView = LogInVM; });
            RegistrationViewCommand = new RelayCommand(o => { CurrentView = RegistrationVM; });
            ProductsViewCommand = new RelayCommand(o => { CurrentView =  ProductsVM; });
        }
    }
}
