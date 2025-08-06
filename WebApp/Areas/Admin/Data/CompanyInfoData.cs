using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;

namespace WebApp.Areas.Admin.Data
{
    public class CompanyInfoData
    {
        private readonly string _connString;
        public CompanyInfoData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public CompanyInfoMDL GetCompanyInfo()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Select";
                var viewModel = new CompanyInfoMDL();
                SqlCommand cmd = new SqlCommand("SP_CompanyInfo", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new CompanyInfoMDL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CompanyName = dr["CompanyName"].ToString(),
                        CompanyCode = dr["CompanyCode"].ToString(),
                        Description = dr["Description"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        Address = dr["Address"].ToString(),
                        Email = dr["Email"].ToString(),
                        EmailPassword = dr["EmailPassword"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        FacebookUrl = dr["FacebookUrl"].ToString(),
                        TwitterUrl = dr["TwitterUrl"].ToString(),
                        LinkedInUrl = dr["LinkedInUrl"].ToString(),
                        YouTubeUrl = dr["YouTubeUrl"].ToString(),
                        Website = dr["Website"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"]),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"]),
                        UpdatedAt = dr["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(dr["UpdatedAt"]) : (DateTime?)null,
                        UpdatedBy = dr["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(dr["UpdatedBy"]) : (int?)null
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CompanyInfo data get: " + ex.Message);
            }
        }
        public CompanyInfoMDL CompanyInfoInsertUpdate(CompanyInfoMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_CompanyInfo", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@CompanyName", viewModel.CompanyName);
                cmd.Parameters.AddWithValue("@CompanyCode", viewModel.CompanyCode);
                cmd.Parameters.AddWithValue("@Description", viewModel.Description);
                cmd.Parameters.AddWithValue("@PhotoUrl", viewModel.PhotoUrl);
                cmd.Parameters.AddWithValue("@Address", viewModel.Address);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                cmd.Parameters.AddWithValue("@EmailPassword", viewModel.EmailPassword);
                cmd.Parameters.AddWithValue("@Phone", viewModel.Phone);
                cmd.Parameters.AddWithValue("@FacebookUrl", viewModel.FacebookUrl);
                cmd.Parameters.AddWithValue("@TwitterUrl", viewModel.TwitterUrl);
                cmd.Parameters.AddWithValue("@LinkedInUrl", viewModel.LinkedInUrl);
                cmd.Parameters.AddWithValue("@YouTubeUrl", viewModel.YouTubeUrl);
                cmd.Parameters.AddWithValue("@Website", viewModel.Website);
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
                throw new Exception("Error in CompanyInfo " + Action + ": " + ex.Message);
            }
        }
    }
}
