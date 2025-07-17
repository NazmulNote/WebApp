using System.Data;
using Microsoft.Data.SqlClient;
using WebApp.Areas.Admin.Models;
using WebApp.Helper;
namespace WebApp.Areas.Admin.Data
{
    public class MessageData
    {
        private readonly string _connString;
        public MessageData()
        {
            var configHelper = new ConnHelper();
            _connString = configHelper.GetConnString("DBConn");
        }
        public MessageMDL GetMessage(int? ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectById";
                var viewModel = new MessageMDL();
                SqlCommand cmd = new SqlCommand("SP_Message", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewModel = new MessageMDL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Name = dr["Name"].ToString(),
                        Type = dr["Type"].ToString(),
                        Email = dr["Email"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Body = dr["Body"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"]),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"]),
                        UpdatedBy = Convert.ToInt32(dr["UpdatedBy"])
                    };
                }
                Conn.Close();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Message data get: " + ex.Message);
            }
        }
        public List<MessageMDL> GetMessageList()
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                string Action = "SelectAll";
                var list = new List<MessageMDL>();
                SqlCommand cmd = new SqlCommand("SP_Message", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MessageMDL viewModel = new MessageMDL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Name = dr["Name"].ToString(),
                        Type = dr["Type"].ToString(),
                        Email = dr["Email"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Body = dr["Body"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        InsertId = Convert.ToInt32(dr["InsertId"]),
                        InsertedByIP = dr["InsertedByIP"].ToString(),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"]),
                        UpdatedBy = Convert.ToInt32(dr["UpdatedBy"])
                    };
                    list.Add(viewModel);
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Message list data get: " + ex.Message);
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
        public bool MessageDelete(int ID)
        {
            try
            {
                var Conn = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand("SP_Message", Conn);
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@ID", ID);

                Conn.Open();
                object result = cmd.ExecuteScalar(); // expects SELECT @@ROWCOUNT
                Conn.Close();

                int rowsAffected = Convert.ToInt32(result ?? 0);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Message Delete: " + ex.Message);
            }
        }

    }
}
