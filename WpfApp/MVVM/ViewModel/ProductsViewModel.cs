using DAL.Concrete;
using DTO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private DBConnect _dbConnect = new DBConnect();
        private ProductDal _productDal;
        private List<Product> _productList;
        public List<Product> Products
        {
            get => _productList;
            set
            {
                _productList = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        public ProductsViewModel()
        {
            _productDal = _dbConnect.GetProductDAL();
            LoadProducts();
        }
        private void LoadProducts()
        {
            Products = _productDal.GetAll();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
