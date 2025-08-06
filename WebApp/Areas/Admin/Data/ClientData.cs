using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class ClientData
    {
        private readonly string _connString;
        public ClientData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public ClientMDL GetClient(string? Name, int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new ClientMDL();
                SqlCommand cmd = new SqlCommand("SP_Client", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Name", Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", ID);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new ClientMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString()!,
                        ContactPerson = dr["ContactPerson"]?.ToString(),
                        Email = dr["Email"]?.ToString(),
                        Phone = dr["Phone"]?.ToString(),
                        CountryId = dr["CountryId"] == DBNull.Value ? null : Convert.ToInt32(dr["CountryId"]),
                        CountryName = dr["CountryName"]?.ToString(),
                        DistrictId = dr["DistrictId"] == DBNull.Value ? null : Convert.ToInt32(dr["DistrictId"]),
                        DistrictName = dr["DistrictName"]?.ToString(),
                        PoliceStationId = dr["PoliceStationId"] == DBNull.Value ? null : Convert.ToInt32(dr["PoliceStationId"]),
                        PoliceStationName = dr["PoliceStationName"]?.ToString(),
                        Address = dr["Address"]?.ToString(),
                        Website = dr["Website"]?.ToString(),
                        PhotoUrl = dr["PhotoUrl"]?.ToString(),
                        ClientType = dr["ClientType"]?.ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
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
                throw new Exception("Error in Get Client: " + ex.Message);
            }
        }
        public List<ClientMDL> GetClientList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<ClientMDL>();
                SqlCommand cmd = new SqlCommand("SP_Client", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ClientMDL viewModel = new ClientMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString()!,
                        ContactPerson = dr["ContactPerson"]?.ToString(),
                        Email = dr["Email"]?.ToString(),
                        Phone = dr["Phone"]?.ToString(),
                        CountryId = dr["CountryId"] == DBNull.Value ? null : Convert.ToInt32(dr["CountryId"]),
                        CountryName = dr["CountryName"]?.ToString(),
                        DistrictId = dr["DistrictId"] == DBNull.Value ? null : Convert.ToInt32(dr["DistrictId"]),
                        DistrictName = dr["DistrictName"]?.ToString(),
                        PoliceStationId = dr["PoliceStationId"] == DBNull.Value ? null : Convert.ToInt32(dr["PoliceStationId"]),
                        PoliceStationName = dr["PoliceStationName"]?.ToString(),
                        Address = dr["Address"]?.ToString(),
                        Website = dr["Website"]?.ToString(),
                        PhotoUrl = dr["PhotoUrl"]?.ToString(),
                        ClientType = dr["ClientType"]?.ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
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
                throw new Exception("Error in Client Data List Get: " + ex.Message);
            }
        }
        public ClientMDL ClientSetUpdate(ClientMDL viewModel, string? Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Client", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@ContactPerson", viewModel.ContactPerson ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", viewModel.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CountryId", viewModel.CountryId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DistrictId", viewModel.DistrictId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PoliceStationId", viewModel.PoliceStationId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", viewModel.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Website", viewModel.Website ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PhotoUrl", viewModel.PhotoUrl ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ClientType", viewModel.ClientType ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", viewModel.InsertedByIP ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", viewModel.UpdatedBy ?? (object)DBNull.Value);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                string result = returnId != null ? returnId.ToString() : "ID not available";
                Conn.Close();

                if (result != null && int.TryParse(result, out int newId))
                {
                    viewModel.ID = newId;
                }
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Client " + Action + ": " + ex.Message);
            }
        }
        public bool ClientDelete(int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Client", Conn);
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
                throw new Exception("Error in Client Delete: " + ex.Message);
            }
        }

    }
}
