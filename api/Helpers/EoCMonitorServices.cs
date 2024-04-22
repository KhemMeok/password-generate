using CoreFunction;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static ITOAPP_API.Models.EoCMonitoringModel;

namespace ITOAPP_API.Helpers
{
    public class EoCMonitorServices
    {
        public static async Task<ResParamConfig> GetParamConfig()
        {
            ResParamConfig Res = new ResParamConfig();
            DataParamConfig data = new DataParamConfig();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_GET_PARAM_CONFIG";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_TODAY_DATE", OracleDbType.Varchar2, 11).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_NEXWORKING_DATE", OracleDbType.Varchar2, 11).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_NEXT_NEXTWORKING_DATE", OracleDbType.Varchar2, 11).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                data.today_date= cmd.Parameters["OP_TODAY_DATE"].Value.ToString();
                data.nextworking_day_date= cmd.Parameters["OP_NEXWORKING_DATE"].Value.ToString();
                data.next_nextworking_day_date = cmd.Parameters["OP_NEXT_NEXTWORKING_DATE"].Value.ToString();
                Res.data = data;
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResParamConfig>(Res);
        }
        public static async Task<ResEoDSummary>GetEoDSummary(ReqEoDSummary param)
        {
            ResEoDSummary Res = new ResEoDSummary();
            DataEoDSummary data = new DataEoDSummary();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_EOD_SUMMARY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TODAY_DATE", OracleDbType.Varchar2).Value = param.today_date.ToString();
                cmd.Parameters.Add("P_NEXTWORKING_DATE", OracleDbType.Varchar2).Value = param.nextworking_day_date.ToString();
                cmd.Parameters.Add("P_TARGET_STAGE", OracleDbType.Varchar2).Value = param.target_stage.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_TOT_BRANCHS", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_TOT_FIN_EOD", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_TOT_FIN_EODM", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_TOT_NOT_FIN_EODM", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_TOT_FAILED_EODM", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_TOT_PULLED_GL_BAL", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MISMATCH_GL_BAL", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_REAL_DEBUG_STAT", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                data.total_branches = cmd.Parameters["OP_TOT_BRANCHS"].Value.ToString();
                data.total_finished_eod = cmd.Parameters["OP_TOT_FIN_EOD"].Value.ToString();
                data.total_finished_eodm = cmd.Parameters["OP_TOT_FIN_EODM"].Value.ToString();
                data.total_not_finished_eodm = cmd.Parameters["OP_TOT_NOT_FIN_EODM"].Value.ToString();
                data.total_failed_eodm = cmd.Parameters["OP_TOT_FAILED_EODM"].Value.ToString();
                data.total_pulled_gl_bal = cmd.Parameters["OP_TOT_PULLED_GL_BAL"].Value.ToString();
                data.mismatch_gl_bal = cmd.Parameters["OP_MISMATCH_GL_BAL"].Value.ToString();
                data.real_debug_enabled = cmd.Parameters["OP_REAL_DEBUG_STAT"].Value.ToString();
                Res.data = data;
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResEoDSummary>(Res);
        }
        public static async Task<ResRunAbleBranches> GetRunAbleBranches(ReqRunAbleBranches param)
        {
            ResRunAbleBranches Res = new ResRunAbleBranches();
            List<DataRunAbleBranches> data = new List<DataRunAbleBranches>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_BRANCH_TO_RUN";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TO_STAGE", OracleDbType.Varchar2).Value = param.target_stage.ToString();
                cmd.Parameters.Add("P_TODAY_DATE", OracleDbType.Varchar2).Value = param.today_date.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRunAbleBranches d = new DataRunAbleBranches();
                        d.group_code = dr[0].ToString();
                        d.branch_code = dr[1].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                

            }
            return await Task.FromResult<ResRunAbleBranches>(Res);
        }
        public static async Task<ResEoDM> GetFinishEoDMBranches(ReqFinishEoDM param)
        {
            ResEoDM Res = new ResEoDM();
            List<DataEoDM> data = new List<DataEoDM>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_BRANCH_FIN_EODM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TODAY_DATE", OracleDbType.Varchar2).Value = param.today_date.ToString();
                cmd.Parameters.Add("P_NEXTWORKING_DATE", OracleDbType.Varchar2).Value = param.nextworking_day_date.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataEoDM d = new DataEoDM();
                        d.group_code = dr[0].ToString();
                        d.branch_code = dr[1].ToString();
                        d.current_posting_date = dr[2].ToString();
                        d.next_posting_date = dr[3].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResEoDM>(Res);
        }
        public static async Task<ResEoDM> GetNotFinishEoDMBranches(ReqNotFinishEoDM param)
        {
            ResEoDM Res = new ResEoDM();
            List<DataEoDM> data = new List<DataEoDM>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_BRANCH_NOT_FIN_EODM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_NEXTWORKING_DATE", OracleDbType.Varchar2).Value = param.nextworking_day_date.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataEoDM d = new DataEoDM();
                        d.group_code = dr[0].ToString();
                        d.branch_code = dr[1].ToString();
                        d.current_posting_date = dr[2].ToString();
                        d.next_posting_date = dr[3].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResEoDM>(Res);
        }
        public static async Task<ResEoDM> GetFailedEoDMBranches(ReqFailedEoDM param)
        {
            ResEoDM Res = new ResEoDM();
            List<DataEoDM> data = new List<DataEoDM>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_BRANCH_FAILED_EODM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TODAY_DATE", OracleDbType.Varchar2).Value = param.today_date.ToString();
                cmd.Parameters.Add("P_NEXTWORKING_DATE", OracleDbType.Varchar2).Value = param.nextworking_day_date.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataEoDM d = new DataEoDM();
                        d.group_code = dr[0].ToString();
                        d.branch_code = dr[1].ToString();
                        d.current_posting_date = dr[2].ToString();
                        d.next_posting_date = dr[3].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResEoDM>(Res);
        }
        public static async Task<ResFinishEoC> GetFinishEoC(ReqFinishEoC param)
        {
            ResFinishEoC Res = new ResFinishEoC();
            List<DataEocBranches> data = new List<DataEocBranches>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_BRANCH_FIN_EOC";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TODAY_DATE", OracleDbType.Varchar2).Value = param.today_date.ToString();
                cmd.Parameters.Add("P_TARGET_STAGE", OracleDbType.Varchar2).Value = param.target_stage.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataEocBranches d = new DataEocBranches();
                        d.group_code = dr[0].ToString();
                        d.eoc_status = dr[1].ToString();
                        d.branch_code = dr[2].ToString();
                        d.eod_date = dr[3].ToString();
                        d.branch_date = dr[4].ToString();
                        d.target_stage = dr[5].ToString();
                        d.running_stage = dr[6].ToString();
                        d.current_stage = dr[7].ToString();
                        d.eoc_ref_no = dr[8].ToString();
                        d.error_code = dr[9].ToString();
                        d.error_param = dr[10].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResFinishEoC>(Res);
        }
        public static async Task<ResSubmitBranches> GetSubmitBranches()
        {
            ResSubmitBranches Res = new ResSubmitBranches();
            DataSubmitBranches data = new DataSubmitBranches();
            List<DataEocBranches> running_branches = new List<DataEocBranches>();
            List<DataEocBranches> queue_branches = new List<DataEocBranches>();
            List<DataEocBranches> aborted_branches = new List<DataEocBranches>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter1 = new OracleDataAdapter();
            OracleDataAdapter adapter2 = new OracleDataAdapter();
            OracleDataAdapter adapter3 = new OracleDataAdapter();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_SUBMITTED_BRANCHES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_RUNNING", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_QUEUE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_ABORTED", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STAGE_RUNNING", OracleDbType.Varchar2,100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_CHECK_RUNNING", OracleDbType.Varchar2,100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    data.running_stage = cmd.Parameters["OP_STAGE_RUNNING"].Value.ToString();
                    data.running_count = cmd.Parameters["OP_CHECK_RUNNING"].Value.ToString();

                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_DATA_RUNNING"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_DATA_QUEUE"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_DATA_ABORTED"].Value;

                    adapter1.Fill(ds1, c1);
                    adapter2.Fill(ds2, c2);
                    adapter3.Fill(ds3, c3);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];

                    foreach (DataRow dr in dt1.Rows)
                    {
                        DataEocBranches d = new DataEocBranches();
                        d.group_code = dr[0].ToString();
                        d.eoc_status = dr[1].ToString();
                        d.branch_code = dr[2].ToString();
                        d.eod_date = dr[3].ToString();
                        d.branch_date = dr[4].ToString();
                        d.target_stage = dr[5].ToString();
                        d.running_stage = dr[6].ToString();
                        d.current_stage = dr[7].ToString();
                        d.eoc_ref_no = dr[8].ToString();
                        d.error_code = dr[9].ToString();
                        d.error_param = dr[10].ToString();
                        running_branches.Add(d);
                    }
                    foreach (DataRow dr in dt2.Rows)
                    {
                        DataEocBranches d = new DataEocBranches();
                        d.group_code = dr[0].ToString();
                        d.eoc_status = dr[1].ToString();
                        d.branch_code = dr[2].ToString();
                        d.eod_date = dr[3].ToString();
                        d.branch_date = dr[4].ToString();
                        d.target_stage = dr[5].ToString();
                        d.running_stage = dr[6].ToString();
                        d.current_stage = dr[7].ToString();
                        d.eoc_ref_no = dr[8].ToString();
                        d.error_code = dr[9].ToString();
                        d.error_param = dr[10].ToString();
                        queue_branches.Add(d);
                    }
                    foreach (DataRow dr in dt3.Rows)
                    {
                        DataEocBranches d = new DataEocBranches();
                        d.group_code = dr[0].ToString();
                        d.eoc_status = dr[1].ToString();
                        d.branch_code = dr[2].ToString();
                        d.eod_date = dr[3].ToString();
                        d.branch_date = dr[4].ToString();
                        d.target_stage = dr[5].ToString();
                        d.running_stage = dr[6].ToString();
                        d.current_stage = dr[7].ToString();
                        d.eoc_ref_no = dr[8].ToString();
                        d.error_code = dr[9].ToString();
                        d.error_param = dr[10].ToString();
                        aborted_branches.Add(d);
                    }
                    data.running_branch = running_branches;
                    data.queue_branch = queue_branches;
                    data.aborted_branch = aborted_branches;

                    Res.data = data;

                    c1.Dispose();
                    c2.Dispose();
                    c3.Dispose();
                    dt1.Dispose();
                    dt2.Dispose();
                    dt3.Dispose();
                    ds1.Dispose();
                    ds2.Dispose();
                    ds3.Dispose();
                    adapter1.Dispose();
                    adapter2.Dispose();
                    adapter3.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResSubmitBranches>(Res);
        }
        public static async Task<ResCBSTBS> GetCBSTBS()
        {
            ResCBSTBS Res = new ResCBSTBS();
            List<DataCBSTBS> data = new List<DataCBSTBS>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_CBS_TBS_UTIL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataCBSTBS d = new DataCBSTBS();
                        d.tablespace_name = dr[0].ToString();
                        d.size = dr[1].ToString();
                        d.used = dr[2].ToString();
                        d.free = dr[3].ToString();
                        d.used_pct = dr[4].ToString();
                        d.free_pct = dr[5].ToString();
                        d.max_size = dr[6].ToString();
                        d.used_max_pct = dr[7].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResCBSTBS>(Res);
        }
        public static async Task<ResCBSDBS> GetCBSDBS(ReqCBSDBS param)
        {
            ResCBSDBS Res = new ResCBSDBS();
            List<DataCBSDBS> data = new List<DataCBSDBS>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_CBS_DBF_UTIL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TBS_NAME", OracleDbType.Varchar2).Value = param.tablespace_name.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataCBSDBS d = new DataCBSDBS();
                        d.tablespace_name = dr[0].ToString();
                        d.file_name = dr[1].ToString();
                        d.file_id = dr[2].ToString();
                        d.size = dr[3].ToString();
                        d.free = dr[4].ToString();
                        d.max_size = dr[5].ToString();
                        d.used_max_pct = dr[6].ToString();
                        d.used_size_pct = dr[7].ToString();
                        d.auto_extended = dr[8].ToString();
                        d.status = dr[9].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResCBSDBS>(Res);
        }
        public static async Task<ResCBSDBSize> GetCBSDBSize()
        {
            ResCBSDBSize Res = new ResCBSDBSize();
            ResDataCBSDBSize data = new ResDataCBSDBSize();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_CBS_DB_SIZE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_TOTAL_SIZE", OracleDbType.Varchar2,20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_USED_SIZE", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    data.total_size_gb= cmd.Parameters["P_TOTAL_SIZE"].Value.ToString();
                    data.used_size_gb = cmd.Parameters["P_USED_SIZE"].Value.ToString();
                }
                Res.data = data;
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResCBSDBSize>(Res);
        }

        //Contact
        public static async Task<ResContact> GetContact()
        {
            ResContact Res = new ResContact();
            List<ResDataContact> data = new List<ResDataContact>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_CONTACT_NUMBERS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResDataContact d = new ResDataContact();
                        d.branch_code = dr[0].ToString();
                        d.staff_id = dr[1].ToString();
                        d.name = dr[2].ToString();
                        
                        d.position = dr[3].ToString();
                        d.telephone = dr[4].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResContact>(Res);
        }



        //pending
        public static async Task<ResPending> GetPending()
        {
            ResPending Res = new ResPending();
            List<ResDataPending> data = new List<ResDataPending>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_CHECK_PENDING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResDataPending d = new ResDataPending();
                        d.no = dr[0].ToString();
                        d.pending_type = dr[1].ToString();
                        d.branch_code = dr[2].ToString();
                        d.mudule= dr[3].ToString();
                        d.reference_number = dr[4].ToString();
                        d.even = dr[5].ToString();
                        d.marker_id= dr[6].ToString();
                        d.till_id = dr[7].ToString();
                        d.function_id = dr[8].ToString();
                        d.key_id = dr[9].ToString();
                        d.table_name = dr[10].ToString();
                        d.record_stat = dr[11].ToString();
                        d.auth_stat = dr[12].ToString();
                        d.username = dr[13].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResPending>(Res);
        }



        //MissmatchBalance
        public static async Task<ResMissmatchBalance> GetMissmatchBalance()
        {
            ResMissmatchBalance Res = new ResMissmatchBalance();
            List<ResDataMissmatchBalance> data = new List<ResDataMissmatchBalance>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                cmd.CommandText = "EOC_MONITORING_PKG.PR_MISSMATCH_BALANCE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResDataMissmatchBalance d = new ResDataMissmatchBalance();
                        d.no = dr[0].ToString();
                        d.branch_code = dr[1].ToString();
                        d.mudule = dr[2].ToString();
                        d.marker_id = dr[3].ToString();
                        d.real_dr = dr[4].ToString();
                        d.real_cr = dr[5].ToString();
                        d.dr_minus_cr = dr[6].ToString();
                        d.cont_dr = dr[7].ToString();
                        d.cont_cr = dr[8].ToString();
                        d.memo_dr = dr[9].ToString();
                        d.memo_cr = dr[10].ToString();
                        d.posn_dr = dr[11].ToString();
                        d.posn_cr = dr[12].ToString();
                        d.financial_cycle = dr[13].ToString();
                        d.period_code = dr[14].ToString();
                        data.Add(d);
                    }
                    Res.data = data;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResMissmatchBalance>(Res);
        }
    }
}
