using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;

namespace FinalProjectCodingIDBE.Repositories
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public Users? GetByEmailAndPassword(string email, string password)
        {
            Users? user = null;

            //get connection to database
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();
                // able to query after open
                // Perform database operations
                MySqlCommand cmd = new MySqlCommand("SELECT user_id, user_email, role_user FROM users WHERE user_email=@Email and user_password=@Password", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = new Users();
                    user.Id = reader.GetInt32("user_id");
                    user.Email = reader.GetString("user_email");
                    user.Role = reader.GetString("role_user");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //required
            conn.Close();

            return user;
        }

        public string CreateAccount(string email, string hashedPassword)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "INSERT INTO users (user_id, user_email, user_password, created_at, updated_at, is_active, is_delete, role_user) VALUES (@userID, @userEmail, @userPass, @createdAt, @updatedAt, @isActive, @isDeleted, @Role )";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", null);
                cmd.Parameters.AddWithValue("@userEmail", email);
                cmd.Parameters.AddWithValue("@userPass", hashedPassword);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@isActive", true);
                cmd.Parameters.AddWithValue("@isDeleted", false);
                cmd.Parameters.AddWithValue("@Role", "user");
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