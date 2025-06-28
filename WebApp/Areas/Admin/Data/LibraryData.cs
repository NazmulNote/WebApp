using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;

namespace WebApp.Areas.Admin.Data
{
    public class LibraryData
    {
        private readonly string _connString;
        public LibraryData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public List<LibraryMDL> GetLibraryList(string Item,int? CategoryId,int? SubCatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<LibraryMDL>();
                SqlCommand cmd = new SqlCommand("SP_Library",Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item", Item);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("@SubCatId", SubCatId);
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    LibraryMDL viewModel = new LibraryMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Item = dr["Item"].ToString(),
                        CategoryId = dr["CategoryId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["CategoryId"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        SubCatId = dr["SubCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubCatId"].ToString()),
                        SubCatName = dr["SubCatName"].ToString(),
                        SubChildCatId = dr["SubChildCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubChildCatId"].ToString()),
                        SubChildCatName = dr["SubChildCatName"].ToString(),
                        ItemName = dr["ItemName"].ToString(),
                        ItemCode = dr["ItemCode"].ToString(),
                        Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString(),
                        PhotoUrl = dr["PhotoUrl"] == DBNull.Value ? null : dr["PhotoUrl"].ToString(),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"].ToString())
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }catch(Exception ex)
            {
                throw new Exception("Error in Library List data get" + ex.Message);
            }
        }
        public LibraryMDL GetLibrary(string Item,int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new LibraryMDL();
                SqlCommand cmd = new SqlCommand("SP_Library",Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item", Item);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                     viewModel = new LibraryMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Item = dr["Item"].ToString(),
                         CategoryId = dr["CategoryId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["CategoryId"].ToString()),
                         CategoryName = dr["CategoryName"].ToString(),
                         SubCatId = dr["SubCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubCatId"].ToString()),
                         SubCatName = dr["SubCatName"].ToString(),
                         SubChildCatId = dr["SubChildCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubChildCatId"].ToString()),
                         SubChildCatName = dr["SubChildCatName"].ToString(),
                         ItemName = dr["ItemName"].ToString(),
                        ItemCode = dr["ItemCode"].ToString(),
                        Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString(),
                        PhotoUrl = dr["PhotoUrl"] == DBNull.Value ? null : dr["PhotoUrl"].ToString(),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Library data get" + ex.Message);
            }
        }
        public LibraryMDL CheckLibrary(string Item,int? CategoryId,int? SubCatId,string ItemName)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "CheckLibrary";
                var viewModel = new LibraryMDL();
                SqlCommand cmd = new SqlCommand("SP_Library", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item", Item);
                cmd.Parameters.AddWithValue("CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("@SubCatId", SubCatId);
                cmd.Parameters.AddWithValue("@ItemName", ItemName);
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new LibraryMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Item = dr["Item"].ToString(),
                        CategoryId = dr["CategoryId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["CategoryId"].ToString()),
                        SubCatId = dr["SubCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubCatId"].ToString()),
                        SubChildCatId = dr["SubChildCatId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["SubChildCatId"].ToString()),
                        ItemName = dr["ItemName"].ToString(),
                        ItemCode = dr["ItemCode"].ToString(),
                        Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString(),
                        PhotoUrl = dr["PhotoUrl"] == DBNull.Value ? null : dr["PhotoUrl"].ToString(),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Library data get" + ex.Message);
            }
        }
        public LibraryMDL LibraryInsertUpdate(LibraryMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Library", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item", viewModel.Item);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@CategoryId", (object?)viewModel.CategoryId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubCatId", (object?)viewModel.SubCatId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubChildCatId", (object?)viewModel.SubChildCatId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ItemName", viewModel.ItemName);
                cmd.Parameters.AddWithValue("@ItemCode", viewModel.ItemCode);
                cmd.Parameters.AddWithValue("@Description", (object?)viewModel.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PhotoUrl", (object?)viewModel.PhotoUrl ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@CreatedAt", viewModel.CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", (object?)viewModel.UpdatedAt ?? DBNull.Value);

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
                throw new Exception("Error in Library " + Action + ": " + ex.Message);
            }
        }
        public LibraryMDL LibraryDelete(int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Delete";
                LibraryMDL viewModel = new LibraryMDL();
                SqlCommand cmd = new SqlCommand("SP_Library", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);

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
                throw new Exception("Error in Library  Delete" + ex.Message);
            }
        }
    }
}
