using CoreFunction;
using ITOAPP_API.Controllers;
using ITOAPP_API.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static ITOAPP_API.Models.FCUBSModel;
using static ITOAPP_API.Models.RPTDatabaseModel;

namespace ITOAPP_API.Helpers
{
    public class FCUBSServices
    {
        public static async Task<ResRealDebugUpdate> UpdateRealDebug(ReqUpdateRealDebug param)
        {
            ResRealDebugUpdate Res = new ResRealDebugUpdate();
            RealDebugStat data = new RealDebugStat();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_REAL_DEBUG_HANDLER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ENV_ID", OracleDbType.Varchar2).Value = param.environment_id.ToString();
                cmd.Parameters.Add("P_VALUE", OracleDbType.Varchar2).Value = param.enable.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_REAL_DEBUG_STAT", OracleDbType.Varchar2, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                data.real_debug = cmd.Parameters["OP_REAL_DEBUG_STAT"].Value.ToString();
                Res.data = data;
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
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
            return await Task.FromResult<ResRealDebugUpdate>(Res);
        }
        public static async Task<ResRealDebugUpdate> CurrentRealDebugStat(ReqRealDebugState param)
        {
            ResRealDebugUpdate Res = new ResRealDebugUpdate();
            RealDebugStat data = new RealDebugStat();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_REAL_DEBUG_STAT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ENVIRONMENT", OracleDbType.Varchar2).Value = param.environment_id.ToString();
                cmd.Parameters.Add("OP_REAL_DEBUG_STAT", OracleDbType.Varchar2, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                data.real_debug = cmd.Parameters["OP_REAL_DEBUG_STAT"].Value.ToString();
                Res.data = data;
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
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
            return await Task.FromResult<ResRealDebugUpdate>(Res);
        }
        public static async Task<BasicResponse> UpdateUserDebug(ReqUpdateUserDebug param)
        {
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_USER_DEBUG_HANDLER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ENV_ID", OracleDbType.Varchar2).Value = param.environment_id.ToString();
                cmd.Parameters.Add("P_VALUE", OracleDbType.Varchar2).Value = param.enable.ToString();
                cmd.Parameters.Add("P_DEBUG_USER", OracleDbType.Varchar2).Value = param.user_id.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("OP_REAL_DEBUG_STAT", OracleDbType.Varchar2, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
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
            return await Task.FromResult<BasicResponse>(Res);

        }
        public static async Task<ResUpdateDebuglogs> GetUpdateDebugLogs(ReqUpdateDebuglogsdate param)
        {
            ResUpdateDebuglogs RE = new ResUpdateDebuglogs();
            List<ExeResUpdateDebuglogs> TMP_DATA = new List<ExeResUpdateDebuglogs>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_API_UPDATE_DEBUG_LOG";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2).Value = param.Logs_Date.ToString();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob, 2).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;
                if (cmd.Parameters["OP_STATUS"].Value.ToString() == "1")
                {
                    adapter.Fill(ds, oraCursor);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExeResUpdateDebuglogs tmplogs = new ExeResUpdateDebuglogs();
                        tmplogs.log_id = Convert.ToInt32(dr[0]);
                        tmplogs.enviroment = dr[1].ToString();
                        tmplogs.time_stamp = dr[2].ToString();
                        tmplogs.debug_status = dr[3].ToString();
                        tmplogs.debug_param = dr[4].ToString();
                        tmplogs.completed = dr[5].ToString();
                        tmplogs.user_id = dr[6].ToString();
                        tmplogs.completed_by = dr[7].ToString();
                        TMP_DATA.Add(tmplogs);
                    }
                    oraCursor.Dispose();
                }
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RE.message = msgclob.Value.ToString();
                RE.data = TMP_DATA;
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                RE.status = "-1";
                RE.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
            }
            return await Task.FromResult<ResUpdateDebuglogs>(RE);
        }
        public static async Task<ResUserDebugStat> CurrentUserDebugStat(ReqUserDebugStat param)
        {
            ResUserDebugStat Res = new ResUserDebugStat();
            ExeResUserDebugStat data = new ExeResUserDebugStat();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_USER_DEBUG_STAT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ENVIRONMENT", OracleDbType.Varchar2).Value = param.environment_id.ToString();
                cmd.Parameters.Add("P_DEBUG_USER", OracleDbType.Varchar2).Value = param.user_id.ToString();
                cmd.Parameters.Add("OP_REAL_DEBUG_STAT", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                data.user_debug = cmd.Parameters["OP_REAL_DEBUG_STAT"].Value.ToString();
                Res.data = data;
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
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
            return await Task.FromResult<ResUserDebugStat>(Res);
        }
        public static async Task<ResFcubParamfirstLoad> FcubParamFirstLoad()
        {
            ResFcubParamfirstLoad Res = new ResFcubParamfirstLoad();
            List<ExeResDebugEnv> environment = new List<ExeResDebugEnv>();
            List<ExeResUpdateDebuglogs> debug_log = new List<ExeResUpdateDebuglogs>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_FCUB_DEBUG_FIRST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_CURRENT_DATE", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("TBL_LOGS_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ENVIRONMENT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    Res.current_date = cmd.Parameters["OP_CURRENT_DATE"].Value.ToString();
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["TBL_LOGS_CUR"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["ENVIRONMENT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResUpdateDebuglogs tmplog = new ExeResUpdateDebuglogs();
                        tmplog.log_id = Convert.ToInt32(dr1[0]);
                        tmplog.enviroment = dr1[1].ToString();
                        tmplog.time_stamp = dr1[2].ToString();
                        tmplog.debug_status = dr1[3].ToString();
                        tmplog.debug_param = dr1[4].ToString();
                        tmplog.completed = dr1[5].ToString();
                        tmplog.user_id = dr1[6].ToString();
                        tmplog.completed_by = dr1[7].ToString();
                        debug_log.Add(tmplog);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResDebugEnv tmpenvir = new ExeResDebugEnv();
                        tmpenvir.envir_id = Convert.ToInt32(dr2[0]);
                        tmpenvir.enviroment_name = dr2[1].ToString();
                        environment.Add(tmpenvir);
                    }


                    Res.all_environment = environment;
                    Res.debug_log = debug_log;
                    ds1.Dispose();
                    ds2.Dispose();
                    dt1.Dispose();
                    dt2.Dispose();
                    ad1.Dispose();
                    ad2.Dispose();
                    c1.Dispose();
                    c2.Dispose();

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
            return await Task.FromResult<ResFcubParamfirstLoad>(Res);
        }
        public static async Task<ResHandoffFailedFirstLoad> ResHandoffFailedFirstLoad()
        {
            ResHandoffFailedFirstLoad REF = new ResHandoffFailedFirstLoad();
            DataResHandoffFailedFirstLoad data = new DataResHandoffFailedFirstLoad();
            List<ExeResHandoffFailedIssueType> fcub_issue_type = new List<ExeResHandoffFailedIssueType>();
            List<ExeResLogHandoffList> handoff_entries_log = new List<ExeResLogHandoffList>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_FCUB_ISSUE_FIRST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_ISSUE_TYPE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ISSUE_LOG_LISTING", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_CHK_USER_REQ", OracleDbType.Int64).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                REF.chk_req = cmd.Parameters["OP_CHK_USER_REQ"].Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_ISSUE_TYPE"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["ISSUE_LOG_LISTING"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResHandoffFailedIssueType tmpIssuetype = new ExeResHandoffFailedIssueType();
                        tmpIssuetype.issue_id = dr1[0].ToString();
                        tmpIssuetype.issue_name = dr1[1].ToString();
                        fcub_issue_type.Add(tmpIssuetype);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResLogHandoffList tmpHanddofflog = new ExeResLogHandoffList();

                        tmpHanddofflog.log_id = dr2[0].ToString();
                        tmpHanddofflog.trn_ref_no = dr2[1].ToString();
                        tmpHanddofflog.txn_branch = dr2[2].ToString();
                        tmpHanddofflog.value_dt = dr2[3].ToString();
                        tmpHanddofflog.txn_dt = dr2[4].ToString();
                        tmpHanddofflog.user_id = dr2[5].ToString();
                        tmpHanddofflog.error_cd = dr2[6].ToString();
                        tmpHanddofflog.txn_stat = dr2[7].ToString();
                        tmpHanddofflog.source_code = dr2[8].ToString();
                        tmpHanddofflog.request_by = dr2[9].ToString();
                        tmpHanddofflog.request_date = dr2[10].ToString();
                        tmpHanddofflog.status = dr2[11].ToString();
                        tmpHanddofflog.resolve_by = dr2[12].ToString();
                        tmpHanddofflog.resolve_date = dr2[13].ToString();
                        tmpHanddofflog.reject_by = dr2[14].ToString();
                        tmpHanddofflog.reject_date = dr2[15].ToString();
                        tmpHanddofflog.error_message = dr2[16].ToString();

                        handoff_entries_log.Add(tmpHanddofflog);
                    }
                    REF.data = data;
                    REF.data.fcub_issue_type = fcub_issue_type;
                    REF.data.handoff_entries_log = handoff_entries_log;

                    ds1.Dispose();
                    ds2.Dispose();

                    dt1.Dispose();
                    dt2.Dispose();

                    ad1.Dispose();
                    ad2.Dispose();

                    c1.Dispose();
                    c2.Dispose();
                }
            }
            catch (Exception ex)
            {
                REF.status = "-1";
                REF.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResHandoffFailedFirstLoad>(REF);
        }
        public static async Task<BasicResponse> InsertRequestFixHandoff(ReqFixHandoffFailedEntries param)
        {
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_INSERT_REQUEST_FIX_HANDOFF";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_TRN_REF_NO", OracleDbType.Varchar2).Value = param.trn_ref_no.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
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
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<BasicResponse> RejectRequestFixHandoff(ReqRejectHandoffFailedEntries param)
        {
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_REJECT_HANDOFF_REQ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_TRN_REF_NO", OracleDbType.Varchar2).Value = param.trn_ref_no.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
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
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<BasicResponse> FixHandoffFailedEntries(ReqFixHandoffFailedEntries param)
        {
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_FIX_HOFF_ENTRIES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_TRN_REF_NO", OracleDbType.Varchar2).Value = param.trn_ref_no.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
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
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<ResHandoffFailedEntries> HandoffFailedEntriesListing(ReqHandoffFailedEntries param)
        {
            ResHandoffFailedEntries Res = new ResHandoffFailedEntries();
            List<ExeResHandofffaliedEntriesList> data = new List<ExeResHandofffaliedEntriesList>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_GET_HANDOFF_FAILED_ENTRIES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TRN_REF", OracleDbType.Varchar2).Value = param.trn_ref.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_TYPE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.op_type = cmd.Parameters["OP_TYPE"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResHandofffaliedEntriesList tmp_list = new ExeResHandofffaliedEntriesList();
                        tmp_list.trn_ref_no = dr1[0].ToString();
                        tmp_list.txn_branch = dr1[1].ToString();
                        tmp_list.value_dt = dr1[2].ToString();
                        tmp_list.txn_date = dr1[3].ToString();
                        tmp_list.user_id = dr1[4].ToString();
                        tmp_list.error_cd = dr1[5].ToString();
                        tmp_list.txn_stat = dr1[6].ToString();
                        tmp_list.source_code = dr1[7].ToString();
                        tmp_list.error_message = dr1[8].ToString();

                        data.Add(tmp_list);
                    }
                    Res.data = data;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResHandoffFailedEntries>(Res);
        }
        public static async Task<ResLogHandoffLising> HandoffLogListing(ReqLogDateHandoff param)
        {
            ResLogHandoffLising Res = new ResLogHandoffLising();
            List<ExeResLogHandoffList> data = new List<ExeResLogHandoffList>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_GET_HANDOFF_LOG";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2).Value = param.log_date.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResLogHandoffList tmp_list = new ExeResLogHandoffList();
                        tmp_list.log_id = dr1[0].ToString();
                        tmp_list.trn_ref_no = dr1[1].ToString();
                        tmp_list.txn_branch = dr1[2].ToString();
                        tmp_list.value_dt = dr1[3].ToString();
                        tmp_list.txn_dt = dr1[4].ToString();
                        tmp_list.user_id = dr1[5].ToString();
                        tmp_list.error_cd = dr1[6].ToString();
                        tmp_list.txn_stat = dr1[7].ToString();
                        tmp_list.source_code = dr1[8].ToString();
                        tmp_list.request_by = dr1[9].ToString();
                        tmp_list.request_date = dr1[10].ToString();
                        tmp_list.status = dr1[11].ToString();
                        tmp_list.resolve_by = dr1[12].ToString();
                        tmp_list.resolve_date = dr1[13].ToString();
                        tmp_list.reject_by = dr1[14].ToString();
                        tmp_list.reject_date = dr1[15].ToString();
                        tmp_list.error_message = dr1[16].ToString();
                        data.Add(tmp_list);
                    }
                    Res.data = data;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResLogHandoffLising>(Res);
        }
        public static async Task<ResErrorSMS> Get_Fcub_Error_SMS(ReqError_Code param)
        {
            ResErrorSMS Res = new ResErrorSMS();
            ExeResErrorSMS data = new ExeResErrorSMS();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "FCUBS_PKG.PR_FCUB_GET_ERROR_SMS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_ERROR_CODE", OracleDbType.Varchar2).Value = param.error_code.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResErrorSMS tmp_list = new ExeResErrorSMS();
                        tmp_list.error_code = dr1[0].ToString();
                        tmp_list.sms = dr1[1].ToString();
                        tmp_list.type = dr1[2].ToString();
                        tmp_list.function = dr1[3].ToString();

