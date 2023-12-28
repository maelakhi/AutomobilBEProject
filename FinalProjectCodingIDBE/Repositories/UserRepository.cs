using FinalProjectCodingIDBE.Dto.Auth;
using FinalProjectCodingIDBE.DTOs.AuthDTO;
using FinalProjectCodingIDBE.DTOs.UsersDTO;
using FinalProjectCodingIDBE.Helpers;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;
using System.Data;

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

        public string CreateAccount(RegisterDto data, string verificationToken)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                 conn.Open();

                 string sql = "INSERT INTO users (user_id, user_email, user_password, user_name, created_at, updated_at, is_active, is_delete, role_user, verification_token, verification_expired_token) VALUES (@userID, @userEmail, @userPass, @userName, @createdAt, @updatedAt, @isActive, @isDeleted, @Role, @vrToken, @vrExpiredDate)";
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
                 cmd.Parameters.AddWithValue("@vrToken", verificationToken);
                 cmd.Parameters.AddWithValue("@vrExpiredDate", now.AddDays(1));
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

        public Users GetAccountByToken(VerifiedDTO data)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;
            Users? user = new Users();
            try
            {
                conn.Open();
           
                string sql = "SELECT user_id, user_email, verification_expired_token FROM Users WHERE verification_token = @vrToken ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@vrToken", data.Token);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = new Users();
                    user.Id = reader.GetInt32("user_id");
                    user.Email = reader.GetString("user_email");
                    user.Role = reader.GetString("role_user");
                    user.VerificationExpiredToken = reader.GetDateTime("verification_expired_token");
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return user;
        }
        public string SetAccountVerified(int Id)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "UPDATE Users SET is_verified = @isVerified, is_active = @isActive WHERE user_id = @idUser";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@isVerified", true);
                cmd.Parameters.AddWithValue("@isActive", true);
                cmd.Parameters.AddWithValue("@idUser", Id);
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
        public string CreateOTPCode(string email, string otpCode)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "UPDATE Users SET reset_password_token = @isOTP, reset_password_token_exp = @OTPexp WHERE user_email = @emailUser";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@isOTP", otpCode);
                cmd.Parameters.AddWithValue("@emailUser", email);
                cmd.Parameters.AddWithValue("@OTPexp", now.AddHours(1));
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

        public Users? GetByOTPCode(OTPverifiedDTO data)
        {
            Users? user = null;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "SELECT user_id, user_email, reset_password_token_exp FROM Users WHERE reset_password_token = @isOTP";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@isOTP", data.OTPCode);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = new Users();
                    user.Id = reader.GetInt32("user_id");
                    user.Email = reader.GetString("user_email");
                    user.ResetPasswordTokenExpired = reader.GetDateTime("reset_password_token_exp");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return user;
        }

        public string SetNewPassword(CreateNewPasswordDTO data, int Id)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "UPDATE users SET user_password = @newPass, updated_at = @updatedAt,reset_password_token_exp = @vrExp,reset_password_token =@vrToken WHERE user_id = @otpCode";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@vrExp", null);
                cmd.Parameters.AddWithValue("@vrToken", string.Empty);
                cmd.Parameters.AddWithValue("@newPass", data.Password);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@otpCode", Id);
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