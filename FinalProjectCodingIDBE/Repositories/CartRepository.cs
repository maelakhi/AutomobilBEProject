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

        public List<CartResponseDTO> GetAllCart(int userId) { 
            List<CartResponseDTO> carts = new List<CartResponseDTO>();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT c.carts_id, p.*, c.date_schedule,cg.category_name, c.id_user FROM Carts c LEFT JOIN Products p ON p.product_id = c.id_product LEFT JOIN Category cg ON p.id_category = cg.category_id WHERE c.id_user = @IdUser;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    ProductsResponseDTO product = new ProductsResponseDTO() {
                        Id = reader.GetInt32("product_id"),
                        Name = reader.GetString("product_name"),
                        Description = reader.GetString("product_desc"),
                        Price = reader.GetInt32("product_price"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        IdCategory = reader.GetInt32("id_category"),
                        IsActive = reader.GetBoolean("is_active"),
                        ImagePath = reader.GetString("image_path"),
                        CategoryName = reader.GetString("category_name")
                    }; 
                    carts.Add(new CartResponseDTO
                    {
                        Id = reader.GetInt32("carts_id"),
                        IdProduct = reader.GetInt32("product_id"),
                        IdUser = reader.GetInt32("id_user"),
                        DateSchedule = reader.GetString("date_schedule"),
                        product = product,
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return carts;
        }

        public CartResponseDTO GetByIdCart(int userId, int idCart)
        {
            CartResponseDTO carts = new CartResponseDTO();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT c.carts_id, p.*, c.date_schedule,cg.category_name, c.id_user FROM Carts c LEFT JOIN Products p ON p.product_id = c.id_product LEFT JOIN Category cg ON p.id_category = cg.category_id WHERE c.id_user = @IdUser AND c.carts_id = @idCarts;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@idCarts", idCart);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductsResponseDTO product = new ProductsResponseDTO()
                    {
                        Id = reader.GetInt32("product_id"),
                        Name = reader.GetString("product_name"),
                        Description = reader.GetString("product_desc"),
                        Price = reader.GetInt32("product_price"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        IdCategory = reader.GetInt32("id_category"),
                        IsActive = reader.GetBoolean("is_active"),
                        ImagePath = reader.GetString("image_path"),
                        CategoryName = reader.GetString("category_name")
                    };
                    carts.Id = reader.GetInt32("carts_id");
                    carts.IdProduct = reader.GetInt32("product_id");
                    carts.IdUser = reader.GetInt32("id_user");
                    carts.DateSchedule = reader.GetString("date_schedule");
                    carts.product = product;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return carts;
        }

        public string CreateCart(AddCartDTO cartData)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "INSERT INTO Carts (carts_id,id_product,id_user, date_schedule, created_at, updated_at, is_delete) VALUES (@cartsId,@productId,@idUser,@dateSchedule, @createdAt, @updatedAt, @isDelete)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cartsId", null);
                cmd.Parameters.AddWithValue("@productId", cartData.IdProduct);
                cmd.Parameters.AddWithValue("@idUser", cartData.IdUser);
                cmd.Parameters.AddWithValue("@dateSchedule", cartData.DateSchedule);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@isDelete", false);
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

        public string DeleteCart(int userId, int idCart)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "DELETE FROM Carts WHERE id_user=@idUser AND carts_id IN (@idCart)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCart", idCart);
                cmd.Parameters.AddWithValue("@idUser", userId);
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
