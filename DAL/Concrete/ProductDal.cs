using DAL.Interface;
using DTO;
using System.Data.SqlClient;

namespace DAL.Concrete
{
    public class ProductDal : IProductDal
    {
        private readonly SqlConnection _connection;
        public ProductDal(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }
        public List<Product> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT product_id, title, price, category_id FROM product";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var products = new List<Product>();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = Convert.ToInt32(reader["product_id"]),
                        Title = reader["title"].ToString(),
                        Price = Convert.ToInt32(reader["price"]),
                        CategoryId = Convert.ToInt32(reader["category_id"])
                    });
                }
                _connection.Close();
                return products;
            }
        }
        public Product GetById(int id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT product_id, title, price, category_id FROM product WHERE product_id = @productId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("productId", id);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Product? product = null;
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["bank_card_id"]),
                        Title = reader["title"].ToString(),
                        Price = Convert.ToInt32(reader["price"]),
                        CategoryId = Convert.ToInt32(reader["category_id"])
                    };
                }
                _connection.Close();
                if (product == null)
                {
                    throw new NullReferenceException("Invalid Bank Card ID");
                }
                return product;
            }
        }
        public Product InsertProduct(Product product)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO product (title, price, category_id) " +
                    "OUTPUT inserted.product_id VALUES(@Title, @Price, @cateforyId)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("title", product.Title);
                cmd.Parameters.AddWithValue("price", product.Price);
                cmd.Parameters.AddWithValue("cateforyId", product.CategoryId);
                _connection.Open();
                product.ProductId = Convert.ToInt32(cmd.ExecuteScalar());
                _connection.Close();
                return product;
            }
        }
        public void DeleteProduct(int productID)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM product WHERE product_id = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", productID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public void UpdateProduct(int productID, string productProperty, string propertyValue)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = $"UPDATE product SET {productProperty} = @value WHERE product_id = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("value", propertyValue);
                cmd.Parameters.AddWithValue("id", productID);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
