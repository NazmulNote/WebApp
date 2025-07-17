using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class VendorData
    {
        private readonly string _connString;
        public VendorData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public VendorMDL CheckVendor(string? Name, string? Phone)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "CheckVendor";
                var viewModel = new VendorMDL();
                SqlCommand cmd = new SqlCommand("SP_Vendor", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Name",Name);
                cmd.Parameters.AddWithValue("@Phone", Phone);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new VendorMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                        ContactPerson = dr["ContactPerson"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Email = dr["Email"].ToString(),
                        CountryId = Convert.ToInt32(dr["CountryId"].ToString()),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = Convert.ToInt32(dr["DistrictId"].ToString()),
                        DistrictName = dr["DistrictName"].ToString(),
                        PoliceStationId = Convert.ToInt32(dr["PoliceStationId"].ToString()),
                        PoliceStationName = dr["PoliceStationName"].ToString(),
                        Address = dr["Address"].ToString(),
                        TIN = dr["TIN"].ToString(),
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
                throw new Exception("Error in Vendor Check data get" + ex.Message);
            }
        }
        public VendorMDL GetVendor(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new VendorMDL();
                SqlCommand cmd = new SqlCommand("SP_Vendor", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new VendorMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                        ContactPerson = dr["ContactPerson"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Email = dr["Email"].ToString(),
                        CountryId = Convert.ToInt32(dr["CountryId"].ToString()),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = Convert.ToInt32(dr["DistrictId"].ToString()),
                        DistrictName = dr["DistrictName"].ToString(),
                        PoliceStationId = Convert.ToInt32(dr["PoliceStationId"].ToString()),
                        PoliceStationName = dr["PoliceStationName"].ToString(),
                        Address = dr["Address"].ToString(),
                        TIN = dr["TIN"].ToString(),
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
                throw new Exception("Error in Vendor data get" + ex.Message);
            }
        }
        public List<VendorMDL> GetVendorList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<VendorMDL>();
                SqlCommand cmd = new SqlCommand("SP_Vendor", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    VendorMDL viewModel = new VendorMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                        ContactPerson = dr["ContactPerson"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Email = dr["Email"].ToString(),
                        CountryId = Convert.ToInt32(dr["CountryId"].ToString()),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = Convert.ToInt32(dr["DistrictId"].ToString()),
                        DistrictName = dr["DistrictName"].ToString(),
                        PoliceStationId = Convert.ToInt32(dr["PoliceStationId"].ToString()),
                        PoliceStationName = dr["PoliceStationName"].ToString(),
                        Address = dr["Address"].ToString(),
                        TIN = dr["TIN"].ToString(),
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
                throw new Exception("Error in Vendor List data get" + ex.Message);
            }
        }

        public VendorMDL VendorInsertUpdate(VendorMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Vendor", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@ContactPerson", viewModel.ContactPerson);
                cmd.Parameters.AddWithValue("@Phone", viewModel.Phone);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                cmd.Parameters.AddWithValue("@CountryId", viewModel.CountryId);
                cmd.Parameters.AddWithValue("@DistrictId", viewModel.DistrictId);
                cmd.Parameters.AddWithValue("@PoliceStationId", viewModel.PoliceStationId);
                cmd.Parameters.AddWithValue("@Address", viewModel.Address);
                cmd.Parameters.AddWithValue("@TIN", viewModel.TIN);
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
                {
                    throw new Exception("Error in Vendor " + Action + ":" + ex.Message);
                }
            }

        }
    }
}
