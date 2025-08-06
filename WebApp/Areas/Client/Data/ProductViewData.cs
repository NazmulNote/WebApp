using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Areas.Client.Models;
using WebApp.Helper;
namespace WebApp.Areas.Client.Data
{
    public class ProductViewData
    {

        private readonly string _connString;
        public ProductViewData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public List<CatMDL> GetCategoryList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Category";
                var list = new List<CatMDL>();
                SqlCommand cmd = new SqlCommand("SP_ProductView", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CatMDL viewModel = new CatMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["CatName"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        Description = dr["Description"].ToString(),
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Category List data get: " + ex.Message);
            }
        }
        public List<ProductMDL> GetSubCatList(int? CatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SubCatByCat";
                var list = new List<ProductMDL>();
                SqlCommand cmd = new SqlCommand("SP_ProductView", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@CatId",CatId);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductMDL viewModel = new ProductMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        SubCatName = dr["SubCatName"].ToString(),
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Sub Category List data get: " + ex.Message);
            }
        }
        public List<ProductMDL> GetSubChildCatList(int? SubCatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SubChildCatBySubCat";
                var list = new List<ProductMDL>();
                SqlCommand cmd = new SqlCommand("SP_ProductView", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SubCatId", SubCatId);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductMDL viewModel = new ProductMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        SubChildCatName = dr["SubChildCatName"].ToString(),
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Sub Child Category List data get: " + ex.Message);
            }
        }
        public List<ProductMDL> GetProductList(string? Area,int? CatId,int? SubCatId,int? SubChildCatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "ProductList";
                var list = new List<ProductMDL>();
                SqlCommand cmd = new SqlCommand("SP_ProductView", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Area", Area);
                cmd.Parameters.AddWithValue("@CatId", CatId);
                cmd.Parameters.AddWithValue("@SubCatId", SubCatId);
                cmd.Parameters.AddWithValue("@SubChildCatId", SubChildCatId);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductMDL viewModel = new ProductMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        CatId = Convert.ToInt32(dr["CatId"].ToString()),
                        CatName = dr["CatName"].ToString(),
                        SubCatId = dr["SubCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubCatId"].ToString()),
                        SubCatName = dr["SubCatName"].ToString(),
                        SubChildCatId = dr["SubChildCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubChildCatId"].ToString()),
                        SubChildCatName = dr["SubChildCatName"].ToString(),
                        Name = dr["Name"].ToString(),
                        Code = dr["Code"].ToString(),
                        Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString(),
                        Specification = dr["Specification"] == DBNull.Value ? null : dr["Specification"].ToString(),
                        SellingPrice = Convert.ToDecimal(dr["SellingPrice"].ToString()),
                        OfferPercent = Convert.ToDecimal(dr["OfferPercent"].ToString()),
                        OfferPrice = Convert.ToDecimal(dr["OfferPrice"].ToString()),
                        Price = Convert.ToDecimal(dr["Price"].ToString()),
                        PhotoUrl = dr["PhotoUrl"] == DBNull.Value ? null : dr["PhotoUrl"].ToString(),
                        IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"] == DBNull.Value ? null : dr["InsertedByIP"].ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Product List data get: " + ex.Message);
            }
        }
        public ProductMDL GetProduct(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "ProductById";
                var viewModel = new ProductMDL();
                SqlCommand cmd = new SqlCommand("SP_ProductView", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new ProductMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        CatId = Convert.ToInt32(dr["CatId"].ToString()),
                        CatName = dr["CatName"].ToString(),
                        SubCatId = dr["SubCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubCatId"].ToString()),
                        SubCatName = dr["SubCatName"].ToString(),
                        SubChildCatId = dr["SubChildCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubChildCatId"].ToString()),
                        SubChildCatName = dr["SubChildCatName"].ToString(),
                        Name = dr["Name"].ToString(),
                        Code = dr["Code"].ToString(),
                        Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString(),
                        Specification = dr["Specification"] == DBNull.Value ? null : dr["Specification"].ToString(),
                        SellingPrice = Convert.ToDecimal(dr["SellingPrice"].ToString()),
                        OfferPercent = Convert.ToDecimal(dr["OfferPercent"].ToString()),
                        OfferPrice = Convert.ToDecimal(dr["OfferPrice"].ToString()),
                        Price = Convert.ToDecimal(dr["Price"].ToString()),
                        PhotoUrl = dr["PhotoUrl"] == DBNull.Value ? null : dr["PhotoUrl"].ToString(),
                        IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"] == DBNull.Value ? null : dr["InsertedByIP"].ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Product data get" + ex.Message);
            }
        }
    }
}
