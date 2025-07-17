using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class SiteInfoData
    {
        private readonly string _connString;
        public SiteInfoData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public SiteInfoMDL GetSiteInfo()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Select";
                var viewModel = new SiteInfoMDL();
                SqlCommand cmd = new SqlCommand("SP_SiteInfo", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new SiteInfoMDL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        HAboutTitle = dr["HAboutTitle"].ToString(),
                        HAboutDescription = dr["HAboutDescription"].ToString(),
                        HAboutPhotoUrl = dr["HAboutPhotoUrl"].ToString(),
                        HAboutUrl = dr["HAboutUrl"].ToString(),
                        ContactSliderUrl = dr["ContactSliderUrl"].ToString(),
                        ContactShortDesc = dr["ContactShortDesc"].ToString(),
                        ContactMap = dr["ContactMap"].ToString(),
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
                throw new Exception("Error in Home Page Info data get: " + ex.Message);
            }
        }
        public SiteInfoMDL SiteInfoInsertUpdate(SiteInfoMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_SiteInfo", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@HAboutTitle", viewModel.HAboutTitle);
                cmd.Parameters.AddWithValue("@HAboutDescription", viewModel.HAboutDescription);
                cmd.Parameters.AddWithValue("@HAboutPhotoUrl", viewModel.HAboutPhotoUrl);
                cmd.Parameters.AddWithValue("@HAboutUrl", viewModel.HAboutUrl);
                cmd.Parameters.AddWithValue("@ContactSliderUrl", viewModel.ContactSliderUrl);
                cmd.Parameters.AddWithValue("@ContactShortDesc", viewModel.ContactShortDesc);
                cmd.Parameters.AddWithValue("@ContactMap", viewModel.ContactMap);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP);
                cmd.Parameters.AddWithValue("@CreatedAt", viewModel.CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", viewModel.UpdatedAt);
                cmd.Parameters.AddWithValue("@UpdatedBy", viewModel.UpdatedBy);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                string result = returnId?.ToString() ?? "0";
                Conn.Close();

                if (!string.IsNullOrEmpty(result))
                {
                    viewModel.ID = Convert.ToInt32(result);
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in SiteInfo " + Action + ": " + ex.Message);
            }
        }
    }
}
