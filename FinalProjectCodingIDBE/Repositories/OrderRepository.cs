using FinalProjectCodingIDBE.DTOs.OrderDTO;
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
            List<OrderResponseDTO> carts = new List<OrderResponseDTO>();

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
                    carts.Add(new OrderResponseDTO
                    {
                        Id = reader.GetInt32("order_id"),
                        IdUser = reader.GetInt32("id_user"),
                        IdPayment = reader.GetInt32("id_payment")
                    });
                }
                cmd.ExecuteNonQuery();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return carts;
        }
    }
}
