using CoreFunction;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using static ITOAPP_API.Models.UserCreationModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using ITOAPP_API.Helper;

namespace ITOAPP_API.Helpers
{
    public class UserCreationService
    {
        public static async Task<ResDataUserTemplete> GetTemplete()
        {

            ResDataUserTemplete Res = new ResDataUserTemplete();
            DataUserTmpModuleSub data = new DataUserTmpModuleSub();
            List<DataUserTemplete> listTemplete = new List<DataUserTemplete>();
            List<MainModule> listmodules = new List<MainModule>();
            List<SubModule> listSubModule = new List<SubModule>();
            List<DataEndPointAction> listEndPointAction = new List<DataEndPointAction>();
            List<DataListModuleEndPoint> listModuleEndPoint = new List<DataListModuleEndPoint>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.GET_USER_TEMPLETE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_USER_TMP", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_MODULE_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_SUB_MUDULE_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_END_POINT_ACTION", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_LIST_MODULE_END_POINT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_USER_TMP"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["P_MODULE_NAME"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["P_SUB_MUDULE_NAME"].Value;
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["P_END_POINT_ACTION"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["P_LIST_MODULE_END_POINT"].Value;

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
                        DataUserTemplete a = new DataUserTemplete();
                        a.templete_Id = dr1[0].ToString();
                        a.project = dr1[1].ToString();
                        listTemplete.Add(a);
                    }

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        MainModule m = new MainModule();
                        m.module_id = dr2[0].ToString();
                        m.module_name = dr2[1].ToString();
                        m.project_name = dr2[2].ToString();
                        listmodules.Add(m);
                    }
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        SubModule s = new SubModule();
                        s.main_module_id = dr3[0].ToString();
                        s.sub_module_id = dr3[1].ToString();
                        s.sub_module_name = dr3[2].ToString();
                        listSubModule.Add(s);

                    }
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        DataEndPointAction a = new DataEndPointAction();
                        a.action_id = dr4[0].ToString();
                        a.action_name = dr4[1].ToString();
                        listEndPointAction.Add(a);
                    }
                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        DataListModuleEndPoint a = new DataListModuleEndPoint();
                        a.module_id = dr5[0].ToString();
                        a.action_name = dr5[1].ToString();
                        listModuleEndPoint.Add(a);
                    }


                    Res.data = data;
                    Res.data.user_templete = listTemplete;
                    Res.data.modules = listmodules;
                    Res.data.submodules = listSubModule;
                    Res.data.endPointActions = listEndPointAction;
                    Res.data.modeleEndPoint = listModuleEndPoint;

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


            return await Task.FromResult<ResDataUserTemplete>(Res);
        }
        public static async Task<ResDataAPIEndPoint> GetAPIEndPoint()
        {

            ResDataAPIEndPoint Res = new ResDataAPIEndPoint();
            dataEndPoint data = new dataEndPoint();
            List<DataAPIEndPoint> listEndPoint = new List<DataAPIEndPoint>();           
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            


            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.GET_END_POINT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_END_POINT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_END_POINT"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];



                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataAPIEndPoint a = new DataAPIEndPoint();
                        a.end_point_id = dr1[0].ToString();
                        a.module_id = dr1[1].ToString();
                        a.module_name = dr1[2].ToString();
                        a.sub_module_id = dr1[3].ToString();
                        a.sub_module_name = dr1[4].ToString();
                        a.project = dr1[5].ToString();
                        a.end_point_url = dr1[6].ToString();
                        a.end_point_url_substring = dr1[7].ToString();
                        a.end_point_action_name = dr1[8].ToString();
                        a.create_date = dr1[9].ToString();
                        a.action_id = dr1[10].ToString();
                        listEndPoint.Add(a);
                    }
                    Res.data = data;
                    Res.data.dataAPIEndPoints= listEndPoint;

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


            return await Task.FromResult<ResDataAPIEndPoint>(Res);
        }


        public static async Task<ResAllowUserAccessApi> AcessApi(ReqAllowUserAccessApi param)
        {

            ResAllowUserAccessApi Res = new ResAllowUserAccessApi();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.ENABLE_API_ACCESS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_NEW_USER_ID", OracleDbType.Varchar2).Value = param.new_user_id.ToString();
                cmd.Parameters.Add("P_FULLNAME", OracleDbType.Varchar2).Value = param.fullname.ToString();
                cmd.Parameters.Add("P_TEMPLETE_ID", OracleDbType.Varchar2).Value = param.templete_id.ToString();
                cmd.Parameters.Add("P_ACCESS_API", OracleDbType.Varchar2).Value = param.access_api.ToString();
                cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2).Value = param.email.ToString();
                cmd.Parameters.Add("P_PWD", OracleDbType.Varchar2).Value = CoreFunctions.Encryption(param.pwd.ToString());
                cmd.Parameters.Add("P_REQUEST_DATE", OracleDbType.Varchar2).Value = param.request_date.ToString();
                cmd.Parameters.Add("P_END_POINT_ID", OracleDbType.Varchar2).Value = param.end_point_id.ToString();
                cmd.Parameters.Add("P_ENABLE_LDAP", OracleDbType.Varchar2).Value = param.enable_ldap.ToString();
                cmd.Parameters.Add("P_TYPE", OracleDbType.Varchar2).Value = param.type.ToString();
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


            return await Task.FromResult<ResAllowUserAccessApi>(Res);
        }


        public static async Task<ResAddNewEndPoint> AddNewEndPoint(ReqAddNewEndPoint param)
        {

            ResAddNewEndPoint Res = new ResAddNewEndPoint();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.INSERT_NEW_END_POINT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_PROJECT", OracleDbType.Varchar2).Value = param.project.ToString();
                cmd.Parameters.Add("P_END_POINT_URL", OracleDbType.Varchar2).Value = param.end_point_url.ToString();
                cmd.Parameters.Add("P_REQUIRED_ENCRYPT", OracleDbType.Varchar2).Value = param.required_encrypt.ToString();
                cmd.Parameters.Add("P_MODULE", OracleDbType.Varchar2).Value = param.module.ToString();
                cmd.Parameters.Add("P_SUB_MODULE", OracleDbType.Varchar2).Value = param.sub_module.ToString();
                cmd.Parameters.Add("P_ACTION_ID", OracleDbType.Varchar2).Value = param.action_id.ToString();
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


            return await Task.FromResult<ResAddNewEndPoint>(Res);
        }
        public static async Task<ResUpdateEndPoint> UpdateEndPoint(ReqUpdateEndPoint param)
        {

            ResUpdateEndPoint Res = new ResUpdateEndPoint();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.UPDATE_NEW_END_POINT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_END_POINT_ID", OracleDbType.Varchar2).Value = param.end_point_id.ToString();
                cmd.Parameters.Add("P_PROJECT", OracleDbType.Varchar2).Value = param.project.ToString();
                cmd.Parameters.Add("P_END_POINT_URL", OracleDbType.Varchar2).Value = param.end_point_url.ToString();
                cmd.Parameters.Add("P_REQUIRED_ENCRYPT", OracleDbType.Varchar2).Value = param.required_encrypt.ToString();
                cmd.Parameters.Add("P_MODULE", OracleDbType.Varchar2).Value = param.module.ToString();
                cmd.Parameters.Add("P_SUB_MODULE", OracleDbType.Varchar2).Value = param.sub_module.ToString();
                cmd.Parameters.Add("P_ACTION_ID", OracleDbType.Varchar2).Value = param.action_id.ToString();
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


            return await Task.FromResult<ResUpdateEndPoint>(Res);
        }
        public static async Task<ResDeleteEndPoint> DeleteEndPoint(ReqDeleteEndPoint param)
        {

            ResDeleteEndPoint Res = new ResDeleteEndPoint();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.DELETE_END_POINT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_END_POINT_ID", OracleDbType.Varchar2).Value = param.end_point_id.ToString();
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


            return await Task.FromResult<ResDeleteEndPoint>(Res);
        }
        public static async Task<ResGetEndPointForUpdate> GetEndPointForUpdate(ReqGetEndPointForUpdate param)
        {

            ResGetEndPointForUpdate Res = new ResGetEndPointForUpdate();
            DataGetEndPointForUpdate data = new DataGetEndPointForUpdate();


            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.GET_END_POINT_FOR_UPDATE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_END_POINT_ID", OracleDbType.Varchar2).Value = param.end_point_id.ToString();
                cmd.Parameters.Add("P_END_POINT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_END_POINT"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataGetEndPointForUpdate a = new DataGetEndPointForUpdate();
                        a.end_point_id = dr1[0].ToString();
                        a.project_id = dr1[1].ToString();
                        a.module_id = dr1[2].ToString();
                        a.module_name = dr1[3].ToString();
                        a.sub_module_id = dr1[4].ToString();
                        a.sub_module_name = dr1[5].ToString();
                        a.end_point_url = dr1[6].ToString();
                        a.required_encrypt = dr1[7].ToString();
                        a.action_id = dr1[8].ToString();
                        data = a;


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


            return await Task.FromResult<ResGetEndPointForUpdate>(Res);
        }
        public static async Task<ResUserForUpdate> GetUserForUpdate(ReqUserForUpdate param)
        {

            ResUserForUpdate Res = new ResUserForUpdate();
            DataUserForUpdate data = new DataUserForUpdate();
            List<DataUserEndPoint> listUserEndPoint = new List<DataUserEndPoint>();
            HashSet<string> listActionSelect = new HashSet<string>();
            string arrayListAction = "";
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.GET_USER_DATA_FOR_UPDATE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = param.user_id.ToString();
                cmd.Parameters.Add("P_END_POINT_ID", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_ACCESS_API", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_USRE_PROJECT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_ALLOW_LDAP", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_END_POINT_ID"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["P_USRE_PROJECT"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["P_ACCESS_API"].Value;
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["P_ALLOW_LDAP"].Value;


                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    OracleDataAdapter ad3 = new OracleDataAdapter();
                    OracleDataAdapter ad4 = new OracleDataAdapter();


                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    DataSet ds3 = new DataSet();
                    DataSet ds4 = new DataSet();


                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();


                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);
                    ad2.Fill(ds3, c3);
                    ad2.Fill(ds4, c4);


                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    dt3 = ds3.Tables[0];
                    dt4 = ds4.Tables[0];



                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataUserEndPoint a = new DataUserEndPoint();
                        a.end_point_id = dr1[0].ToString();
                        a.end_point_url = dr1[1].ToString();
                        a.action_id = dr1[2].ToString();
                        a.sub_module_id = dr1[3].ToString();
                        listActionSelect.Add(dr1[2].ToString() + "_" + dr1[3].ToString() + ",");
                        listUserEndPoint.Add(a);

                    }

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        data.p_usre_project = dr2[0].ToString();
                    }
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        data.p_access_api = dr3[0].ToString();
                    }
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        data.p_allow_ldap = dr4[0].ToString();
                    }

                    foreach (string element in listActionSelect)
                    {
                        arrayListAction += element;
                    }
                    
                    Res.data = data;
                    Res.data.p_end_point_id = listUserEndPoint;
                    Res.action_id_and_module = arrayListAction;


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


            return await Task.FromResult<ResUserForUpdate>(Res);
        }
        public static async Task<ResFilterEndPoint> FilterEndPoint(ReqFilterEndPoint param)
        {

            ResFilterEndPoint Res = new ResFilterEndPoint();

            List<DataFilterEndPoint> listData = new List<DataFilterEndPoint>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.GET_END_POINT_BY_FILTER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_MODULE", OracleDbType.Varchar2).Value = param.p_module.ToString();
                cmd.Parameters.Add("P_SUB_MODULE", OracleDbType.Varchar2).Value = param.p_sub_module.ToString();
                cmd.Parameters.Add("P_START_DATE", OracleDbType.Varchar2).Value = param.p_start_date.ToString();
                cmd.Parameters.Add("P_END_DATE", OracleDbType.Varchar2).Value = param.p_end_date.ToString();
                cmd.Parameters.Add("P_END_POINT_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_END_POINT_DATA"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataFilterEndPoint a = new DataFilterEndPoint();
                        a.end_point_id = dr1[0].ToString();
                        a.module_name = dr1[1].ToString();
                        a.sub_module_name = dr1[2].ToString();
                        a.project = dr1[3].ToString();
                        a.end_point_url = dr1[4].ToString();
                        a.action_id = dr1[5].ToString();
                        a.create_date = dr1[6].ToString();
                        listData.Add(a);

                    }


                    Res.data = listData;
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


            return await Task.FromResult<ResFilterEndPoint>(Res);
        }

        public static async Task<ResDeleteUser> DeleteUser(ReqDeleteUser param)
        {

            ResDeleteUser Res = new ResDeleteUser();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.DELETE_USER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = param.user_id.ToString();
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


            return await Task.FromResult<ResDeleteUser>(Res);
        }
        public static async Task<ResGetEndPointSelect> GetEndPointSelect()
        {

            ResGetEndPointSelect Res = new ResGetEndPointSelect();
            ListOfEndPointSelect data = new ListOfEndPointSelect();
            List<DataGetEndPointSelect> list = new List<DataGetEndPointSelect>();


            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "ITO_USER_MGT_API.GET_END_POINT_SELECT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                //cmd.Parameters.Add("P_SUB_MODULE_ID", OracleDbType.Varchar2).Value = param.sub_module_id.ToString();
                cmd.Parameters.Add("P_END_POINT_ID", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_END_POINT_ID"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataGetEndPointSelect a = new DataGetEndPointSelect();
                        a.end_point_id = dr1[0].ToString();
                        a.sub_module_id = dr1[1].ToString();
                        list.Add(a);
                    }

                    data.listEndPoint = list;
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


            return await Task.FromResult<ResGetEndPointSelect>(Res);
        }

    }
}
