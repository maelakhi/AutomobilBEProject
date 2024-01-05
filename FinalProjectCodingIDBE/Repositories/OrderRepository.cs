using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;

namespace FinalProjectCodingIDBE.Repositories
{
    public class OrderRepository
    {
        private readonly string _connectionString = string.Empty;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public List<OrderResponseDTO> GetAllOrders(int userId)
        {
            List<OrderResponseDTO> orders = new List<OrderResponseDTO>();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();
                

                string sql = "SELECT * FROM order_header WHERE id_user = @idUser";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(new OrderResponseDTO
                    {
                        Id = reader.GetInt32("order_id"),
                        IdUser = reader.GetInt32("id_user"),
                        IdPayment = reader.GetInt32("id_payment"),
                        StatusPayment = reader.GetString("status_payment"),
<<<<<<< Updated upstream
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at")
=======
                        TotalAmount = reader.GetInt32("total_amount"),
                        CreatedAt = reader.GetDateTime("created_at"),
                        UpdatedAt = reader.GetDateTime("updated_at"),
>>>>>>> Stashed changes
                    });
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            /*Fill order detail*/
            foreach (var item in orders)
            {
                item.OrderDetails = GetAllOrderDetails(item.IdUser, item.Id);
            }

            return orders;
        }

        public List<OrderDetails> GetAllOrderDetails(int userId, int orderId)
        {
            List<OrderDetails> orderDetails = new List<OrderDetails>();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();


                string sql = "SELECT od.* FROM order_details od LEFT JOIN order_header oh ON oh.order_id = od.id_order WHERE oh.id_user = @idUser AND od.id_order = @idDetail";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@idDetail", orderId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orderDetails.Add(new OrderDetails
                    {
                        Id = reader.GetInt32("order_detail_id"),
                        IdOrder = reader.GetInt32("id_order"),
                        IdProduct = reader.GetInt32("id_product"),
                        Quantity = reader.GetInt32("quantity"),
                        AmountProduct = reader.GetInt32("amount_product"),
                        TotalAmount = reader.GetInt32("total_amount"),
<<<<<<< Updated upstream
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at")
=======
                        CreatedAt = reader.GetDateTime("created_at"),
                        UpdatedAt = reader.GetDateTime("updated_at"),
                        DateSchedule = reader.GetString("date_schedule")
>>>>>>> Stashed changes
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return orderDetails;
        }

        public OrderResponseDTO GetByIdOrders(int userId, int orderId)
        {
            OrderResponseDTO order = new OrderResponseDTO();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();


                string sql = "SELECT * FROM order_header WHERE id_user = @idUser AND order_id=@idOrder";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@idOrder", orderId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    order.Id = reader.GetInt32("order_id");
                    order.IdUser = reader.GetInt32("id_user");
                    order.IdPayment = reader.GetInt32("id_payment");
                    order.StatusPayment = reader.GetString("status_paymnet");
                    order.TotalAmount = reader.GetInt32("total_amount");
                    order.CreatedAt = reader.GetDateTime("created_at");
                    order.UpdatedAt = reader.GetDateTime("updated_at");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            /*Fill order detail*/
            order.OrderDetails = GetAllOrderDetails(userId, orderId);

            return order;
        }

        public string CreateOrder(int userId, AddOrderDTO addOrderDTO)
        {
            string response = string.Empty;
            int totalAmount = 0;
            List<CartResponseDTO> cartsList = new List<CartResponseDTO>();
            OrderResponseDTO order = new OrderResponseDTO();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = new DateTime();

            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction();

            foreach (var item in addOrderDTO.CartsID)
            {
                CartResponseDTO cartForAmount = GetByIdCartOrder(userId, item);
                totalAmount += cartForAmount.product.Price;
                cartsList.Add(cartForAmount);
            }

            try
            {

                string sql = "INSERT INTO order_header (order_id,id_user,id_payment,status_payment,total_amount,created_at,updated_at,is_delete) VALUES (@idOrder,@idUser,@idPayment,@statusPayment,@totalAmount,@createdAt,@updatedAt,@isDelete)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@idOrder", null);
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@idPayment", addOrderDTO.IdPayment);
                cmd.Parameters.AddWithValue("@statusPayment", "PAID");
                cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@isDelete", false);
                cmd.ExecuteNonQuery();

                var lastInsertedId = cmd.LastInsertedId;

                foreach (var item in cartsList)
                {
                    sql = "INSERT INTO order_details (order_detail_id,id_order,id_product,quantity,amount_product,total_amount,created_at,updated_at,is_delete) VALUES (@idOrderDetail,@idOrder,@idProduct,@quantity,@amountProduct,@totalAmount,@createdAt,@updatedAt,@isDelete)";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@idOrderDetail", null);
                    cmd.Parameters.AddWithValue("@idOrder", lastInsertedId);
                    cmd.Parameters.AddWithValue("@idProduct", item.IdProduct);
                    cmd.Parameters.AddWithValue("@quantity", 1);
                    cmd.Parameters.AddWithValue("@amountProduct", item.product.Price);
                    cmd.Parameters.AddWithValue("@totalAmount", item.product.Price);
                    cmd.Parameters.AddWithValue("@createdAt", now);
                    cmd.Parameters.AddWithValue("@updatedAt", now);
                    cmd.Parameters.AddWithValue("@isDelete", false);
                    cmd.ExecuteNonQuery();

                    sql = "DELETE FROM Carts WHERE carts_id = @idCart AND id_user=@idUser";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@idCart", item.Id);
                    cmd.Parameters.AddWithValue("@idUser", userId);
                    cmd.ExecuteNonQuery();

                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            conn.Close();


            return response;
        }

        public CartResponseDTO GetByIdCartOrder(int userId, int idCart)
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
    }
}
