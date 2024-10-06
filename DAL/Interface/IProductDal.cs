using DTO;
namespace DAL.Interface
{
    public interface IProductDal
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product InsertProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(int id, string productProperty, string propertyValue);
    }
}
