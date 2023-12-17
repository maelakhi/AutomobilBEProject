using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace FinalProjectCodingIDBE.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString = string.Empty;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public List<ProductsResponseDTO> GetProductsAll()
        {
            List<ProductsResponseDTO> products = new List<ProductsResponseDTO>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT p.*,c.category_name FROM Products p LEFT JOIN Category c ON p.id_category = c.category_id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new ProductsResponseDTO()
                    {
                        Id = reader.GetInt32("product_id"),
                        Name = reader.GetString("product_name"),
                        Description = reader.GetString("product_desc"),
                        Price = reader.GetInt32("product_price"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        IdCategory = reader.GetInt32("id_category"),
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

        public ProductsResponseDTO GetProductsById(int Id)
        {
            ProductsResponseDTO product = new ProductsResponseDTO();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = $"SELECT p.*,c.category_name FROM Products p LEFT JOIN Category c ON p.id_category = c.category_id WHERE p.product_id = {Id};";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    product.Id = reader.GetInt32("product_id");
                    product.Name = reader.GetString("product_name");
                    product.Description = reader.GetString("product_desc");
                    product.Price = reader.GetInt32("product_price");
                    product.CreatedAt = reader.GetString("created_at");
                    product.UpdatedAt = reader.GetString("updated_at");
                    product.IdCategory = reader.GetInt32("id_category");
                    product.CategoryName = reader.GetString("category_name");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return product;
        }

        public Products CreateProduct(AddProductsDTO productsDTO)
        {
            Products product = new Products();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;
            try
            {
                conn.Open();

                string sql = "INSERT INTO Products (product_id, product_name, product_desc,product_price,created_at,updated_at,id_category) VALUES (@productId, @productName, @productDesc, @productPrice, @createdAt, @updatedAt,@idCategory )";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", null);
                cmd.Parameters.AddWithValue("@productName", productsDTO.Name);
                cmd.Parameters.AddWithValue("@productDesc", productsDTO.Description);
                cmd.Parameters.AddWithValue("@productPrice", productsDTO.Price);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@idCategory", productsDTO.IdCategory);
                cmd.ExecuteNonQuery();
                product.Id = (int)cmd.LastInsertedId;
                product.Name = productsDTO.Name;
                product.Description = productsDTO.Description;
                product.Price = productsDTO.Price;
                product.CreatedAt = now.ToString();
                product.UpdatedAt = now.ToString();
                product.IdCategory = productsDTO.IdCategory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return product;
        }
        public Products UpdateProduct(int Id,AddProductsDTO productsDTO)
        {
            Products product = new Products();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;
            ProductsResponseDTO productsDTOResponse = GetProductsById(Id);
 
            try
            {
                conn.Open();
                string sql = "UPDATE Products SET product_name=@productName, product_desc=@productDesc, product_price=@productPrice, updated_at=@updatedAt, id_category=@idCategory WHERE product_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productName", productsDTO.Name);
                cmd.Parameters.AddWithValue("@productDesc", productsDTO.Description);
                cmd.Parameters.AddWithValue("@productPrice", productsDTO.Price);
                cmd.Parameters.AddWithValue("@idCategory", productsDTO.IdCategory);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@Id", Id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if(rowsAffected < 1)
                {
                    //fail
                    throw new Exception("Failed to Update");
                }
                product.Id = productsDTOResponse.Id;
                product.Name = productsDTO.Name;
                product.Description = productsDTO.Description;
                product.Price = productsDTO.Price;
                product.CreatedAt = now.ToString();
                product.UpdatedAt = now.ToString();
                product.IdCategory = productsDTO.IdCategory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return product;
        }
        public bool DeleteProduct(int Id)
        {
            MySqlConnection conn = new MySqlConnection(_connectionString);
            ProductsResponseDTO productsDTOResponse = GetProductsById(Id);

            if(productsDTOResponse == null) {
                return false;
            }

            try
            {
                conn.Open();
                string sql = "DELETE FROM Products WHERE product_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return true;
        }


        /*Landing page*/
        public List<ProductsResponseDTO> GetProductsLimit()
        {
            List<ProductsResponseDTO> products = new List<ProductsResponseDTO>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT p.*,c.category_name FROM Products p LEFT JOIN Category c ON p.id_category = c.category_id LIMIT 8;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new ProductsResponseDTO()
                    {
                        Id = reader.GetInt32("product_id"),
                        Name = reader.GetString("product_name"),
                        Description = reader.GetString("product_desc"),
                        Price = reader.GetInt32("product_price"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        IdCategory = reader.GetInt32("id_category"),
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
        public List<ProductsResponseDTO> GetProductsByCategory(String category)
        {
            List<ProductsResponseDTO> products = new List<ProductsResponseDTO>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = $"SELECT p.*,c.category_name FROM Products p LEFT JOIN Category c ON p.id_category = c.category_id WHERE p.id_category IN (select cc.category_id from Category cc where cc.category_name = @categoryName);";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@categoryName", category);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new ProductsResponseDTO()
                    {
                        Id = reader.GetInt32("product_id"),
                        Name = reader.GetString("product_name"),
                        Description = reader.GetString("product_desc"),
                        Price = reader.GetInt32("product_price"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        IdCategory = reader.GetInt32("id_category"),
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
