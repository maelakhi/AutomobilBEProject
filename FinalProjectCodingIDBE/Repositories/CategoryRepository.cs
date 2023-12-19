using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;

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

                string sql = "SELECT * FROM Category;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    category.Add(new Category()
                    {
                        Id = reader.GetInt32("category_id"),
                        Name = reader.GetString("category_name"),
                        Description = reader.GetString("category_desc"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
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

                    category.Id = reader.GetInt32("category_id");
                    category.Name = reader.GetString("category_name");
                    category.Description = reader.GetString("category_desc");
                    category.CreatedAt = reader.GetString("created_at");
                    category.UpdatedAt = reader.GetString("updated_at");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return category;
        }

        public Category CreateCategory(AddCategoryDTO categoryDTO)
        {
            Category category = new Category();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;
            try
            {
                conn.Open();

                string sql = "INSERT INTO category (category_id, category_name, category_desc, created_at, updated_at) VALUES (@categoryID, @categoryName, @categoryDesc, @createdAt, @updatedAt )";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@categoryID", null);
                cmd.Parameters.AddWithValue("@categoryName", category.Name);
                cmd.Parameters.AddWithValue("@categoryDesc", category.Description);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.ExecuteNonQuery();
                category.Id = (int)cmd.LastInsertedId;
                category.Name = categoryDTO.Name;
                category.Description = categoryDTO.Description;
                category.CreatedAt = now.ToString();
                category.UpdatedAt = now.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return category;
        }
        public Category UpdateCategory(int Id, AddCategoryDTO categoryDTO)
        {
            Category category = GetCategoryById(Id);
            MySqlConnection conn = new MySqlConnection(_connectionString);
            DateTime now = DateTime.Now;

            try
            {
                conn.Open();
                string sql = "UPDATE Category SET category_name=@categoryName, category_desc=@categoryDesc, updated_at=@updatedAt WHERE category_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@categoryName", category.Name);
                cmd.Parameters.AddWithValue("@categoryDesc", category.Description);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@Id", Id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected < 1)
                {
                    //fail
                    throw new Exception("Failed to Update");
                }
                category.Id = (int)cmd.LastInsertedId;
                category.Name = categoryDTO.Name;
                category.Description = categoryDTO.Description;
                category.CreatedAt = now.ToString();
                category.UpdatedAt = now.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return category;
        }
        public bool DeleteCategory(int Id)
        {
            MySqlConnection conn = new MySqlConnection(_connectionString);
            Category category = GetCategoryById(Id);

            if (category == null)
            {
                return false;
            }

            try
            {
                conn.Open();
                string sql = "DELETE FROM Category WHERE category_id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return true;
        }


        /*Landing page*/
        public List<Category> GetCategoryLimit()
        {
            List<Category> categories = new List<Category>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM Category LIMIT 6;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new Category()
                    {
                        Id = reader.GetInt32("category_id"),
                        Name = reader.GetString("category_name"),
                        Description = reader.GetString("category_desc"),
                        CreatedAt = reader.GetString("created_at"),
                        UpdatedAt = reader.GetString("updated_at"),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return categories;
        }

    }
}
