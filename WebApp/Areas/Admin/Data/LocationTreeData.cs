using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class LocationTreeData
    {
        private readonly string _connString;
        public LocationTreeData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public LocationTreeMDL CheckLocationTree(string? Name, int? PId)
        {
            try
            {
                using var Conn = new SqlConnection(_connString);
                string Action = "CheckLocationTree";
                var viewModel = new LocationTreeMDL();
                using var cmd = new SqlCommand("SP_LocationTree", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@PId", PId);


                Conn.Open();
                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new LocationTreeMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        PId = dr["PId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["PId"].ToString()),
                        Name = dr["Name"].ToString(),
                        Code = dr["Code"] == DBNull.Value ? null : dr["Code"].ToString(),
                        IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"] == DBNull.Value ? null : dr["InsertedByIP"].ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in LocationTree data get: " + ex.Message);
            }
        }
        public LocationTreeMDL GetLocationTree(int ID)
        {
            try
            {
                using var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new LocationTreeMDL();
                using var cmd = new SqlCommand("SP_LocationTree", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);

                Conn.Open();
                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new LocationTreeMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Item = dr["Item"].ToString(),
                        PId = dr["PId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["PId"].ToString()),
                        Name = dr["Name"].ToString(),
                        CountryId = dr["CountryId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["CountryId"].ToString()),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = dr["DistrictId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["DistrictId"].ToString()),
                        DistrictName = dr["DistrictName"].ToString(),
                        Code = dr["Code"] == DBNull.Value ? null : dr["Code"].ToString(),
                        IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"] == DBNull.Value ? null : dr["InsertedByIP"].ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in LocationTree data get: " + ex.Message);
            }
        }
        public List<LocationTreeMDL> GetLocationTreeList(string? Item,int? PId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<LocationTreeMDL>();
                SqlCommand cmd = new SqlCommand("SP_LocationTree", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Item", Item);
                cmd.Parameters.AddWithValue("@PId", PId);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    LocationTreeMDL viewModel = new LocationTreeMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Item = dr["Item"].ToString(),
                        PId = dr["PId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["PId"].ToString()),
                        Name = dr["Name"].ToString(),
                        CountryId = dr["CountryId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["CountryId"].ToString()),
                        CountryName = dr["CountryName"].ToString(),
                        DistrictId = dr["DistrictId"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["DistrictId"].ToString()),
                        DistrictName = dr["DistrictName"].ToString(),
                        Code = dr["Code"] == DBNull.Value ? null : dr["Code"].ToString(),
                        IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"].ToString()),
                        InsertedByIP = dr["InsertedByIP"] == DBNull.Value ? null : dr["InsertedByIP"].ToString(),
                        CreatedAt = dr["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedAt"].ToString()),
                        UpdatedAt = dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"].ToString()),
                        UpdatedBy = dr["UpdatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["UpdatedBy"].ToString())
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in LocationTree List data get: " + ex.Message);
            }
        }
        public LocationTreeMDL LocationTreeInsertUpdate(LocationTreeMDL viewModel, string Action)
        {
            try
            {
                using var Conn = new SqlConnection(_connString);
                using var cmd = new SqlCommand("SP_LocationTree", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Item", viewModel.Item ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PId", (object?)viewModel.PId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Code", (object?)viewModel.Code ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", (object?)viewModel.IsActive ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", (object?)viewModel.InsertedByIP ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", (object?)viewModel.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedAt", (object?)viewModel.UpdatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", (object?)viewModel.UpdatedBy ?? DBNull.Value);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                Conn.Close();

                if (returnId != null && int.TryParse(returnId.ToString(), out int newId))
                {
                    viewModel.ID = newId;
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in LocationTree " + Action + ": " + ex.Message);
            }
        }

    }
}
