using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;

namespace FinalProjectCodingIDBE.Repositories
{
    public class PaymentMethodRepository
    {
        private readonly string _connectionString = string.Empty;

        public PaymentMethodRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public List<PaymentMethod> PaymentList()
        {
            List<PaymentMethod> paymentList = new List<PaymentMethod>();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM payment_method WHERE is_active = true;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    paymentList.Add(new PaymentMethod()
                    {
                        Id = reader.GetInt32("payment_id"),
                        Name = reader.GetString("payment_name"),
                        AccountNumber = reader.GetInt32("payment_number_account"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at")
                    });
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            return paymentList;
        }
    }
}
