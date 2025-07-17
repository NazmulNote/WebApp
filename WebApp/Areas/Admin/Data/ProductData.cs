using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class ProductData
    {
        private readonly string _connString;
        public ProductData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public ProductMDL CheckProduct(string? Name,int? CatId, int? SubCatId, int? SubChildCatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "CheckProduct";
                var viewModel = new ProductMDL();
                SqlCommand cmd = new SqlCommand("SP_Product", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@CatId", CatId);
                cmd.Parameters.AddWithValue("@SubCatId", SubCatId);
                cmd.Parameters.AddWithValue("@SubChildCatId", SubChildCatId);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new ProductMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        CatId = Convert.ToInt32(dr["CatId"].ToString()),
                        SubCatId = dr["SubCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubCatId"].ToString()),
                        SubChildCatId = dr["SubChildCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubChildCatId"].ToString()),
                        Name = dr["Name"].ToString(),
                        Code = dr["Code"].ToString(),
                        Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString(),
                        Specification = dr["Specification"] == DBNull.Value ? null : dr["Specification"].ToString(),
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
        public ProductMDL GetProduct(int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new ProductMDL();
                SqlCommand cmd = new SqlCommand("SP_Product", Conn);
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
        public List<ProductMDL> GetProductList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<ProductMDL>();
                SqlCommand cmd = new SqlCommand("SP_Product", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
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
                throw new Exception("Error in Product List data get" + ex.Message);
            }
        }

        public ProductMDL ProductInsertUpdate(ProductMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Product", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@CatId", viewModel.CatId);
                cmd.Parameters.AddWithValue("@SubCatId", viewModel.SubCatId);
                cmd.Parameters.AddWithValue("@SubChildCatId", viewModel.SubChildCatId);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@Code", viewModel.Code);
                cmd.Parameters.AddWithValue("@Description", viewModel.Description);
                cmd.Parameters.AddWithValue("@Specification", viewModel.Specification);
                cmd.Parameters.AddWithValue("@SellingPrice", viewModel.SellingPrice);
                cmd.Parameters.AddWithValue("@OfferPercent", viewModel.OfferPercent);
                cmd.Parameters.AddWithValue("@OfferPrice", viewModel.OfferPrice);
                cmd.Parameters.AddWithValue("@PhotoUrl", viewModel.PhotoUrl);
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
                    throw new Exception("Error in Product " + Action + ":" + ex.Message);
                }
            }

        }
        #region Report--------------------------------------------------------------------
        public List<ProductMDL> GetProductBySubChildCatIdList(int? SubChildCatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "ProductBySubChildCatId";
                var list = new List<ProductMDL>();
                SqlCommand cmd = new SqlCommand("SP_Product", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SubChildCatId", SubChildCatId);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductMDL viewModel = new ProductMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Product List data get by sub child catagory id." + ex.Message);
            }
        }
        #endregion
    }
}
