using FinalProjectCodingIDBE.Dto.Auth;
using FinalProjectCodingIDBE.DTOs.UsersDTO;
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
        public List<UserResponseDTO> GetUserAll()
        {
            List<UserResponseDTO> userList = new List<UserResponseDTO>();

            //get connection to database
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();
                // able to query after open
                // Perform database operations
                MySqlCommand cmd = new MySqlCommand("SELECT user_id, user_email, role_user, user_name FROM users ", conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new UserResponseDTO()
                    { 
                        Id = reader.GetInt32("user_id"),
                        Email = reader.GetString("user_email"),
                        Role = reader.GetString("role_user"),
                        Name = reader.GetString("user_name")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //required
            conn.Close();

            return userList;
        }

        public Users? GetByEmailAndPassword(LoginDto data)
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
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

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

        public Users? GetByEmail(string email)
        {
            Users? user = null;

            //get connection to database
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();
                // able to query after open
                // Perform database operations
                MySqlCommand cmd = new MySqlCommand("SELECT user_id, user_email, role_user FROM users WHERE user_email=@Email", conn);
                cmd.Parameters.AddWithValue("@Email", email);

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

        public string CreateAccount(RegisterDto data)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "INSERT INTO users (user_id, user_email, user_password, user_name, created_at, updated_at, is_active, is_delete, role_user) VALUES (@userID, @userEmail, @userPass, @userName, @createdAt, @updatedAt, @isActive, @isDeleted, @Role )";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", null);
                cmd.Parameters.AddWithValue("@userEmail", data.Email);
                cmd.Parameters.AddWithValue("@userPass", data.Password);
                cmd.Parameters.AddWithValue("@userName", data.Name);
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