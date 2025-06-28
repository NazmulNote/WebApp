using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class AdminUserData
    {
        private readonly string _connString;
        public AdminUserData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public List<AdminUserMDL> GetAdminUserList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Selected";
                var list = new List<AdminUserMDL>();
                SqlCommand cmd = new SqlCommand("SP_AdminUser",Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AdminUserMDL viewModel = new AdminUserMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        EmpID = Convert.ToInt64(dr["EmpID"].ToString()),
                        Name = dr["Name"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        Email = dr["Email"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        Password = dr["PasswordHash"].ToString(),
                        ConfirmPassword = dr["PasswordHash"].ToString(),
                        Role = dr["Role"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        CanInsert = Convert.ToBoolean(dr["CanInsert"].ToString()),
                        CanUpdate = Convert.ToBoolean(dr["CanUpdate"].ToString()),
                        CanDelete = Convert.ToBoolean(dr["CanDelete"].ToString()),
                        IsActive = Convert.ToBoolean(dr["IsActive"].ToString()),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        LastLogin = dr["LastLogin"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["LastLogin"].ToString())
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Admin User Data Get"+ex.Message);
            }
        }
        public AdminUserMDL GetAdminUser(string Email,int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "GetAdminUser";
                var viewModel = new AdminUserMDL();
                SqlCommand cmd = new SqlCommand("SP_AdminUser", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@ID", ID);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                     viewModel = new AdminUserMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        EmpID = Convert.ToInt64(dr["EmpID"].ToString()),
                        Name = dr["Name"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        Email = dr["Email"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        Password = dr["PasswordHash"].ToString(),
                        ConfirmPassword = dr["PasswordHash"].ToString(),
                        Role = dr["Role"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        CanInsert = Convert.ToBoolean(dr["CanInsert"].ToString()),
                        CanUpdate = Convert.ToBoolean(dr["CanUpdate"].ToString()),
                        CanDelete = Convert.ToBoolean(dr["CanDelete"].ToString()),
                        IsActive = Convert.ToBoolean(dr["IsActive"].ToString()),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        LastLogin = dr["LastLogin"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["LastLogin"].ToString())
                    };
                    
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Get Admin User" + ex.Message);
            }
        }
        public AdminUserMDL AdminUserSetUpdate(AdminUserMDL viewModel,string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_AdminUser", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@EmpID", viewModel.EmpID);
                cmd.Parameters.AddWithValue("@UserName", viewModel.UserName);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PhoneNumber", viewModel.PhoneNumber ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PasswordHash", viewModel.ConfirmPassword);
                cmd.Parameters.AddWithValue("@Role", viewModel.Role ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PhotoUrl", viewModel.PhotoUrl);
                cmd.Parameters.AddWithValue("@CanInsert", viewModel.CanInsert);
                cmd.Parameters.AddWithValue("@CanUpdate", viewModel.CanUpdate);
                cmd.Parameters.AddWithValue("@CanDelete", viewModel.CanDelete);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);

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
                throw new Exception("Error in Admin User "+Action+": " + ex.Message);
            }
        }
        public AdminUserMDL AdminUserLogin(string Email, string Password)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Login";
                var viewModel = new AdminUserMDL();
                SqlCommand cmd = new SqlCommand("SP_AdminUser", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Email",Email);
                cmd.Parameters.AddWithValue("@PasswordHash", Password);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) {
                    viewModel = new AdminUserMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        EmpID = Convert.ToInt64(dr["EmpID"].ToString()),
                        Name = dr["Name"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        Email = dr["Email"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        Password = dr["PasswordHash"].ToString(),
                        ConfirmPassword = dr["PasswordHash"].ToString(),
                        Role = dr["Role"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        CanInsert = Convert.ToBoolean(dr["CanInsert"].ToString()),
                        CanUpdate = Convert.ToBoolean(dr["CanUpdate"].ToString()),
                        CanDelete = Convert.ToBoolean(dr["CanDelete"].ToString()),
                        IsActive = Convert.ToBoolean(dr["IsActive"].ToString()),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        LastLogin = dr["LastLogin"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["LastLogin"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;

            }catch(Exception ex) 
            {
                throw new Exception ("Error in Get Admin User Login "+ex.Message);
            }

        }
    }
}
