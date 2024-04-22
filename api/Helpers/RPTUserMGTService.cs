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
using static ITOAPP_API.Models.RPTUserMGTModel;

namespace ITOAPP_API.Helpers
{
    public class RPTUserMGTService
    {
        public static async Task<ResInfoStaff> GetInfoStaff(ReqInfoStaff param)
        {
            ResInfoStaff RIS = new ResInfoStaff();
            List<ExeResInfoStaff> data = new List<ExeResInfoStaff>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.GET_INFO_STAFF";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_STAFF_ID", OracleDbType.Varchar2).Value = param.staff_id.ToString();
                cmd.Parameters.Add("OP_DATA_TYPE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RIS.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RIS.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RIS.status == "1")
                {
                    OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_TYPE"].Value;
                    OracleDataAdapter adapter = new OracleDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    adapter.Fill(ds, oraCursor);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExeResInfoStaff tmp_list_staff = new ExeResInfoStaff();
                        tmp_list_staff.staff_id = dr[0].ToString();
                        tmp_list_staff.full_name = dr[1].ToString();
                        tmp_list_staff.e_mail = dr[2].ToString();
                        tmp_list_staff.jobtile = dr[3].ToString();
                        tmp_list_staff.branch_id = dr[4].ToString();
                        tmp_list_staff.brn_dep_name = dr[5].ToString();
                        data.Add(tmp_list_staff);

                    }
                    RIS.data = data;
                    ds.Dispose();
                    dt.Dispose();
                    adapter.Dispose();
                    oraCursor.Dispose();
                }

            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RIS.status = "-1";
                RIS.message = ex.Message.ToString();

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResInfoStaff>(RIS);
        }

