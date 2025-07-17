using System.Data;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class StockData
    {
        private readonly string _connString;
        public StockData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public List<ProductStockMDL> GetProductStockList(DateTime? FromDate,DateTime? ToDate = null, int? CatId = null, int? SubCatId = null, int? SubChildCatId = null, int? ProductId = null)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "ProductStockAll";
                var list = new List<ProductStockMDL>();

                SqlCommand cmd = new SqlCommand("SP_Stock", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FromDate", FromDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", ToDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CatId", CatId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SubCatId", SubCatId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SubChildCatId", SubChildCatId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductId", ProductId ?? (object)DBNull.Value);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductStockMDL viewModel = new ProductStockMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        ProductId = Convert.ToInt32(dr["ProductId"].ToString()),
                        AveragePrice = Convert.ToDecimal(dr["AveragePrice"].ToString()),
                        Quantity = Convert.ToInt32(dr["Quantity"].ToString()),

                        ProductName = dr["ProductName"] == DBNull.Value ? null : dr["ProductName"].ToString(),
                        CatId = Convert.ToInt32(dr["CatId"].ToString()),
                        CatName = dr["CatName"] == DBNull.Value ? null : dr["CatName"].ToString(),

                        SubCatId = dr["SubCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubCatId"].ToString()),
                        SubCatName = dr["SubCatName"] == DBNull.Value ? null : dr["SubCatName"].ToString(),

                        SubChildCatId = dr["SubChildCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubChildCatId"].ToString()),
                        SubChildCatName = dr["SubChildCatName"] == DBNull.Value ? null : dr["SubChildCatName"].ToString()
                    };

                    list.Add(viewModel);
                }

                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Product Stock List data get: " + ex.Message);
            }
        }
        public List<ProductStockMDL> GetProductStockDropDownList(string? Action,int? CatId, int? SubCatId, int? SubChildCatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                var list = new List<ProductStockMDL>();

                SqlCommand cmd = new SqlCommand("SP_Stock", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CatId", CatId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SubCatId", SubCatId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SubChildCatId", SubChildCatId ?? (object)DBNull.Value);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductStockMDL viewModel = new ProductStockMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"] == DBNull.Value ? null : dr["Name"].ToString(),
                    };

                    list.Add(viewModel);
                }

                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Product Stock DropDown List data get: "+Action +" "+ ex.Message);
            }
        }
    }
}
