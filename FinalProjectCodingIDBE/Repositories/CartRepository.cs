using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;

namespace FinalProjectCodingIDBE.Repositories
{
    public class CartRepository
    {
        private readonly string _connectionString = string.Empty;

        public CartRepository(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public List<Cart> GetAllCart(int userId) { 
            List<Cart> carts = new List<Cart>();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT c.carts_id, p.*, c.id_user FROM Carts c LEFT JOIN Products p ON p.product_id = c.id_product WHERE c.id_user = @IdUser;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    carts.Add(new Cart{
                        Id = reader.GetInt32("carts_id"),
                        IdProduct = reader.GetInt32("product_id"),
                        IdUser = reader.GetInt32("id_user")
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return carts;
        }

        public CartResponseDTO GetCartById(int Id, int userId)
        {
            CartResponseDTO cart = new CartResponseDTO();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT c.carts_id, p.*, c.id_user FROm Carts c LEFT JOIN Products p ON p.product_id = c.id_product WHERE c.id_user = @idUser AND c.carts_id = @idProduct;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@idProduct", Id);
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    cart.Id = reader.GetInt32("carts_id");
                    cart.product.Id = reader.GetInt32("product_id");
                    cart.IdUser = reader.GetInt32("id_user");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return cart;
        }

        public string CreateCart(Cart Cart)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "INSERT INTO Products (product_id,product_name,product_desc,product_price,image_path,created_at,updated_at,id_category,is_active, is_delete) VALUES (@productId,@productName,@productDesc,@productPrice,@imagePath,@createdAt,@updatedAt,@isActive,@idCategory,@isDelete)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", null);
                cmd.Parameters.AddWithValue("@productName", null);
                cmd.Parameters.AddWithValue("@productDesc", null);
                cmd.Parameters.AddWithValue("@productPrice", null);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@imagePath", null);
                cmd.Parameters.AddWithValue("@isActive", true);
                cmd.Parameters.AddWithValue("@isDelete", false);
                cmd.Parameters.AddWithValue("@idCategory", null);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                response = ex.Message;
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return response;
        }

    }
}
