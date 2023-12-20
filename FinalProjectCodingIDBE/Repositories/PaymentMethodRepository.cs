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

        public List<PaymentMethod> GetPaymentList()
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

        public PaymentMethod GetPaymentById(int Id)
        {
            PaymentMethod payment = new PaymentMethod();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM payment_method WHERE is_active = true AND paymnet_id = @idPayment;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@adPaymnet", Id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    payment.Id = reader.GetInt32("payment_id");
                    payment.Name = reader.GetString("payment_name");
                    payment.AccountNumber = reader.GetInt32("payment_number_account");
                    payment.CreatedAt = reader.GetString("created_at");
                    payment.UpdatedAt = reader.GetString("updated_at");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            return payment;
        }
    }
}
