using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace FinalProjectCodingIDBE.Repositories
{
    public class CategoryRepository
    {
        private readonly string _connectionString = string.Empty;
        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public List<Category> GetCategories()
        {
            List<Category> category = new List<Category>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM Category WHERE is_delete = false;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var image = reader.IsDBNull("image_path") ? string.Empty : reader.GetString("image_path");
                    category.Add(new Category()
                    {
                        Id = reader.GetInt32("category_id"),
                        Name = reader.GetString("category_name"),
                        Description = reader.GetString("category_desc"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        IsActive = reader.GetBoolean("is_active"),
                        ImagePath = image
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return category;
        }

        public Category GetCategoryById(int Id)
        {
            Category category = new Category();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = $"SELECT * FROM category WHERE category_id = {Id};";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var image = reader.IsDBNull("image_path") ? string.Empty : reader.GetString("image_path");
                    category.Id = reader.GetInt32("category_id");
                    category.Name = reader.GetString("category_name");
                    category.Description = reader.GetString("category_desc");
                    category.CreatedAt = reader.GetString("created_at");
                    category.UpdatedAt = reader.GetString("updated_at");
                    category.IsActive = reader.GetBoolean("is_active");
                    category.ImagePath = image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return category;
        }

        public string CreateCategory(AddCategoryDTO categoryDTO, string fileUrlPath)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();

                string sql = "INSERT INTO category (category_id, category_name, category_desc, created_at, updated_at, is_active, is_delete, image_path) VALUES (@categoryID, @categoryName, @categoryDesc, @createdAt, @updatedAt, @isActive, @isDeleted, @imagePath)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@categoryID", null);
                cmd.Parameters.AddWithValue("@categoryName", categoryDTO.Name);
                cmd.Parameters.AddWithValue("@categoryDesc", categoryDTO.Description);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@isActive", true);
                cmd.Parameters.AddWithValue("@isDeleted", false);
                cmd.Parameters.AddWithValue("@imagePath", fileUrlPath);
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
        public string UpdateCategory(int Id, EditCategoryDTO categoryDTO, string fileUrlPath)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            Category categoryResponse = GetCategoryById(Id);
            if (categoryResponse.Id == 0)
            {
                return "Data tidak ditemukan";
            }

            try
            {
                conn.Open();
                string sql = "UPDATE Category SET category_name=@categoryName, category_desc=@categoryDesc, image_path=@imagePath,updated_at=@updatedAt WHERE category_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@categoryName", categoryDTO.Name);
                cmd.Parameters.AddWithValue("@categoryDesc", categoryDTO.Description);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@imagePath", fileUrlPath);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected < 1)
                {
                    //fail
                    throw new Exception("Failed to Update");
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return response;
        }
        public string DeleteCategory(int Id)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            Category category = GetCategoryById(Id);

            if (category.Id == 0)
            {
                return "Data tidak ditemukan";
            }

            try
            {
                conn.Open();
                /*string sql = "DELETE FROM Products WHERE product_id = @Id";*/
                string sql = "UPDATE category SET is_delete=@isDelete WHERE category_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@isDelete", true);
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


        public List<Category> GetCategoryLimit()
        {
            List<Category> category = new List<Category>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM Category WHERE is_delete = false LIMIT 8;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var image = reader.IsDBNull("image_path") ? string.Empty : reader.GetString("image_path");
                    category.Add(new Category()
                    {
                        Id = reader.GetInt32("category_id"),
                        Name = reader.GetString("category_name"),
                        Description = reader.GetString("category_desc"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                        ImagePath = image
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return category;
        }

        public string UpdateStatusCategory(int Id, bool Status)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            Category category = GetCategoryById(Id);

            if (category.Id == 0)
            {
                return "Data tidak ditemukan";
            }

            try
            {
                conn.Open();
                /*string sql = "DELETE FROM Products WHERE product_id = @Id";*/
                string sql = "UPDATE category SET is_active=@isActive WHERE category_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@isActive", Status);
                cmd.Parameters.AddWithValue("@Id", Id);
                var rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    return "Updated Failed";
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
