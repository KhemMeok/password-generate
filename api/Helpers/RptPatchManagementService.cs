using CoreFunction;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using static ITOAPP_API.Models.RptPatchManagementModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace ITOAPP_API.Helpers
{
    public class RptPatchManagementService
    {
        public static async Task<ResDataGetTeamName> DataFristLoad()
        {

            ResDataGetTeamName resData = new ResDataGetTeamName();
            DataGetTeamName data = new DataGetTeamName();
            List<GetTeamName> listGetTeamName = new List<GetTeamName>();
            List<GetSystemName> listGetPatchNames = new List<GetSystemName>();
            List<GetHostAndVS> listGetHostName = new List<GetHostAndVS>();
            List<DataPatchComponent> listPatchComponent = new List<DataPatchComponent>();
            List<DataPatchType> listPatchType = new List<DataPatchType>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_PATCH_MANAGEMENT_PKG.GET_PATCH_MANAGEMENT_FIRST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_GET_ISA_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_GET_SYSTEM_TYPE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_GET_PATCH_TYPE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_GET_HOST_VS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_GET_PATCH_COMPONENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                resData.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                resData.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (resData.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_GET_ISA_NAME"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["P_GET_SYSTEM_TYPE"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["P_GET_HOST_VS"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["P_GET_PATCH_COMPONENT"].Value;
                    OracleRefCursor c6 = (OracleRefCursor)cmd.Parameters["P_GET_PATCH_TYPE"].Value;


                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    OracleDataAdapter ad3 = new OracleDataAdapter();
                    OracleDataAdapter ad5 = new OracleDataAdapter();
                    OracleDataAdapter ad6 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    DataSet ds3 = new DataSet();
                    DataSet ds5 = new DataSet();
                    DataSet ds6 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt5 = new DataTable();
                    DataTable dt6 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);
                    ad3.Fill(ds3, c3);
                    ad5.Fill(ds5, c5);
                    ad6.Fill(ds6, c6);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];
                    dt5 = ds5.Tables[0];
                    dt6 = ds6.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        GetTeamName gn = new GetTeamName();
                        gn.Id = dr1[0].ToString();
                        gn.Name = dr1[1].ToString();
                        listGetTeamName.Add(gn);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        GetSystemName gp = new GetSystemName();
                        gp.Id = dr2[0].ToString();
                        gp.Name = dr2[1].ToString();
                        listGetPatchNames.Add(gp);
                    }
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        GetHostAndVS gh = new GetHostAndVS();
                        gh.sysId = dr3[0].ToString();
                        gh.sysName = dr3[1].ToString();
                        gh.host_name = dr3[2].ToString();
                        gh.osVersion = dr3[3].ToString();
                        gh.site = dr3[4].ToString();
                        gh.hostId = dr3[5].ToString();
                        gh.environment = dr3[6].ToString();
                        gh.ip = dr3[7].ToString();
                        listGetHostName.Add(gh);
                    }
                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        DataPatchComponent d = new DataPatchComponent();
                        d.id = dr5[0].ToString();
                        d.name = dr5[1].ToString();
                        d.type = dr5[2].ToString();
                        listPatchComponent.Add(d);
                    }
                    foreach (DataRow dr6 in dt6.Rows)
                    {
                        DataPatchType d = new DataPatchType();
                        d.id = dr6[0].ToString();
                        d.name = dr6[1].ToString();
                        listPatchType.Add(d);
                    }

                    resData.data = data;
                    resData.data.listgetTeamName = listGetTeamName;
                    resData.data.listSystemType = listGetPatchNames;
                    resData.data.listHostAndVs = listGetHostName;
                    resData.data.listPatchComponent = listPatchComponent;
                    resData.data.listPatchType = listPatchType;

                    ds1.Dispose();
                    ds2.Dispose();
                    ds3.Dispose();
                    ds5.Dispose();

                    dt1.Dispose();
                    dt2.Dispose();
                    dt3.Dispose();
                    dt5.Dispose();

                    ad1.Dispose();
                    ad2.Dispose();
                    ad3.Dispose();
                    ad5.Dispose();

                    c1.Dispose();
                    c2.Dispose();
                    c3.Dispose();
                    c5.Dispose();
                }
            }
            catch (Exception ex)
            {
                resData.status = "-1";
                resData.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResDataGetTeamName>(resData);
        }
        public static async Task<ResGetTicKetFromHDSys> DataGetTicketFromHDSys()
        {
            ResGetTicKetFromHDSys resData = new ResGetTicKetFromHDSys();
            DataGetTicketFromHSSys data = new DataGetTicketFromHSSys();
            List<DataGetTicket> listGetTicket = new List<DataGetTicket>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_PATCH_MANAGEMENT_PKG.GET_TICKET_HD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_TICKET_HD", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                resData.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                resData.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (resData.status == "1")
                {
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["P_TICKET_HD"].Value;
                    OracleDataAdapter ad4 = new OracleDataAdapter();
                    DataSet ds4 = new DataSet();
                    DataTable dt4 = new DataTable();
                    ad4.Fill(ds4, c4);
                    dt4 = ds4.Tables[0];
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        DataGetTicket gt = new DataGetTicket();
                        gt.ticketId = dr4[0].ToString();
                        gt.request_date = dr4[1].ToString();
                        gt.requester = dr4[2].ToString();
                        gt.uat = dr4[3].ToString();
                        gt.patch_description = dr4[4].ToString();
                        gt.service_impact = dr4[5].ToString();
                        gt.priority = dr4[6].ToString();
                        gt.reviewed_by = dr4[7].ToString();
                        gt.reviewed_date = dr4[8].ToString();
                        gt.approved_by = dr4[9].ToString();
                        gt.approved_date = dr4[10].ToString();
                        gt.applied_by = dr4[11].ToString();
                        gt.applied_date = dr4[12].ToString();
                        gt.criticality = dr4[13].ToString();
                        gt.criticality_note = dr4[14].ToString();
                        listGetTicket.Add(gt);
                    }
                    data.listGetTicket = listGetTicket;
                    resData.data = data;
                    ds4.Dispose();
                    dt4.Dispose();
                    ad4.Dispose();
                    c4.Dispose();
                }
            }
            catch (Exception ex)
            {
                resData.status = "-1";
                resData.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetTicKetFromHDSys>(resData);
        }
        public static async Task<ResInsertPatch> InsertNewPatch(InsertPatch param)
        {
            ResInsertPatch Res = new ResInsertPatch();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_PATCH_MANAGEMENT_PKG.INSERT_NEW_PATCH";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_REQUEST_DATE", OracleDbType.Varchar2).Value = param.requestDate.ToString();
                cmd.Parameters.Add("P_REQUESTER", OracleDbType.Varchar2).Value = param.requester.ToString();
                cmd.Parameters.Add("P_CURR_VERSION", OracleDbType.Varchar2).Value = param.curr_version.ToString();
                cmd.Parameters.Add("P_UPGRADE_VERSION", OracleDbType.Varchar2).Value = param.upgrade_version.ToString();
                cmd.Parameters.Add("P_HOST_ID", OracleDbType.Varchar2).Value = param.host_id.ToString();
                cmd.Parameters.Add("P_SYSTEM_TYPE", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("P_PRIORITY", OracleDbType.Varchar2).Value = param.priority.ToString();
                cmd.Parameters.Add("P_UAT", OracleDbType.Varchar2).Value = param.uat.ToString();
                cmd.Parameters.Add("P_IMPACT", OracleDbType.Varchar2).Value = param.impact.ToString();
                cmd.Parameters.Add("P_OBJECTIVE", OracleDbType.Varchar2).Value = param.objective.ToString();
                cmd.Parameters.Add("P_DESCRIPTIONS", OracleDbType.Varchar2).Value = param.descriptions.ToString();
                cmd.Parameters.Add("P_DOC_ID", OracleDbType.Varchar2).Value = param.doc_id.ToString();
                cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = param.status.ToString();
                cmd.Parameters.Add("P_APPLIED_BY", OracleDbType.Varchar2).Value = param.applied_by.ToString();
                cmd.Parameters.Add("P_APPLIED_DATE", OracleDbType.Varchar2).Value = param.applied_date.ToString();
                cmd.Parameters.Add("P_REVIEWED_BY", OracleDbType.Varchar2).Value = param.reviewed_by.ToString();
                cmd.Parameters.Add("P_REVIEWED_DATE", OracleDbType.Varchar2).Value = param.reviewed_date.ToString();
                cmd.Parameters.Add("P_APPROVED_BY", OracleDbType.Varchar2).Value = param.approved_by.ToString();
                cmd.Parameters.Add("P_APPROVED_DATE", OracleDbType.Varchar2).Value = param.approved_date.ToString();
                cmd.Parameters.Add("P_SR_ID", OracleDbType.Varchar2).Value = param.sr_id.ToString();
                cmd.Parameters.Add("P_PATCH_TYPE", OracleDbType.Varchar2).Value = param.patch_type.ToString();
                cmd.Parameters.Add("P_SITE", OracleDbType.Varchar2).Value = param.site.ToString();
                cmd.Parameters.Add("P_PATCH_COMPONENT", OracleDbType.Varchar2).Value = param.patch_component.ToString();
                cmd.Parameters.Add("P_CRITICALITY", OracleDbType.Varchar2).Value = param.criticality.ToString();
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.remark.ToString();
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
            return await Task.FromResult<ResInsertPatch>(Res);
        }
        public static async Task<ResGetDataTable> GetDataTable()
        {
            ResGetDataTable resData = new ResGetDataTable();
            List<AllPatchDataTable> listGetDataTable = new List<AllPatchDataTable>();
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
                cmd.CommandText = "RPT_PATCH_MANAGEMENT_PKG.GET_DATA_TABLE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_PATCH_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                resData.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                resData.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (resData.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["P_PATCH_DATA"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {

                        AllPatchDataTable d = new AllPatchDataTable();
                        d.patch_mid = dr[0].ToString();
                        d.requestDate = dr[1].ToString();
                        d.requester = dr[2].ToString();
                        d.host_name = dr[3].ToString();
                        d.system_type = dr[4].ToString();
                        d.curr_version = dr[5].ToString();
                        d.patch_type = dr[6].ToString();
                        d.status = dr[7].ToString();
                        d.upgrade_version = dr[8].ToString();
                        d.priority = dr[9].ToString();
                        d.uat = dr[10].ToString();
                        d.host_id = dr[11].ToString();
                        d.impact = dr[12].ToString();
                        d.objective = dr[13].ToString();
                        d.descriptions = dr[14].ToString();
                        d.doc_id = dr[15].ToString();
                        d.applied_by = dr[16].ToString();
                        d.applied_date = dr[17].ToString();
                        d.reviewed_by = dr[18].ToString();
                        d.reviewed_date = dr[19].ToString();
                        d.approved_by = dr[20].ToString();
                        d.approved_date = dr[21].ToString();
                        d.sr_id = dr[22].ToString();
                        d.site = dr[23].ToString();
                        d.patch_component = dr[24].ToString();
                        d.criticality = dr[25].ToString();
                        d.remark = dr[26].ToString();
                        listGetDataTable.Add(d);
                    }
                    resData.data = listGetDataTable;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                resData.status = "-1";
                resData.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }
            return await Task.FromResult<ResGetDataTable>(resData);
        }
        public static async Task<ResDeletePatch> DeletePatch(DataDeletePatch param)
        {
            ResDeletePatch Res = new ResDeletePatch();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_PATCH_MANAGEMENT_PKG.DELETE_PATCH_INSERT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_PATCH_ID,", OracleDbType.Varchar2).Value = param.id.ToString();
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
            return await Task.FromResult<ResDeletePatch>(Res);

        }
        public static async Task<ResUpdatePatch> UpdatePatch(AllPatchDataTable param)
        {
            ResUpdatePatch Res = new ResUpdatePatch();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = " RPT_PATCH_MANAGEMENT_PKG.UPDATE_PATCH";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_PATCH_ID,", OracleDbType.Varchar2).Value = param.patch_mid.ToString();
                cmd.Parameters.Add("P_REQUEST_DATE,", OracleDbType.Varchar2).Value = param.requestDate.ToString();
                cmd.Parameters.Add("P_REQUESTER,", OracleDbType.Varchar2).Value = param.requester.ToString();
                cmd.Parameters.Add("P_CURR_VERSION,", OracleDbType.Varchar2).Value = param.curr_version.ToString();
                cmd.Parameters.Add("P_UPGRADE_VERSION,", OracleDbType.Varchar2).Value = param.upgrade_version.ToString();
                cmd.Parameters.Add("P_HOST_ID,", OracleDbType.Varchar2).Value = param.host_id.ToString();
                cmd.Parameters.Add("P_SYSTEM_TYPE,", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("P_PRIORITY,", OracleDbType.Varchar2).Value = param.priority.ToString();
                cmd.Parameters.Add("P_UAT,", OracleDbType.Varchar2).Value = param.uat.ToString();
                cmd.Parameters.Add("P_IMPACT,", OracleDbType.Varchar2).Value = param.impact.ToString();
                cmd.Parameters.Add("P_OBJECTIVE,", OracleDbType.Varchar2).Value = param.objective.ToString();
                cmd.Parameters.Add("P_DESCRIPTIONS,", OracleDbType.Varchar2).Value = param.descriptions.ToString();
                cmd.Parameters.Add("P_DOC_ID,", OracleDbType.Varchar2).Value = param.doc_id.ToString();
                cmd.Parameters.Add("P_STATUS,", OracleDbType.Varchar2).Value = param.status.ToString();
                cmd.Parameters.Add("P_APPLIED_BY,", OracleDbType.Varchar2).Value = param.applied_by.ToString();
                cmd.Parameters.Add("P_APPLIED_DATE,", OracleDbType.Varchar2).Value = param.applied_date.ToString();
                cmd.Parameters.Add("P_REVIEWED_BY,", OracleDbType.Varchar2).Value = param.reviewed_by.ToString();
                cmd.Parameters.Add("P_REVIEWED_DATE,", OracleDbType.Varchar2).Value = param.reviewed_date.ToString();
                cmd.Parameters.Add("P_APPROVED_BY,", OracleDbType.Varchar2).Value = param.approved_by.ToString();
                cmd.Parameters.Add("P_APPROVED_DATE,", OracleDbType.Varchar2).Value = param.approved_date.ToString();
                cmd.Parameters.Add("P_SR_ID,", OracleDbType.Varchar2).Value = param.sr_id.ToString();
                cmd.Parameters.Add("P_PATCH_TYPE", OracleDbType.Varchar2).Value = param.patch_type.ToString();
                cmd.Parameters.Add("P_SITE", OracleDbType.Varchar2).Value = param.site.ToString();
                cmd.Parameters.Add("P_PATCH_COMPONENT", OracleDbType.Varchar2).Value = param.patch_component.ToString();
                cmd.Parameters.Add("P_CRITICALITY", OracleDbType.Varchar2).Value = param.criticality.ToString();
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.remark.ToString();
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
            return await Task.FromResult<ResUpdatePatch>(Res);
        }
        public static async Task<ResGetPatchEdit> GetPatchEdit(DataForGetPatchEdit param)
        {
            ResGetPatchEdit resData = new ResGetPatchEdit();
            AllPatchDataTable listGetDataTable = new AllPatchDataTable();
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
                cmd.CommandText = "RPT_PATCH_MANAGEMENT_PKG.GET_PATCH_FOR_EDIT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_PATCH_ID", OracleDbType.Varchar2).Value = param.patchId.ToString();
                cmd.Parameters.Add("P_PATCH_SELECT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                resData.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                resData.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (resData.status == "1")
                {
                    OracleRefCursor c = (OracleRefCursor)cmd.Parameters["P_PATCH_SELECT"].Value;
                    adapter.Fill(ds, c);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        listGetDataTable.patch_mid = dr[0].ToString();
                        listGetDataTable.requestDate = dr[1].ToString();
                        listGetDataTable.requester = dr[2].ToString();
                        listGetDataTable.host_name = dr[3].ToString();
                        listGetDataTable.system_type = dr[4].ToString();
                        listGetDataTable.curr_version = dr[5].ToString();
                        listGetDataTable.patch_type = dr[6].ToString();
                        listGetDataTable.status = dr[7].ToString();
                        listGetDataTable.upgrade_version = dr[8].ToString();
                        listGetDataTable.priority = dr[9].ToString();
                        listGetDataTable.uat = dr[10].ToString();
                        listGetDataTable.host_id = dr[11].ToString();
                        listGetDataTable.impact = dr[12].ToString();
                        listGetDataTable.objective = dr[13].ToString();
                        listGetDataTable.descriptions = dr[14].ToString();
                        listGetDataTable.doc_id = dr[15].ToString();
                        listGetDataTable.applied_by = dr[16].ToString();
                        listGetDataTable.applied_date = dr[17].ToString();
                        listGetDataTable.reviewed_by = dr[18].ToString();
                        listGetDataTable.reviewed_date = dr[19].ToString();
                        listGetDataTable.approved_by = dr[20].ToString();
                        listGetDataTable.approved_date = dr[21].ToString();
                        listGetDataTable.sr_id = dr[22].ToString();
                        listGetDataTable.site = dr[23].ToString();
                        listGetDataTable.criticality = dr[24].ToString();
                        listGetDataTable.patch_component = dr[25].ToString();
                        listGetDataTable.remark = dr[26].ToString();
                        listGetDataTable.env = dr[27].ToString();
                    }
                    resData.data = listGetDataTable;
                    c.Dispose();
                    dt.Dispose();
                    ds.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                resData.status = "-1";
                resData.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetPatchEdit>(resData);
        }
        public static async Task<ResGetCurrentVersion> GetCurrentVerison(ReqGetCurrentVersion param)
        {
            ResGetCurrentVersion resData = new ResGetCurrentVersion();
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
                cmd.CommandText = "RPT_PATCH_MANAGEMENT_PKG.GET_CURRENT_VERSION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_PATHC_COMPONENT", OracleDbType.Varchar2).Value = param.patch_component.ToString();
                cmd.Parameters.Add("P_SYS_TYPE", OracleDbType.Varchar2).Value = param.sys_type.ToString();
                cmd.Parameters.Add("P_HOST_ID", OracleDbType.Varchar2).Value = param.host_id.ToString();
                cmd.Parameters.Add("P_VERSION", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                resData.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                resData.version = cmd.Parameters["P_VERSION"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                resData.message = msgclob.Value.ToString();
                msgclob.Dispose();

                dt.Dispose();
                ds.Dispose();
                adapter.Dispose();

            }
            catch (Exception ex)
            {
                resData.status = "-1";
                resData.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetCurrentVersion>(resData);
        }
    }
}
