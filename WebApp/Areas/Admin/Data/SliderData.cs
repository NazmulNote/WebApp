using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;

namespace WebApp.Areas.Admin.Data
{
    public class SliderData
    {
        private readonly string _connString;
        public SliderData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public SliderMDL GetSlider(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new SliderMDL();
                SqlCommand cmd = new SqlCommand("SP_Slider", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new SliderMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
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
                throw new Exception("Error in Slider data get: " + ex.Message);
            }
        }
        public List<SliderMDL> GetSliderList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<SliderMDL>();
                SqlCommand cmd = new SqlCommand("SP_Slider", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SliderMDL viewModel = new SliderMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
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
                throw new Exception("Error in Slider List data get: " + ex.Message);
            }
        }
        public SliderMDL SliderInsertUpdate(SliderMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Slider", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Title", viewModel.Title);
                cmd.Parameters.AddWithValue("@Description", viewModel.Description);
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
                throw new Exception("Error in Slider " + Action + ": " + ex.Message);
            }
        }
        public bool SliderDelete(int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Slider", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@ID", ID);

                Conn.Open();
                object result = cmd.ExecuteScalar(); // changed line
                Conn.Close();

                int rowsAffected = Convert.ToInt32(result ?? 0); // handle null
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Slider Delete: " + ex.Message);
            }
        }

    }
}
