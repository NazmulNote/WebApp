using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Client.Data
{
    public class HomeData
    {
        private readonly string _connString;
        public HomeData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public List<SliderMDL> GetSliderList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "cSelectAll";
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
        public List<TeamMemberMDL> GetTeamMemberList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectForWeb";
                var list = new List<TeamMemberMDL>();

                SqlCommand cmd = new SqlCommand("SP_TeamMember", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TeamMemberMDL viewModel = new TeamMemberMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                        Designation = dr["Designation"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),

                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        PresentAddress = dr["PresentAddress"].ToString(),
                        PermanentAddress = dr["PermanentAddress"].ToString(),

                        NID = dr["NID"].ToString(),
                        EmployeeCode = dr["EmployeeCode"].ToString(),
                        JoinDate = dr["JoinDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["JoinDate"]),
                        DateOfBirth = dr["DateOfBirth"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["DateOfBirth"]),

                        Gender = dr["Gender"].ToString(),
                        BloodGroup = dr["BloodGroup"].ToString(),
                        MaritalStatus = dr["MaritalStatus"].ToString(),
                        Nationality = dr["Nationality"].ToString(),

                        Department = dr["Department"].ToString(),
                        TeamCategory = dr["TeamCategory"].ToString(),
                        Skills = dr["Skills"].ToString(),
                        ShortBio = dr["ShortBio"].ToString(),

                        Facebook = dr["Facebook"].ToString(),
                        LinkedIn = dr["LinkedIn"].ToString(),
                        Twitter = dr["Twitter"].ToString(),

                        ViewOrder = Convert.ToInt32(dr["ViewOrder"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["CreatedAt"]),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"]),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"])
                    };

                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in TeamMember List data get: " + ex.Message);
            }
        }
        public MessageMDL MessageInsertUpdate(MessageMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Message", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@Type", viewModel.Type);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                cmd.Parameters.AddWithValue("@Subject", viewModel.Subject);
                cmd.Parameters.AddWithValue("@Body", viewModel.Body);
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
                throw new Exception("Error in Message " + Action + ": " + ex.Message);
            }
        }
    }
}
