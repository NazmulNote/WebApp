using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class TeamMemberData
    {
        private readonly string _connString;
        public TeamMemberData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public TeamMemberMDL GetTeamMember(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new TeamMemberMDL();
                SqlCommand cmd = new SqlCommand("SP_TeamMember", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID ?? (object)DBNull.Value);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new TeamMemberMDL
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
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in TeamMember data get: " + ex.Message);
            }
        }
        public List<TeamMemberMDL> GetTeamMemberList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
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
        public TeamMemberMDL TeamMemberInsertUpdate(TeamMemberMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_TeamMember", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@Designation", viewModel.Designation);
                cmd.Parameters.AddWithValue("@PhotoUrl", viewModel.PhotoUrl);

                cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                cmd.Parameters.AddWithValue("@Phone", viewModel.Phone);
                cmd.Parameters.AddWithValue("@PresentAddress", viewModel.PresentAddress);
                cmd.Parameters.AddWithValue("@PermanentAddress", viewModel.PermanentAddress);

                cmd.Parameters.AddWithValue("@NID", viewModel.NID);
                cmd.Parameters.AddWithValue("@EmployeeCode", viewModel.EmployeeCode);
                cmd.Parameters.AddWithValue("@JoinDate", (object?)viewModel.JoinDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateOfBirth", (object?)viewModel.DateOfBirth ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@Gender", viewModel.Gender);
                cmd.Parameters.AddWithValue("@BloodGroup", viewModel.BloodGroup);
                cmd.Parameters.AddWithValue("@MaritalStatus", viewModel.MaritalStatus);
                cmd.Parameters.AddWithValue("@Nationality", viewModel.Nationality);

                cmd.Parameters.AddWithValue("@Department", viewModel.Department);
                cmd.Parameters.AddWithValue("@TeamCategory", viewModel.TeamCategory);
                cmd.Parameters.AddWithValue("@Skills", viewModel.Skills);
                cmd.Parameters.AddWithValue("@ShortBio", viewModel.ShortBio);

                cmd.Parameters.AddWithValue("@Facebook", viewModel.Facebook);
                cmd.Parameters.AddWithValue("@LinkedIn", viewModel.LinkedIn);
                cmd.Parameters.AddWithValue("@Twitter", viewModel.Twitter);

                cmd.Parameters.AddWithValue("@ViewOrder", viewModel.ViewOrder);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP);
                cmd.Parameters.AddWithValue("@CreatedAt", (object?)viewModel.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedAt", (object?)viewModel.UpdatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", (object?)viewModel.UpdatedBy ?? DBNull.Value);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                string result = returnId?.ToString() ?? "Id Not Available";
                Conn.Close();

                if (result != null && int.TryParse(result, out int id))
                {
                    viewModel.ID = id;
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in TeamMember " + Action + ": " + ex.Message);
            }
        }
        public bool TeamMemberDelete(int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_TeamMember", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@ID", ID);

                Conn.Open();
                object result = cmd.ExecuteScalar();
                Conn.Close();

                int rowsAffected = Convert.ToInt32(result ?? 0);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in TeamMember Delete: " + ex.Message);
            }
        }
    }
}
