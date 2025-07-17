using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class ProductPurchaseData
    {
        private readonly string _connString;
        public ProductPurchaseData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public ProductPurchaseMDL CheckProductPurchase(int? ProductId, int? VendorId, string? InvoiceNo)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "CheckProductPurchase";
                var viewModel = new ProductPurchaseMDL();
                SqlCommand cmd = new SqlCommand("SP_ProductPurchase", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new ProductPurchaseMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        ProductId = Convert.ToInt32(dr["ProductId"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        VendorId = Convert.ToInt32(dr["VendorId"].ToString()),
                        VendorName = dr["VendorName"].ToString(),
                        Qty = Convert.ToInt32(dr["Qty"].ToString()),
                        PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"].ToString()),
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"].ToString()),
                        InvoiceNo = dr["InvoiceNo"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ProductPurchase data get" + ex.Message);
            }
        }
        public ProductPurchaseMDL GetProductPurchase(int?ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new ProductPurchaseMDL();
                SqlCommand cmd = new SqlCommand("SP_ProductPurchase", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new ProductPurchaseMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        ProductId = Convert.ToInt32(dr["ProductId"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        VendorId = Convert.ToInt32(dr["VendorId"].ToString()),
                        VendorName = dr["VendorName"].ToString(),
                        Qty = Convert.ToInt32(dr["Qty"].ToString()),
                        PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"].ToString()),
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"].ToString()),
                        InvoiceNo = dr["InvoiceNo"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Product Purchase data get" + ex.Message);
            }
        }
        public List<ProductPurchaseMDL> GetProductPurchaseList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<ProductPurchaseMDL>();
                SqlCommand cmd = new SqlCommand("SP_ProductPurchase", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductPurchaseMDL viewModel = new ProductPurchaseMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        ProductId = Convert.ToInt32(dr["ProductId"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        VendorId = Convert.ToInt32(dr["VendorId"].ToString()),
                        VendorName = dr["VendorName"].ToString(),
                        Qty = Convert.ToInt32(dr["Qty"].ToString()),
                        PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"].ToString()),
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"].ToString()),
                        InvoiceNo = dr["InvoiceNo"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ProductPurchase List data get" + ex.Message);
            }
        }

        public ProductPurchaseMDL ProductPurchaseInsertUpdate(ProductPurchaseMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_ProductPurchase", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@ProductId", viewModel.ProductId);
                cmd.Parameters.AddWithValue("@VendorId", viewModel.VendorId);
                cmd.Parameters.AddWithValue("@Qty", viewModel.Qty);
                cmd.Parameters.AddWithValue("@PurchasePrice", viewModel.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", viewModel.PurchaseDate);
                cmd.Parameters.AddWithValue("@InvoiceNo", viewModel.InvoiceNo);
                cmd.Parameters.AddWithValue("@Remarks", viewModel.Remarks);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP);
                cmd.Parameters.AddWithValue("@CreatedAt", viewModel.CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", viewModel.UpdatedAt);
                cmd.Parameters.AddWithValue("@UpdatedBy", viewModel.UpdatedBy);
                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                string result = returnId?.ToString() ?? "Id Not Available";
                Conn.Close();
                if (result != null)
                {
                    viewModel.ID = Convert.ToInt32(result);
                }
                return viewModel;
            }
            catch (Exception ex)
            {
                {
                    throw new Exception("Error in ProductPurchase " + Action + ":" + ex.Message);
                }
            }

        }
    }
}
