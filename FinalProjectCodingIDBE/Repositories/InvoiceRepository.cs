using FinalProjectCodingIDBE.DTOs;
using FinalProjectCodingIDBE.DTOs.InvoiceDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;

namespace FinalProjectCodingIDBE.Repositories
{
    public class InvoiceRepository
    {
        private readonly string _connectionString = string.Empty;
        private readonly OrderRepository _orderRepository;
        public InvoiceRepository(IConfiguration configuration, OrderRepository orderRepository)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _orderRepository = orderRepository;
        }

        public List<MenuInvoiceDTO> GetInvoiceAll(int userId)
        {
            List<MenuInvoiceDTO> invoiceList = new List<MenuInvoiceDTO>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT inv.invoice_id, inv.created_at, COUNT(inv.id_order) AS total_course, oh.total_amount AS total_price FROM invoices inv LEFT JOIN order_header oh ON inv.id_order = oh.order_id LEFT JOIN order_details od ON oh.order_id = od.id_order  WHERE oh.id_user = @idUser GROUP BY inv.id_order;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    invoiceList.Add(new MenuInvoiceDTO()
                    {
                        Id = reader.GetInt32("invoice_id"),
                        createdAt = reader.GetString("created_at"),
                        totalCourse = reader.GetInt32("total_course"),
                        totalPrice = reader.GetInt32("total_price"),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return invoiceList;
        }
        public InvoiceResponseDTO GetInvoiceById(int userId, int idInvoice)
        {
            InvoiceResponseDTO invoice = new InvoiceResponseDTO();
            MySqlConnection conn = new MySqlConnection(_connectionString);

            try
            {
                conn.Open();

                string sql = "SELECT * FROM invoices inv LEFT JOIN order_header oh ON inv.id_order = oh.order_id WHERE oh.id_user = @idUser AND inv.invoice_id =@idInvoice";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@idInvoice", idInvoice);
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    invoice.Id = reader.GetInt32("invoice_id");
                    invoice.IdOrder = reader.GetInt32("id_order");
                    invoice.Status = reader.GetString("status");
                    invoice.createdAt = reader.GetString("created_at");
                    invoice.updatedAt = reader.GetString("updated_at");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

            OrderResponseDTO orderResponseDTO = _orderRepository.GetByIdOrders(userId, invoice.IdOrder);
            invoice.OrderResponse = orderResponseDTO;

            return invoice;
        }

        public string CreateInvoice(int userId, int idOrder)
        {
            string response = string.Empty;
            MySqlConnection conn = new MySqlConnection(_connectionString);
            OrderResponseDTO orderResponseDTO = _orderRepository.GetByIdOrders(userId, idOrder);
            DateTime now = new DateTime();

            try
            {
                conn.Open();

                string sql = "INSERT INTO Invoices (invoice_id, id_order, status, created_at, updated_at, is_delete) VALUES (@idInvoice, @idOrder, @status, @createdAt, @updatedAt, @isDelete)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idInvoice", null);
                cmd.Parameters.AddWithValue("@idOrder", idOrder);
                cmd.Parameters.AddWithValue("@status", orderResponseDTO.StatusPayment);
                cmd.Parameters.AddWithValue("@createdAt", now);
                cmd.Parameters.AddWithValue("@updatedAt", now);
                cmd.Parameters.AddWithValue("@isDelete", false);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    response = "Created Failed";
                    return response;
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


    }
}
