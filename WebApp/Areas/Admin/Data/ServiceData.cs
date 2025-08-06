using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class ServiceData
    {
        private readonly string _connString;
        public ServiceData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public ServiceMDL ServiceCheck(int? ServiceCatId, string? Name)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "ServiceCheck";
                var viewModel = new ServiceMDL();
                SqlCommand cmd = new SqlCommand("sp_Service", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ServiceCatId", ServiceCatId);
                cmd.Parameters.AddWithValue("@Name", Name);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new ServiceMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        ServiceCatId = Convert.ToInt32(dr["ServiceCatId"].ToString()),
                        ServiceCat = dr["ServiceCat"].ToString(),
                        Name = dr["Name"].ToString(),
                        ShortDesc = dr["ShortDesc"].ToString(),
                        FullDesc = dr["FullDesc"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        VideoUrl = dr["VideoUrl"].ToString(),
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
                throw new Exception("Error in Service data get: " + ex.Message);
            }
        }
        public ServiceMDL GetService(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new ServiceMDL();
                SqlCommand cmd = new SqlCommand("sp_Service", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new ServiceMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        ServiceCatId = Convert.ToInt32(dr["ServiceCatId"].ToString()),
                        ServiceCat = dr["ServiceCat"].ToString(),
                        Name = dr["Name"].ToString(),
                        ShortDesc = dr["ShortDesc"].ToString(),
                        FullDesc = dr["FullDesc"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        VideoUrl = dr["VideoUrl"].ToString(),
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
                throw new Exception("Error in Service data get: " + ex.Message);
            }
        }
        public List<ServiceMDL> GetServiceList(int? ServiceCatId)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<ServiceMDL>();
                SqlCommand cmd = new SqlCommand("sp_Service", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ServiceCatId", ServiceCatId);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ServiceMDL viewModel = new ServiceMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        ServiceCatId = Convert.ToInt32(dr["ServiceCatId"].ToString()),
                        ServiceCat = dr["ServiceCat"].ToString(),
                        Name = dr["Name"].ToString(),
                        ShortDesc = dr["ShortDesc"].ToString(),
                        FullDesc = dr["FullDesc"].ToString(),
                        PhotoUrl = dr["PhotoUrl"].ToString(),
                        VideoUrl = dr["VideoUrl"].ToString(),
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
                throw new Exception("Error in Service List data get: " + ex.Message);
            }
        }
        public ServiceMDL ServiceInsertUpdate(ServiceMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("sp_Service", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@ServiceCatId", viewModel.ServiceCatId);
                cmd.Parameters.AddWithValue("@Name", viewModel.Name);
                cmd.Parameters.AddWithValue("@ShortDesc", (object?)viewModel.ShortDesc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FullDesc", (object?)viewModel.FullDesc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PhotoUrl", (object?)viewModel.PhotoUrl ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@VideoUrl", (object?)viewModel.VideoUrl ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", (object?)viewModel.IsActive ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@InsertId", viewModel.InsertId);
                cmd.Parameters.AddWithValue("@InsertedByIP", (object?)viewModel.InsertedByIP ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", (object?)viewModel.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedAt", (object?)viewModel.UpdatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", (object?)viewModel.UpdatedBy ?? DBNull.Value);

                Conn.Open();
                object returnId = cmd.ExecuteScalar();
                string result = returnId?.ToString() ?? "Id Not Available";
                Conn.Close();

                if (int.TryParse(result, out int newId))
                {
                    viewModel.ID = newId;
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Service " + Action + ": " + ex.Message);
            }
        }
        public bool ServiceDelete(int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("sp_Service", Conn);
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
                throw new Exception("Error in Service Delete: " + ex.Message);
            }
        }
        public List<ServiceMDL> ServiceCat()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "ServiceCat";
                var list = new List<ServiceMDL>();
                SqlCommand cmd = new SqlCommand("sp_Service", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ServiceMDL viewModel = new ServiceMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Service Cat List data get: " + ex.Message);
            }
        }

    }
}