                        data.error_code = tmp_list.error_code;
                        data.sms = tmp_list.sms;
                        data.type = tmp_list.type;
                        data.function = tmp_list.function;
                    }
                    Res.data = data;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResErrorSMS>(Res);
        }


        public static async Task<ResEoC_Branch_Group> Calculator_EoC_Branch_Group (ReqCalculator_EoC_Branch_Group param)
        {
            ResEoC_Branch_Group REBG= new ResEoC_Branch_Group();
            DataExeEoC_Branch_Group data = new DataExeEoC_Branch_Group();
            List<ExeResEoC_Total_Group> get_eoc_total_group_listing=new List<ExeResEoC_Total_Group>();
            List<ExeResEoC_Branch_Group> get_eoc_branch_group_listing = new List<ExeResEoC_Branch_Group>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_CALCULATOR_GROUP.CALCULATOR_GROUP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();                
                cmd.Parameters.Add("P_EOC_TYPE", OracleDbType.Varchar2).Value = param.eoc_type.ToString();
                cmd.Parameters.Add("P_TO_STAGE", OracleDbType.Varchar2).Value = param.to_stage.ToString();
                cmd.Parameters.Add("P_DATE_START", OracleDbType.Varchar2).Value = param.date_start.ToString();
                cmd.Parameters.Add("P_DATE_END", OracleDbType.Varchar2).Value = param.date_end.ToString();
                cmd.Parameters.Add("GROUP_NUMBER", OracleDbType.Varchar2).Value = param.group_number.ToString();

                cmd.Parameters.Add("OP_LISTING_TOTAL", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_LISTING_GROUP", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_CHECK_EoC_TYPE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                

                cmd.ExecuteNonQuery();

               
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REBG.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                REBG.eoc_run_time = cmd.Parameters["OP_CHECK_EoC_TYPE"].Value.ToString();
                REBG.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REBG.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_LISTING_TOTAL"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_LISTING_GROUP"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResEoC_Total_Group tmpEoC_Total_Group = new ExeResEoC_Total_Group();
                        tmpEoC_Total_Group.group_code = dr1[0].ToString();
                        tmpEoC_Total_Group.total_duration = dr1[1].ToString();
                        tmpEoC_Total_Group.total_branches = dr1[2].ToString();
                        tmpEoC_Total_Group.status_pull = dr1[3].ToString();
                        tmpEoC_Total_Group.date_pull= dr1[4].ToString();
                        tmpEoC_Total_Group.status= dr1[5].ToString();
                        tmpEoC_Total_Group.maker_id= dr1[6].ToString();

                        get_eoc_total_group_listing.Add(tmpEoC_Total_Group);
                    };

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResEoC_Branch_Group tmpEoC_Branch_Group = new ExeResEoC_Branch_Group();
                        tmpEoC_Branch_Group.group_code = dr2[0].ToString();
                        tmpEoC_Branch_Group.branch_code = dr2[1].ToString();
                        tmpEoC_Branch_Group.duraction_avg = dr2[2].ToString();
                        tmpEoC_Branch_Group.add_date = dr2[3].ToString();
                        tmpEoC_Branch_Group.branch_seq = dr2[4].ToString();
                        tmpEoC_Branch_Group.status = dr2[5].ToString();
                        tmpEoC_Branch_Group.maker_id = dr2[6].ToString();

                        get_eoc_branch_group_listing.Add(tmpEoC_Branch_Group);
                    };
                    REBG.data = data;
                    REBG.data.get_eoc_total_group_listing = get_eoc_total_group_listing;
                    REBG.data.get_eoc_branch_group_listing = get_eoc_branch_group_listing;


                    ds1.Dispose();
                    ds2.Dispose();

                    dt1.Dispose();
                    dt2.Dispose();

                    ad1.Dispose();
                    ad2.Dispose();

                    c1.Dispose();
                    c2.Dispose();
                }
            }
            catch(Exception ex)
            {
                REBG.status = "-1";
                REBG.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return await Task.FromResult<ResEoC_Branch_Group>(REBG);
        }
        public static async Task<ResCalculator_EoC_Branch_Group_Push> Calculator_EoC_Branch_Group_Push()
        {
            ResCalculator_EoC_Branch_Group_Push RCEBGP = new ResCalculator_EoC_Branch_Group_Push();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_CALCULATOR_GROUP.PUSH_NOTIFICATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();


                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RCEBGP.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RCEBGP.message = msgclob.Value.ToString();
                msgclob.Dispose();

            }
            catch (Exception ex)
            {
                RCEBGP.status = "-1";
                RCEBGP.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return await Task.FromResult<ResCalculator_EoC_Branch_Group_Push>(RCEBGP);
        }

        public static async Task<ResCalculator_EoC_Branch_Group_Generate> Calculator_EoC_Branch_Group_Generate()
        {
            ResCalculator_EoC_Branch_Group_Generate RCEBGG = new ResCalculator_EoC_Branch_Group_Generate();            
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_CALCULATOR_GROUP.GENERATE_EOC_BRANCH_GROUP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();


                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RCEBGG.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RCEBGG.message = msgclob.Value.ToString();
                msgclob.Dispose();
               
            }
            catch (Exception ex)
            {
                RCEBGG.status = "-1";
                RCEBGG.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return await Task.FromResult<ResCalculator_EoC_Branch_Group_Generate>(RCEBGG);
        }
        public static async Task<ResCalculator_EoC_Branch_Group_Reject> Calculator_EoC_Branch_Group_Reject()
        {
            ResCalculator_EoC_Branch_Group_Reject RCEBGR = new ResCalculator_EoC_Branch_Group_Reject();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_CALCULATOR_GROUP.REJECT_CALCULATOR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();


                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RCEBGR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RCEBGR.message = msgclob.Value.ToString();
                msgclob.Dispose();

            }
            catch (Exception ex)
            {
                RCEBGR.status = "-1";
                RCEBGR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResCalculator_EoC_Branch_Group_Reject>(RCEBGR);
        }
        public static async Task<ResCalculator_EoC_Branch_Group_Refresh> Calculator_EoC_Branch_Group_Refresh(ReqCalculator_EoC_Branch_Group_Refresh param)
        {
            ResCalculator_EoC_Branch_Group_Refresh RCEBGR = new ResCalculator_EoC_Branch_Group_Refresh();
            DataExeEoC_Branch_Group_Refresh data = new DataExeEoC_Branch_Group_Refresh();
            List<ExeResEoC_Total_Group_Refresh> get_eoc_total_group_refresh_listing = new List<ExeResEoC_Total_Group_Refresh>();
            List<ExeResEoC_Branch_Group_Refresh> get_eoc_branch_group_refresh_listing = new List<ExeResEoC_Branch_Group_Refresh>();
            List<ExeResEoC_Total_Group_his> get_eoc_total_group_his_listing = new List<ExeResEoC_Total_Group_his>();
            List<ExeResEoC_Branch_Group_his> get_eoc_branch_group_his_listing = new List<ExeResEoC_Branch_Group_his>();
            List<ExeResEoC_Group_Branches> get_eoc_branch_group_brn_listing = new List<ExeResEoC_Group_Branches>();
            List<ExeResEoC_Group_Master> get_eoc_branch_group_mas_listing=new List<ExeResEoC_Group_Master>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "EOC_CALCULATOR_GROUP.REFRESH_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_START_DATE", OracleDbType.Varchar2).Value = param.range_date_start.ToString();
                cmd.Parameters.Add("P_END_DATE", OracleDbType.Varchar2).Value = param.range_date_end.ToString();

                cmd.Parameters.Add("OP_LISTING_TOTAL_REFRESH", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_LISTING_GROUP_REFRESH", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_LISTING_TOTAL_HIS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_LISTING_GROUP_HIS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_LISTING_GROUP_BRANCHES", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_LISTING_GROUP_MASTER", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_TYPE", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RCEBGR.option_type = cmd.Parameters["OP_TYPE"].Value.ToString();
                RCEBGR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RCEBGR.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RCEBGR.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_LISTING_TOTAL_REFRESH"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_LISTING_GROUP_REFRESH"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_LISTING_TOTAL_HIS"].Value;
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["OP_LISTING_GROUP_HIS"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["OP_LISTING_GROUP_BRANCHES"].Value;
                    OracleRefCursor c6 = (OracleRefCursor)cmd.Parameters["OP_LISTING_GROUP_MASTER"].Value;


                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    OracleDataAdapter ad3 = new OracleDataAdapter();
                    OracleDataAdapter ad4 = new OracleDataAdapter();
                    OracleDataAdapter ad5 = new OracleDataAdapter();
                    OracleDataAdapter ad6 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    DataSet ds3 = new DataSet();
                    DataSet ds4 = new DataSet();
                    DataSet ds5 = new DataSet();
                    DataSet ds6 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();
                    DataTable dt5 = new DataTable();
                    DataTable dt6 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);
                    ad3.Fill(ds3, c3);
                    ad4.Fill(ds4, c4);
                    ad5.Fill(ds5, c5);
                    ad6.Fill(ds6, c6);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];
                    dt4 = ds4.Tables[0];
                    dt5 = ds5.Tables[0];
                    dt6 = ds6.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResEoC_Total_Group_Refresh tmpEoC_Total_Group_Refresh = new ExeResEoC_Total_Group_Refresh();
                        tmpEoC_Total_Group_Refresh.group_code = dr1[0].ToString();
                        tmpEoC_Total_Group_Refresh.total_duration = dr1[1].ToString();
                        tmpEoC_Total_Group_Refresh.total_branches = dr1[2].ToString();
                        tmpEoC_Total_Group_Refresh.status_pull = dr1[3].ToString();
                        tmpEoC_Total_Group_Refresh.date_pull = dr1[4].ToString();
                        tmpEoC_Total_Group_Refresh.status = dr1[5].ToString();
                        tmpEoC_Total_Group_Refresh.maker_id = dr1[6].ToString();

                        get_eoc_total_group_refresh_listing.Add(tmpEoC_Total_Group_Refresh);
                    };

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResEoC_Branch_Group_Refresh tmpEoC_Branch_Group_Refresh = new ExeResEoC_Branch_Group_Refresh();
                        tmpEoC_Branch_Group_Refresh.group_code = dr2[0].ToString();
                        tmpEoC_Branch_Group_Refresh.branch_code = dr2[1].ToString();
                        tmpEoC_Branch_Group_Refresh.duraction_avg = dr2[2].ToString();
                        tmpEoC_Branch_Group_Refresh.add_date = dr2[3].ToString();
                        tmpEoC_Branch_Group_Refresh.branch_seq = dr2[4].ToString();
                        tmpEoC_Branch_Group_Refresh.status = dr2[5].ToString();
                        tmpEoC_Branch_Group_Refresh.maker_id = dr2[6].ToString();

                        get_eoc_branch_group_refresh_listing.Add(tmpEoC_Branch_Group_Refresh);
                    };

                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResEoC_Total_Group_his tmpEoC_Total_Group_His = new ExeResEoC_Total_Group_his();
                        tmpEoC_Total_Group_His.group_code = dr3[0].ToString();
                        tmpEoC_Total_Group_His.total_duration = dr3[1].ToString();
                        tmpEoC_Total_Group_His.total_branches = dr3[2].ToString();
                        tmpEoC_Total_Group_His.status_pull = dr3[3].ToString();
                        tmpEoC_Total_Group_His.status = dr3[4].ToString();
                        tmpEoC_Total_Group_His.request_date = dr3[5].ToString();
                        tmpEoC_Total_Group_His.maker_id = dr3[6].ToString();
                        tmpEoC_Total_Group_His.authorize_date = dr3[7].ToString();
                        tmpEoC_Total_Group_His.autthorize = dr3[8].ToString();

                        get_eoc_total_group_his_listing.Add(tmpEoC_Total_Group_His);
                    };
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        ExeResEoC_Branch_Group_his tmpEoC_Branch_Group_His = new ExeResEoC_Branch_Group_his();
                        tmpEoC_Branch_Group_His.group_code = dr4[0].ToString();
                        tmpEoC_Branch_Group_His.branch_code = dr4[1].ToString();
                        tmpEoC_Branch_Group_His.duraction_avg = dr4[2].ToString();
                        tmpEoC_Branch_Group_His.branch_seq = dr4[3].ToString();
                        tmpEoC_Branch_Group_His.status = dr4[4].ToString();
                        tmpEoC_Branch_Group_His.request_date = dr4[5].ToString();
                        tmpEoC_Branch_Group_His.maker_id = dr4[6].ToString();
                        tmpEoC_Branch_Group_His.authorize_date = dr4[7].ToString();
                        tmpEoC_Branch_Group_His.autthorize = dr4[8].ToString();

                        get_eoc_branch_group_his_listing.Add(tmpEoC_Branch_Group_His);
                    };

                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        ExeResEoC_Group_Branches tmpEoC_Branch_Group_Branches = new ExeResEoC_Group_Branches();
                        tmpEoC_Branch_Group_Branches.group_code = dr5[0].ToString();
                        tmpEoC_Branch_Group_Branches.branch_code = dr5[1].ToString();
                        tmpEoC_Branch_Group_Branches.branch_name = dr5[2].ToString();
                        tmpEoC_Branch_Group_Branches.branch_seq = dr5[3].ToString();

                        get_eoc_branch_group_brn_listing.Add(tmpEoC_Branch_Group_Branches);
                    };
                    foreach (DataRow dr6 in dt6.Rows)
                    {
                        ExeResEoC_Group_Master tmpEoC_Branch_Group_Mas = new ExeResEoC_Group_Master();
                        tmpEoC_Branch_Group_Mas.group_code = dr6[0].ToString();
                        tmpEoC_Branch_Group_Mas.group_des = dr6[1].ToString();
                        tmpEoC_Branch_Group_Mas.record_stat = dr6[2].ToString();
                        tmpEoC_Branch_Group_Mas.auth_stat = dr6[3].ToString();
                        tmpEoC_Branch_Group_Mas.maker_id = dr6[4].ToString();
                        tmpEoC_Branch_Group_Mas.maker_date = dr6[5].ToString();
                        tmpEoC_Branch_Group_Mas.checker_id = dr6[6].ToString();
                        tmpEoC_Branch_Group_Mas.checker_date = dr6[7].ToString();

                        get_eoc_branch_group_mas_listing.Add(tmpEoC_Branch_Group_Mas);
                    };


                    RCEBGR.data = data;
                    RCEBGR.data.get_eoc_total_group_refresh_listing = get_eoc_total_group_refresh_listing;
                    RCEBGR.data.get_eoc_branch_group_refresh_listing = get_eoc_branch_group_refresh_listing;
                    RCEBGR.data.get_eoc_total_group_his_listing = get_eoc_total_group_his_listing;
                    RCEBGR.data.get_eoc_branch_group_his_listing = get_eoc_branch_group_his_listing;
                    RCEBGR.data.get_eoc_branch_group_brn_listing = get_eoc_branch_group_brn_listing;
                    RCEBGR.data.get_eoc_branch_group_mas_listing = get_eoc_branch_group_mas_listing;


                    ds1.Dispose();
                    ds2.Dispose();
                    ds3.Dispose();
                    ds4.Dispose();

                    dt1.Dispose();
                    dt2.Dispose();
                    dt3.Dispose();
                    dt4.Dispose();

                    ad1.Dispose();
                    ad2.Dispose();
                    ad3.Dispose();
                    ad4.Dispose();

                    c1.Dispose();
                    c2.Dispose();
                    c3.Dispose();
                    c4.Dispose();
                }
            }
            catch (Exception ex)
            {
                RCEBGR.status = "-1";
                RCEBGR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return await Task.FromResult<ResCalculator_EoC_Branch_Group_Refresh>(RCEBGR);
        }

    }
}