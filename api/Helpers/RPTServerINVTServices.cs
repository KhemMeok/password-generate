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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ITOAPP_API.Models.RPT_ServerINVT_Model;

namespace ITOAPP_API.Helpers
{
    public class RPTServerINVTServices
    {
        public static async Task<ResServerINVTonLoad> ServerINVTonLoad()
        {
            ResServerINVTonLoad REF = new ResServerINVTonLoad();
            DataResServerINVT data = new DataResServerINVT();
            List<ExeResServerINVTSystemType> all_system_type = new List<ExeResServerINVTSystemType>();
            List<ExeResServerINVTOSPlatform> all_OSPlatform = new List<ExeResServerINVTOSPlatform>();
            List<ExeResServerINVTCSI> all_csi = new List<ExeResServerINVTCSI>();
            List<ExeResServerINVTSYSTEMTYPEService> all_system_type_service = new List<ExeResServerINVTSYSTEMTYPEService>();
            List<ExeResServerINVTHostListing> host_listing = new List<ExeResServerINVTHostListing>();
            List<ExeResServerINVTCSIListing> csi_listing = new List<ExeResServerINVTCSIListing>();
            List<ExeResServerINVTSERVICEListing> service_listing = new List<ExeResServerINVTSERVICEListing>();
            List<DataResServerINVTGetDRof> all_dc_host = new List<DataResServerINVTGetDRof>();
            List<DataResServerINVTProductType> all_product_type = new List<DataResServerINVTProductType>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_RPT_SERVER_INVT_ONLOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("all_system_typeFORM_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_OSPlatform_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_CSI_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_SERVICE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_SYSTEM_TYPE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_DC_HOST_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("HOST_LISTING_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("CSI_LISTING_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_PRODUCT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["all_system_typeFORM_CUR"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["ALL_OSPlatform_CUR"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["HOST_LISTING_CUR"].Value;
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["ALL_CSI_CUR"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["CSI_LISTING_CUR"].Value;
                    OracleRefCursor c6 = (OracleRefCursor)cmd.Parameters["ALL_SYSTEM_TYPE_CUR"].Value;
                    OracleRefCursor c7 = (OracleRefCursor)cmd.Parameters["ALL_SERVICE_CUR"].Value;
                    OracleRefCursor c8 = (OracleRefCursor)cmd.Parameters["ALL_DC_HOST_CUR"].Value;
                    OracleRefCursor c9 = (OracleRefCursor)cmd.Parameters["ALL_PRODUCT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    OracleDataAdapter ad3 = new OracleDataAdapter();
                    OracleDataAdapter ad4 = new OracleDataAdapter();
                    OracleDataAdapter ad5 = new OracleDataAdapter();
                    OracleDataAdapter ad6 = new OracleDataAdapter();
                    OracleDataAdapter ad7 = new OracleDataAdapter();
                    OracleDataAdapter ad8 = new OracleDataAdapter();
                    OracleDataAdapter ad9 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    DataSet ds3 = new DataSet();
                    DataSet ds4 = new DataSet();
                    DataSet ds5 = new DataSet();
                    DataSet ds6 = new DataSet();
                    DataSet ds7 = new DataSet();
                    DataSet ds8 = new DataSet();
                    DataSet ds9 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();
                    DataTable dt5 = new DataTable();
                    DataTable dt6 = new DataTable();
                    DataTable dt7 = new DataTable();
                    DataTable dt8 = new DataTable();
                    DataTable dt9 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);
                    ad3.Fill(ds3, c3);
                    ad4.Fill(ds4, c4);
                    ad5.Fill(ds5, c5);
                    ad6.Fill(ds6, c6);
                    ad7.Fill(ds7, c7);
                    ad8.Fill(ds8, c8);
                    ad9.Fill(ds9, c9);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];
                    dt4 = ds4.Tables[0];
                    dt5 = ds5.Tables[0];
                    dt6 = ds6.Tables[0];
                    dt7 = ds7.Tables[0];
                    dt8 = ds8.Tables[0];
                    dt9 = ds9.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResServerINVTSystemType tmpPL = new ExeResServerINVTSystemType();
                        tmpPL.system_type = dr1[0].ToString();
                        tmpPL.pl_name = dr1[1].ToString();
                        all_system_type.Add(tmpPL);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResServerINVTOSPlatform tmpOSPlatform = new ExeResServerINVTOSPlatform();
                        tmpOSPlatform.os_id = dr2[0].ToString();
                        tmpOSPlatform.os_name = dr2[1].ToString();
                        all_OSPlatform.Add(tmpOSPlatform);
                    }
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResServerINVTHostListing tmphostlisting = new ExeResServerINVTHostListing();
                        tmphostlisting.host_id = dr3[0].ToString();
                        tmphostlisting.host_name = dr3[1].ToString();
                        tmphostlisting.product_desc = dr3[2].ToString();
                        tmphostlisting.site = dr3[3].ToString();
                        tmphostlisting.dr_of = dr3[4].ToString();
                        tmphostlisting.system_type = dr3[5].ToString();
                        tmphostlisting.os_platform = dr3[6].ToString();
                        tmphostlisting.run_service = dr3[7].ToString();
                        tmphostlisting.csi = dr3[8].ToString();
                        tmphostlisting.ip_mgmt = dr3[9].ToString();
                        tmphostlisting.ip_lan = dr3[10].ToString();
                        tmphostlisting.os_version = dr3[11].ToString();
                        tmphostlisting.environment = dr3[12].ToString();
                        tmphostlisting.remark = dr3[13].ToString();
                        tmphostlisting.record_stat = dr3[14].ToString();
                        tmphostlisting.create_date = dr3[15].ToString();
                        tmphostlisting.create_by = dr3[16].ToString();
                        tmphostlisting.last_oper_dt = dr3[17].ToString();
                        host_listing.Add(tmphostlisting);
                    }
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        ExeResServerINVTCSI tmpcsi = new ExeResServerINVTCSI();
                        tmpcsi.no = dr4[0].ToString();
                        tmpcsi.csi_number = dr4[1].ToString();
                        tmpcsi.sn = dr4[2].ToString();
                        all_csi.Add(tmpcsi);
                    }
                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        ExeResServerINVTCSIListing tmpcsilisting = new ExeResServerINVTCSIListing();
                        tmpcsilisting.no = dr5[0].ToString();
                        tmpcsilisting.csi_sla = dr5[1].ToString();
                        tmpcsilisting.sn = dr5[2].ToString();
                        tmpcsilisting.contract_type = dr5[3].ToString();
                        tmpcsilisting.product_type = dr5[4].ToString();
                        tmpcsilisting.supporter = dr5[5].ToString();
                        tmpcsilisting.contact_person = dr5[6].ToString();
                        tmpcsilisting.asr = dr5[7].ToString();
                        tmpcsilisting.start_date = dr5[8].ToString();
                        tmpcsilisting.expire_date = dr5[9].ToString();
                        tmpcsilisting.remark = dr5[10].ToString();
                        tmpcsilisting.create_date = dr5[11].ToString();
                        tmpcsilisting.create_by = dr5[12].ToString();
                        tmpcsilisting.last_oper_id = dr5[13].ToString();
                        tmpcsilisting.last_oper_dt = dr5[14].ToString();
                        csi_listing.Add(tmpcsilisting);
                    }
                    foreach (DataRow dr6 in dt6.Rows)
                    {
                        ExeResServerINVTSYSTEMTYPEService tmpsystemtype = new ExeResServerINVTSYSTEMTYPEService();
                        tmpsystemtype.system_id = dr6[0].ToString();
                        tmpsystemtype.system_name = dr6[1].ToString();
                        all_system_type_service.Add(tmpsystemtype);
                    }
                    foreach (DataRow dr7 in dt7.Rows)
                    {
                        ExeResServerINVTSERVICEListing tmpservice = new ExeResServerINVTSERVICEListing();
                        tmpservice.service_id = dr7[0].ToString();
                        tmpservice.service_type = dr7[1].ToString();
                        tmpservice.service_run = dr7[2].ToString();
                        tmpservice.host_id = dr7[3].ToString();
                        tmpservice.remark = dr7[4].ToString();
                        tmpservice.map_date = dr7[5].ToString();
                        tmpservice.map_by = dr7[6].ToString();
                        tmpservice.last_oper_id = dr7[7].ToString();
                        tmpservice.last_oper_dt = dr7[8].ToString();
                        service_listing.Add(tmpservice);
                    }
                    foreach (DataRow dr8 in dt8.Rows)
                    {
                        DataResServerINVTGetDRof tmpdchost = new DataResServerINVTGetDRof();
                        tmpdchost.host_id = dr8[0].ToString();
                        tmpdchost.host_name = dr8[1].ToString();
                        all_dc_host.Add(tmpdchost);
                    }
                    foreach (DataRow dr9 in dt9.Rows)
                    {
                        DataResServerINVTProductType tmpproduct = new DataResServerINVTProductType();
                        tmpproduct.pd_id = dr9[0].ToString();
                        tmpproduct.product_name = dr9[1].ToString();
                        all_product_type.Add(tmpproduct);
                    }
                    REF.data = data;
                    REF.data.all_system_type = all_system_type;
                    REF.data.all_os_platform = all_OSPlatform;
                    REF.data.all_csi = all_csi;
                    REF.data.host_listing = host_listing;
                    REF.data.csi_listing = csi_listing;
                    REF.data.all_system_type_service = all_system_type_service;
                    REF.data.service_listing = service_listing;
                    REF.data.all_dc_host = all_dc_host;
                    REF.data.all_product_type = all_product_type;

