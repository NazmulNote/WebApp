using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class CustomerData
    {
        private readonly string _connString;
        public CustomerData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public List<CustomerMDL> GetCustomerList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<CustomerMDL>();
                SqlCommand cmd = new SqlCommand("SP_Customer", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CustomerMDL viewModel = new CustomerMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString()!,
                        Email = dr["Email"]?.ToString(),
                        Phone = dr["Phone"]?.ToString(),
                        CountryId = dr["CountryId"] == DBNull.Value ? null : Convert.ToInt32(dr["CountryId"]),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = dr["DistrictId"] == DBNull.Value ? null : Convert.ToInt32(dr["DistrictId"]),
                        DistrictName = dr["DistrictName"].ToString(),
                        PoliceStationId = dr["PoliceStationId"] == DBNull.Value ? null : Convert.ToInt32(dr["PoliceStationId"]),
                        PoliceStationName = dr["PoliceStationName"].ToString(),
                        Address = dr["Address"]?.ToString(),
                        PhotoUrl = dr["PhotoUrl"]?.ToString(),
                        Password = dr["Password"].ToString()!,
                        ConfirmPassword = dr["Password"].ToString()!,
                        IsActive = Convert.ToBoolean(dr["IsActive"].ToString()),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"]?.ToString(),
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
                throw new Exception("Error in Customer Data List Get: " + ex.Message);
            }
        }
        public CustomerMDL GetCustomer(string Email, int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new CustomerMDL();
                SqlCommand cmd = new SqlCommand("SP_Customer", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@ID", ID);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new CustomerMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString()!,
                        Email = dr["Email"]?.ToString(),
                        Phone = dr["Phone"]?.ToString(),
                        CountryId = dr["CountryId"] == DBNull.Value ? null : Convert.ToInt32(dr["CountryId"]),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = dr["DistrictId"] == DBNull.Value ? null : Convert.ToInt32(dr["DistrictId"]),
                        DistrictName = dr["DistrictName"].ToString(),
                        PoliceStationId = dr["PoliceStationId"] == DBNull.Value ? null : Convert.ToInt32(dr["PoliceStationId"]),
                        PoliceStationName = dr["PoliceStationName"].ToString(),
                        Address = dr["Address"]?.ToString(),
                        PhotoUrl = dr["PhotoUrl"]?.ToString(),
                        Password = dr["Password"].ToString()!,
                        ConfirmPassword = dr["Password"].ToString()!,
                        IsActive = Convert.ToBoolean(dr["IsActive"].ToString()),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"]?.ToString(),
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
                throw new Exception("Error in Get Customer: " + ex.Message);
            }
        }
        public CustomerMDL CustomerSetUpdate(CustomerMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Customer", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", viewModel.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CountryId", viewModel.CountryId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DistrictId", viewModel.DistrictId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PoliceStationId", viewModel.PoliceStationId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", viewModel.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PhotoUrl", viewModel.PhotoUrl ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Password", viewModel.Password);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", viewModel.UpdatedBy ?? (object)DBNull.Value);

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
                throw new Exception("Error in Customer " + Action + ": " + ex.Message);
            }
        }
        public CustomerMDL CustomerLogin(string Email, string Password)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "Login";
                var viewModel = new CustomerMDL();
                SqlCommand cmd = new SqlCommand("SP_Customer", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new CustomerMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString()!,
                        Email = dr["Email"]?.ToString(),
                        Phone = dr["Phone"]?.ToString(),
                        CountryId = dr["CountryId"] == DBNull.Value ? null : Convert.ToInt32(dr["CountryId"]),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = dr["DistrictId"] == DBNull.Value ? null : Convert.ToInt32(dr["DistrictId"]),
                        DistrictName = dr["DistrictName"].ToString(),
                        PoliceStationId = dr["PoliceStationId"] == DBNull.Value ? null : Convert.ToInt32(dr["PoliceStationId"]),
                        PoliceStationName = dr["PoliceStationName"].ToString(),
                        Address = dr["Address"]?.ToString(),
                        PhotoUrl = dr["PhotoUrl"]?.ToString(),
                        Password = dr["Password"].ToString()!,
                        ConfirmPassword = dr["Password"].ToString()!,
                        IsActive = Convert.ToBoolean(dr["IsActive"].ToString()),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"]?.ToString(),
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
                throw new Exception("Error in Get Customer Login: " + ex.Message);
            }
        }
    }
}
