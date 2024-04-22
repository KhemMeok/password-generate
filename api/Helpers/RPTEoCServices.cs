using CoreFunction;
using ITOAPP_API.Controllers;
using ITOAPP_API.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;



namespace ITOAPP_API.Helpers
{
    public class RPTEoCServices
    {
        public static async Task<ResEoCReportFirstLoad> EoCReportFirstLoad()
        {
            ResEoCReportFirstLoad REF = new ResEoCReportFirstLoad();
            List<ExeResEoCStep> steps = new List<ExeResEoCStep>();
            List<ExeResBranch> branches = new List<ExeResBranch>();
            List<ExeResAllResources> resources = new List<ExeResAllResources>();
            List<ExeResAllStorages> storages = new List<ExeResAllStorages>();
            List<ExeResBranch> failure_branches = new List<ExeResBranch>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_API_RPT_EOC_FIRST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_EOC_TYPE_NAME", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_EOC_REPORT_DATE", OracleDbType.Varchar2, 11).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_COMP_PCT", OracleDbType.Varchar2, 5).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_STEPS_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_BRANCHES_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_RESOURCES_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_STORAGE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_FAILURE_BRANCHES_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    REF.eoc_type = cmd.Parameters["OP_EOC_TYPE_NAME"].Value.ToString();
                    REF.eoc_report_date = cmd.Parameters["OP_EOC_REPORT_DATE"].Value.ToString();
                    REF.eoc_report_comp_pct = cmd.Parameters["OP_COMP_PCT"].Value.ToString();
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_STEPS_CUR"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["ALL_BRANCHES_CUR"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["ALL_RESOURCES_CUR"].Value;
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["ALL_STORAGE_CUR"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["ALL_FAILURE_BRANCHES_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    OracleDataAdapter ad3 = new OracleDataAdapter();
                    OracleDataAdapter ad4 = new OracleDataAdapter();
                    OracleDataAdapter ad5 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    DataSet ds3 = new DataSet();
                    DataSet ds4 = new DataSet();
                    DataSet ds5 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();
                    DataTable dt5 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);
                    ad3.Fill(ds3, c3);
                    ad4.Fill(ds4, c4);
                    ad5.Fill(ds5, c5);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];
                    dt4 = ds4.Tables[0];
                    dt5 = ds5.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResEoCStep tmpStep = new ExeResEoCStep();
                        tmpStep.step_no = Convert.ToInt32(dr1[0]);
                        tmpStep.step_name = dr1[1].ToString();
                        tmpStep.is_auto = dr1[2].ToString();
                        steps.Add(tmpStep);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResBranch tmpbr = new ExeResBranch();
                        tmpbr.branch_code = dr2[0].ToString();
                        tmpbr.branch_name = dr2[1].ToString();
                        branches.Add(tmpbr);
                    }
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResAllResources tmpResource = new ExeResAllResources();
                        tmpResource.resource_id = Convert.ToInt32(dr3[0]);
                        tmpResource.resource_name = dr3[1].ToString();
                        resources.Add(tmpResource);
                    }
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        ExeResAllStorages tmpStorage = new ExeResAllStorages();
                        tmpStorage.storage_id = Convert.ToInt32(dr4[0]);
                        tmpStorage.storage_name = dr4[1].ToString();
                        storages.Add(tmpStorage);
                    }
                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        ExeResBranch tmpbr = new ExeResBranch();
                        tmpbr.branch_code = dr5[0].ToString();
                        tmpbr.branch_name = dr5[1].ToString();
                        failure_branches.Add(tmpbr);
                    }
                    
                    REF.eoc_steps = steps;
                    REF.branches = branches;
                    REF.resources = resources;
                    REF.storages = storages;
                    REF.failure_branches = failure_branches;
                    ds1.Dispose();
                    ds2.Dispose();
                    ds3.Dispose();
                    ds4.Dispose();
                    ds5.Dispose();
                    dt1.Dispose();
                    dt2.Dispose();
                    dt3.Dispose();
                    dt4.Dispose();
                    dt5.Dispose();
                    ad1.Dispose();
                    ad2.Dispose();
                    ad3.Dispose();
                    ad4.Dispose();
                    ad5.Dispose();
                    c1.Dispose();
                    c2.Dispose();
                    c3.Dispose();
                    c4.Dispose();
                    c5.Dispose();
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
            return await Task.FromResult<ResEoCReportFirstLoad>(REF);
        }
        public static async Task<ResGetEoCSteps> GetEoCSteps()
        {
            ResGetEoCSteps RE = new ResGetEoCSteps();
            List<ExeResEoCStep> TMP_DATA = new List<ExeResEoCStep>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_ALL_STEPS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
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
                        ExeResEoCStep tmpStep = new ExeResEoCStep();
                        tmpStep.step_no = Convert.ToInt32(dr[0]);
                        tmpStep.step_name = dr[1].ToString();
                        tmpStep.is_auto = dr[2].ToString();
                        TMP_DATA.Add(tmpStep);
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
            return await Task.FromResult<ResGetEoCSteps>(RE);
        }
        public static async Task<BasicResponse> InsertEoCDuration(ReqInsertEoCDuration param)
        {
            BasicResponse BR = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_INSERT_EOC_DURATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_STEP_NO", OracleDbType.Varchar2).Value = param.step_no.ToString();
                cmd.Parameters.Add("P_START_TIME", OracleDbType.Varchar2).Value = param.start_time.ToString();
                cmd.Parameters.Add("P_END_TIME", OracleDbType.Varchar2).Value = param.end_time.ToString();
                cmd.Parameters.Add("P_COMPLETED_STAT", OracleDbType.Varchar2).Value = param.completed_stat.ToString();
                cmd.Parameters.Add("P_REGISTER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                BR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                BR.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                BR.status = "-1";
                BR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<BasicResponse>(BR);
        }
        public static async Task<ResEoCDurationData> EoCDurationData(ReqEoCDuationData param)
        {
            ResEoCDurationData RED = new ResEoCDurationData();
            List<ExeResEoCDurationData> data = new List<ExeResEoCDurationData>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_REC_EOC_DURATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RED.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RED.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResEoCDurationData tmpdata = new ExeResEoCDurationData();

                    tmpdata.id = dr[0].ToString();
                    tmpdata.eoc_type = dr[1].ToString();
                    tmpdata.step_name = dr[2].ToString();
                    tmpdata.start_time = dr[3].ToString();
                    tmpdata.end_time = dr[4].ToString();
                    tmpdata.elapsed_minutes = dr[5].ToString();
                    tmpdata.completed = dr[6].ToString();
                    tmpdata.registered_by = dr[7].ToString();
                    tmpdata.registered_date = dr[8].ToString();
                    tmpdata.last_modifier = dr[9].ToString();
                    tmpdata.last_modified_date = dr[10].ToString();

                    data.Add(tmpdata);
                }
                oraCursor.Dispose();
                RED.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RED.status = "-1";
                RED.message = ex.Message.ToString();
                RED.data = data;

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResEoCDurationData>(RED);
        }
        public static async Task<ResRptEoCCompPct> EoCCompletedPct(ReqRptEoCCompPct param)
        {
            ResRptEoCCompPct REC = new ResRptEoCCompPct();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_COMP_PCT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_COMP_PCT", OracleDbType.Varchar2, 5).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_TOTAL_GL_PULLED", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REC.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                REC.message = msgclob.Value.ToString();
                REC.eoc_report_comp_pct = cmd.Parameters["OP_COMP_PCT"].Value.ToString();
                REC.total_br_pulled_gl = cmd.Parameters["OP_TOTAL_GL_PULLED"].Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                REC.status = "-1";
                REC.message = ex.Message.ToString();
                REC.eoc_report_comp_pct = "";
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResRptEoCCompPct>(REC);
        }
        public static async Task<ResRptEoCStepDuration> GetEoCStepTimesByRptID(RegRptGetEoCStepDuration param)
        {
            ResRptEoCStepDuration Res = new ResRptEoCStepDuration();
            ExeResStepDuration step = new ExeResStepDuration();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_START_END_TIME_STEP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_START_TIME", OracleDbType.Varchar2, 17).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_END_TIME", OracleDbType.Varchar2, 17).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                step.start_time = cmd.Parameters["OP_START_TIME"].Value.ToString();
                step.end_time = cmd.Parameters["OP_END_TIME"].Value.ToString();
                Res.data = step;
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResRptEoCStepDuration>(Res);
        }
        public static async Task<BasicResponse> UpdateEoCStepDuration(ReqRptEoCUpdateStepDuration param)
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
                cmd.CommandText = "RPT_EOC_PKG.PR_UPDATE_EOC_DURATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_START_TIME", OracleDbType.Varchar2).Value = param.start_time.ToString();
                cmd.Parameters.Add("P_END_TIME", OracleDbType.Varchar2).Value = param.end_time.ToString();
                cmd.Parameters.Add("P_COMP_STAT", OracleDbType.Varchar2).Value = param.completed.ToString();
                cmd.Parameters.Add("P_UPDATER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        
        public static async Task<ResRptEoCStepDuration> GetEoCStepTimesByStepNo(RegRptGetEoCStepDurationByStepNo param)
        {
            ResRptEoCStepDuration Res = new ResRptEoCStepDuration();
            ExeResStepDuration step = new ExeResStepDuration();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_API_STEP_TIME_BY_STEP_NO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_STEP_NO", OracleDbType.Varchar2).Value = param.step_no.ToString();
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_START_TIME", OracleDbType.Varchar2, 17).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_END_TIME", OracleDbType.Varchar2, 17).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                step.start_time = cmd.Parameters["OP_START_TIME"].Value.ToString();
                step.end_time = cmd.Parameters["OP_END_TIME"].Value.ToString();
                Res.data = step;
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResRptEoCStepDuration>(Res);
        }
        public static async Task<BasicResponse> InsertPending(ReqRptEoCInsertPending param)
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
                cmd.CommandText = "RPT_EOC_PKG.PR_INSERT_EOC_PENDING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ISSUE_CATEGORY", OracleDbType.Varchar2).Value = param.issue_category.ToString();
                cmd.Parameters.Add("P_BRANCH_CODE", OracleDbType.Varchar2).Value = param.branch_code.ToString();
                cmd.Parameters.Add("P_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();
                cmd.Parameters.Add("P_MODULE", OracleDbType.Varchar2).Value = param.module.ToString();
                cmd.Parameters.Add("P_FUNCTION_ID", OracleDbType.Varchar2).Value = param.function_id.ToString();
                cmd.Parameters.Add("P_MAKER_CHALLENGE", OracleDbType.Varchar2).Value = param.maker_challenge.ToString();
                cmd.Parameters.Add("P_MAKER_SOLUTION", OracleDbType.Varchar2).Value = param.maker_solution.ToString();
                cmd.Parameters.Add("P_MAKER_SOLUTION_HTML", OracleDbType.Varchar2).Value = param.maker_solution_html.ToString();
                cmd.Parameters.Add("P_RESOLVER_SOLUTION", OracleDbType.Varchar2).Value = param.resolved_solution.ToString();
                cmd.Parameters.Add("P_RESOLVER_SOLUTION_DETAIL", OracleDbType.Varchar2).Value = param.resolved_detail.ToString();
                cmd.Parameters.Add("P_RESOLVER_SOLUCTION_DETAIL_HTML", OracleDbType.Varchar2).Value = param.resolved_detail_html.ToString();
                cmd.Parameters.Add("P_RESOLVED_STAT", OracleDbType.Varchar2).Value = param.resolved_stat.ToString();
                cmd.Parameters.Add("P_REGISTER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();

            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<ResRptEoCDataFirstLoad> EoCDataFirstLoad(ReqRptEoCData param)
        {
            ResRptEoCDataFirstLoad Res = new ResRptEoCDataFirstLoad();
            ExeResRptEoCData data = new ExeResRptEoCData();
            List<ExeResEoCDurationData> step_duration = new List<ExeResEoCDurationData>();
            List<ExeResRptEoCPendingData> pending_trn = new List<ExeResRptEoCPendingData>();
            List<ExeResRptResourceData> resource_utl = new List<ExeResRptResourceData>();
            List<ExeResRptStorageData> storage_utl = new List<ExeResRptStorageData>();
            List<ExeResRptFailureBrData> failure_branches = new List<ExeResRptFailureBrData>();
            List<ExeResRptRestorePointData> restorepoint = new List<ExeResRptRestorePointData>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            

            OracleDataAdapter a1 = new OracleDataAdapter();
            OracleDataAdapter a2 = new OracleDataAdapter();
            OracleDataAdapter a3 = new OracleDataAdapter();
            OracleDataAdapter a4 = new OracleDataAdapter();
            OracleDataAdapter a5 = new OracleDataAdapter();
            OracleDataAdapter a6 = new OracleDataAdapter();

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

            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_API_PRT_DATA_FIRST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DURATION_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_PENDING_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_RESOURCE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STORAGE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_EOC_FAILURE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_RESTOREPOINT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_DURATION_CUR"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_PENDING_CUR"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_RESOURCE_CUR"].Value;
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["OP_STORAGE_CUR"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["OP_EOC_FAILURE_CUR"].Value;
                    OracleRefCursor c6 = (OracleRefCursor)cmd.Parameters["OP_RESTOREPOINT_CUR"].Value;

                    a1.Fill(ds1, c1);
                    a2.Fill(ds2, c2);
                    a3.Fill(ds3, c3);
                    a4.Fill(ds4, c4);
                    a5.Fill(ds5, c5);
                    a6.Fill(ds6, c6);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];
                    dt4 = ds4.Tables[0];
                    dt5 = ds5.Tables[0];
                    dt6 = ds6.Tables[0];

                    foreach (DataRow dr in dt1.Rows)
                    {
                        ExeResEoCDurationData e = new ExeResEoCDurationData();

                        e.id = dr[0].ToString();
                        e.eoc_type = dr[1].ToString();
                        e.step_name = dr[2].ToString();
                        e.start_time = dr[3].ToString();
                        e.end_time = dr[4].ToString();
                        e.elapsed_minutes = dr[5].ToString();
                        e.completed = dr[6].ToString();
                        e.registered_by = dr[7].ToString();
                        e.registered_date = dr[8].ToString();
                        e.last_modifier = dr[9].ToString();
                        e.last_modified_date = dr[10].ToString();

                        step_duration.Add(e);


                    }

                    foreach (DataRow dr in dt2.Rows)
                    {
                        ExeResRptEoCPendingData e = new ExeResRptEoCPendingData();
                        e.report_id = dr[0].ToString();
                        e.issue_category = dr[1].ToString();
                        e.branch_code = dr[2].ToString();
                        e.maker_id = dr[3].ToString();
                        e.module = dr[4].ToString();
                        e.function_id = dr[5].ToString();
                        e.resolved_type = dr[6].ToString();
                        e.resolved_detail = dr[7].ToString();
                        e.resolved_stat = dr[8].ToString();
                        e.registered_by = dr[9].ToString();
                        e.registered_date = dr[10].ToString();
                        e.last_modifier = dr[11].ToString();
                        e.last_modified_date = dr[12].ToString();
                        pending_trn.Add(e);
                    }

                    foreach (DataRow dr in dt3.Rows)
                    {
                        ExeResRptResourceData e = new ExeResRptResourceData();
                        e.report_id= dr[0].ToString();
                        e.resource_name = dr[1].ToString();
                        e.min_used_memory = dr[2].ToString();
                        e.max_used_memory = dr[3].ToString();
                        e.min_used_cpu = dr[4].ToString();
                        e.max_used_cpu = dr[5].ToString();
                        e.record_stat = dr[6].ToString();
                        e.registered_by = dr[7].ToString();
                        e.registered_date = dr[8].ToString();
                        e.last_modifier = dr[9].ToString();
                        e.last_modified_date = dr[10].ToString();
                        resource_utl.Add(e);
                    }
                    foreach (DataRow dr in dt4.Rows)
                    {
                        ExeResRptStorageData e = new ExeResRptStorageData();
                        e.report_id = dr[0].ToString();
                        e.storage_name = dr[1].ToString();
                        e.used_size = dr[2].ToString();
                        e.free_size = dr[3].ToString();
                        e.total_size = dr[4].ToString();
                        e.record_stat = dr[5].ToString();
                        e.registered_by = dr[6].ToString();
                        e.registered_date = dr[7].ToString();
                        e.last_modifier = dr[8].ToString();
                        e.last_modified_date = dr[9].ToString();
                        storage_utl.Add(e);
                    }
                    foreach(DataRow dr in dt5.Rows)
                    {
                        ExeResRptFailureBrData e = new ExeResRptFailureBrData();
                        e.report_id= dr[0].ToString();
                        e.branch_code= dr[1].ToString();
                        e.eoc_ref_no = dr[2].ToString();
                        e.eod_date = dr[3].ToString();
                        e.branch_date = dr[4].ToString();
                        e.curr_stage= dr[5].ToString(); 
                        e.target_stage= dr[6].ToString();
                        e.running_stage = dr[7].ToString();
                        e.eoc_status = dr[8].ToString();
                        e.error_code = dr[9].ToString();
                        e.error_param= dr[10].ToString();
                        e.error = dr[11].ToString();
                        e.sr_no = dr[12].ToString();
                        e.resolved_stat = dr[13].ToString();
                        e.record_stat = dr[14].ToString();
                        e.registered_by = dr[15].ToString();
                        e.registered_date = dr[16].ToString();
                        e.last_modifier = dr[17].ToString();
                        e.last_modified_date = dr[18].ToString();
                        failure_branches.Add(e);
                    }
                    foreach (DataRow dr in dt6.Rows)
                    {
                        ExeResRptRestorePointData e = new ExeResRptRestorePointData();
                        e.scn = dr[0].ToString();
                        e.database_incarnation = dr[1].ToString();
                        e.guarantee_flashback_database = dr[2].ToString();
                        e.storage_size = dr[3].ToString();
                        e.time = dr[4].ToString();
                        e.restore_point_time = dr[5].ToString();
                        e.preserved = dr[6].ToString();
                        e.name = dr[7].ToString();
                        e.pdb_restore_point = dr[8].ToString();
                        e.clean_pdb_restore_point = dr[9].ToString();
                        e.pdb_incarnation = dr[10].ToString();
                        e.replicated = dr[11].ToString();
                        e.con_id = dr[12].ToString();
                        restorepoint.Add(e);
                    }
                    c1.Dispose();
                    c2.Dispose();
                    c3.Dispose();
                    c4.Dispose();
                    c5.Dispose();
                    c6.Dispose();
                }
                data.step_duration = step_duration;
                data.pending_trn = pending_trn;
                data.resource_utl = resource_utl;
                data.storage_utl = storage_utl;
                data.failure_branches = failure_branches;
                data.restorepoint = restorepoint;
                Res.data = data;

            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds1.Dispose();
                ds2.Dispose();
                ds3.Dispose();
                ds4.Dispose();
                ds5.Dispose();
                ds6.Dispose();
                dt1.Dispose();
                dt2.Dispose();
                dt3.Dispose();
                dt4.Dispose();
                dt5.Dispose();
                dt6.Dispose();
                a1.Dispose();
                a2.Dispose();
                a3.Dispose();
                a4.Dispose();
                a5.Dispose();
                a6.Dispose();
            }
            return await Task.FromResult<ResRptEoCDataFirstLoad>(Res);
        }
        public static async Task<ResEoCPendingByIDData> GetPendingDataByID(ReqRptEoCPendingByID param)
        {
            ResEoCPendingByIDData Res = new ResEoCPendingByIDData();
            ExeResEoCPendingByIDData data = new ExeResEoCPendingByIDData();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_PENDING_DATA_BY_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    data.branch_code = dr[0].ToString();
                    data.maker_id= dr[1].ToString();
                    data.module= dr[2].ToString();
                    data.function_id= dr[3].ToString();
                    data.maker_chg= dr[4].ToString();
                    data.maker_sol_html= dr[5].ToString();
                    data.resolved_type= dr[6].ToString();
                    data.resolved_detail= dr[7].ToString();
                    data.resolved_stat= dr[8].ToString();
                    data.issue_category = dr[9].ToString();
                }
                oraCursor.Dispose();
                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Res.data = data;

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResEoCPendingByIDData>(Res);
        }
        public static async Task<BasicResponse>UpdatePending(ResEoCUpdatePending param)
        {
            Core.DebugInfo("Inside UpdatePending");
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_UPDATE_EOC_PENDING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_PENDING_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_ISSUE_CATEGORY", OracleDbType.Varchar2).Value = param.issue_category.ToString();
                cmd.Parameters.Add("P_BRANCH_CODE", OracleDbType.Varchar2).Value = param.branch_code.ToString();
                cmd.Parameters.Add("P_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();
                cmd.Parameters.Add("P_MODULE", OracleDbType.Varchar2).Value = param.module.ToString();
                cmd.Parameters.Add("P_FUNCTION_ID", OracleDbType.Varchar2).Value = param.function_id.ToString();
                cmd.Parameters.Add("P_MAKER_CHALLENGE", OracleDbType.Varchar2).Value = param.maker_chg.ToString();
                cmd.Parameters.Add("P_MAKER_SOLUTION", OracleDbType.Varchar2).Value = param.maker_solution.ToString();
                cmd.Parameters.Add("P_MAKER_SOLUTION_HTML", OracleDbType.Varchar2).Value = param.maker_solution_html.ToString();
                cmd.Parameters.Add("P_RESOLVER_TYPE", OracleDbType.Varchar2).Value = param.resolved_type.ToString();
                cmd.Parameters.Add("P_RESOLVER_SOLUTION_DETAIL", OracleDbType.Varchar2).Value = param.resolved_solution.ToString();
                cmd.Parameters.Add("P_RESOLVER_SOLUCTION_DETAIL_HTML", OracleDbType.Varchar2).Value = param.resolved_solution_html.ToString();
                cmd.Parameters.Add("P_RESOLVED_STAT", OracleDbType.Varchar2).Value = param.resolved_stat.ToString();
                cmd.Parameters.Add("P_UPDATER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2,2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                
            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<ResEoCPendingData> EoCPendingData(ReqEoCPendingData param)
        {
            ResEoCPendingData Res = new ResEoCPendingData();
            List<ExeResRptEoCPendingData> data = new List<ExeResRptEoCPendingData>();

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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_PENDING_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResRptEoCPendingData e = new ExeResRptEoCPendingData();
                    e.report_id = dr[0].ToString();
                    e.issue_category = dr[1].ToString();
                    e.branch_code = dr[2].ToString();
                    e.maker_id = dr[3].ToString();
                    e.module = dr[4].ToString();
                    e.function_id = dr[5].ToString();
                    e.resolved_type = dr[6].ToString();
                    e.resolved_detail = dr[7].ToString();
                    e.resolved_stat = dr[8].ToString();
                    e.registered_by = dr[9].ToString();
                    e.registered_date = dr[10].ToString();
                    e.last_modifier = dr[11].ToString();
                    e.last_modified_date = dr[12].ToString();
                    data.Add(e);
                }
                oraCursor.Dispose();
                
                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Res.data = data;

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResEoCPendingData>(Res);
        }
        public static async Task<BasicResponse> DeleteEoCReports(ReqDeleteEoCReports param)
        {
            ArrayList report_ids = new ArrayList();
            report_ids = Core.ToArrayList(param.report_id);

            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            int I = 0;
            try
            {
                conn.Open();
                foreach (string report_id in report_ids)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "RPT_EOC_PKG.PR_API_DELETE_REPORTS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = report_id.ToString();
                    cmd.Parameters.Add("P_REPORT_TYPE", OracleDbType.Varchar2).Value = param.report_type.ToString();
                    cmd.Parameters.Add("P_DELETER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                    cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                    cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                    Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                    Res.message = msgclob.Value.ToString();
                    msgclob.Dispose();
                    if (Res.status == "-1")
                    {
                        I += 1;
                    }
                }
                if (I > 0 && report_ids.Count > 1)
                {
                    Res.status = "-1";
                    Res.message = "Some reports are failed to delete";
                }
                if (I == 0 && report_ids.Count > 1)
                {
                    Res.message = "Report ID " + param.report_id + " successfully deleted";
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                report_ids.Clear();

            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<BasicResponse> InsertEoCResource(ReqInsertEoCResource param)
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
                cmd.CommandText = "RPT_EOC_PKG.PR_INSERT_RESOURCE_UTL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RESOURCE_ID", OracleDbType.Varchar2).Value = param.resource_id.ToString();
                cmd.Parameters.Add("P_MIN_USED_MEM", OracleDbType.Varchar2).Value = param.min_mem_used.ToString();
                cmd.Parameters.Add("P_MAX_USED_MEM", OracleDbType.Varchar2).Value = param.max_mem_used.ToString();
                cmd.Parameters.Add("P_MIN_USED_CPU", OracleDbType.Varchar2).Value = param.min_cpu_used.ToString();
                cmd.Parameters.Add("P_MAX_USED_CPU", OracleDbType.Varchar2).Value = param.max_cpu_used.ToString();
                cmd.Parameters.Add("P_REGISTER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<ResEoCResourceData> EoCResourceData(ReqEoCRsourceData param)
        {
            ResEoCResourceData Res = new ResEoCResourceData();
            
            List<ExeResRptResourceData> resource_utl = new List<ExeResRptResourceData>();

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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_RESOURCES_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResRptResourceData e = new ExeResRptResourceData();
                    e.report_id = dr[0].ToString();
                    e.resource_name = dr[1].ToString();
                    e.min_used_memory = dr[2].ToString();
                    e.max_used_memory = dr[3].ToString();
                    e.min_used_cpu = dr[4].ToString();
                    e.max_used_cpu = dr[5].ToString();
                    e.record_stat = dr[6].ToString();
                    e.registered_by = dr[7].ToString();
                    e.registered_date = dr[8].ToString();
                    e.last_modifier = dr[9].ToString();
                    e.last_modified_date = dr[10].ToString();
                    resource_utl.Add(e);
                    
                }
                oraCursor.Dispose();

                Res.data = resource_utl;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
               

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResEoCResourceData>(Res);
        }
        public static async Task<ResRefEoCResource> RefreshResource()
        {
            ResRefEoCResource Res = new ResRefEoCResource();
            List<ExeResAllResources> data = new List<ExeResAllResources>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_ALL_RESOURCES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResAllResources e = new ExeResAllResources();
                    e.resource_id = Convert.ToInt32(dr[0].ToString());
                    e.resource_name = dr[1].ToString();
                    data.Add(e);

                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();


            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResRefEoCResource>(Res);
        }
        public static async Task<BasicResponse> UpdateResourceUtl(ReqUpdateResourceUtl param)
        {
            Core.DebugInfo("Inside UpdateResourceUtl");
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_UPDATE_RESOURCE_UTL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_MIN_USED_MEM", OracleDbType.Varchar2).Value = param.min_used_memory.ToString();
                cmd.Parameters.Add("P_MAX_USED_MEM", OracleDbType.Varchar2).Value = param.max_used_memory.ToString();
                cmd.Parameters.Add("P_MIN_USED_CPU", OracleDbType.Varchar2).Value = param.min_used_cpu.ToString();
                cmd.Parameters.Add("P_MAX_USED_CPU", OracleDbType.Varchar2).Value = param.max_used_cpu.ToString();
                
                cmd.Parameters.Add("P_UPDATER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<ResResourceByID> GetResourceByID(ReqGetResourceByID param)
        {
            ResResourceByID Res = new ResResourceByID();

            ExResResourceByID resource_utl = new ExResResourceByID();

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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_RESOURCE_BY_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    resource_utl.min_used_memory = dr[0].ToString();
                    resource_utl.max_used_memory = dr[1].ToString();
                    resource_utl.min_used_cpu = dr[2].ToString();
                    resource_utl.max_used_cpu = dr[3].ToString();

                }
                oraCursor.Dispose();

                Res.data = resource_utl;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();


            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResResourceByID>(Res);
        }
        public static async Task<BasicResponse> InsertSorageUtl(ReqInsertStorageUtl param)
        {
            Core.DebugInfo("Inside InsertSorageUtl");
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_INSERT_STORAGE_UTL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_STORAGE_ID", OracleDbType.Varchar2).Value = param.storage_id.ToString();
                cmd.Parameters.Add("P_TOTAL_SIZE", OracleDbType.Varchar2).Value = param.total_size.ToString();
                cmd.Parameters.Add("P_USED_SIZE", OracleDbType.Varchar2).Value = param.used_size.ToString();
                cmd.Parameters.Add("P_MESU_TOTAL_SIZE", OracleDbType.Varchar2).Value = param.total_size_mesu.ToString();
                cmd.Parameters.Add("P_MESU_USED_SIZE", OracleDbType.Varchar2).Value = param.used_size_mesu.ToString();

                cmd.Parameters.Add("P_REGISTER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<ResRefreshStorage> RefreshStorage()
        {
            ResRefreshStorage Res = new ResRefreshStorage();
            List<ExeResAllStorages> data = new List<ExeResAllStorages>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_ALL_STORAGES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResAllStorages e = new ExeResAllStorages();
                    e.storage_id = Convert.ToInt32(dr[0].ToString());
                    e.storage_name = dr[1].ToString();
                    data.Add(e);

                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();


            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResRefreshStorage>(Res);
        }
        public static async Task<ResRefreshStorageData> RefreshStorageData(ReqStorageUtlData param)
        {
            ResRefreshStorageData Res = new ResRefreshStorageData();
            List<ExeResRptStorageData> data = new List<ExeResRptStorageData>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_STORAGE_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResRptStorageData e = new ExeResRptStorageData();
                    e.report_id = dr[0].ToString();
                    e.storage_name = dr[1].ToString();
                    e.used_size = dr[2].ToString();
                    e.free_size = dr[3].ToString();
                    e.total_size = dr[4].ToString();
                    e.record_stat = dr[5].ToString();
                    e.registered_by = dr[6].ToString();
                    e.registered_date = dr[7].ToString();
                    e.last_modifier = dr[8].ToString();
                    e.last_modified_date = dr[9].ToString();
                    data.Add(e);

                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();


            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResRefreshStorageData>(Res);
        }
        public static async Task<ResGetStorageByID> GetStorageByID(ReqGetStorageByID param)
        {
            ResGetStorageByID Res = new ResGetStorageByID();
            ExeRestGetStorageByID data = new ExeRestGetStorageByID();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_STORAGE_BY_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    data.used_size = dr[0].ToString();
                    data.total_size = dr[1].ToString();

                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();


            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResGetStorageByID>(Res);
        }
        public static async Task<BasicResponse> UpdateStorageUtl(ReqEoCUpdateStorageUtl param)
        {
            Core.DebugInfo("Inside UpdateStorageUtl");
            BasicResponse Res = new BasicResponse();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_UPDATE_STORAGE_UTL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_TOTAL_SIZE", OracleDbType.Varchar2).Value = param.total_size.ToString();
                cmd.Parameters.Add("P_USED_SIZE", OracleDbType.Varchar2).Value = param.used_size.ToString();
                cmd.Parameters.Add("P_MESU_TOTAL_SIZE", OracleDbType.Varchar2).Value = param.total_size_mesu.ToString();
                cmd.Parameters.Add("P_MESU_USED_SIZE", OracleDbType.Varchar2).Value = param.used_size_mesu.ToString();

                cmd.Parameters.Add("P_UPDATER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<BasicResponse>InsertEoCFailure(ReqEoCInsertFailure param)
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
                cmd.CommandText = "RPT_EOC_PKG.PR_INSERT_EOC_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_BRANCH_CODE", OracleDbType.Varchar2).Value = param.branch_code.ToString();
                cmd.Parameters.Add("P_FAILURE_REF", OracleDbType.Varchar2).Value = param.eoc_ref_no.ToString();
                cmd.Parameters.Add("P_RESOLVE_DETAIL", OracleDbType.Varchar2).Value = param.resolved_detail.ToString();
                cmd.Parameters.Add("P_RESOLVE_DETAIL_HTML", OracleDbType.Varchar2).Value = param.resolved_detail_html.ToString();
                cmd.Parameters.Add("P_SR_NO", OracleDbType.Varchar2).Value = param.sr_no.ToString();
                cmd.Parameters.Add("P_ROOT_CAUSE_SUMMARY", OracleDbType.Varchar2).Value = param.root_cause_summary.ToString();
                cmd.Parameters.Add("P_RESOLVED_STAT", OracleDbType.Varchar2).Value = param.resolved_stat.ToString();

                cmd.Parameters.Add("P_REGISTER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<ResRefreshEoCBranchFailure> RefreshBranchFailure()
        {
            ResRefreshEoCBranchFailure Res = new ResRefreshEoCBranchFailure();
            List<ExeResBranch> data = new List<ExeResBranch>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_BRANCHES_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResBranch e = new ExeResBranch();
                    e.branch_code = dr[0].ToString();
                    e.branch_name = dr[1].ToString();
                    data.Add(e);
                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResRefreshEoCBranchFailure>(Res);
        }
        public static async Task<ResRefreshEoCRestorePointData> RefreshEoCRestorePointData()
        {
            ResRefreshEoCRestorePointData Res = new ResRefreshEoCRestorePointData();
            List<ExeResRptRestorePointData> data = new List<ExeResRptRestorePointData>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_RESTOREPOINT_EOC_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResRptRestorePointData e = new ExeResRptRestorePointData();
                    e.scn = dr[0].ToString();
                    e.database_incarnation = dr[1].ToString();
                    e.guarantee_flashback_database = dr[2].ToString();
                    e.storage_size = dr[3].ToString();
                    e.time = dr[4].ToString();
                    e.restore_point_time = dr[5].ToString();
                    e.preserved = dr[6].ToString();
                    e.name = dr[7].ToString();
                    e.pdb_restore_point = dr[8].ToString();
                    e.clean_pdb_restore_point = dr[9].ToString();
                    e.pdb_incarnation = dr[10].ToString();
                    e.replicated = dr[11].ToString();
                    e.con_id = dr[12].ToString();
                    data.Add(e);
                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResRefreshEoCRestorePointData>(Res);
        }
        public static async Task<ResRefreshEoCFailureData> RefreshEoCFailureData(ReqRefreshEoCFailureData param)
        {
            ResRefreshEoCFailureData Res = new ResRefreshEoCFailureData();
            List<ExeResRptFailureBrData> data = new List<ExeResRptFailureBrData>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_FAILURE_EOC_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = param.report_date.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeResRptFailureBrData e = new ExeResRptFailureBrData();
                    e.report_id = dr[0].ToString();
                    e.branch_code = dr[1].ToString();
                    e.eoc_ref_no = dr[2].ToString();
                    e.eod_date = dr[3].ToString();
                    e.branch_date = dr[4].ToString();
                    e.curr_stage = dr[5].ToString();
                    e.target_stage = dr[6].ToString();
                    e.running_stage = dr[7].ToString();
                    e.eoc_status = dr[8].ToString();
                    e.error_code = dr[9].ToString();
                    e.error_param = dr[10].ToString();
                    e.error = dr[11].ToString();
                    e.sr_no = dr[12].ToString();
                    e.resolved_stat = dr[13].ToString();
                    e.record_stat = dr[14].ToString();
                    e.registered_by = dr[15].ToString();
                    e.registered_date = dr[16].ToString();
                    e.last_modifier = dr[17].ToString();
                    e.last_modified_date = dr[18].ToString();
                    data.Add(e);

                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResRefreshEoCFailureData>(Res);
        }
        public static async Task<ResGetEoCFailureDataByID> GetEoCFailureDataByID (ReqGetEoCFailureDataByID param)
        {
            ResGetEoCFailureDataByID Res = new ResGetEoCFailureDataByID();
            ExeGetEoCFailureDataByID data = new ExeGetEoCFailureDataByID();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_GET_EOC_FAILURE_BY_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_CUR"].Value;
                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    data.sr_no = dr[0].ToString();
                    data.root_cause_summary = dr[1].ToString();
                    data.resolved_stat = dr[2].ToString();
                    data.resolved_detail_html = dr[3].ToString();

                }
                oraCursor.Dispose();

                Res.data = data;
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();
                dt.Dispose();
                adapter.Dispose();
            }
            return await Task.FromResult<ResGetEoCFailureDataByID>(Res);
        }
        public static async Task<BasicResponse> UpdateEoCFailure(ReqUpdateEoCFailure param)
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
                cmd.CommandText = "RPT_EOC_PKG.PR_UPDATE_EOC_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_RESOLVE_DETAIL", OracleDbType.Varchar2).Value = param.resolved_detail.ToString();
                cmd.Parameters.Add("P_RESOLVE_DETAIL_HTML", OracleDbType.Varchar2).Value = param.resolved_detail_html.ToString();
                cmd.Parameters.Add("P_SR_NO", OracleDbType.Varchar2).Value = param.sr_no.ToString();
                cmd.Parameters.Add("P_RESOLVED_STAT", OracleDbType.Varchar2).Value = param.resolved_stat.ToString();
                cmd.Parameters.Add("P_ROOT_CAUSE_SUMMARY", OracleDbType.Varchar2).Value = param.root_cause_summary.ToString();
               

                cmd.Parameters.Add("P_UPDATER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                Res.status = "-1";
                Res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<string>CurrentReportDate()
        {
            
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            string report_date="";
            try
            {
                conn.Open();
                cmd.CommandText = "SELECT TO_CHAR(RPT_EOC_PKG.FN_RPT_DATE,'DD-MON-YYYY') FROM DUAL";
                cmd.CommandType = CommandType.Text;
                report_date = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
              
                Core.DebugError(ex);
                
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            return await Task.FromResult<string>(report_date);
        }
    }

}
