using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class AboutPageData
    {
        private readonly string _connString;
        public AboutPageData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public AboutPageMDL GetAboutPageInfo()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Select";
                var viewModel = new AboutPageMDL();
                SqlCommand cmd = new SqlCommand("SP_AboutPage", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new AboutPageMDL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        SliderPhotoUrl = dr["SliderPhotoUrl"].ToString(),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        AboutPhotoUrl = dr["AboutPhotoUrl"].ToString(),
                        Vision = dr["Vision"].ToString(),
                        Mission = dr["Mission"].ToString(),
                        Achievements = dr["Achievements"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"]),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = dr["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(dr["CreatedAt"]) : (DateTime?)null,
                        UpdatedAt = dr["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(dr["UpdatedAt"]) : (DateTime?)null,
                        UpdatedBy = dr["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(dr["UpdatedBy"]) : (int?)null
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in About Page Info data get: " + ex.Message);
            }
        }
        public AboutPageMDL AboutPageInsertUpdate(AboutPageMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_AboutPage", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@SliderPhotoUrl", viewModel.SliderPhotoUrl ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Title", viewModel.Title ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", viewModel.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AboutPhotoUrl", viewModel.AboutPhotoUrl ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Vision", viewModel.Vision ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Mission", viewModel.Mission ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Achievements", viewModel.Achievements ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", viewModel.CreatedAt ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedAt", viewModel.UpdatedAt ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", viewModel.UpdatedBy ?? (object)DBNull.Value);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                Conn.Close();

                if (returnId != null && int.TryParse(returnId.ToString(), out int id))
                {
                    viewModel.ID = id;
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AboutPage " + Action + ": " + ex.Message);
            }
        }

    }
}
