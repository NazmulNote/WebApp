using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Areas.Client.Models;
using WebApp.Helper;
namespace WebApp.Areas.Client.Data
{
    public class OrderProductData
    {
        private readonly string _connString;
        public OrderProductData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public OrderProductMDL GetMaxOrderNo()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "MaxOrderNo";
                var viewModel = new OrderProductMDL();
                SqlCommand cmd = new SqlCommand("SP_OrderProduct", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new OrderProductMDL
                    {
                        OrderNo = Convert.ToInt32(dr["OrderNo"].ToString()),
                       
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Get Max Order No: " + ex.Message);
            }
        }
        public OrderProductMDL OrderProductSetUpdate(OrderProductMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_OrderProduct", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@OrderNo", viewModel.OrderNo);
                cmd.Parameters.AddWithValue("@CustomerId", viewModel.CustomerId);
                cmd.Parameters.AddWithValue("@OrderDate", viewModel.OrderDate);
                cmd.Parameters.AddWithValue("@Status", viewModel.Status ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalAmount", viewModel.TotalAmount);
                cmd.Parameters.AddWithValue("@DeliveryAddress", viewModel.DeliveryAddress ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CourierChargeId", viewModel.CourierChargeId);
                cmd.Parameters.AddWithValue("@PaymentMethodId", viewModel.PaymentMethodId);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", viewModel.UpdatedBy);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                string result = returnId != null ? returnId.ToString() : "ID not available";
                Conn.Close();
                if (result != null)
                {
                    viewModel.ID = Convert.ToInt32(result);
                }
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderProduct " + Action + ": " + ex.Message);
            }
        }
        public OrderProductItemMDL OrderProductItemSetUpdate(OrderProductItemMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_OrderProductItem", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@OrderProductId", viewModel.OrderProductId);
                cmd.Parameters.AddWithValue("@ProductId", viewModel.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", viewModel.Quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", viewModel.UnitPrice);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", viewModel.UpdatedBy ?? (object)DBNull.Value);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                string result = returnId != null ? returnId.ToString() : "ID not available";
                Conn.Close();

                if (result != null)
                {
                    viewModel.ID = Convert.ToInt32(result);
                }
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderProductItem " + Action + ": " + ex.Message);
            }
        }
        public List<OrderProductMDL> GetOrderProductList(int? CustomerId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectByCustomerId";
                var list = new List<OrderProductMDL>();
                SqlCommand cmd = new SqlCommand("SP_OrderProduct", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    OrderProductMDL model = new OrderProductMDL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        OrderNo = dr["OrderNo"] == DBNull.Value ? null : (long?)Convert.ToInt64(dr["OrderNo"]),
                        CustomerId = dr["CustomerId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["CustomerId"]),
                        OrderDate = dr["OrderDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["OrderDate"]),
                        Status = dr["Status"]?.ToString(),
                        TotalAmount = dr["TotalAmount"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dr["TotalAmount"]),
                        DeliveryAddress = dr["DeliveryAddress"]?.ToString(),
                        CourierChargeId = dr["CourierChargeId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["CourierChargeId"]),
                        PaymentMethodId = dr["PaymentMethodId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["PaymentMethodId"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"]),
                        InsertedByIP = dr["InsertedByIP"]?.ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["CreatedAt"]),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"]),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"])
                    };
                    list.Add(model);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderProduct List Get: " + ex.Message);
            }
        }
        public List<OrderProductItemMDL> GetOrderProductItemList(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectByOrderProductId";
                var list = new List<OrderProductItemMDL>();
                SqlCommand cmd = new SqlCommand("SP_OrderProductItem", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    OrderProductItemMDL model = new OrderProductItemMDL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        OrderProductId = Convert.ToInt32(dr["OrderProductId"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        ProductPhotoUrl = dr["ProductPhotoUrl"].ToString(),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"]),
                        InsertedByIP = dr["InsertedByIP"]?.ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["CreatedAt"]),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"]),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"])
                    };
                    list.Add(model);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Order Product Item List Get: " + ex.Message);
            }
        }
    }
}
