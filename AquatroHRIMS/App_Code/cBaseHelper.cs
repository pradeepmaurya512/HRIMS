using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


/// <summary>
/// Summary description for cBaseHelper
/// </summary>

    public class cBaseHelper
    {
        string _strDbConnection;

        SqlConnection sqlcon;
        SqlCommand sqlcmd;
        SqlDataAdapter sqlAdp;
        DataTable dt;
        DataSet ds;
        int iResult = 0;
        int iIDs = 0;

        public cBaseHelper()
        {
            //
            // TODO: Add constructor logic here
            //
            _strDbConnection = ConfigurationManager.ConnectionStrings["ClientConnection"].ToString();
        }

        public string strDbConnection
        {
            set
            {
                _strDbConnection = value;
            }
            get
            {
                return _strDbConnection;
            }
        }


        public DataSet GenericStatementReturnDataSet(string strQueryString)
        {
            DataSet ds = new DataSet();
            try
            {
                //ds = null;

                OpenConnection();
                sqlcmd = new SqlCommand(strQueryString, sqlcon);
                //sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlAdp = new SqlDataAdapter(sqlcmd);
                sqlAdp.Fill(ds);
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return ds;
        }

        public DataSet GenericStatementReturnDataSet(string strStoredProcedure, SqlParameterCollection sqlparCollection)
        {
            DataSet ds = new DataSet();
            try
            {
                OpenConnection();
                sqlcmd = new SqlCommand(strStoredProcedure, sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in sqlparCollection)
                {
                    sqlcmd.Parameters.AddWithValue(param.ParameterName, param.SqlValue);
                }

                sqlAdp = new SqlDataAdapter(sqlcmd);
                sqlAdp.Fill(ds);
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return ds;
        }


        public DataTable GenericStatementReturnDataTable(string strQueryString)
        {
            DataTable dt1 = new DataTable();
            try
            {
                dt = null;

                OpenConnection();
                sqlcmd = new SqlCommand(strQueryString, sqlcon);
                sqlcmd.CommandType = CommandType.Text;
                //sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlAdp = new SqlDataAdapter(sqlcmd);
                sqlAdp.Fill(dt1);
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return dt1;
        }

        public DataTable GenericStatementReturnDataTable(string strStoredProcedure, SqlParameterCollection sqlparCollection)
        {
            DataTable dt2 = new DataTable();
            try
            {
                dt = null;
                OpenConnection();
                sqlcmd = new SqlCommand(strStoredProcedure, sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in sqlparCollection)
                {
                    sqlcmd.Parameters.AddWithValue(param.ParameterName, param.SqlValue);
                }

                sqlAdp = new SqlDataAdapter(sqlcmd);
                sqlAdp.Fill(dt2);
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return dt2;
        }

        public int GenericStatementReturnRowsAffected(string strQueryString)
        {
            try
            {
                OpenConnection();
                sqlcmd = new SqlCommand(strQueryString, sqlcon);
                sqlcmd.CommandType = CommandType.Text;

                iResult = sqlcmd.ExecuteNonQuery();
                CloseConnection();

            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return iResult;
        }

        public int GenericStatementReturnRowsAffected(string strStoredProcedure, SqlParameterCollection sqlparCollection)
        {
            try
            {
                OpenConnection();
                sqlcmd = new SqlCommand(strStoredProcedure, sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in sqlparCollection)
                {

                    sqlcmd.Parameters.AddWithValue(param.ParameterName, param.Value);

                }
                SqlParameter GetID= sqlcmd.Parameters.Add("iID", SqlDbType.Int);
                GetID.Direction = ParameterDirection.Output;
                iResult = sqlcmd.ExecuteNonQuery();
                 iIDs = Convert.ToInt32(GetID.Value.ToString());

                CloseConnection();

            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return iIDs;
        }
        public int RowCount(string strQueryString)
        {
            try
            {
                OpenConnection();
                sqlcmd = new SqlCommand(strQueryString, sqlcon);
                sqlcmd.CommandType = CommandType.Text;

                iResult = Convert.ToInt32(sqlcmd.ExecuteScalar());
                CloseConnection();

            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return iResult;
        }
        private void CloseConnection()
        {
            try
            {
                if (sqlcon.State != ConnectionState.Closed)
                {
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                cErrorLog.LogError(ex);
            }
        }
        private void OpenConnection()
        {
            try
            {
                sqlcon = new SqlConnection(strDbConnection);
                sqlcon.Open();
            }
            catch (Exception ex)
            {
                cErrorLog.LogError(ex);
            }
        }
        public int GenericStatementBulkInsert(string strStoredProcedure, DataTable dt)
        {
            try
            {
                OpenConnection();
                sqlcmd = new SqlCommand(strStoredProcedure, sqlcon);
                sqlcmd.CommandText = strStoredProcedure;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@tblSkill", SqlDbType.Structured).Value = dt;
                iResult = sqlcmd.ExecuteNonQuery();
                // int iIDs = Convert.ToInt32(GetID.Value.ToString());

                CloseConnection();

            }
            catch (Exception ex)
            {
                CloseConnection();
                cErrorLog.LogError(ex);
            }
            return iResult;
        }   
    }

