using FinalProjectCodingIDBE.DTOs;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;

namespace FinalProjectCodingIDBE.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString = string.Empty;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public List<ProductsDTO> GetProductsAll()
        {
            List<ProductsDTO> products = new List<ProductsDTO>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT p.*,c.category_name FROM Products p LEFT JOIN Category c ON p.id_category = c.category_id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new ProductsDTO()
                    {
                        Id = reader.GetInt32("product_id"),
                        Name = reader.GetString("product_name"),
                        Description = reader.GetString("product_desc"),
                        Price = reader.GetInt32("product_price"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        CategoryName = reader.GetString("category_name")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return products;
        }
    }
}