        public static async Task<ResSystem> GetSystem()
        {
            ResSystem RSR = new ResSystem();
            DataResSystem_type data = new DataResSystem_type();
            List<ExeResSystem_type> list_system = new List<ExeResSystem_type>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.GET_SYSTEM_NAME_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("OP_DATA_SYSTEM", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                RSR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RSR.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (RSR.status == "1")
                {
                    OracleRefCursor refcuror1 = (OracleRefCursor)cmd.Parameters["OP_DATA_SYSTEM"].Value;
                    OracleDataAdapter dadapter1 = new OracleDataAdapter();
                    DataSet dset1 = new DataSet();
                    DataTable dtable1 = new DataTable();
                    dadapter1.Fill(dset1, refcuror1);
                    dtable1 = dset1.Tables[0];
                    foreach (DataRow drow1 in dtable1.Rows)
                    {
                        ExeResSystem_type templist_system = new ExeResSystem_type();
                        templist_system.system_id = drow1[0].ToString();
                        templist_system.system_name = drow1[1].ToString();
                        list_system.Add(templist_system);
                    }
                    RSR.data = data;
                    RSR.data.list_system = list_system;

                    dset1.Dispose();
                    dtable1.Dispose();
                    dadapter1.Dispose();
                    refcuror1.Dispose();
                }

            }
            catch (Exception ex)
            {
                RSR.status = "-1";
                RSR.message = ex.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return await Task.FromResult<ResSystem>(RSR);
        }

        public static async Task<ResSystem> GetSystemByCategory(ReqCategory param)
        {
            ResSystem RSR = new ResSystem();
            DataResSystem_type data = new DataResSystem_type();
            List<ExeResSystem_type> list_system = new List<ExeResSystem_type>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.GET_SYSTEM_NAME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2).Value = param.category_id.ToString();
                cmd.Parameters.Add("OP_DATA_SYSTEM", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                RSR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RSR.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (RSR.status == "1")
                {
                    OracleRefCursor refcuror1 = (OracleRefCursor)cmd.Parameters["OP_DATA_SYSTEM"].Value;
                    OracleDataAdapter dadapter1 = new OracleDataAdapter();
                    DataSet dset1 = new DataSet();
                    DataTable dtable1 = new DataTable();
                    dadapter1.Fill(dset1, refcuror1);
                    dtable1 = dset1.Tables[0];
                    foreach (DataRow drow1 in dtable1.Rows)
                    {
                        ExeResSystem_type templist_system = new ExeResSystem_type();
                        templist_system.system_id = drow1[0].ToString();
                        templist_system.system_name = drow1[1].ToString();
                        list_system.Add(templist_system);
                    }
                    RSR.data = data;
                    RSR.data.list_system = list_system;

                    dset1.Dispose();
                    dtable1.Dispose();
                    dadapter1.Dispose();
                    refcuror1.Dispose();
                }

            }
            catch (Exception ex)
            {
                RSR.status = "-1";
                RSR.message = ex.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return await Task.FromResult<ResSystem>(RSR);
        }

        public static async Task<ResRole_system> GetRole_system(ReqSystem_type param)
        {
            ResRole_system RRS = new ResRole_system();
            DataResRole_System data = new DataResRole_System();
            List<ExeRole_System> list_role = new List<ExeRole_System>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.GET_ROLE_SYSTEM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_SYSTEM_TYPE", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("OP_DATA_ROLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                RRS.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RRS.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RRS.status == "1")
                {
                    OracleRefCursor cu1 = (OracleRefCursor)cmd.Parameters["OP_DATA_ROLE"].Value;
                    OracleDataAdapter adapter = new OracleDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    adapter.Fill(ds, cu1);
                    dt = ds.Tables[0];
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        ExeRole_System tmp_role_system = new ExeRole_System();
                        tmp_role_system.role_system_id = dr1[0].ToString();
                        tmp_role_system.role_system_name = dr1[1].ToString();
                        list_role.Add(tmp_role_system);
                    }
                    RRS.data = data;
                    RRS.data.list_role = list_role;

                    adapter.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    cu1.Dispose();
                }

            }
            catch (Exception ex)
            {
                RRS.status = "-1";
                RRS.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResRole_system>(RRS);
        }
        public static async Task<ResHost_Name> GetHost_Name(ReqRole_System param)
        {
            ResHost_Name RHN = new ResHost_Name();
            DataResHost_Name data = new DataResHost_Name();
            List<ExeHost_Name> list_hostname = new List<ExeHost_Name>();

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
                cmd.CommandText = "RPT_USER_MGT.GET_HOST_NAME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ROLE_SYSTEM", OracleDbType.Varchar2).Value = param.role_system_id.ToString();
                cmd.Parameters.Add("OP_DATA_HOST", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                RHN.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RHN.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (RHN.status == "1")
                {
                    OracleRefCursor cu1 = (OracleRefCursor)cmd.Parameters["OP_DATA_HOST"].Value;

                    adapter.Fill(ds, cu1);
                    dt = ds.Tables[0];
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        ExeHost_Name tmp_hostname = new ExeHost_Name();
                        tmp_hostname.host_id = dr1[0].ToString();
                        tmp_hostname.host_name = dr1[1].ToString();
                        list_hostname.Add(tmp_hostname);
                    }
                    RHN.data = data;
                    RHN.data.list_hostname = list_hostname;

                    adapter.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    cu1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RHN.status = "-1";
                RHN.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResHost_Name>(RHN);
        }
        public static async Task<ResService_Run> GetService_Run(ReqHost_Name param)
        {
            ResService_Run RSR = new ResService_Run();
            DataService_Run data = new DataService_Run();
            List<ExeService_Run> list_service_run = new List<ExeService_Run>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;

            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.GET_SERVICE_RUN";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_HOST", OracleDbType.Varchar2).Value = param.host_id.ToString();
                cmd.Parameters.Add("OP_DATA_SERVICE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                RSR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RSR.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (RSR.status == "1")
                {
                    OracleRefCursor cu1 = (OracleRefCursor)cmd.Parameters["OP_DATA_SERVICE"].Value;
                    OracleDataAdapter adapter = new OracleDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    adapter.Fill(ds, cu1);
                    dt = ds.Tables[0];
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        ExeService_Run tmp_service_run = new ExeService_Run();
                        tmp_service_run.service_id = dr1[0].ToString();
                        tmp_service_run.service_run = dr1[1].ToString();
                        list_service_run.Add(tmp_service_run);
                    }
                    RSR.data = data;
                    RSR.data.list_service_run = list_service_run;

                    adapter.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    cu1.Dispose();
                }

            }
            catch (Exception ex)
            {
                RSR.status = "-1";
                RSR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResService_Run>(RSR);
        }
        public static async Task<ResFirstLoad> GetFirstLoad()
        {
            ResFirstLoad RFL = new ResFirstLoad();
            DataFirstLoad data = new DataFirstLoad();
            List<ExeResCategorySystem> list_category = new List<ExeResCategorySystem>();
            List<ExeView_Request> list_view_request = new List<ExeView_Request>();
            List<ExeResSystem_type> list_system = new List<ExeResSystem_type>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.GET_FIRST_LOAD_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("OP_DATA_GATEGORG", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_VIEW", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_SYSTEM", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                RFL.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RFL.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RFL.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_DATA_GATEGORG"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_DATA_VIEW"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_DATA_SYSTEM"].Value;


                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    OracleDataAdapter ad3 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    DataSet ds3 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);
                    ad3.Fill(ds3, c3);


                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];


                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResCategorySystem tmpCategorySystem = new ExeResCategorySystem();
                        tmpCategorySystem.category_id = dr1[0].ToString();
                        tmpCategorySystem.category_name = dr1[1].ToString();
                        list_category.Add(tmpCategorySystem);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeView_Request tmpViewRequest = new ExeView_Request();
                        tmpViewRequest.system_id = dr2[0].ToString();
                        tmpViewRequest.request_id = dr2[1].ToString();
                        tmpViewRequest.request_staff_id = dr2[2].ToString();
                        tmpViewRequest.request_name = dr2[3].ToString();
                        tmpViewRequest.request_email = dr2[4].ToString();
                        tmpViewRequest.request_position = dr2[5].ToString();
                        tmpViewRequest.branch_code = dr2[6].ToString();
                        tmpViewRequest.branch_name = dr2[7].ToString();
                        tmpViewRequest.system_user = dr2[8].ToString();
                        tmpViewRequest.system_type = dr2[9].ToString();
                        tmpViewRequest.system_role = dr2[10].ToString();
                        tmpViewRequest.system_host_name = dr2[11].ToString();
                        tmpViewRequest.system_service_no = dr2[12].ToString();
                        tmpViewRequest.request_date = dr2[13].ToString();
                        tmpViewRequest.verify_date = dr2[14].ToString();
                        tmpViewRequest.effective_date = dr2[15].ToString();
                        tmpViewRequest.system_status = dr2[16].ToString();
                        tmpViewRequest.doc_name = dr2[17].ToString();
                        tmpViewRequest.doc_file = dr2[18].ToString();
                        tmpViewRequest.doc_size = dr2[19].ToString();
                        tmpViewRequest.doc_date = dr2[20].ToString();
                        tmpViewRequest.request_remark = dr2[21].ToString();
                        tmpViewRequest.record_status = dr2[22].ToString();
                        tmpViewRequest.mod_no = dr2[23].ToString();
                        tmpViewRequest.maker_id = dr2[24].ToString();
                        tmpViewRequest.create_date = dr2[25].ToString();
                        tmpViewRequest.last_oper_id = dr2[26].ToString();
                        tmpViewRequest.last_oper_date = dr2[27].ToString();
                        list_view_request.Add(tmpViewRequest);
                    }                    
                    foreach(DataRow dr3 in dt3.Rows)
                    {
                        ExeResSystem_type tempSystemType=new ExeResSystem_type();
                        tempSystemType.system_id= dr3[0].ToString();
                        tempSystemType.system_name= dr3[1].ToString();
                        list_system.Add(tempSystemType);
                    }

                    RFL.data = data;
                    RFL.data.list_category = list_category;
                    RFL.data.list_view_request = list_view_request;
                    RFL.data.list_system = list_system;

                    ds1.Dispose();
                    ds2.Dispose();
                    ds3.Dispose();

                    dt1.Dispose();
                    dt2.Dispose();
                    dt3.Dispose();

                    ad1.Dispose();
                    ad2.Dispose();
                    ad3.Dispose();

                    c1.Dispose();
                    c2.Dispose();
                    c3.Dispose();
                }
            }
            catch (Exception ex)
            {
                RFL.status = "-1";
                RFL.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResFirstLoad>(RFL);
        }

        public static async Task<ResUserSystem> GetSystem_User(ReqMaker param)
        {
            ResUserSystem RUS = new ResUserSystem();
            DataExeUserSystem data = new DataExeUserSystem();
            List<ExeUserSystem> user_system_listing = new List<ExeUserSystem>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.GET_USER_SYSTEM_PRE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_SYSTEM_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();
                cmd.Parameters.Add("OP_DATA_SYSTEM_PRE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RUS.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RUS.message = msgclob.Value.ToString();
                if (RUS.status == "1")
                {
                    OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_SYSTEM_PRE"].Value;
                    OracleDataAdapter adapter = new OracleDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    adapter.Fill(ds, oraCursor);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExeUserSystem tmpdata = new ExeUserSystem();
                        tmpdata.system_id = dr[0].ToString();
                        tmpdata.system_user = dr[1].ToString();
                        tmpdata.system_type = dr[2].ToString();
                        tmpdata.system_role = dr[3].ToString();
                        tmpdata.host_id = dr[4].ToString();
                        tmpdata.service_run = dr[5].ToString();
                        tmpdata.system_status = dr[6].ToString();
                        user_system_listing.Add(tmpdata);
                    }                    
                    RUS.data = data;
                    RUS.data.user_system_listing = user_system_listing;
                    oraCursor.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    adapter.Dispose();
                    oraCursor.Dispose();
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RUS.status = "-1";
                RUS.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResUserSystem>(RUS);
        }

        public static async Task<ResUserSystemPre> InsertSystemPre(ReqUserSystemPre param)
        {
            ResUserSystemPre RUSP = new ResUserSystemPre();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.INSERT_USER_SYSTEM_PRE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REQ_STAFF_ID", OracleDbType.Varchar2).Value = param.req_staff_id.ToString();
                cmd.Parameters.Add("P_SYSTEM_USER", OracleDbType.Varchar2).Value = param.system_user_id.ToString();
                cmd.Parameters.Add("P_SYSTEM_TYPE", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("P_SYSTEM_ROLE", OracleDbType.Varchar2).Value = param.system_role.ToString();
                cmd.Parameters.Add("P_SYSTEM_HOST_NAME", OracleDbType.Varchar2).Value = param.host_name.ToString();
                cmd.Parameters.Add("P_SYSTEM_SERVICE_NO", OracleDbType.Varchar2).Value = param.service_run.ToString();
                cmd.Parameters.Add("P_SYSTEM_STATUS", OracleDbType.Varchar2).Value = param.system_status.ToString();
                cmd.Parameters.Add("P_SYSTEM_MAKER_ID", OracleDbType.Varchar2).Value = param.system_maker.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RUSP.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RUSP.message = msgclob.Value.ToString();
            }
            catch (Exception ex)
            {
                RUSP.status = "-1";
                RUSP.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResUserSystemPre>(RUSP);
        }
        public static async Task<ResDeleteUserSystem> DeleteUserSystemPre(ReqDeleteUserSystemID param)
        {
            ArrayList report_ids = new ArrayList();
            report_ids = Core.ToArrayList(param.system_id);

            ResDeleteUserSystem RDUS = new ResDeleteUserSystem();           
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            int I = 0;
            try
            {
                conn.Open();
                foreach (string system_id in report_ids)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "RPT_USER_MGT.DELETE_USER_SYSTEM_PRE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_SYSTEM_ID", OracleDbType.Varchar2).Value = system_id.ToString();
                    cmd.Parameters.Add("P_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();
                    cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                    RDUS.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                    RDUS.message = msgclob.Value.ToString();
                    msgclob.Dispose();
                    if (RDUS.status == "-1")
                    {
                        I += 1;
                    }
                }

                if (I > 0 && report_ids.Count > 1)
                {
                    RDUS.status = "-1";
                    RDUS.message = "Some user id are failed to delete";
                }
                if (I == 0 && report_ids.Count > 1)
                {
                    RDUS.message = "User id " + param.system_id + " successfully deleted";
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RDUS.status = "-1";
                RDUS.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                report_ids.Clear();

            }
            return await Task.FromResult<ResDeleteUserSystem>(RDUS);
        }

        public static async Task<ResDeleteUserSystem> DeleteUserSystemMaker(ReqDeleteUserSystemMaker param)
        {            
            ResDeleteUserSystem RDUS = new ResDeleteUserSystem();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.DELETE_USER_SYSTEM_PRE_MAKER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RDUS.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RDUS.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RDUS.status = "-1";
                RDUS.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }
            return await Task.FromResult<ResDeleteUserSystem>(RDUS);
        }

        public static async Task<ResInsertRecord> InsertRecord(ReqInsertRecord param)
        {
            ResInsertRecord RIR = new ResInsertRecord();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            byte[] doc_file = Convert.FromBase64String(param.doc_file);
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.INSERT_REQUEST";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REQUEST_STAFF_ID", OracleDbType.Varchar2).Value = param.request_staff_id.ToString();
                cmd.Parameters.Add("P_REQUEST_NAME", OracleDbType.Varchar2).Value = param.request_name.ToString();
                cmd.Parameters.Add("P_REQUEST_EMAIL", OracleDbType.Varchar2).Value = param.request_eamil.ToString();
                cmd.Parameters.Add("P_REQUEST_POSITION", OracleDbType.Varchar2).Value = param.request_position.ToString();
                cmd.Parameters.Add("P_BRANCH_CODE", OracleDbType.Varchar2).Value = param.branch_code.ToString();
                cmd.Parameters.Add("P_BRANCH_NAME", OracleDbType.Varchar2).Value = param.branch_name.ToString();
                cmd.Parameters.Add("P_REQUEST_DATE", OracleDbType.Varchar2).Value = param.request_date.ToString();
                cmd.Parameters.Add("P_REQUEST_REMARK", OracleDbType.Varchar2).Value = param.remarks.ToString();
                cmd.Parameters.Add("P_REQUEST_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();
               
                cmd.Parameters.Add("P_SYSTEM_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();

                cmd.Parameters.Add("P_DOC_NAME", OracleDbType.Varchar2).Value = param.doc_name.ToString();
                cmd.Parameters.Add("P_DOC_FILE", OracleDbType.Blob).Value = doc_file;
                cmd.Parameters.Add("P_UPLOADER", OracleDbType.Varchar2).Value = param.maker_id.ToString();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RIR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RIR.message = msgclob.Value.ToString();
            }
            catch (Exception ex)
            {
                RIR.status = "-1";
                RIR.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResInsertRecord>(RIR);
        }
        public static async Task<ResViewRequest>ViewRequest(ReqViewRequest param)
        {
            ResViewRequest RVR = new ResViewRequest();
            List<ExeView_Request> data = new List<ExeView_Request>();

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
                cmd.CommandText = "RPT_USER_MGT.SEARCH_REQUEST";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_SYSTEM_TYPE", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("P_START_DATE", OracleDbType.Varchar2).Value = param.start_date.ToString();
                cmd.Parameters.Add("P_END_DATE", OracleDbType.Varchar2).Value = param.end_date.ToString();

                cmd.Parameters.Add("OP_DATA_VIEW", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_VIEW"].Value;
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RVR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RVR.message = msgclob.Value.ToString();

                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeView_Request tmpdata = new ExeView_Request();
                    tmpdata.system_id = dr[0].ToString();
                    tmpdata.request_id = dr[1].ToString();
                    tmpdata.request_staff_id = dr[2].ToString();
                    tmpdata.request_name = dr[3].ToString();
                    tmpdata.request_email = dr[4].ToString();
                    tmpdata.request_position = dr[5].ToString();
                    tmpdata.branch_code = dr[6].ToString();
                    tmpdata.branch_name = dr[7].ToString();
                    tmpdata.system_user = dr[8].ToString();
                    tmpdata.system_type = dr[9].ToString();
                    tmpdata.system_role = dr[10].ToString();
                    tmpdata.system_host_name = dr[11].ToString();
                    tmpdata.system_service_no = dr[12].ToString();
                    tmpdata.request_date = dr[13].ToString();
                    tmpdata.verify_date = dr[14].ToString();
                    tmpdata.effective_date = dr[15].ToString();
                    tmpdata.system_status = dr[16].ToString();
                    tmpdata.doc_name = dr[17].ToString();
                    tmpdata.doc_file = dr[18].ToString();
                    tmpdata.doc_size = dr[19].ToString();
                    tmpdata.doc_date = dr[20].ToString();
                    tmpdata.request_remark = dr[21].ToString();
                    tmpdata.record_status = dr[22].ToString();
                    tmpdata.mod_no = dr[23].ToString();
                    tmpdata.maker_id = dr[24].ToString();
                    tmpdata.create_date=dr[25].ToString();
                    tmpdata.last_oper_id= dr[26].ToString();
                    tmpdata.last_oper_date = dr[27].ToString();
                    data.Add(tmpdata);
                }
                oraCursor.Dispose();
                RVR.data = data;

            }
            catch (Exception ex)
            {
                RVR.status = ("-1");
                RVR.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResViewRequest>(RVR);
        }

        public static async Task<ResViewRequest> RefreshRequest()
        {
            ResViewRequest RVR = new ResViewRequest();
            List<ExeView_Request> data = new List<ExeView_Request>();

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
                cmd.CommandText = "RPT_USER_MGT.VIEW_REQUEST";
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.Add("OP_DATA_VIEW", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DATA_VIEW"].Value;
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RVR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RVR.message = msgclob.Value.ToString();

                adapter.Fill(ds, oraCursor);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ExeView_Request tmpdata = new ExeView_Request();
                    tmpdata.system_id = dr[0].ToString();
                    tmpdata.request_id = dr[1].ToString();
                    tmpdata.request_staff_id = dr[2].ToString();
                    tmpdata.request_name = dr[3].ToString();
                    tmpdata.request_email = dr[4].ToString();
                    tmpdata.request_position = dr[5].ToString();
                    tmpdata.branch_code = dr[6].ToString();
                    tmpdata.branch_name = dr[7].ToString();
                    tmpdata.system_user = dr[8].ToString();
                    tmpdata.system_type = dr[9].ToString();
                    tmpdata.system_role = dr[10].ToString();
                    tmpdata.system_host_name = dr[11].ToString();
                    tmpdata.system_service_no = dr[12].ToString();
                    tmpdata.request_date = dr[13].ToString();
                    tmpdata.verify_date = dr[14].ToString();
                    tmpdata.effective_date = dr[15].ToString();
                    tmpdata.system_status = dr[16].ToString();
                    tmpdata.doc_name = dr[17].ToString();
                    tmpdata.doc_file = dr[18].ToString();
                    tmpdata.doc_size = dr[19].ToString();
                    tmpdata.doc_date = dr[20].ToString();
                    tmpdata.request_remark = dr[21].ToString();
                    tmpdata.record_status = dr[22].ToString();
                    tmpdata.mod_no=dr[23].ToString();
                    tmpdata.maker_id = dr[24].ToString();
                    tmpdata.create_date = dr[25].ToString();
                    tmpdata.last_oper_id = dr[26].ToString();
                    tmpdata.last_oper_date = dr[27].ToString();
                    data.Add(tmpdata);
                }
                oraCursor.Dispose();
                RVR.data = data;

            }
            catch (Exception ex)
            {
                RVR.status = ("-1");
                RVR.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResViewRequest>(RVR);
        }

        public static async Task<ResDeleteRequest> DeleteRequest(ReqDeleteRequest param)
        {
            ArrayList report_ids = new ArrayList();
            report_ids = Core.ToArrayList(param.request_id);

            ResDeleteRequest RDR = new ResDeleteRequest();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            int I = 0;
            try
            {
                conn.Open();
                foreach (string request_id in report_ids)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "RPT_USER_MGT.DELECT_REQUEST";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_SYS_ID", OracleDbType.Varchar2).Value = request_id.ToString();
                    cmd.Parameters.Add("P_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();
                    cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                    RDR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                    RDR.message = msgclob.Value.ToString();
                    msgclob.Dispose();
                    if (RDR.status == "-1")
                    {
                        I += 1;
                    }
                }
                if (I > 0 && report_ids.Count > 1)
                {
                    RDR.status = "-1";
                    RDR.message = "Some request are failed to delete";
                }
                if (I == 0 && report_ids.Count > 1)
                {
                    RDR.message = "Delete request successfully";
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RDR.status = "-1";
                RDR.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                report_ids.Clear();

            }

            return await Task.FromResult<ResDeleteRequest>(RDR);
        }

        public static async Task<ResDownloadDocument> DownloadDocument(ReqDownloadDocument param)
        {
            ResDownloadDocument RDD = new ResDownloadDocument();
            List<ExeDownloadDocument> data = new List<ExeDownloadDocument>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.DOWNLOAD_DOC_REQUEST";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_REQUEST_ID", OracleDbType.Varchar2).Value = param.request_id.ToString();
                cmd.Parameters.Add("OP_DOWNLOAD_DOC", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RDD.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RDD.message = msgclob.Value.ToString();

                msgclob.Dispose();
                if (RDD.status == "1")
                {
                    OracleRefCursor oraCursor = (OracleRefCursor)cmd.Parameters["OP_DOWNLOAD_DOC"].Value;
                    OracleDataAdapter adapter = new OracleDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    adapter.Fill(ds, oraCursor);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExeDownloadDocument tmpdata = new ExeDownloadDocument();
                        tmpdata.request_id = dr[0].ToString();
                        tmpdata.doc_id = dr[1].ToString();
                        tmpdata.doc_name = dr[2].ToString();
                        tmpdata.doc_file = dr[3].ToString();
                        data.Add(tmpdata);
                    }
                    oraCursor.Dispose();
                    RDD.data = data;
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RDD.status = "-1";
                RDD.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResDownloadDocument>(RDD);
        }
        public static async Task<ResDataModify> ModifyRequest(ReqDataModify param)
        {
            ResDataModify RDM = new ResDataModify();
            DataModify data = new DataModify();
            List<ExeDataModify> modify_request = new List<ExeDataModify>();
            List<ExeRole_System> modify_role = new List<ExeRole_System>();
            List<ExeHost_Name> modify_host_name = new List<ExeHost_Name>();
            List<ExeService_Run> modify_service_run = new List<ExeService_Run>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.DATA_MODIFY_REQUEST";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_SYS_ID", OracleDbType.Varchar2).Value = param.request_id.ToString();
                cmd.Parameters.Add("OP_DATA_MODIFY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_ROLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_HOST", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_SERVICE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RDM.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RDM.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RDM.status == "1")
                {
                    OracleRefCursor refcuror1 = (OracleRefCursor)cmd.Parameters["OP_DATA_MODIFY"].Value;
                    OracleRefCursor refcuror2 = (OracleRefCursor)cmd.Parameters["OP_DATA_ROLE"].Value;
                    OracleRefCursor refcuror3 = (OracleRefCursor)cmd.Parameters["OP_DATA_HOST"].Value;
                    OracleRefCursor refcuror4 = (OracleRefCursor)cmd.Parameters["OP_DATA_SERVICE"].Value;


                    OracleDataAdapter dadapter1 = new OracleDataAdapter();
                    OracleDataAdapter dadapter2 = new OracleDataAdapter();
                    OracleDataAdapter dadapter3 = new OracleDataAdapter();
                    OracleDataAdapter dadapter4 = new OracleDataAdapter();

                    DataSet dset1 = new DataSet();
                    DataSet dset2 = new DataSet();
                    DataSet dset3 = new DataSet();
                    DataSet dset4 = new DataSet();

                    DataTable dtable1 = new DataTable();
                    DataTable dtable2 = new DataTable();
                    DataTable dtable3 = new DataTable();
                    DataTable dtable4 = new DataTable();

                    dadapter1.Fill(dset1, refcuror1);
                    dadapter1.Fill(dset2, refcuror2);
                    dadapter1.Fill(dset3, refcuror3);
                    dadapter1.Fill(dset4, refcuror4);

                    dtable1 = dset1.Tables[0];
                    dtable2 = dset2.Tables[0];
                    dtable3 = dset3.Tables[0];
                    dtable4 = dset4.Tables[0];

                    foreach (DataRow drow1 in dtable1.Rows)
                    {
                        ExeDataModify amplist_modify = new ExeDataModify();
                        amplist_modify.request_id = drow1[0].ToString();
                        amplist_modify.staff_id = drow1[1].ToString();
                        amplist_modify.staff_name = drow1[2].ToString();
                        amplist_modify.email = drow1[3].ToString();
                        amplist_modify.position = drow1[4].ToString();
                        amplist_modify.branch_id = drow1[5].ToString();
                        amplist_modify.brn_dep_name = drow1[6].ToString();
                        amplist_modify.req_date = drow1[7].ToString();
                        amplist_modify.verify_date = drow1[8].ToString();
                        amplist_modify.effe_date = drow1[9].ToString();
                        amplist_modify.user_id = drow1[10].ToString();
                        amplist_modify.system_type = drow1[11].ToString();
                        amplist_modify.role_type = drow1[12].ToString();
                        amplist_modify.host_name = drow1[13].ToString();
                        amplist_modify.service_run = drow1[14].ToString();
                        amplist_modify.status = drow1[15].ToString();
                        amplist_modify.req_remark = drow1[16].ToString();
                        //amplist_modify.doc_file = drow1[7].ToString();
                        modify_request.Add(amplist_modify);
                    };

                    foreach (DataRow drow2 in dtable2.Rows)
                    {
                        ExeRole_System tmpRoleSystem = new ExeRole_System();
                        tmpRoleSystem.role_system_id = drow2[0].ToString();
                        tmpRoleSystem.role_system_name = drow2[1].ToString();
                        modify_role.Add(tmpRoleSystem);
                    };
                    foreach (DataRow drow3 in dtable3.Rows)
                    {
                        ExeHost_Name tmp_hostname = new ExeHost_Name();
                        tmp_hostname.host_id = drow3[0].ToString();
                        tmp_hostname.host_name = drow3[1].ToString();
                        modify_host_name.Add(tmp_hostname);
                    };
                    foreach (DataRow drow4 in dtable4.Rows)
                    {
                        ExeService_Run tmp_service_run = new ExeService_Run();
                        tmp_service_run.service_id = drow4[0].ToString();
                        tmp_service_run.service_run = drow4[1].ToString();
                        modify_service_run.Add(tmp_service_run);
                    };


                    RDM.data = data;
                    RDM.data.modify_request = modify_request;
                    RDM.data.modify_role = modify_role;
                    RDM.data.modify_host_name = modify_host_name;
                    RDM.data.modify_service_run = modify_service_run;


                    dset1.Dispose();
                    dset2.Dispose();
                    dset3.Dispose();
                    dset4.Dispose();

                    dtable1.Dispose();
                    dtable2.Dispose();
                    dtable3.Dispose();
                    dtable4.Dispose();

                    dadapter1.Dispose();
                    dadapter2.Dispose();
                    dadapter3.Dispose();
                    dadapter4.Dispose();

                    refcuror1.Dispose();
                    refcuror2.Dispose();
                    refcuror3.Dispose();
                    refcuror4.Dispose();
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RDM.status = "-1";
                RDM.message = ex.Message.ToString();

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return await Task.FromResult<ResDataModify>(RDM);
        }

        public static async Task<ResUpdateRequest> UpdateRequest(ReqUpdateRequest param)
        {
            ResUpdateRequest RUR = new ResUpdateRequest();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            byte[] doc_file = Convert.FromBase64String(param.doc_file);
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_USER_MGT.UPDATE_REQUEST_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_SYS_ID", OracleDbType.Varchar2).Value = param.request_id.ToString();
                cmd.Parameters.Add("P_REQUEST_STAFF_ID", OracleDbType.Varchar2).Value = param.staff_id.ToString();
                cmd.Parameters.Add("P_REQUEST_NAME", OracleDbType.Varchar2).Value = param.staff_name.ToString();
                cmd.Parameters.Add("P_REQUEST_EMAIL", OracleDbType.Varchar2).Value = param.staff_email.ToString();
                cmd.Parameters.Add("P_REQUEST_POSITION", OracleDbType.Varchar2).Value = param.position.ToString();
                cmd.Parameters.Add("P_BRANCH_CODE", OracleDbType.Varchar2).Value = param.brn_code.ToString();
                cmd.Parameters.Add("P_BRANCH_NAME", OracleDbType.Varchar2).Value = param.brn_name.ToString();
                cmd.Parameters.Add("P_REQUEST_DATE", OracleDbType.Varchar2).Value = param.req_date.ToString();
                cmd.Parameters.Add("P_VERIFY_DATE", OracleDbType.Varchar2).Value = param.verify_date.ToString();
                cmd.Parameters.Add("P_EFFECTIVE_DATE", OracleDbType.Varchar2).Value = param.effective_date.ToString();
                cmd.Parameters.Add("P_REQUEST_REMARK", OracleDbType.Varchar2).Value = param.req_remark.ToString();

                cmd.Parameters.Add("P_SYSTEM_USER", OracleDbType.Varchar2).Value = param.system_user.ToString();
                cmd.Parameters.Add("P_SYSTEM_TYPE", OracleDbType.Varchar2).Value = param.system_type.ToString();
                cmd.Parameters.Add("P_SYSTEM_ROLE", OracleDbType.Varchar2).Value = param.system_role.ToString();
                cmd.Parameters.Add("P_SYSTEM_HOST_NAME", OracleDbType.Varchar2).Value = param.system_host_name.ToString();
                cmd.Parameters.Add("P_SYSTEM_SERVICE_NO", OracleDbType.Varchar2).Value = param.system_service_no.ToString();
                cmd.Parameters.Add("P_SYSTEM_STATUS", OracleDbType.Varchar2).Value = param.system_status.ToString();

                cmd.Parameters.Add("P_DOC_NAME", OracleDbType.Varchar2).Value = param.doc_name.ToString();
                cmd.Parameters.Add("P_DOC_FILE", OracleDbType.Blob).Value = doc_file;
                cmd.Parameters.Add("P_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RUR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RUR.message = msgclob.Value.ToString();

            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RUR.status = ("-1");
                RUR.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResUpdateRequest>(RUR);
        }
        public static async Task<ResEffectiveDate> AddEffective(ReqEffectiveDate param)
        {
            ArrayList report_ids = new ArrayList();
            report_ids = Core.ToArrayList(param.system_id);

            ResEffectiveDate RED = new ResEffectiveDate();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            int I = 0;
            try
            {
                conn.Open();
                foreach (string system_id in report_ids)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "RPT_USER_MGT.EFFECTIVE_DATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_SYS_ID", OracleDbType.Varchar2).Value = system_id.ToString();
                    cmd.Parameters.Add("P_VERIFY_DATE", OracleDbType.Varchar2).Value = param.verify_date.ToString();
                    cmd.Parameters.Add("P_EFFECTIVE", OracleDbType.Varchar2).Value = param.effective_date.ToString();
                    cmd.Parameters.Add("P_MAKER_ID", OracleDbType.Varchar2).Value = param.maker_id.ToString();

                    cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                    RED.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                    RED.message = msgclob.Value.ToString();
                    msgclob.Dispose();
                    if (RED.status == "-1")
                    {
                        I += 1;
                    }
                }
                if (I > 0 && report_ids.Count > 1)
                {
                    RED.status = "-1";
                    RED.message = "Some effective date are failed";
                }
                if (I == 0 && report_ids.Count > 1)
                {
                    RED.message = "Add effective date successfully";
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RED.status = ("-1");
                RED.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                report_ids.Clear(); 
            }
            return await Task.FromResult<ResEffectiveDate>(RED);
        }
    }
}
