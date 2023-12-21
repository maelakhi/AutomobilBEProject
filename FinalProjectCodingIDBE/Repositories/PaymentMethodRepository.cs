using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.DTOs.PaymentDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
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
                        AccountNumber = reader.GetString("payment_number_account"),
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

                string sql = $"SELECT * FROM payment_method WHERE is_active = true AND payment_id = {Id};";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    payment.Id = reader.GetInt32("payment_id");
                    payment.Name = reader.GetString("payment_name");
                    payment.AccountNumber = reader.GetString("payment_number_account");
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

        public string AddPaymentMethod(AddPaymentDTO paymentDTO)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "INSERT INTO payment_method (payment_id, payment_name, payment_number_account, created_at, updated_at, is_active, is_delete) VALUES (@paymentID, @paymentName, @NoAccount, @createdAt, @updatedAt, @isActive, @isDeleted)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@paymentID", null);
                cmd.Parameters.AddWithValue("@paymentName", paymentDTO.Name);
                cmd.Parameters.AddWithValue("@NoAccount", paymentDTO.AccountNumber);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@isActive", true);
                cmd.Parameters.AddWithValue("@isDeleted", false);
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

        public string UpdatePaymentMethod(int Id, AddPaymentDTO paymentDTO)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;
            PaymentMethod paymentDTOResponse = GetPaymentById(Id);
            if (paymentDTOResponse.Id == 0)
            {
                return "Data tidak ditemukan";
            }

            try
            {
                conn.Open();
                string sql = "UPDATE payment_method SET payment_name=@paymentName, payment_number_account=@accountNumber, updated_at=@updatedAt WHERE payment_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@paymentName", paymentDTO.Name);
                cmd.Parameters.AddWithValue("@accountNumber", paymentDTO.AccountNumber);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@Id", Id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected < 1)
                {
                    //fail
                    throw new Exception("Failed to Update");
                }            }
            catch (Exception ex)
            {
                response = ex.Message;
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return response;
        }

        public string DeletePaymentMethod(int Id)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            PaymentMethod payment = GetPaymentById(Id);

            if (payment.Id == 0)
            {
                return "Data tidak ditemukan";
            }

            try
            {
                conn.Open();
                string sql = "UPDATE payment_method SET is_delete=@isDelete, is_active=@isActive WHERE payment_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@isDelete", true);
                cmd.Parameters.AddWithValue("@isActive", false);
                cmd.Parameters.AddWithValue("@Id", Id);
                var rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    response = "Updated Failed";
                };
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