                    ds1.Dispose();
                    ds2.Dispose();
                    ds3.Dispose();
                    ds4.Dispose();
                    ds5.Dispose();
                    ds6.Dispose();
                    ds7.Dispose();
                    ds8.Dispose();
                    ds9.Dispose();

                    dt1.Dispose();
                    dt2.Dispose();
                    dt3.Dispose();
                    dt4.Dispose();
                    dt5.Dispose();
                    dt6.Dispose();
                    dt7.Dispose();
                    dt8.Dispose();
                    dt9.Dispose();

                    ad1.Dispose();
                    ad2.Dispose();
                    ad3.Dispose();
                    ad4.Dispose();
                    ad5.Dispose();
                    ad6.Dispose();
                    ad7.Dispose();
                    ad8.Dispose();
                    ad9.Dispose();

                    c1.Dispose();
                    c2.Dispose();
                    c3.Dispose();
                    c4.Dispose();
                    c5.Dispose();
                    c6.Dispose();
                    c7.Dispose();
                    c8.Dispose();
                    c9.Dispose();
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
            return await Task.FromResult<ResServerINVTonLoad>(REF);
        }
        public static async Task<ResServerINVTRegisterHost> ServerINVTRegisterHost(ReqServerINVTRegisterHost param)
        {
            ResServerINVTRegisterHost Res = new ResServerINVTRegisterHost();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_RPT_SERVER_INVT_REGISTER_HOST";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_HOST_NAME,", OracleDbType.Varchar2).Value = param.host_name.ToString();
                cmd.Parameters.Add("P_PRODUCT_DESC", OracleDbType.Varchar2).Value = param.product_des.ToString();
                cmd.Parameters.Add("P_site", OracleDbType.Varchar2).Value = param.site.ToString();
                cmd.Parameters.Add("P_DR_OF_HOST_ID", OracleDbType.Varchar2).Value = param.dr_of.ToString();
                cmd.Parameters.Add("P_SYSTEM_TYPE", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("P_os_platform", OracleDbType.Varchar2).Value = param.os_platform.ToString();
                cmd.Parameters.Add("P_OS_VERSION", OracleDbType.Varchar2).Value = param.os_version.ToString();
                cmd.Parameters.Add("P_CSI", OracleDbType.Varchar2).Value = param.csi.ToString();
                cmd.Parameters.Add("P_IP_MGMT", OracleDbType.Varchar2).Value = param.ip_mgt.ToString();
                cmd.Parameters.Add("P_IP_LAN", OracleDbType.Varchar2).Value = param.ip_lan.ToString();
                cmd.Parameters.Add("P_ENVIRONMENT", OracleDbType.Varchar2).Value = param.environment.ToString();
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
            return await Task.FromResult<ResServerINVTRegisterHost>(Res);
        }
        public static async Task<ResServerINVTRegisterCSI> ServerINVTRegisterCSI(ReqServerINVTRegisterCSI param)
        {
            ResServerINVTRegisterCSI Res = new ResServerINVTRegisterCSI();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;

            byte[] doc_support = Convert.FromBase64String(param.doc_support);

            try
                {
                    conn.Open();
                    cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_RPT_SERVER_INVT_REGISTER_CSI";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                    cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                    cmd.Parameters.Add("P_CSI_SLA,", OracleDbType.Varchar2).Value = param.csi_sla.ToString();
                    cmd.Parameters.Add("P_SN,", OracleDbType.Varchar2).Value = param.sn.ToString();
                    cmd.Parameters.Add("P_CONTRACT_TYPE", OracleDbType.Varchar2).Value = param.contract_type.ToString();
                    cmd.Parameters.Add("P_PRODUCT_TYPE", OracleDbType.Varchar2).Value = param.product_type.ToString();
                    cmd.Parameters.Add("P_SUPPORTER", OracleDbType.Varchar2).Value = param.supporter.ToString();
                    cmd.Parameters.Add("P_ASR", OracleDbType.Varchar2).Value = param.asr.ToString();
                    cmd.Parameters.Add("P_START_DATE", OracleDbType.Varchar2).Value = param.start_date.ToString();
                    cmd.Parameters.Add("P_EXPIRE_DATE", OracleDbType.Varchar2).Value = param.expire_date.ToString();
                    cmd.Parameters.Add("P_CONTACT_PERSON", OracleDbType.Varchar2).Value = param.contact_person.ToString();
                    cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.remark.ToString();
                    cmd.Parameters.Add("P_DOC_SUPPORT", OracleDbType.Blob).Value = doc_support;
                    cmd.Parameters.Add("P_DOC_NAME", OracleDbType.Varchar2).Value = param.doc_name.ToString();
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
            return await Task.FromResult<ResServerINVTRegisterCSI>(Res);
        }
        public static async Task<ResServerINVTServiceMapping> ServerINVTServiceMapping(ReqServerINVTServiceMapping param)
        {
            ResServerINVTServiceMapping Res = new ResServerINVTServiceMapping();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_RPT_SERVER_INVT_SERVICE_MAPPING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_TYPE,", OracleDbType.Varchar2).Value = param.service_type.ToString();
                cmd.Parameters.Add("P_SERVICE_RUN,", OracleDbType.Varchar2).Value = param.service_run.ToString();
                cmd.Parameters.Add("P_HOST_ID", OracleDbType.Varchar2).Value = param.host_id.ToString();
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
            return await Task.FromResult<ResServerINVTServiceMapping>(Res);
        }
        public static async Task<ResServerINVTAllServerListing> ServerINVTAllServerListing()
        {
            ResServerINVTAllServerListing RES = new ResServerINVTAllServerListing();
            List<ExeResServerINVTHostListing> all_host_listing = new List<ExeResServerINVTHostListing>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_GET_ALL_HOST_LISTING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResServerINVTHostListing tmp_host = new ExeResServerINVTHostListing();
                        tmp_host.host_id = dr1[0].ToString();
                        tmp_host.host_name = dr1[1].ToString();
                        tmp_host.product_desc = dr1[2].ToString();
                        tmp_host.site = dr1[3].ToString();
                        tmp_host.dr_of = dr1[4].ToString();
                        tmp_host.system_type = dr1[5].ToString();
                        tmp_host.os_platform = dr1[6].ToString();
                        tmp_host.run_service = dr1[7].ToString();
                        tmp_host.csi = dr1[8].ToString();
                        tmp_host.ip_mgmt = dr1[9].ToString();
                        tmp_host.ip_lan = dr1[10].ToString();
                        tmp_host.os_version = dr1[11].ToString();
                        tmp_host.environment = dr1[12].ToString();
                        tmp_host.remark = dr1[13].ToString();
                        tmp_host.record_stat = dr1[14].ToString();
                        tmp_host.create_date = dr1[15].ToString();
                        tmp_host.create_by = dr1[16].ToString();
                        tmp_host.last_oper_dt = dr1[17].ToString();
                        all_host_listing.Add(tmp_host);
                    }
                    RES.data = all_host_listing;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResServerINVTAllServerListing>(RES);
        }
        public static async Task<ResServerINVTAllCSIListing> ServerINVTAllCSIListing()
        {
            ResServerINVTAllCSIListing RES = new ResServerINVTAllCSIListing();
            List<ExeResServerINVTCSIListing> all_csi_listing = new List<ExeResServerINVTCSIListing>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_GET_ALL_CSI_LISTING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResServerINVTCSIListing tmp_service = new ExeResServerINVTCSIListing();
                        tmp_service.no = dr1[0].ToString();
                        tmp_service.csi_sla = dr1[1].ToString();
                        tmp_service.sn = dr1[2].ToString();
                        tmp_service.contract_type = dr1[3].ToString();
                        tmp_service.product_type = dr1[4].ToString();
                        tmp_service.supporter = dr1[5].ToString();
                        tmp_service.contact_person = dr1[6].ToString();
                        tmp_service.asr = dr1[7].ToString();
                        tmp_service.start_date = dr1[8].ToString();
                        tmp_service.expire_date = dr1[9].ToString();
                        tmp_service.remark = dr1[10].ToString();
                        tmp_service.create_date = dr1[11].ToString();
                        tmp_service.create_by = dr1[12].ToString();
                        tmp_service.last_oper_id = dr1[13].ToString();
                        tmp_service.last_oper_dt = dr1[14].ToString();
                        all_csi_listing.Add(tmp_service);
                    }
                    RES.data = all_csi_listing;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResServerINVTAllCSIListing>(RES);
        }
        public static async Task<ResServerINVTAllServiceListing> ServerINVTAllServiceListing()
        {
            ResServerINVTAllServiceListing RES = new ResServerINVTAllServiceListing();
            List<ExeResServerINVTSERVICEListing> all_service_listing = new List<ExeResServerINVTSERVICEListing>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_GET_ALL_SERVICE_LISTING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResServerINVTSERVICEListing tmp_service = new ExeResServerINVTSERVICEListing();
                        tmp_service.service_id = dr1[0].ToString();
                        tmp_service.service_type = dr1[1].ToString();
                        tmp_service.service_run = dr1[2].ToString();
                        tmp_service.host_id = dr1[3].ToString();
                        tmp_service.remark = dr1[4].ToString();
                        tmp_service.map_date = dr1[5].ToString();
                        tmp_service.map_by = dr1[6].ToString();
                        tmp_service.last_oper_id = dr1[7].ToString();
                        tmp_service.last_oper_dt = dr1[8].ToString();
                        all_service_listing.Add(tmp_service);
                    }
                    RES.data = all_service_listing;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResServerINVTAllServiceListing>(RES);
        }
        public static async Task<ResServerINVTDeleteReport> ServerINVTDeleteReport(ReqServerINVTDeleteReport param)
        {
            ResServerINVTDeleteReport RES = new ResServerINVTDeleteReport();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_DELETE_REPORTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_REPORT_ID,", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_REPORT_TYPE,", OracleDbType.Varchar2).Value = param.report_type.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResServerINVTDeleteReport>(RES);
        }
        public static async Task<ResServerINVTEditReport> ServerINVTEditReport(ReqServerINVTEditReport param)
        {
            ResServerINVTEditReport RES = new ResServerINVTEditReport();
            DataResServerINVTEditReport data = new DataResServerINVTEditReport();
            List<ExeResServerINVTEditServerData> server_data = new List<ExeResServerINVTEditServerData>();
            List<ExeResServerINVTEditServiceData> service_data = new List<ExeResServerINVTEditServiceData>();
            List<ExeResServerINVTEditCSIData> csi_data = new List<ExeResServerINVTEditCSIData>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_EDIT_REPORTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_REPORT_ID,", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_REPORT_TYPE,", OracleDbType.Varchar2).Value = param.report_type.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {

                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    if (param.report_type == "SERVER")
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            ExeResServerINVTEditServerData tmp_server = new ExeResServerINVTEditServerData()
                            {
                                host_id = dr1["HOST_ID"].ToString(),
                                host_name = dr1["HOST_NAME"].ToString(),
                                product_desc = dr1["PRODUCT_DESC"].ToString(),
                                site = dr1["SITE"].ToString(),
                                dr_of = dr1["DR_OF_HOST_ID"].ToString(),
                                system_type = dr1["SYSTEM_TYPE"].ToString(),
                                enviroment = dr1["ENVIRONMENT"].ToString(),
                                os_plat = dr1["OS_PLATFORM"].ToString(),
                                os_version = dr1["OS_VERSION"].ToString(),
                                csi = dr1["CSI"].ToString(),
                                ip_mgt = dr1["IP_MGMT"].ToString(),
                                ip_lan = dr1["IP_LAN"].ToString(),
                                remark = dr1["REMARK"].ToString(),
                            };
                            server_data.Add(tmp_server);
                        }
                    }
                    else if (param.report_type == "CSI")
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            ExeResServerINVTEditCSIData tmp_csi = new ExeResServerINVTEditCSIData()
                            {
                                csi_id = dr1["NO"].ToString(),
                                csi = dr1["CSI_SLA"].ToString(),
                                sn = dr1["SN"].ToString(),
                                contract_type = dr1["CONTRACT_TYPE"].ToString(),
                                product_type = dr1["PRODUCT_TYPE"].ToString(),
                                supporter = dr1["SUPPORTER"].ToString(),
                                asr = dr1["ASR"].ToString(),
                                start_date = dr1["START_DATE"].ToString(),
                                expire_date = dr1["EXPIRE_DATE"].ToString(),
                                contact_person = dr1["CONTACT_PERSON"].ToString(),
                                remark = dr1["REMARK"].ToString()
                            };
                            csi_data.Add(tmp_csi);
                        }
                    }
                    else if (param.report_type == "SERVICE")
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            ExeResServerINVTEditServiceData tmp_service = new ExeResServerINVTEditServiceData()
                            {
                                service_id = dr1["SERVICE_ID"].ToString(),
                                host_id = dr1["HOST_ID"].ToString(),
                                service_type = dr1["SERVICE_TYPE"].ToString(),
                                service_run = dr1["SERVICE_RUN"].ToString(),
                                remark = dr1["REMARK"].ToString()
                            };
                            service_data.Add(tmp_service);
                        }
                    }
                    RES.data = data;
                    RES.data.server_data = server_data;
                    RES.data.csi_data = csi_data;
                    RES.data.service_data = service_data;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResServerINVTEditReport>(RES);
        }
        public static async Task<ResServerINVTUpdateServerReport> ServerINVTUpdateServerReport(ReqServerINVTUpdateServerReport param)
        {
            ResServerINVTUpdateServerReport RES = new ResServerINVTUpdateServerReport();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_UPDATE_SERVER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_HOST_ID,", OracleDbType.Varchar2).Value = param.host_id.ToString();
                cmd.Parameters.Add("P_HOST_NAME,", OracleDbType.Varchar2).Value = param.host_name.ToString();
                cmd.Parameters.Add("P_PRODUCT_DESC,", OracleDbType.Varchar2).Value = param.product_desc.ToString();
                cmd.Parameters.Add("P_SITE,", OracleDbType.Varchar2).Value = param.site.ToString();
                cmd.Parameters.Add("P_DR_OF,", OracleDbType.Varchar2).Value = param.dr_of.ToString();
                cmd.Parameters.Add("P_SYSTEM_TYPE,", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("P_ENVIROMENT,", OracleDbType.Varchar2).Value = param.enviroment.ToString();
                cmd.Parameters.Add("P_OS_PLATFORM,", OracleDbType.Varchar2).Value = param.os_platform.ToString();
                cmd.Parameters.Add("P_OS_VERSION,", OracleDbType.Varchar2).Value = param.os_version.ToString();
                cmd.Parameters.Add("P_CSI,", OracleDbType.Varchar2).Value = param.csi.ToString();
                cmd.Parameters.Add("P_IP_MGT,", OracleDbType.Varchar2).Value = param.ip_mgt.ToString();
                cmd.Parameters.Add("P_IP_LAN,", OracleDbType.Varchar2).Value = param.ip_lan.ToString();
                cmd.Parameters.Add("P_REMARK,", OracleDbType.Varchar2).Value = param.remark.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResServerINVTUpdateServerReport>(RES);
        }
        public static async Task<ResServerINVTUpdateServiceReport> ServerINVTUpdateServiceReport(ReqServerINVTUpdateServiceReport param)
        {
            ResServerINVTUpdateServiceReport RES = new ResServerINVTUpdateServiceReport();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_UPDATE_SERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_ID,", OracleDbType.Varchar2).Value = param.service_id.ToString();
                cmd.Parameters.Add("P_HOST_ID,", OracleDbType.Varchar2).Value = param.host_id.ToString();
                cmd.Parameters.Add("P_SERVICE_TYPE,", OracleDbType.Varchar2).Value = param.service_type.ToString();
                cmd.Parameters.Add("P_SERVICE_RUN,", OracleDbType.Varchar2).Value = param.service_run.ToString();
                cmd.Parameters.Add("P_REMARK,", OracleDbType.Varchar2).Value = param.remark.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResServerINVTUpdateServiceReport>(RES);
        }
        public static async Task<ResServerINVTUpdateCSIReport> ServerINVTUpdateCSIReport(ReqServerINVTUpdateCSIReport param)
        {
            ResServerINVTUpdateCSIReport Res = new ResServerINVTUpdateCSIReport();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;


            //if (fileloop.Length <= 0) return;
            //byte[] fileData;
            //using (var stream = new MemoryStream((int)fileloop.Length))
            //{
            //    fileloop.CopyTo(stream);
            //    fileData = stream.ToArray();
            //}
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_UPDATE_CSI";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CSI_NO,", OracleDbType.Varchar2).Value = param.csi_no.ToString();
                cmd.Parameters.Add("P_CSI_ID,", OracleDbType.Varchar2).Value = param.csi_id.ToString();
                cmd.Parameters.Add("P_SN,", OracleDbType.Varchar2).Value = param.sn.ToString();
                cmd.Parameters.Add("P_CONTRACT_TYPE", OracleDbType.Varchar2).Value = param.contract_type.ToString();
                cmd.Parameters.Add("P_PRODUCT_TYPE", OracleDbType.Varchar2).Value = param.product_type.ToString();
                cmd.Parameters.Add("P_SUPPORTER", OracleDbType.Varchar2).Value = param.supporter.ToString();
                cmd.Parameters.Add("P_ASR", OracleDbType.Varchar2).Value = param.asr.ToString();
                cmd.Parameters.Add("P_START_DATE", OracleDbType.Varchar2).Value = param.start_date.ToString();
                cmd.Parameters.Add("P_EXPIRE_DATE", OracleDbType.Varchar2).Value = param.expire_date.ToString();
                cmd.Parameters.Add("P_CONTACT_PERSON", OracleDbType.Varchar2).Value = param.contact_person.ToString();
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
            return await Task.FromResult<ResServerINVTUpdateCSIReport>(Res);
        }
        public static async Task<ResServerINVTUploadCSIDoc> ServerINVTUploadCSIDoc(ReqServerINVTUploadCSIDoc param)
        {
            ResServerINVTUploadCSIDoc Res = new ResServerINVTUploadCSIDoc();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob; 
            if (param.doc_file.Length <= 0 || param.doc_file.Length > 10485760)
                {
                    Res.status = "-1";
                    Res.message = "Processing not allow to upload zero bytes or bigger than 10mb";
                }
            else {
                byte[] doc_support = Convert.FromBase64String(param.doc_file);
                try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_UPLOAD_DOC";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CSI_NO,", OracleDbType.Varchar2).Value = param.csi_no.ToString();
                cmd.Parameters.Add("P_DOC_NAME,", OracleDbType.Varchar2).Value = param.doc_name.ToString();
                cmd.Parameters.Add("P_DOC_FILE", OracleDbType.Blob).Value = doc_support;
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
            }
            return await Task.FromResult<ResServerINVTUploadCSIDoc>(Res);
        }
        public static async Task<ResServerINVTServerINVTGetCSIDoc> ServerINVTGetCSIDoc(ReqServerINVTGetCSIDoc param)
        {
            ResServerINVTServerINVTGetCSIDoc RES = new ResServerINVTServerINVTGetCSIDoc();
            List<ExeServerINVTServerINVTGetCSIDoc> csi_doc = new List<ExeServerINVTServerINVTGetCSIDoc>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_GET_ALL_CSI_DOC_LISTING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CSI_NO,", OracleDbType.Varchar2).Value = param.csi_no.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeServerINVTServerINVTGetCSIDoc tmp = new ExeServerINVTServerINVTGetCSIDoc();
                        tmp.doc_id = dr1[0].ToString();
                        tmp.doc_name = dr1[1].ToString();
                        tmp.doc_size = dr1[2].ToString();
                        tmp.upload_date = dr1[3].ToString();
                        tmp.uploader = dr1[4].ToString();
                        csi_doc.Add(tmp);
                    }
                    RES.data = csi_doc;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResServerINVTServerINVTGetCSIDoc>(RES);
        }
        public static async Task<ResServerINVTGetDoc4Download> ServerINVTGetDoc4Download(ReqServerINVTGetDoc4Download param)
        {
            ResServerINVTGetDoc4Download RES = new ResServerINVTGetDoc4Download();
            List<ExeServerINVTGetDoc4Download> doc_file = new List<ExeServerINVTGetDoc4Download>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_SERVER_INVT_PKG.PR_API_GET_DOC4DOWNLOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_REPORT_TYPE,", OracleDbType.Varchar2).Value = param.report_type.ToString();
                cmd.Parameters.Add("P_ID,", OracleDbType.Varchar2).Value = param.p_id.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeServerINVTGetDoc4Download tmp = new ExeServerINVTGetDoc4Download();
                        tmp.csi_no = dr1[0].ToString();
                        tmp.doc_id = dr1[1].ToString();
                        tmp.doc_name = dr1[2].ToString();
                        tmp.doc_file = dr1[3].ToString();
                        
                        doc_file.Add(tmp);
                    }
                    RES.data = doc_file;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RES.status = "-1";
                RES.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResServerINVTGetDoc4Download>(RES);
        }
    }
}
