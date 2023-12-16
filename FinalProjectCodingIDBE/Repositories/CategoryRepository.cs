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
    }
}
