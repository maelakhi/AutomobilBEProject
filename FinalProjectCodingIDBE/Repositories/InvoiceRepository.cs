using FinalProjectCodingIDBE.DTOs;
using FinalProjectCodingIDBE.DTOs.InvoiceDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Text.RegularExpressions;

namespace FinalProjectCodingIDBE.Repositories
{
    public class InvoiceRepository
    {
        private readonly string _connectionString = string.Empty;
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;
        private readonly PaymentMethodRepository _paymentMethodRepository;
        public InvoiceRepository(
            IConfiguration configuration, 
            OrderRepository orderRepository, 
            ProductRepository productRepository, 
            PaymentMethodRepository paymentMethodRepository
        )
        {
            _connectionString = configuration.GetConnectionString("Default");
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _paymentMethodRepository = paymentMethodRepository;
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
        public InvoiceResponseDTO GetInvoiceById(int Id)
        {
            InvoiceResponseDTO orders = new InvoiceResponseDTO();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT inv.invoice_id, oh.id_user, oh.total_amount, inv.created_at, oh.order_id FROM invoices inv LEFT JOIN order_header oh ON inv.id_order = oh.order_id WHERE inv.invoice_id = @idInvoice;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idInvoice", Id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Id = reader.GetInt32("invoice_id");
                    orders.IdUser = reader.GetInt32("id_user");
                    orders.TotalAmount = reader.GetInt32("total_amount");
                    orders.CreatedAt = reader.GetString("created_at");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            /*Fill order detail*/
            orders.OrderDetails = GetAllOrderDetailsInvoice(orders.IdUser, orders.Id);
            return orders;
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

        public List<InvoiceAdminResposeDTO> GetInvoiceAllAdmin()
        {
            List<InvoiceAdminResposeDTO> invoiceList = new List<InvoiceAdminResposeDTO>();
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT inv.invoice_id, inv.status, oh.id_user, oh.order_id, inv.created_at, COUNT(od.id_order) as total_course, oh.total_amount AS total_price FROM invoices inv LEFT JOIN order_header oh ON inv.id_order = oh.order_id LEFT JOIN order_details od ON oh.order_id = od.id_order GROUP BY inv.invoice_id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    invoiceList.Add(new InvoiceAdminResposeDTO()
                    {
                        Id = reader.GetInt32("invoice_id"),
                        IdOrder = reader.GetInt32("order_id"),
                        IdUser = reader.GetInt32("id_user"),
                        Status = reader.GetString("status"),
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

        public InvoiceDetailAdminResponse GetInvoiceByIdAdmin(int Id)
        {
            InvoiceDetailAdminResponse orders = new InvoiceDetailAdminResponse();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();
                
                string sql = "SELECT inv.invoice_id, oh.status_payment, oh.id_user, oh.id_payment, oh.total_amount, inv.created_at, oh.id_user, oh.order_id FROM invoices inv LEFT JOIN order_header oh ON inv.id_order = oh.order_id WHERE inv.invoice_id = @idInvoice";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idInvoice", Id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Id = reader.GetInt32("invoice_id");
                    orders.IdUser = reader.GetInt32("id_user");
                    orders.IdPayment = reader.GetInt32("id_payment");
                    orders.StatusPayment = reader.GetString("status_payment");
                    orders.TotalAmount = reader.GetInt32("total_amount");
                    orders.CreatedAt = reader.GetString("created_at");
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            /*Fill order detail*/
            orders.OrderDetails = GetAllOrderDetailsInvoice(orders.IdUser, orders.Id);
            orders.PaymentMethod = _paymentMethodRepository.GetPaymentById(orders.IdPayment);

            return orders;
        }

        public List<OrderDetailsInvoice> GetAllOrderDetailsInvoice(int userId, int orderId)
        {
            List<OrderDetailsInvoice> orderDetails = new List<OrderDetailsInvoice>();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT od.* FROM order_details od LEFT JOIN order_header oh ON oh.order_id = od.id_order WHERE oh.id_user = @idUser AND od.id_order = @idDetail";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@idDetail", orderId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orderDetails.Add(new OrderDetailsInvoice
                    {
                        Id = reader.GetInt32("order_detail_id"),
                        IdOrder = reader.GetInt32("id_order"),
                        IdProduct = reader.GetInt32("id_product"),
                        Quantity = reader.GetInt32("quantity"),
                        AmountProduct = reader.GetInt32("amount_product"),
                        TotalAmount = reader.GetInt32("total_amount"),
                        DateSchedule = reader.GetDateTime("date_schedule")
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            foreach (var item in orderDetails)
            {
                item.product = _productRepository.InvoiceGetProductsById(item.IdProduct);
            }

            return orderDetails;
        }

    }
}
