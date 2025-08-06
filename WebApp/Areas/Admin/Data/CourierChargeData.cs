using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class CourierChargeData
    {
        private readonly string _connString;
        public CourierChargeData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public CourierChargeMDL CheckCourierCharge(string? Location)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "CheckCC";
                var viewModel = new CourierChargeMDL();
                SqlCommand cmd = new SqlCommand("SP_CourierCharge", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Location", Location);

                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new CourierChargeMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Location = dr["Location"].ToString(),
                        ChargeAmount = Convert.ToInt32(dr["ChargeAmount"]),
                        EstimatedDays = Convert.ToInt32(dr["EstimatedDays"]),
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
                throw new Exception("Error in Courier Charge Check data get" + ex.Message);
            }
        }
        public CourierChargeMDL GetCourierCharge(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new CourierChargeMDL();
                SqlCommand cmd = new SqlCommand("SP_CourierCharge", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new CourierChargeMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Location = dr["Location"].ToString(),
                        ChargeAmount = Convert.ToInt32(dr["ChargeAmount"]),
                        EstimatedDays = Convert.ToInt32(dr["EstimatedDays"]),
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
                throw new Exception("Error in Courier Charge data get" + ex.Message);
            }
        }
        public List<CourierChargeMDL> GetCourierChargeList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<CourierChargeMDL>();
                SqlCommand cmd = new SqlCommand("SP_CourierCharge", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CourierChargeMDL viewModel = new CourierChargeMDL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Location = dr["Location"].ToString(),
                        ChargeAmount = Convert.ToInt32(dr["ChargeAmount"]),
                        EstimatedDays = Convert.ToInt32(dr["EstimatedDays"]),
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
                throw new Exception("Error in Courier Charge List data get" + ex.Message);
            }
        }

        public CourierChargeMDL CourierChargeInsertUpdate(CourierChargeMDL viewModel, string Action)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_CourierCharge", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", viewModel.ID);
                cmd.Parameters.AddWithValue("@Location", viewModel.Location);
                cmd.Parameters.AddWithValue("@ChargeAmount", viewModel.ChargeAmount);
                cmd.Parameters.AddWithValue("@EstimatedDays", viewModel.EstimatedDays);
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
                    throw new Exception("Error in Courier Charge " + Action + ":" + ex.Message);
                }
            }

        }
    }
}
