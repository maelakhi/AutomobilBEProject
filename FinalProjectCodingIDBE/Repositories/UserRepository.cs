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
    }
}