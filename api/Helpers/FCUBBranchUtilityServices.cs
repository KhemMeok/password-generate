using ITOAPP_API.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CoreFunction;

namespace ITOAPP_API.Helpers
{
    public class FCUBBranchUtilityServices
    {
        public static async Task<DataTable> GetallBranches()
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY1");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "BRANCH_UTILITY.PR_GET_ALL_BRANCHES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("BR_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<DataTable>(dt);
        }
        public static async Task<BranchInfoModel> GetBranches(string branch_code)
        {
            BranchInfoModel BI = new BranchInfoModel();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY1");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            
            try
            {
                conn.Open();
                cmd.CommandText = "BRANCH_UTILITY.PR_GET_BRANCH";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_BRANCH_CODE", OracleDbType.Varchar2).Value = branch_code;
                cmd.Parameters.Add("OP_BRANCH_CODE", OracleDbType.Varchar2,3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_BRANCH_NAME", OracleDbType.Varchar2,105).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_BRANCH_DATE", OracleDbType.Varchar2,11).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                BI.branch_code = cmd.Parameters["OP_BRANCH_CODE"].Value.ToString();
                BI.branch_name = cmd.Parameters["OP_BRANCH_NAME"].Value.ToString();
                BI.branch_date = cmd.Parameters["OP_BRANCH_DATE"].Value.ToString();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<BranchInfoModel>(BI);
        }
    }
}
