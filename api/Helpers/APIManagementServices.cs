using CoreFunction;
using ITOAPP_API.Helper;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ITOAPP_API.Models.APIManagementModel;

namespace ITOAPP_API.Helpers
{
    public class APIManagementServices
    {
        

        public static async Task<ResAPIMonLoad> ResAPIMonLoad()
        {
            ResAPIMonLoad REF = new ResAPIMonLoad();
            DataResServiceType data = new DataResServiceType();
            List<ExeResAPIMServiceType> all_service_type = new List<ExeResAPIMServiceType>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_SERVICE_ONLOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("all_service_typeFORM_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["all_service_typeFORM_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIMServiceType tmpPL = new ExeResAPIMServiceType();
                        tmpPL.service_name = dr1[0].ToString();
                        tmpPL.service_desc = dr1[1].ToString();
                        all_service_type.Add(tmpPL);
                    }

                    REF.data = data;
                    REF.data.all_service_type = all_service_type;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIMonLoad>(REF);
        }
        public static async Task<ResAPITonLoad> ResAPITonLoad()
        {
            ResAPITonLoad REF = new ResAPITonLoad();
            DataResTraServiceType data = new DataResTraServiceType();
            List<ExeResAPITServiceType> all_transaction_service_type = new List<ExeResAPITServiceType>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_TRANSACTION_SERVICE_ONLOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("all_service_typeFORM_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["all_service_typeFORM_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPITServiceType tmpPL = new ExeResAPITServiceType();
                        tmpPL.service_name = dr1[0].ToString();
                        tmpPL.service_desc = dr1[1].ToString();
                        all_transaction_service_type.Add(tmpPL);
                    }

                    REF.data = data;
                    REF.data.all_transaction_service_type = all_transaction_service_type;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPITonLoad>(REF);
        }
        public static async Task<ResAPIPQuery> GetAPIPQuery(ReqAPIParameter parameter)
        {
            ResAPIPQuery REF = new ResAPIPQuery();
            List<ExeResAPIPType> all_parameter_type = new List<ExeResAPIPType>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_PARAMETER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_NAME", OracleDbType.Varchar2).Value = parameter.parameter_id.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPType tmpPL = new ExeResAPIPType();
                        tmpPL.param_name = dr1[0].ToString();
                        tmpPL.param_value = dr1[1].ToString();
                        tmpPL.system = dr1[2].ToString();
                        all_parameter_type.Add(tmpPL);
                    }

                    REF.data = all_parameter_type;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPQuery>(REF);
        }
        public static async Task<ResAPIPQuery> GetAPIPCheckQuery(ReqAPIParamCheck param)
        {
            ResAPIPQuery REF = new ResAPIPQuery();
            List<ExeResAPIPType> all_parameter_type = new List<ExeResAPIPType>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_PARAM_CHECK_EDIT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_PARAM_NAME", OracleDbType.Varchar2).Value = param.param_name.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPType tmpPL = new ExeResAPIPType();
                        tmpPL.param_name = dr1[0].ToString();
                        tmpPL.param_value = dr1[1].ToString();
                        tmpPL.system = dr1[2].ToString();
                        all_parameter_type.Add(tmpPL);
                    }

                    REF.data = all_parameter_type;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPQuery>(REF);
        }
        public static async Task<ResAPIPUpdate> GetAPIPUpdate(ReqAPIParam param)
        {
            ResAPIPUpdate RE = new ResAPIPUpdate();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_UPDATE_PARAMETER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_TYPE", OracleDbType.Varchar2).Value = param.type.ToString();
                cmd.Parameters.Add("P_PARAMETER_NAME", OracleDbType.Varchar2).Value = param.parameter.ToString();
                cmd.Parameters.Add("P_PARAM_NAME", OracleDbType.Varchar2).Value = param.param_name.ToString();
                cmd.Parameters.Add("P_PARAM_VALUE", OracleDbType.Varchar2).Value = param.param_value.ToString();
                cmd.Parameters.Add("P_SYSTEM", OracleDbType.Varchar2).Value = param.system.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
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

            }
            return await Task.FromResult<ResAPIPUpdate>(RE);
        }
        
        public static async Task<ResAPITQuery> GetAPITransactionQuery(ReqAPITransaction transaction)
        {
            ResAPITQuery RE = new ResAPITQuery();
            List<ExeResAPITransaction> all_transaction_type = new List<ExeResAPITransaction>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_TRANSACTION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_TRANSACTION_TYPE", OracleDbType.Varchar2).Value = transaction.transaction_type.ToString();
                cmd.Parameters.Add("P_FROMDATE", OracleDbType.Varchar2).Value = transaction.fromdate.ToString();
                cmd.Parameters.Add("P_TODATE", OracleDbType.Varchar2).Value = transaction.todate.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPITransaction tmpPL = new ExeResAPITransaction();
                        tmpPL.reference_no = dr1[0].ToString();
                        tmpPL.reference_no1 = dr1[1].ToString();
                        tmpPL.date = dr1[2].ToString();
                        tmpPL.status = dr1[3].ToString();
                        tmpPL.message = dr1[4].ToString();
                        tmpPL.customer = dr1[5].ToString();
                        tmpPL.amount = dr1[6].ToString();
                        tmpPL.currency = dr1[7].ToString();
                        all_transaction_type.Add(tmpPL);
                    }

                    RE.data = all_transaction_type;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
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

            }
            return await Task.FromResult<ResAPITQuery>(RE);
        }
        
        public static async Task<ResAPITDetail> GetAPITransactionDetail(ReqAPITransactionDetail transactions)
        {
            ResAPITDetail RE = new ResAPITDetail();
            List<ExeResAPITransactionDetail> all_transaction_detail_type = new List<ExeResAPITransactionDetail>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_ACTIONLOG_TRANSACTION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_TYPE", OracleDbType.Varchar2).Value = transactions.service_type.ToString();
                cmd.Parameters.Add("P_TRANSACTION_REF", OracleDbType.Varchar2).Value = transactions.ref_no.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPITransactionDetail tmpPL = new ExeResAPITransactionDetail();
                        tmpPL.record_no = dr1[0].ToString();
                        tmpPL.reference_no = dr1[1].ToString();
                        tmpPL.action_type = dr1[2].ToString();
                        tmpPL.action_date = dr1[3].ToString();
                        tmpPL.request_data = dr1[4].ToString();
                        tmpPL.response_data = dr1[5].ToString();
                        tmpPL.header_data = dr1[6].ToString();
                        all_transaction_detail_type.Add(tmpPL);
                    }

                    RE.data = all_transaction_detail_type ;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
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

            }
            return await Task.FromResult<ResAPITDetail>(RE);
        }
        public static async Task<ResAPITGetRequestData> GetAPITRequestData(ReqAPITGetRequestdata req_data)
        {
            ResAPITGetRequestData RE = new ResAPITGetRequestData();
            List<ExeResAPITGetRequestData> detail_data1 = new List<ExeResAPITGetRequestData>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_TRANSACTION_GET_REQUEST_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_TYPE", OracleDbType.Varchar2).Value = req_data.service_type.ToString();
                cmd.Parameters.Add("P_RECORD_NO", OracleDbType.Varchar2).Value = req_data.record_no.ToString();
                cmd.Parameters.Add("P_TRANSACTION_REF", OracleDbType.Varchar2).Value = req_data.ref_no.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPITGetRequestData tmpPL = new ExeResAPITGetRequestData();
                        tmpPL.data1 = dr1[0].ToString();
                        detail_data1.Add(tmpPL);
                    }

                    RE.data = detail_data1;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
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

            }
            return await Task.FromResult<ResAPITGetRequestData>(RE);
        }
        public static async Task<ResAPITGetResponsetData> GetAPITResponseData(ReqAPITGetResponsetdata res_data)
        {
            ResAPITGetResponsetData RE = new ResAPITGetResponsetData();
            List<ExeResAPITGetResponseData> detail_data2 = new List<ExeResAPITGetResponseData>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_TRANSACTION_GET_RESPONSE_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_TYPE", OracleDbType.Varchar2).Value = res_data.service_type.ToString();
                cmd.Parameters.Add("P_RECORD_NO", OracleDbType.Varchar2).Value = res_data.record_no.ToString();
                cmd.Parameters.Add("P_TRANSACTION_REF", OracleDbType.Varchar2).Value = res_data.ref_no.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPITGetResponseData tmpPL = new ExeResAPITGetResponseData();
                        tmpPL.data2 = dr1[0].ToString();
                        detail_data2.Add(tmpPL);
                    }

                    RE.data = detail_data2;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
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

            }
            return await Task.FromResult<ResAPITGetResponsetData>(RE);
        }
        public static async Task<ResAPITGetHeaderData> GetAPITHeaderData(ReqAPITGetHeaderdata hea_data)
        {
            ResAPITGetHeaderData RE = new ResAPITGetHeaderData();
            List<ExeResAPITGetHeaderData> detail_data3 = new List<ExeResAPITGetHeaderData>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_TRANSACTION_GET_HEADER_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_TYPE", OracleDbType.Varchar2).Value = hea_data.service_type.ToString();
                cmd.Parameters.Add("P_RECORD_NO", OracleDbType.Varchar2).Value = hea_data.record_no.ToString();
                cmd.Parameters.Add("P_TRANSACTION_REF", OracleDbType.Varchar2).Value = hea_data.ref_no.ToString();
                cmd.Parameters.Add("DATA_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["DATA_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPITGetHeaderData tmpPL = new ExeResAPITGetHeaderData();
                        tmpPL.data3 = dr1[0].ToString();
                        detail_data3.Add(tmpPL);
                    }

                    RE.data = detail_data3;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
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

            }
            return await Task.FromResult<ResAPITGetHeaderData>(RE);
        }
        public static async Task<ResAPIConLoad> GetAPIConLoad()
        {
            ResAPIConLoad REF = new ResAPIConLoad();
            ResAPIConnectionType data = new ResAPIConnectionType();
            List<ExeResAPIConnectionTypeAll> all_service = new List<ExeResAPIConnectionTypeAll>();
            List<ExeResAPIConnectionType> service = new List<ExeResAPIConnectionType>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CONNECTION_ONLOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_CONNECTIONTYPEALL_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_CONNECTIONTYPE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CONNECTIONTYPEALL_CUR"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["ALL_CONNECTIONTYPE_CUR"].Value;

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
                        ExeResAPIConnectionTypeAll tmpPL = new ExeResAPIConnectionTypeAll();
                        tmpPL.endpoint_ids = dr1[0].ToString();
                        tmpPL.descriptions = dr1[1].ToString();
                        all_service.Add(tmpPL);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResAPIConnectionType tmpPL = new ExeResAPIConnectionType();
                        tmpPL.endpoint_id = dr2[0].ToString();
                        tmpPL.description = dr2[1].ToString();
                        service.Add(tmpPL);
                    }

                    REF.data = data;
                    REF.data.all_service = all_service;
                    REF.data.service = service;
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
            return await Task.FromResult<ResAPIConLoad>(REF);
        }
        
        public static async Task<ResAPICCheck> GetAPIConnectionCheck(ReqAPICConnection req_checkconnection)
        {
            ResAPICCheck REF = new ResAPICCheck();
            string[] result = new string[5];
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclobs;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CHECK_CONNECTION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_RECORD_NO", OracleDbType.Varchar2).Value = req_checkconnection.endpoint_id.ToString();
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("STATUS_CODE", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_RESP", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                
                result[0] = cmd.Parameters["STATUS"].Value.ToString();
                result[1] = cmd.Parameters["STATUS_CODE"].Value.ToString();
                msgclobs = (OracleClob)cmd.Parameters["OP_DATA_RESP"].Value;
                result[2] = msgclobs.Value.ToString();
                result[3] = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                result[4] = msgclob.Value.ToString();
                msgclob.Dispose();
                REF = new ResAPICCheck()
                {
                    status = result[3],
                    message = result[4],
                    data = new GetAPICCheck
                    {
                        status_con = result[0],
                        statuscode_con = result[1],
                        data_responce = result[2],
                    }
                };
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
            return await Task.FromResult<ResAPICCheck>(REF);
        }

        
        public static async Task<ResAPICDowntime> GetAPICDowntime()
        {
            ResAPICDowntime REF = new ResAPICDowntime();
            List<ExeResAPICDowntime> all_connection_dowmtime = new List<ExeResAPICDowntime>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CONNECTION_LIST_DOWNTIME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_CONNECTION_DOWNTIME_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CONNECTION_DOWNTIME_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPICDowntime tmpPL = new ExeResAPICDowntime();
                        tmpPL.service_name = dr1[0].ToString();
                        tmpPL.location = dr1[1].ToString();
                        tmpPL.down_date = dr1[2].ToString();
                        tmpPL.up_date = dr1[3].ToString();
                        tmpPL.downtime = dr1[4].ToString();
                        tmpPL.endpoint = dr1[5].ToString();
                        all_connection_dowmtime.Add(tmpPL);
                    }

                    REF.data = all_connection_dowmtime;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPICDowntime>(REF);
        }

        public static async Task<ResAPICGetAllService> GetAPICGetAllService()
        {
            ResAPICGetAllService REF = new ResAPICGetAllService();
            List<ExeResAPICGetAllService> all_connection_service = new List<ExeResAPICGetAllService>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CONNECTION_LIST_SERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_CONNECTION_ALL_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CONNECTION_ALL_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPICGetAllService tmpPL = new ExeResAPICGetAllService();
                        tmpPL.service_name = dr1[0].ToString();
                        tmpPL.endpoint_type = dr1[1].ToString();
                        tmpPL.location = dr1[2].ToString(); 
                        tmpPL.offline_date = dr1[3].ToString();
                        tmpPL.online_date = dr1[4].ToString();
                        tmpPL.status = dr1[5].ToString();
                        tmpPL.status_code = dr1[6].ToString();
                        all_connection_service.Add(tmpPL);
                    }

                    REF.data = all_connection_service;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPICGetAllService>(REF);
        }
        public static async Task<ResAPIPGetEndpoint> APIPGetEndpoint()
        {
            ResAPIPGetEndpoint REF = new ResAPIPGetEndpoint();
            List<ExeResAPIPGetEndpoint> all_endpoint_service = new List<ExeResAPIPGetEndpoint>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_ENDPOINT_SERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_ENDPOINT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_ENDPOINT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetEndpoint tmpPL = new ExeResAPIPGetEndpoint();
                        tmpPL.endpoint_id = dr1[0].ToString();
                        tmpPL.description = dr1[1].ToString();
                        tmpPL.endpoint = dr1[2].ToString();
                        tmpPL.status = dr1[3].ToString();
                        tmpPL.endpoint_type = dr1[4].ToString();
                        tmpPL.key_name = dr1[5].ToString();
                        tmpPL.key_value = dr1[6].ToString();
                        tmpPL.method = dr1[7].ToString();
                        tmpPL.content_type = dr1[8].ToString();
                        tmpPL.dc_or_dr = dr1[9].ToString();
                        tmpPL.current_service = dr1[10].ToString();
                        tmpPL.status_code = dr1[11].ToString();
                        all_endpoint_service.Add(tmpPL);
                    }

                    REF.data = all_endpoint_service;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPGetEndpoint>(REF);
        }
        public static async Task<ResAPIPRegisterEndpoint> APIPRegisterEndpoint(ReqAPIPRegisterEndpoint param)
        {
            ResAPIPRegisterEndpoint Res = new ResAPIPRegisterEndpoint();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_ENDPOINT_SERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_ENDPOINT_ID", OracleDbType.Varchar2).Value = param.endpoint_id.ToString();
                cmd.Parameters.Add("P_ENDPOINT_DESC", OracleDbType.Varchar2).Value = param.endpoint_description.ToString();
                cmd.Parameters.Add("P_ENDPOINT", OracleDbType.Varchar2).Value = param.endpoint.ToString();
                cmd.Parameters.Add("P_ENDPOINT_STATUS", OracleDbType.Varchar2).Value = param.endpoint_status.ToString();
                cmd.Parameters.Add("P_ENDPOINT_TYPE", OracleDbType.Varchar2).Value = param.endpoint_type.ToString();
                cmd.Parameters.Add("P_MESSAGE_BODY", OracleDbType.Varchar2).Value = param.message_body.ToString();
                cmd.Parameters.Add("P_KEY_NAME", OracleDbType.Varchar2).Value = param.key_name.ToString();
                cmd.Parameters.Add("P_KEY_VALUE", OracleDbType.Varchar2).Value = param.key_value.ToString();
                cmd.Parameters.Add("P_METHOD", OracleDbType.Varchar2).Value = param.method.ToString();
                cmd.Parameters.Add("P_CONTENT_TYPE", OracleDbType.Varchar2).Value = param.content_type.ToString();
                cmd.Parameters.Add("P_SERVICE_TYPE", OracleDbType.Varchar2, 2).Value = param.service_type.ToString();
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
            return await Task.FromResult<ResAPIPRegisterEndpoint>(Res);
        }
        public static async Task<ResAPIPEditEndpoint> APIPEditEndpoint(ReqAPIPEditEndpointt param)
        {
            ResAPIPEditEndpoint RES = new ResAPIPEditEndpoint();
            List<ExeResAPIPUpdateEndpoint> endpoint_service = new List<ExeResAPIPUpdateEndpoint>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_EDIT_ENDPOINT_SERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_ENDPOINT_TYPE", OracleDbType.Varchar2).Value = param.endpoint_type.ToString();
                cmd.Parameters.Add("P_ENDPOINT_ID", OracleDbType.Varchar2).Value = param.endpoint_ids.ToString();
                cmd.Parameters.Add("ENDPOINT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ENDPOINT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPUpdateEndpoint tmpPL = new ExeResAPIPUpdateEndpoint();
                        tmpPL.endpoint_id = dr1[0].ToString();
                        tmpPL.endpoint_description = dr1[1].ToString();
                        tmpPL.endpoint = dr1[2].ToString();
                        tmpPL.endpoint_status = dr1[3].ToString();
                        tmpPL.endpoint_type = dr1[4].ToString();
                        tmpPL.message_body = dr1[5].ToString();
                        tmpPL.key_name = dr1[6].ToString();
                        tmpPL.key_value = dr1[7].ToString();
                        tmpPL.method = dr1[8].ToString();
                        tmpPL.content_type = dr1[9].ToString();
                        tmpPL.service_type = dr1[10].ToString();
                        endpoint_service.Add(tmpPL);
                    }

                    RES.data = endpoint_service;
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
            return await Task.FromResult<ResAPIPEditEndpoint>(RES);
        }
        public static async Task<ResAPIPUpdateEndpoint> APIPUpdateEndpoint(ReqAPIPUpdateEndpoint param)
        {
            ResAPIPUpdateEndpoint Res = new ResAPIPUpdateEndpoint();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_UPDATE_ENDPOINT_SERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_ENDPOINT_ID", OracleDbType.Varchar2).Value = param.endpoint_id.ToString();
                cmd.Parameters.Add("P_ENDPOINT_DESC", OracleDbType.Varchar2).Value = param.endpoint_description.ToString();
                cmd.Parameters.Add("P_ENDPOINT", OracleDbType.Varchar2).Value = param.endpoint.ToString();
                cmd.Parameters.Add("P_ENDPOINT_STATUS", OracleDbType.Varchar2).Value = param.endpoint_status.ToString();
                cmd.Parameters.Add("P_ENDPOINT_TYPE", OracleDbType.Varchar2).Value = param.endpoint_type.ToString();
                cmd.Parameters.Add("P_MESSAGE_BODY", OracleDbType.Varchar2).Value = param.message_body.ToString();
                cmd.Parameters.Add("P_KEY_NAME", OracleDbType.Varchar2).Value = param.key_name.ToString();
                cmd.Parameters.Add("P_KEY_VALUE", OracleDbType.Varchar2).Value = param.key_value.ToString();
                cmd.Parameters.Add("P_METHOD", OracleDbType.Varchar2).Value = param.method.ToString();
                cmd.Parameters.Add("P_CONTENT_TYPE", OracleDbType.Varchar2).Value = param.content_type.ToString();
                cmd.Parameters.Add("P_SERVICE_TYPE", OracleDbType.Varchar2, 2).Value = param.service_type.ToString();
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
            return await Task.FromResult<ResAPIPUpdateEndpoint>(Res);
        }
        public static async Task<ResAPIPDeleteEndpoint> APIPDeleteEndpoint(ReqAPIPDeleteEndpoint param)
        {
            ResAPIPDeleteEndpoint RES = new ResAPIPDeleteEndpoint();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_DELETE_ENDPOINT_SERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_ENDPOINT_TYPE", OracleDbType.Varchar2).Value = param.endpoint_type.ToString();
                cmd.Parameters.Add("P_ENDPOINT_ID", OracleDbType.Varchar2).Value = param.endpoint_id.ToString();
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
            return await Task.FromResult<ResAPIPDeleteEndpoint>(RES);
        }
        public static async Task<ResAPICDTQuery> GetAPICDowntimeQuery(ReqAPICDownTime DT)
        {
            ResAPICDTQuery RE = new ResAPICDTQuery();
            List<ExeResAPICDTQuery> all_downtime_service = new List<ExeResAPICDTQuery>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CONNECTION_DOWNTIME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CONNECTION_TYPE", OracleDbType.Varchar2).Value = DT.connection_type.ToString();
                cmd.Parameters.Add("P_FROMDATE", OracleDbType.Varchar2).Value = DT.fromdate.ToString();
                cmd.Parameters.Add("P_TODATE", OracleDbType.Varchar2).Value = DT.todate.ToString();
                cmd.Parameters.Add("ALL_CONNECTION_DOWNTIME_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CONNECTION_DOWNTIME_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPICDTQuery tmpPL = new ExeResAPICDTQuery();
                        tmpPL.service_name = dr1[0].ToString();
                        tmpPL.location = dr1[1].ToString();
                        tmpPL.down_time = dr1[2].ToString();
                        tmpPL.up_time = dr1[3].ToString();
                        tmpPL.total_downTime = dr1[4].ToString();
                        all_downtime_service.Add(tmpPL);
                    }

                    RE.data = all_downtime_service;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
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

            }
            return await Task.FromResult<ResAPICDTQuery>(RE);
        }
        public static async Task<ResAPIPGetClient> APIPGetClient()
        {
            ResAPIPGetClient REF = new ResAPIPGetClient();
            List<ExeResAPIPGetClient> all_client = new List<ExeResAPIPGetClient>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_CLIENT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CLIENT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetClient tmpPL = new ExeResAPIPGetClient();
                        tmpPL.appid_client = dr1[0].ToString();
                        tmpPL.client_id = dr1[1].ToString();
                        tmpPL.client_secert = dr1[2].ToString();
                        tmpPL.client_name = dr1[3].ToString();
                        tmpPL.client_des = dr1[4].ToString();
                        tmpPL.grent_type = dr1[5].ToString();
                        tmpPL.client_status = dr1[6].ToString();
                        tmpPL.create_date = dr1[7].ToString();
                        tmpPL.update_date = dr1[8].ToString();
                        all_client.Add(tmpPL);
                    }

                    REF.data = all_client;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPGetClient>(REF);
        }
        public static async Task<ResAPIPRegisterClient> APIPRegisterClient(ReqAPIPRegisterClient param)
        {
            ResAPIPRegisterClient Res = new ResAPIPRegisterClient();
            OracleConnection conn = new OracleConnection();
            var client_id = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var client_secret = CoreFunctions.Encryption(Guid.NewGuid().ToString().Replace("-", string.Empty));
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_APPID_CLIENT", OracleDbType.Varchar2).Value = param.appid_client.ToString();
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = client_id;
                cmd.Parameters.Add("P_CLIENT_SECRET", OracleDbType.Varchar2).Value = client_secret;
                cmd.Parameters.Add("P_CLIENT_NAME", OracleDbType.Varchar2).Value = param.client_name.ToString();
                cmd.Parameters.Add("P_CLIENT_DES", OracleDbType.Varchar2).Value = param.client_des.ToString();
                cmd.Parameters.Add("P_GRENT_TYPE", OracleDbType.Varchar2).Value = param.grent_type.ToString();
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
            return await Task.FromResult<ResAPIPRegisterClient>(Res);
        }
        public static async Task<ResAPIPDeleteClient> APIPDeleteClient(ReqAPIPDeleteClient param)
        {
            ResAPIPDeleteClient RES = new ResAPIPDeleteClient();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_DELETE_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_TYPE", OracleDbType.Varchar2).Value = param.client_type.ToString();
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = param.client_id.ToString();
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
            return await Task.FromResult<ResAPIPDeleteClient>(RES);
        }
        public static async Task<ResAPIPEditClient> APIPEditClient(ReqAPIPEditClient param)
        {
            ResAPIPEditClient RES = new ResAPIPEditClient();
            List<ExeResAPIPUpdateclient> client = new List<ExeResAPIPUpdateclient>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_EDIT_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_TYPE", OracleDbType.Varchar2).Value = param.client_type.ToString();
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = param.client_id.ToString();
                cmd.Parameters.Add("CLIENT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["CLIENT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPUpdateclient tmpPL = new ExeResAPIPUpdateclient();
                        tmpPL.appid_client = dr1[0].ToString();
                        tmpPL.client_id = dr1[1].ToString();
                        tmpPL.client_secert = dr1[2].ToString();
                        tmpPL.client_name = dr1[3].ToString();
                        tmpPL.client_des = dr1[4].ToString();
                        tmpPL.grent_type = dr1[5].ToString();
                        tmpPL.client_status = dr1[6].ToString();
                        client.Add(tmpPL);
                    }

                    RES.data = client;
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
            return await Task.FromResult<ResAPIPEditClient>(RES);
        }
        public static async Task<ResAPIPUpdateClient> APIPUpdateClient(ReqAPIPUpdateClient param, string param_client_id, string param_client_secert)
        {
            ResAPIPUpdateClient Res = new ResAPIPUpdateClient();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            if (param.p_client_id == param.client_id)
            {
                if (param.p_client_secert == param.client_secert)
                {
                    param_client_id = param.client_id;
                    param_client_secert = param.client_secert;
                }
                else
                {
                    param_client_id = param.client_id;
                    param_client_secert = CoreFunctions.Encryption(param.client_secert);
                }
            }
            else
            {
                if (param.p_client_secert == param.client_secert)
                {
                    param_client_id = param.client_id;
                    param_client_secert = param.client_secert;
                }
                else
                {
                    param_client_id = param.client_id;
                    param_client_secert = CoreFunctions.Encryption(param.client_secert);
                }
            }
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_UPDATE_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_IDPARAM", OracleDbType.Varchar2).Value = param.p_client_id.ToString();
                cmd.Parameters.Add("P_APPID_CLIENT", OracleDbType.Varchar2).Value = param.appid_client.ToString();
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = param_client_id;
                cmd.Parameters.Add("P_CLIENT_SECRET", OracleDbType.Varchar2).Value = param_client_secert;
                cmd.Parameters.Add("P_CLIENT_NAME", OracleDbType.Varchar2).Value = param.client_name.ToString();
                cmd.Parameters.Add("P_CLIENT_DES", OracleDbType.Varchar2).Value = param.client_des.ToString();
                cmd.Parameters.Add("P_GRENT_TYPE", OracleDbType.Varchar2).Value = param.grent_type.ToString();
                cmd.Parameters.Add("P_CLIENT_STATUS", OracleDbType.Varchar2).Value = param.client_status.ToString();
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
            return await Task.FromResult<ResAPIPUpdateClient>(Res);
        }
        public static async Task<ResAPIPGetSinature> APIPGetSinature()
        {
            ResAPIPGetSinature REF = new ResAPIPGetSinature();
            List<ExeResAPIPGetSinature> all_sinature = new List<ExeResAPIPGetSinature>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_SINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_SINATURE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_SINATURE_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetSinature tmpPL = new ExeResAPIPGetSinature();
                        tmpPL.sinature_id = dr1[0].ToString();
                        tmpPL.created_by = dr1[1].ToString();
                        tmpPL.created_date = dr1[2].ToString();
                        tmpPL.modifired_by = dr1[3].ToString();
                        tmpPL.modifired_date = dr1[4].ToString();
                        tmpPL.sinature_status = dr1[5].ToString();
                        tmpPL.sinature_keyid = dr1[6].ToString();
                        tmpPL.sinature_algorithm = dr1[7].ToString();
                        tmpPL.sinature_headers = dr1[8].ToString();
                        tmpPL.sinature_secretkey = dr1[9].ToString();
                        tmpPL.sinature_maxagerequest = dr1[10].ToString();
                        all_sinature.Add(tmpPL);
                    }

                    REF.data = all_sinature;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPGetSinature>(REF);
        }
        public static async Task<ResAPIPRegisterSinature> APIPRegisterSinature(ReqAPIPRegisterSinature param)
        {
            ResAPIPRegisterSinature Res = new ResAPIPRegisterSinature();
            OracleConnection conn = new OracleConnection();
            var sinature_secretkey = Guid.NewGuid().ToString().Replace("-", string.Empty);
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_SINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SINATURE_KEYID", OracleDbType.Varchar2).Value = param.sinature_keyid.ToString();
                cmd.Parameters.Add("P_SINATURE_ALGORITHM", OracleDbType.Varchar2).Value = param.sinature_algorithm.ToString();
                cmd.Parameters.Add("P_SINATURE_HEADERS", OracleDbType.Varchar2).Value = param.sinature_headers.ToString();
                cmd.Parameters.Add("P_SINATURE_SECRETKEY", OracleDbType.Varchar2).Value = sinature_secretkey;
                cmd.Parameters.Add("P_SINATURE_MAX", OracleDbType.Varchar2).Value = param.sinature_max.ToString();
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
            return await Task.FromResult<ResAPIPRegisterSinature>(Res);
        }
        public static async Task<ResAPIPDeleteSinature> APIPDeleteSinature(ReqAPIPDeleteSinature param)
        {
            ResAPIPDeleteSinature RES = new ResAPIPDeleteSinature();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_DELETE_SINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SINATURE_TYPE", OracleDbType.Varchar2).Value = param.sinature_type.ToString();
                cmd.Parameters.Add("P_SINATURE_ID", OracleDbType.Varchar2).Value = param.sinature_id.ToString();
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
            return await Task.FromResult<ResAPIPDeleteSinature>(RES);
        }
        public static async Task<ResAPIPEditSinature> APIPEditSinature(ReqAPIPEditSinature param)
        {
            ResAPIPEditSinature RES = new ResAPIPEditSinature();
            List<ExeResAPIPEditSinature> sinature = new List<ExeResAPIPEditSinature>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_EDIT_SINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SINATURE_TYPE", OracleDbType.Varchar2).Value = param.sinature_type.ToString();
                cmd.Parameters.Add("P_SINATURE_ID", OracleDbType.Varchar2).Value = param.sinature_id.ToString();
                cmd.Parameters.Add("SINATURE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["SINATURE_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPEditSinature tmpPL = new ExeResAPIPEditSinature();
                        tmpPL.sinature_id = dr1[0].ToString();
                        tmpPL.sinature_status = dr1[1].ToString();
                        tmpPL.sinature_keyid = dr1[2].ToString();
                        tmpPL.sinature_algorithm = dr1[3].ToString();
                        tmpPL.sinature_headers = dr1[4].ToString();
                        tmpPL.sinature_secretkey = dr1[5].ToString();
                        tmpPL.sinature_max = dr1[6].ToString();
                        sinature.Add(tmpPL);
                    }

                    RES.data = sinature;
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
            return await Task.FromResult<ResAPIPEditSinature>(RES);
        }
        public static async Task<ResAPIPUpdateSinature> APIPUpdateSinature(ReqAPIPUpdateSinature param)
        {
            ResAPIPUpdateSinature Res = new ResAPIPUpdateSinature();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_UPDATE_SINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SINATURE_ID", OracleDbType.Varchar2).Value = param.sinature_id.ToString();
                cmd.Parameters.Add("P_SINATURE_STATUS", OracleDbType.Varchar2).Value = param.sinature_status.ToString();
                cmd.Parameters.Add("P_SINATURE_KEYID", OracleDbType.Varchar2).Value = param.sinature_keyid.ToString();
                cmd.Parameters.Add("P_SINATURE_ALGORITHM", OracleDbType.Varchar2).Value = param.sinature_algorithm.ToString();
                cmd.Parameters.Add("P_SINATURE_HEADERS", OracleDbType.Varchar2).Value = param.sinature_headers.ToString();
                cmd.Parameters.Add("P_SINATURE_SECRETKEY", OracleDbType.Varchar2).Value = param.sinature_secretkey.ToString();
                cmd.Parameters.Add("P_SINATURE_MAX", OracleDbType.Varchar2).Value = param.sinature_max.ToString();
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
            return await Task.FromResult<ResAPIPUpdateSinature>(Res);
        }
        public static async Task<ResAPIPGetEndpointuser> APIPGetEndpointuser()
        {
            ResAPIPGetEndpointuser REF = new ResAPIPGetEndpointuser();
            List<ExeResAPIPGetEndpointuser> all_endpoint_user = new List<ExeResAPIPGetEndpointuser>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_ENDPOINTUSER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_ENDPOINTUSER_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_ENDPOINTUSER_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetEndpointuser tmpPL = new ExeResAPIPGetEndpointuser();
                        tmpPL.api_id = dr1[0].ToString();
                        tmpPL.endpoint_id = dr1[1].ToString();
                        tmpPL.endpoint = dr1[2].ToString();
                        tmpPL.method = dr1[3].ToString();
                        tmpPL.record_status = dr1[4].ToString();
                        tmpPL.created_by = dr1[5].ToString();
                        tmpPL.created_date = dr1[6].ToString();
                        tmpPL.modifired_by = dr1[7].ToString();
                        tmpPL.modifired_date = dr1[8].ToString();
                        tmpPL.enabled = dr1[9].ToString();
                        tmpPL.debug = dr1[10].ToString();
                        tmpPL.debug_name = dr1[11].ToString();
                        tmpPL.authorize = dr1[12].ToString();
                        tmpPL.validatetrn_id = dr1[13].ToString();
                        tmpPL.validate_createtime = dr1[14].ToString();
                        tmpPL.validate_agerequest = dr1[15].ToString();
                        tmpPL.validate_digest = dr1[16].ToString();
                        tmpPL.validate_sinature = dr1[17].ToString();
                        tmpPL.sourcesystem_check = dr1[18].ToString();
                        tmpPL.allowanonymous = dr1[19].ToString();
                        tmpPL.multipart_data = dr1[20].ToString();
                        tmpPL.auth_type = dr1[21].ToString();
                        tmpPL.api_key = dr1[22].ToString();
                        all_endpoint_user.Add(tmpPL);
                    }

                    REF.data = all_endpoint_user;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPGetEndpointuser>(REF);
        }
        public static async Task<ResAPIPRegisterEndpointuser> APIPRegisterEndpointuser(ReqAPIPRegisterEndpointuser param)
        {
            ResAPIPRegisterEndpointuser Res = new ResAPIPRegisterEndpointuser();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            var api_key = "";
            if (param.api_key != null && param.api_key != "")
            {
                api_key = CoreFunctions.Encryption(param.api_key);
            }
            else
            {
                api_key = param.api_key;
            }
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_ENDPOINTUSER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_API_ID", OracleDbType.Varchar2).Value = param.api_id.ToString();
                cmd.Parameters.Add("P_ENDPOINT", OracleDbType.Varchar2).Value = param.endpoint.ToString();
                cmd.Parameters.Add("P_METHOD", OracleDbType.Varchar2).Value = param.method.ToString();
                cmd.Parameters.Add("P_ENABLE", OracleDbType.Varchar2).Value = param.enabled.ToString();
                cmd.Parameters.Add("P_DEBUGS", OracleDbType.Varchar2).Value = param.debug.ToString();
                cmd.Parameters.Add("P_DEBUG_NAME", OracleDbType.Varchar2).Value = param.debug_name.ToString();
                cmd.Parameters.Add("P_AUTHOIZE", OracleDbType.Varchar2).Value = param.authorize.ToString();
                cmd.Parameters.Add("P_VALIDATETRNID", OracleDbType.Varchar2).Value = param.validatetrn_id.ToString();
                cmd.Parameters.Add("P_VALIDATECREATETIME", OracleDbType.Varchar2).Value = param.validate_createtime.ToString();
                cmd.Parameters.Add("P_VALIDATEAGEREQUEST", OracleDbType.Varchar2).Value = param.validate_agerequest.ToString();
                cmd.Parameters.Add("P_VALIDATE_DIGEST", OracleDbType.Varchar2).Value = param.validate_digest.ToString();
                cmd.Parameters.Add("P_VALIDATE_SINATURE", OracleDbType.Varchar2).Value = param.validate_sinature.ToString();
                cmd.Parameters.Add("P_SOURCE_SYSTEM", OracleDbType.Varchar2).Value = param.sourcesystem_check.ToString();
                cmd.Parameters.Add("P_ALLOWANYMOUS", OracleDbType.Varchar2).Value = param.allowanonymous.ToString();
                cmd.Parameters.Add("P_MULTIPART_DATA", OracleDbType.Varchar2).Value = param.multipart_data.ToString();
                cmd.Parameters.Add("P_AUTH_TYPE", OracleDbType.Varchar2).Value = param.auth_type.ToString();
                cmd.Parameters.Add("P_API_KEY", OracleDbType.Varchar2).Value = api_key.ToString();
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
            return await Task.FromResult<ResAPIPRegisterEndpointuser>(Res);
        }

        public static async Task<ResAPIPDeleteEndpointuser> APIPDeleteEndpointuser(ReqAPIPDeleteEndpointuser param)
        {
            ResAPIPDeleteEndpointuser RES = new ResAPIPDeleteEndpointuser();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_DELETE_ENDPOINTUSER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_ENDPOINT_TYPE", OracleDbType.Varchar2).Value = param.endpoint_type.ToString();
                cmd.Parameters.Add("P_ENDPOINT_ID", OracleDbType.Varchar2).Value = param.endpoint_id.ToString();
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
            return await Task.FromResult<ResAPIPDeleteEndpointuser>(RES);
        }
        public static async Task<ResAPIPEditEndpointuser> APIPEditEndpointuser(ReqAPIPEditEndpointuser param)
        {
            ResAPIPEditEndpointuser RES = new ResAPIPEditEndpointuser();
            List<ExeResAPIPEditEndpointuser> endpoint = new List<ExeResAPIPEditEndpointuser>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_EDIT_ENDPOINTUSER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_ENDPOINT_TYPE", OracleDbType.Varchar2).Value = param.endpoint_type.ToString();
                cmd.Parameters.Add("P_ENDPOINT_ID", OracleDbType.Varchar2).Value = param.endpoint_id.ToString();
                cmd.Parameters.Add("ENDPOINT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ENDPOINT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPEditEndpointuser tmpPL = new ExeResAPIPEditEndpointuser();
                        tmpPL.api_id = dr1[0].ToString();
                        tmpPL.endpoint_id = dr1[1].ToString();
                        tmpPL.endpoint = dr1[2].ToString();
                        tmpPL.method = dr1[3].ToString();
                        tmpPL.record_status = dr1[4].ToString();
                        tmpPL.enabled = dr1[5].ToString();
                        tmpPL.debug = dr1[6].ToString();
                        tmpPL.debug_name = dr1[7].ToString();
                        tmpPL.authorize = dr1[8].ToString();
                        tmpPL.validatetrn_id = dr1[9].ToString();
                        tmpPL.validate_createtime = dr1[10].ToString();
                        tmpPL.validate_agerequest = dr1[11].ToString();
                        tmpPL.validate_digest = dr1[12].ToString();
                        tmpPL.validate_sinature = dr1[13].ToString();
                        tmpPL.sourcesystem_check = dr1[14].ToString();
                        tmpPL.allowanonymous = dr1[15].ToString();
                        tmpPL.multipart_data = dr1[16].ToString();
                        tmpPL.auth_type = dr1[17].ToString();
                        if(dr1[18].ToString() != null)
                        {
                            tmpPL.api_key = CoreFunctions.Decryption(dr1[18].ToString());
                        }
                        else
                        {
                            tmpPL.api_key = dr1[18].ToString();
                        }
                        
                        endpoint.Add(tmpPL);
                    }

                    RES.data = endpoint;
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
            return await Task.FromResult<ResAPIPEditEndpointuser>(RES);
        }
        public static async Task<ResAPIPUpdateEndpointuser> APIPUpdateEndpointuser(ReqAPIPUpdateEndpointuser param)
        {
            ResAPIPUpdateEndpointuser Res = new ResAPIPUpdateEndpointuser();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            var api_key = "";
            if (param.api_key != null && param.api_key != "")
            {
                api_key = CoreFunctions.Encryption(param.api_key);
            }
            else
            {
                api_key = param.api_key;
            }
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_UPDATE_ENDPOINTUSER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_API_ID", OracleDbType.Varchar2).Value = param.api_id.ToString();
                cmd.Parameters.Add("P_ENDPOINT_ID", OracleDbType.Varchar2).Value = param.endpoint_id.ToString();
                cmd.Parameters.Add("P_ENDPOINT", OracleDbType.Varchar2).Value = param.endpoint.ToString();
                cmd.Parameters.Add("P_METHOD", OracleDbType.Varchar2).Value = param.method.ToString();
                cmd.Parameters.Add("P_RECORD_STATUS", OracleDbType.Varchar2).Value = param.record_status.ToString();
                cmd.Parameters.Add("P_ENABLE", OracleDbType.Varchar2).Value = param.enabled.ToString();
                cmd.Parameters.Add("P_DEBUGS", OracleDbType.Varchar2).Value = param.debug.ToString();
                cmd.Parameters.Add("P_DEBUG_NAME", OracleDbType.Varchar2).Value = param.debug_name.ToString();
                cmd.Parameters.Add("P_AUTHOIZE", OracleDbType.Varchar2).Value = param.authorize.ToString();
                cmd.Parameters.Add("P_VALIDATETRNID", OracleDbType.Varchar2).Value = param.validatetrn_id.ToString();
                cmd.Parameters.Add("P_VALIDATECREATETIME", OracleDbType.Varchar2).Value = param.validate_createtime.ToString();
                cmd.Parameters.Add("P_VALIDATEAGEREQUEST", OracleDbType.Varchar2).Value = param.validate_agerequest.ToString();
                cmd.Parameters.Add("P_VALIDATE_DIGEST", OracleDbType.Varchar2).Value = param.validate_digest.ToString();
                cmd.Parameters.Add("P_VALIDATE_SINATURE", OracleDbType.Varchar2).Value = param.validate_sinature.ToString();
                cmd.Parameters.Add("P_SOURCE_SYSTEM", OracleDbType.Varchar2).Value = param.sourcesystem_check.ToString();
                cmd.Parameters.Add("P_ALLOWANYMOUS", OracleDbType.Varchar2).Value = param.allowanonymous.ToString();
                cmd.Parameters.Add("P_MULTIPART_DATA", OracleDbType.Varchar2).Value = param.multipart_data.ToString();
                cmd.Parameters.Add("P_AUTH_TYPE", OracleDbType.Varchar2).Value = param.auth_type.ToString();
                cmd.Parameters.Add("P_API_KEY", OracleDbType.Varchar2).Value = api_key.ToString();
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
            return await Task.FromResult<ResAPIPUpdateEndpointuser>(Res);
        }
        public static async Task<ResAPIPGetClientEndpoint> APIPGetClientEndpoint()
        {
            ResAPIPGetClientEndpoint REF = new ResAPIPGetClientEndpoint();
            List<ExeResAPIPGetClientEndpoint> all_client_endpoint = new List<ExeResAPIPGetClientEndpoint>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_CLIENTENDPOINT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_CLIENTENDPOINT_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CLIENTENDPOINT_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetClientEndpoint tmpPL = new ExeResAPIPGetClientEndpoint();
                        tmpPL.app_id = dr1[0].ToString();
                        tmpPL.client_id = dr1[1].ToString();
                        tmpPL.endpoint_id = dr1[2].ToString();
                        tmpPL.record_status = dr1[3].ToString();
                        tmpPL.created_by = dr1[4].ToString();
                        tmpPL.created_date = dr1[5].ToString();
                        tmpPL.modifired_by = dr1[6].ToString();
                        tmpPL.modifired_date = dr1[7].ToString();
                        tmpPL.action_type = dr1[8].ToString();
                        all_client_endpoint.Add(tmpPL);
                    }

                    REF.data = all_client_endpoint;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPGetClientEndpoint>(REF);
        }
        public static async Task<ResAPIPGetClientSinature> APIPGetClientSinature(ReqAPIPGetClientSinature param)
        {
            ResAPIPGetClientSinature REF = new ResAPIPGetClientSinature();
            List<ExeResAPIPGetClientSinature> all_sinature_endpoint = new List<ExeResAPIPGetClientSinature>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_CLIENTSINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_APPID_CLIENTSIN", OracleDbType.Varchar2).Value = param.client_sinature_appid.ToString();
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = param.client_sinature_id.ToString();
                cmd.Parameters.Add("ALL_CLIENTSINATURE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CLIENTSINATURE_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetClientSinature tmpPL = new ExeResAPIPGetClientSinature();
                        tmpPL.app_id = dr1[0].ToString();
                        tmpPL.client_id = dr1[1].ToString();
                        tmpPL.sig_id = dr1[2].ToString();
                        tmpPL.record_status = dr1[3].ToString();
                        tmpPL.created_by = dr1[4].ToString();
                        tmpPL.created_date = dr1[5].ToString();
                        tmpPL.modifired_by = dr1[6].ToString();
                        tmpPL.modifired_date = dr1[7].ToString();
                        tmpPL.action_type = dr1[8].ToString();
                        all_sinature_endpoint.Add(tmpPL);
                    }

                    REF.data = all_sinature_endpoint;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPGetClientSinature>(REF);
        }
        public static async Task<ResAPIPGetMessage> APIPGetMessage()
        {
            ResAPIPGetMessage REF = new ResAPIPGetMessage();
            List<ExeResAPIPGetMessage> all_message_service = new List<ExeResAPIPGetMessage>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_MESSAGE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_MESSAGE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_MESSAGE_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetMessage tmpPL = new ExeResAPIPGetMessage();
                        tmpPL.message_id = dr1[0].ToString();
                        tmpPL.appid_mes = dr1[1].ToString();
                        tmpPL.message_code = dr1[2].ToString();
                        tmpPL.message_type = dr1[3].ToString();
                        tmpPL.message_language = dr1[4].ToString();
                        tmpPL.message_description = dr1[5].ToString();
                        tmpPL.record_status = dr1[6].ToString();
                        tmpPL.created_by = dr1[7].ToString();
                        tmpPL.created_date = dr1[8].ToString();
                        tmpPL.modifired_by = dr1[9].ToString();
                        tmpPL.modifired_date = dr1[10].ToString();
                        all_message_service.Add(tmpPL);
                    }

                    REF.data = all_message_service;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPGetMessage>(REF);
        }
        public static async Task<ResAPIPGetMessageCode> APIPGetMessageCode(ReqAPIPGetMessageCode param)
        {
            ResAPIPGetMessageCode RES = new ResAPIPGetMessageCode();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_MESSAGECODE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_MESSAGE_APPID", OracleDbType.Varchar2).Value = param.appid_mes.ToString();
                cmd.Parameters.Add("P_MESSAGE_TYPE", OracleDbType.Varchar2).Value = param.message_type.ToString();
                cmd.Parameters.Add("OP_MESSAGE_CODE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                RES.message_code = cmd.Parameters["OP_MESSAGE_CODE"].Value.ToString();
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
            return await Task.FromResult<ResAPIPGetMessageCode>(RES);
        }
        public static async Task<ResAPIPRegisterMessage> APIPRegisterMessage(ReqAPIPRegisterMessage param)
        {
            ResAPIPRegisterMessage Res = new ResAPIPRegisterMessage();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_MESSAGE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_APPID_MES", OracleDbType.Varchar2).Value = param.appid_mes.ToString();
                cmd.Parameters.Add("P_MESSAGE_TYPE", OracleDbType.Varchar2).Value = param.message_type.ToString();
                cmd.Parameters.Add("P_MESSAGE_ENG", OracleDbType.Varchar2).Value = param.message_eng.ToString();
                cmd.Parameters.Add("P_MESSAGE_DES_ENG", OracleDbType.Varchar2).Value = param.message_description_eng.ToString();
                cmd.Parameters.Add("P_MESSAGE_KHR", OracleDbType.Varchar2).Value = param.message_khr.ToString();
                cmd.Parameters.Add("P_MESSAGE_DES_KHR", OracleDbType.Varchar2).Value = param.message_description_khr.ToString();
                cmd.Parameters.Add("P_MESSAGE_THB", OracleDbType.Varchar2).Value = param.message_thb.ToString();
                cmd.Parameters.Add("P_MESSAGE_DES_THB", OracleDbType.Varchar2).Value = param.message_description_thb.ToString();
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
            return await Task.FromResult<ResAPIPRegisterMessage>(Res);
        }
        public static async Task<ResAPIPEditMessage> APIPEditMessage(ReqAPIPEditMessage param)
        {
            ResAPIPEditMessage RES = new ResAPIPEditMessage();
            DataResMessage data = new DataResMessage();
            List<ExeResAPIPEditMessageENG> message_eng = new List<ExeResAPIPEditMessageENG>();
            List<ExeResAPIPEditMessageKHR> message_khr = new List<ExeResAPIPEditMessageKHR>();
            List<ExeResAPIPEditMessageTHB> message_thb = new List<ExeResAPIPEditMessageTHB>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_EDIT_MESSAGE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_MESSAGE_TYPE", OracleDbType.Varchar2).Value = param.message_type.ToString();
                cmd.Parameters.Add("P_MESSAGE_ID", OracleDbType.Varchar2).Value = param.message_ids.ToString();
                cmd.Parameters.Add("OP_MSG_ID", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_APP_ID", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MSG_CODE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MSG_TYPE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("MESSAGE_CUR_ENG", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("MESSAGE_CUR_KHR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("MESSAGE_CUR_THB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                RES.message_id = cmd.Parameters["OP_MSG_ID"].Value.ToString();
                RES.appid_mes = cmd.Parameters["OP_APP_ID"].Value.ToString();
                RES.message_code = cmd.Parameters["OP_MSG_CODE"].Value.ToString();
                RES.message_type = cmd.Parameters["OP_MSG_TYPE"].Value.ToString();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["MESSAGE_CUR_ENG"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["MESSAGE_CUR_KHR"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["MESSAGE_CUR_THB"].Value;

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
                        ExeResAPIPEditMessageENG tmpPL = new ExeResAPIPEditMessageENG();
                        tmpPL.message_language_eng = dr1[0].ToString();
                        tmpPL.message_description_eng = dr1[1].ToString();
                        tmpPL.record_status_eng = dr1[2].ToString();
                        message_eng.Add(tmpPL);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResAPIPEditMessageKHR tmpPL = new ExeResAPIPEditMessageKHR();
                        tmpPL.message_language_khr = dr2[0].ToString();
                        tmpPL.message_description_khr = dr2[1].ToString();
                        tmpPL.record_status_khr = dr2[2].ToString();
                        message_khr.Add(tmpPL);
                    }
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResAPIPEditMessageTHB tmpPL = new ExeResAPIPEditMessageTHB();
                        tmpPL.message_language_thb = dr3[0].ToString();
                        tmpPL.message_description_thb = dr3[1].ToString();
                        tmpPL.record_status_thb = dr3[2].ToString();
                        message_thb.Add(tmpPL);
                    }

                    RES.data = data;
                    RES.data.message_eng = message_eng;
                    RES.data.message_khr = message_khr;
                    RES.data.message_thb = message_thb;
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
            return await Task.FromResult<ResAPIPEditMessage>(RES);
        }
        public static async Task<ResAPIPUpdateMessage> APIPUpdateMessage(ReqAPIPUpdateMessage param)
        {
            ResAPIPUpdateMessage Res = new ResAPIPUpdateMessage();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_UPDATE_MESSAGE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_MESSAGE_ID", OracleDbType.Varchar2).Value = param.message_id.ToString();
                cmd.Parameters.Add("P_APPID_MES", OracleDbType.Varchar2).Value = param.appid_mes.ToString();
                cmd.Parameters.Add("P_MESSAGE_CODE", OracleDbType.Varchar2).Value = param.message_code.ToString();
                cmd.Parameters.Add("P_MESSAGE_TYPE", OracleDbType.Varchar2).Value = param.message_type.ToString();
                cmd.Parameters.Add("P_MESSAGE_LANGUAGE_ENG", OracleDbType.Varchar2).Value = param.message_eng.ToString();
                cmd.Parameters.Add("P_MESSAGE_DES_ENG", OracleDbType.Varchar2).Value = param.message_description_eng.ToString();
                cmd.Parameters.Add("P_RECORD_STATUS_ENG", OracleDbType.Varchar2).Value = param.message_status_eng.ToString();
                cmd.Parameters.Add("P_MESSAGE_LANGUAGE_KHR", OracleDbType.Varchar2).Value = param.message_khr.ToString();
                cmd.Parameters.Add("P_MESSAGE_DES_KHR", OracleDbType.Varchar2).Value = param.message_description_khr.ToString();
                cmd.Parameters.Add("P_RECORD_STATUS_KHR", OracleDbType.Varchar2).Value = param.message_status_khr.ToString();
                cmd.Parameters.Add("P_MESSAGE_LANGUAGE_THB", OracleDbType.Varchar2).Value = param.message_thb.ToString();
                cmd.Parameters.Add("P_MESSAGE_DES_THB", OracleDbType.Varchar2).Value = param.message_description_thb.ToString();
                cmd.Parameters.Add("P_RECORD_STATUS_THB", OracleDbType.Varchar2).Value = param.message_status_thb.ToString();
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
            return await Task.FromResult<ResAPIPUpdateMessage>(Res);
        }
        public static async Task<ResAPIPClientEndpointMap> APIPClientEndpointMap(ReqAPIPClientEndpointMap param)
        {
            ResAPIPClientEndpointMap RES = new ResAPIPClientEndpointMap();
            List<ExeResAPIPClientEndpointMap> Client_Endpoint_Map = new List<ExeResAPIPClientEndpointMap>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CLIENT_ENDPOINT_MAP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_ENDPOINTMAP_TYPE", OracleDbType.Varchar2).Value = param.client_endpoint_type.ToString();
                cmd.Parameters.Add("P_CLIENT_ENDPOINTMAP_ID", OracleDbType.Varchar2).Value = param.client_endpoint_id.ToString();
                cmd.Parameters.Add("OP_CLIENT_ENDPOINT_APPID", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_CLIENT_ENDPOINT_ID", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("CLIENT_ENDPOINTMAP_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                RES.client_endpoint_appid = cmd.Parameters["OP_CLIENT_ENDPOINT_APPID"].Value.ToString();
                RES.client_endpoint_id = cmd.Parameters["OP_CLIENT_ENDPOINT_ID"].Value.ToString();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["CLIENT_ENDPOINTMAP_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPClientEndpointMap tmpPL = new ExeResAPIPClientEndpointMap();
                        tmpPL.endpoint_id = dr1[0].ToString();
                        tmpPL.endpointuser_id = dr1[1].ToString();
                        tmpPL.endpointuser_desc = dr1[2].ToString();
                        Client_Endpoint_Map.Add(tmpPL);
                    }

                    RES.data = Client_Endpoint_Map;
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
            return await Task.FromResult<ResAPIPClientEndpointMap>(RES);
        }
        public static async Task<ResAPIPClientEndpointMapCheck> APIPClientEndpointMapCheck(ReqAPIPClientEndpointMapCheck param)
        {
            ResAPIPClientEndpointMapCheck RES = new ResAPIPClientEndpointMapCheck();
            List<ExeResAPIPClientEndpointMapCheck> Client_Endpoint_Map_Check = new List<ExeResAPIPClientEndpointMapCheck>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CLIENT_ENDPOINT_MAP_CHECK";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_ENDPOINTMAP_APPID", OracleDbType.Varchar2).Value = param.client_endpoint_appid.ToString();
                cmd.Parameters.Add("P_CLIENT_ENDPOINTMAP_ID", OracleDbType.Varchar2).Value = param.client_endpoint_id.ToString();
                cmd.Parameters.Add("CLIENT_ENDPOINTMAP_CHECK_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["CLIENT_ENDPOINTMAP_CHECK_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPClientEndpointMapCheck tmpPL = new ExeResAPIPClientEndpointMapCheck();
                        tmpPL.endpoint_id = dr1[0].ToString();
                        tmpPL.endpointuser_id = dr1[1].ToString();
                        tmpPL.endpointuser_desc = dr1[2].ToString();
                        Client_Endpoint_Map_Check.Add(tmpPL);
                    }

                    RES.data = Client_Endpoint_Map_Check;
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
            return await Task.FromResult<ResAPIPClientEndpointMapCheck>(RES);
        }
        public static async Task<ResAPIPRegisterClientEndpoint> APIPRegisterClientEndpoint(ReqAPIPRegisterClientEndpoint param)
        {
            ResAPIPRegisterClientEndpoint Res = new ResAPIPRegisterClientEndpoint();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                    cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_CLIENTENDPOINT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                    cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                    cmd.Parameters.Add("P_APPID_CLIENTEND", OracleDbType.Varchar2).Value = param.client_endpoint_appid.ToString();
                    cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = param.client_endpoint_id.ToString();
                    cmd.Parameters.Add("P_CLIENT_ENDPOINT", OracleDbType.Varchar2).Value = param.endpointuser_id.ToString();
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
            return await Task.FromResult<ResAPIPRegisterClientEndpoint>(Res);
        }
        public static async Task<ResAPIPClientSinatureMap> APIPClientSinatureMap(ReqAPIPClientSinatureMap param)
        {
            ResAPIPClientSinatureMap RES = new ResAPIPClientSinatureMap();
            List<ExeResAPIPClientSinatureMap> Client_Sinature_Map = new List<ExeResAPIPClientSinatureMap>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CLIENT_SINATURE_MAP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_SINATUREMAP_TYPE", OracleDbType.Varchar2).Value = param.client_sinature_type.ToString();
                cmd.Parameters.Add("P_CLIENT_SINATUREMAP_ID", OracleDbType.Varchar2).Value = param.client_sinature_id.ToString();
                cmd.Parameters.Add("OP_CLIENT_SINATURE_APPID", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_CLIENT_SINATURE_ID", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("CLIENT_SINATUREMAP_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                RES.client_sinature_appid = cmd.Parameters["OP_CLIENT_SINATURE_APPID"].Value.ToString();
                RES.client_sinature_id = cmd.Parameters["OP_CLIENT_SINATURE_ID"].Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["CLIENT_SINATUREMAP_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPClientSinatureMap tmpPL = new ExeResAPIPClientSinatureMap();
                        tmpPL.client_sinatureuser_id = dr1[0].ToString();
                        tmpPL.client_sinatureuser_des = dr1[1].ToString();
                        Client_Sinature_Map.Add(tmpPL);
                    }

                    RES.data = Client_Sinature_Map;
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
            return await Task.FromResult<ResAPIPClientSinatureMap>(RES);
        }
        public static async Task<ResAPIPClientSinatureMapCheck> APIPClientSinatureMapCheck(ReqAPIPClientSinatureMapCheck param)
        {
            ResAPIPClientSinatureMapCheck RES = new ResAPIPClientSinatureMapCheck();
            List<ExeResAPIPClientSinatureMapCheck> Client_Sinature_Map_Check = new List<ExeResAPIPClientSinatureMapCheck>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CLIENT_SINATURE_MAP_CHECK";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_SINATUREMAP_APPID", OracleDbType.Varchar2).Value = param.client_sinature_appid.ToString();
                cmd.Parameters.Add("P_CLIENT_SINATUREMAP_ID", OracleDbType.Varchar2).Value = param.client_sinature_id.ToString();
                cmd.Parameters.Add("CLIENT_SINATUREMAP_CHECK_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["CLIENT_SINATUREMAP_CHECK_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPClientSinatureMapCheck tmpPL = new ExeResAPIPClientSinatureMapCheck();
                        tmpPL.client_sinatureuser_id = dr1[0].ToString();
                        tmpPL.client_sinatureuser_appid = dr1[1].ToString();
                        tmpPL.client_sinatureuser_des = dr1[2].ToString();
                        Client_Sinature_Map_Check.Add(tmpPL);
                    }

                    RES.data = Client_Sinature_Map_Check;
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
            return await Task.FromResult<ResAPIPClientSinatureMapCheck>(RES);
        }
        public static async Task<ResAPIPRegisterClientSinature> APIPRegisterClientSinature(ReqAPIPRegisterClientSinature param)
        {
            ResAPIPRegisterClientSinature Res = new ResAPIPRegisterClientSinature();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_CLIENTSINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_APPID_CLIENTSIN", OracleDbType.Varchar2).Value = param.client_sinature_appid.ToString();
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = param.client_sinature_id.ToString();
                cmd.Parameters.Add("P_CLIENT_SINATURE", OracleDbType.Varchar2).Value = param.sinature_id.ToString();
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
            return await Task.FromResult<ResAPIPRegisterClientSinature>(Res);
        }
        public static async Task<ResAPIPEditClientSinature> APIPEditClientSinature(ReqAPIPEditClientSinature param)
        {
            ResAPIPEditClientSinature RES = new ResAPIPEditClientSinature();
            List<ExeResAPIPEditClientSinature> sinature = new List<ExeResAPIPEditClientSinature>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_EDIT_CLIENTSINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLIENT_SINATURE_TYPE", OracleDbType.Varchar2).Value = param.client_sinature_type.ToString();
                cmd.Parameters.Add("P_CLIENT_SINATURE_ID", OracleDbType.Varchar2).Value = param.client_sinature_id.ToString();
                cmd.Parameters.Add("CLIENT_SINATURE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["CLIENT_SINATURE_CUR"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPEditClientSinature tmpPL = new ExeResAPIPEditClientSinature();
                        tmpPL.app_id = dr1[0].ToString();
                        tmpPL.client_id = dr1[1].ToString();
                        tmpPL.sinature_id = dr1[2].ToString();
                        tmpPL.sinature_status = dr1[3].ToString();
                        sinature.Add(tmpPL);
                    }
                    RES.data = sinature;
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
            return await Task.FromResult<ResAPIPEditClientSinature>(RES);
        }
        public static async Task<ResAPIPUpdateClientSinature> APIPUpdateClientSinature(ReqAPIPUpdateClientSinature param)
        {
            ResAPIPUpdateClientSinature Res = new ResAPIPUpdateClientSinature();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_UPDATE_CLIENTSINATURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CLI_SIGNATURE_PRA", OracleDbType.Varchar2).Value = param.signature_pra.ToString();
                cmd.Parameters.Add("P_APP_ID", OracleDbType.Varchar2).Value = param.app_id.ToString();
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2).Value = param.client_id.ToString();
                cmd.Parameters.Add("P_SINATURE_ID", OracleDbType.Varchar2).Value = param.sinature_id.ToString();
                cmd.Parameters.Add("P_SINATURE_STATUS", OracleDbType.Varchar2).Value = param.sinature_status.ToString();
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
            return await Task.FromResult<ResAPIPUpdateClientSinature>(Res);
        }
        public static async Task<ResAPIUGetValue> APIUGetValue()
        {
            ResAPIUGetValue REF = new ResAPIUGetValue();
            List<ExeResAPIPGetAppID> app_ids = new List<ExeResAPIPGetAppID>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_VALUES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_CLIENT_VALUE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_ENDPOINT_VALUE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_SINATURE_VALUE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGES_VALUE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_APP_ID_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                REF.client_value = cmd.Parameters["OP_CLIENT_VALUE"].Value.ToString();
                REF.endpoint_value = cmd.Parameters["OP_ENDPOINT_VALUE"].Value.ToString();
                REF.sinature_value = cmd.Parameters["OP_SINATURE_VALUE"].Value.ToString();
                REF.messages_value = cmd.Parameters["OP_MESSAGES_VALUE"].Value.ToString();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_APP_ID_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPGetAppID tmpPL = new ExeResAPIPGetAppID();
                        tmpPL.app_id = dr1[0].ToString();
                        app_ids.Add(tmpPL);
                    }

                    REF.data = app_ids;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIUGetValue>(REF);
        }
        public static async Task<ResAPICChartDownService> GetAPICChartDownService(ReqAPICChartDownTime DTC)
        {
            ResAPICChartDownService RE = new ResAPICChartDownService();
            List<ExeAPICChartDownService> all_downtime_chart_service = new List<ExeAPICChartDownService>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                foreach (string service_name in DTC.connection_type) {
                List<ExeAPICCurrentData> data_detail = new List<ExeAPICCurrentData>();
                cmd.Parameters.Clear();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CONNECTION_CHART_DOWNTIME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CONNECTION_TYPE", OracleDbType.Varchar2).Value = service_name.ToString();
                cmd.Parameters.Add("P_FROMDATE", OracleDbType.Varchar2).Value = DTC.fromdate.ToString();
                cmd.Parameters.Add("P_TODATE", OracleDbType.Varchar2).Value = DTC.todate.ToString();
                cmd.Parameters.Add("OP_SERVICE_NAME", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_FORMAT", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_CONNECTION_DOWNTIME_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                RE.format = cmd.Parameters["OP_FORMAT"].Value.ToString();
                RE.from_date = DTC.fromdate.ToString();
                RE.to_date = DTC.todate.ToString();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CONNECTION_DOWNTIME_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeAPICCurrentData current_data = new ExeAPICCurrentData();
                        if (dr1[2].ToString() == "") {
                           current_data.current_date = dr1[0].ToString();
                           current_data.total_Time = "0";
                        }
                        else
                        {
                           current_data.current_date = dr1[0].ToString();
                           current_data.total_Time = dr1[1].ToString();
                        }
                        
                        data_detail.Add(current_data);
                    }
                        
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                        


                }
                    ExeAPICChartDownService tmpPL = new ExeAPICChartDownService();
                    tmpPL.service_id = service_name.ToString();
                    tmpPL.service_name = cmd.Parameters["OP_SERVICE_NAME"].Value.ToString();
                    tmpPL.data_detail = data_detail;
                    all_downtime_chart_service.Add(tmpPL);
                }
                
                RE.data = all_downtime_chart_service;
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

            }
            return await Task.FromResult<ResAPICChartDownService>(RE);
        }
        
        public static async Task<ResAPICChartDownTimeService> GetAPICChartDownTimeService(ReqAPICChartDownTimeDay DTCD)
        {
            ResAPICChartDownTimeService RE = new ResAPICChartDownTimeService();
            List<ExeAPICChartDownTimeService> service_down_up_time = new List<ExeAPICChartDownTimeService>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            OracleClob msgclob;
            try
            {
                
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_CONNECTION_DOWN_UPTIME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_SERVICE_TYPE", OracleDbType.Varchar2).Value = DTCD.service_type.ToString();
                cmd.Parameters.Add("P_DATE", OracleDbType.Varchar2).Value = DTCD.date.ToString();
                cmd.Parameters.Add("OP_SERVICE_NAME", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_FORMAT", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ALL_CONNECTION_DOWN_UPTIME_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RE.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RE.message = msgclob.Value.ToString();
                msgclob.Dispose();
                RE.service_type = cmd.Parameters["OP_SERVICE_NAME"].Value.ToString();
                RE.date = DTCD.date.ToString();
                RE.format = cmd.Parameters["OP_FORMAT"].Value.ToString();
                if (RE.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_CONNECTION_DOWN_UPTIME_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeAPICChartDownTimeService tmpPL = new ExeAPICChartDownTimeService();
                        tmpPL.rang_time = dr1[0].ToString();
                        tmpPL.total_time = dr1[1].ToString();
                        service_down_up_time.Add(tmpPL);
                    }

                    RE.data = service_down_up_time;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
                }
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

            }
            return await Task.FromResult<ResAPICChartDownTimeService>(RE);
        }
        public static async Task<ResAPIUEndpointonLoad> ResAPIUEndpointonLoad()
        {
            ResAPIUEndpointonLoad REF = new ResAPIUEndpointonLoad();
            List<ExeResAPIUEndpointonLoad> all_endpointuser_type = new List<ExeResAPIUEndpointonLoad>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_ENDPOINTUSER_ONLOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_ENDPOINTUSER_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_ENDPOINTUSER_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIUEndpointonLoad tmpPL = new ExeResAPIUEndpointonLoad();
                        tmpPL.endpointuser_id = dr1[0].ToString();
                        tmpPL.endpointuser_desc = dr1[1].ToString();
                        all_endpointuser_type.Add(tmpPL);
                    }

                    REF.data = all_endpointuser_type;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIUEndpointonLoad>(REF);
        }

        public static async Task<ResAPITool> ResAPITool(ReqAPITool Param)
        {
            ResAPITool RE = new ResAPITool();
            string InternalRef = Guid.NewGuid().ToString().Replace("-", "");
            MsgLog msgLog = new MsgLog();
            msgLog.internal_ref = InternalRef;
            msgLog.app_id = "ITO_TOOL";
            msgLog.action_type = "URL_REQ_SIGNATURE";
            msgLog.source = Core.GetContextValue("user_id");
            var body = JsonConvert.DeserializeObject<dynamic>(Param.body.ToString());

            if (Param.type_headersig== "Yes_Signature")
            {
                if (Param.name == null && Param.value == null)
                {
                    RE.transaction_id = "";
                    RE.digest = "";
                    RE.signature = "";
                    RE.data = "Error header not input";
                    msgLog.header_data = "Error header not input";
                    msgLog.request_data = JsonConvert.SerializeObject(Param) + JsonConvert.SerializeObject(body);
                    msgLog.response_data = "";
                    msgLog.InsertLarge();
                    return await Task.FromResult<ResAPITool>(RE);
                }
                else
                {
                    using (var httpClientHandler = new HttpClientHandler())
                    {
                        
                        var tranId = Guid.NewGuid().ToString();
                        var datetime = DateTime.UtcNow;
                        var digest = GenerateDigest(JsonConvert.SerializeObject(body).Replace(@"\\""", @""""));
                        var keyId = Param.keyid;
                        var algorithm = Param.algorithm;
                        var headers = Param.header;
                        var secretKey = Param.secretKey;
                        var endpoint = Param.endpoint;
                        var method = Param.method;
                        var created = (Int32)(datetime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        var signature = GenerateSignature(keyId, algorithm, datetime, headers, digest, secretKey, endpoint, tranId, method, created);
                        msgLog.header_data = tranId + digest + signature + created.ToString();
                        msgLog.request_data = JsonConvert.SerializeObject(Param)+ JsonConvert.SerializeObject(body);
                        var httpClient = new HttpClient(httpClientHandler);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Add("TransactionID", tranId);
                        httpClient.DefaultRequestHeaders.Add("Digest", digest);
                        httpClient.DefaultRequestHeaders.Add("Signature", signature);
                        httpClient.DefaultRequestHeaders.Add("Created", created.ToString());
                        for (int i = 0; i < Param.name.Length; i++)
                        {
                            //var header_name = Param.name[i];
                            //var header_value = Param.value[i];
                            httpClient.DefaultRequestHeaders.Add(Param.name[i], Param.value[i]);
                        };
                        //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBQ0NFU1NfVE9LRU4iLCJqdGkiOiJiOTFkNmE0ZS1mMDU5LTQzZWYtYmY2NC1jMDkwMDljNzhlOGIiLCJpYXQiOiIxMS8xMC8yMDIyIDg6MTY6MDAgQU0iLCJjbGllbnRfaWQiOiI0NTQxOWFjMjI5NDA0M2E3ODJmZWQ1NjQwYzkxODFjMyIsImNsaWVudF9zZWNyZXQiOiJFcDVGc3dXd1FBSVZvb2k0MG9JVytpR3JndEUvMHVlZExuZkV0TnRuZnlkdDF4NHlyRG5DOVR1MWp6YjgvQW5TcnVQOHREN0tSZlQ3T2FiUVpodFRYWDJIV2JSeEtreklRSzgzWCtIRlYwZz0iLCJleHAiOjE2NjgwNzE3NjAsImlzcyI6IkhBVFRIQV9CQU5LIiwiYXVkIjoiQ0hIQU5FTF9CQU5LIn0.rd57hUSqiZAAfjATtyJZX5zK9E6ENTXGFq10LvZRlGw");

                        var response = await httpClient.PostAsync(Param.url_req,
                        new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
                        var content = await response.Content.ReadAsStringAsync();
                        RE.transaction_id = tranId;
                        RE.created = created;
                        RE.digest = digest;
                        RE.signature = signature;
                        RE.data = content;
                        msgLog.response_data = content;
                        msgLog.InsertLarge();
                    }
                    return await Task.FromResult<ResAPITool>(RE);
                }   
            }
            else
            {
                if (Param.name == null && Param.value == null)
                {
                   
                        using (var httpClientHandler = new HttpClientHandler())
                        {
                            //var body = JsonConvert.DeserializeObject<dynamic>(Param.body.ToString());
                            var httpClient = new HttpClient(httpClientHandler);
                            httpClient.DefaultRequestHeaders.Accept.Clear();
                            var response = await httpClient.PostAsync(Param.url_req,
                            new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
                            var content = await response.Content.ReadAsStringAsync();
                            RE.data = content;
                            msgLog.header_data = "";
                            msgLog.request_data = JsonConvert.SerializeObject(Param) + JsonConvert.SerializeObject(body);
                            msgLog.response_data = content;
                            msgLog.InsertLarge();
                         }
                        
                        return await Task.FromResult<ResAPITool>(RE);

                }
                else
                {
                        using (var httpClientHandler = new HttpClientHandler())
                        {
                            var httpClient = new HttpClient(httpClientHandler);
                            httpClient.DefaultRequestHeaders.Accept.Clear();
                            for (int i = 0; i < Param.name.Length; i++)
                            {
                                httpClient.DefaultRequestHeaders.Add(Param.name[i], Param.value[i]);
                            }
                            var response = await httpClient.PostAsync(Param.url_req,
                            new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
                            var content = await response.Content.ReadAsStringAsync();
                            RE.data = content;
                            msgLog.header_data = Param.name;
                            msgLog.request_data = JsonConvert.SerializeObject(Param) + JsonConvert.SerializeObject(body);
                            msgLog.response_data = content;
                            msgLog.InsertLarge();
                    }
                        return await Task.FromResult<ResAPITool>(RE);
                    
                }
                
            }

        }
       
        public static string GenerateDigest(string rawData)
        {
            var digest = "";
            var bodyText = rawData;
            using (var sha256hash = SHA256.Create())
            {
                byte[] payloadBytes = sha256hash
                    .ComputeHash(Encoding.UTF8.GetBytes(bodyText));
                digest = Convert.ToBase64String(payloadBytes);
                digest = "SHA-256=" + digest;
            }
            return digest;
        }

        public static string GenerateSignature(string keyId,
                                                       string algorithm,
                                                       DateTime dateTime,
                                                       string headers,
                                                       string digest,
                                                       string secretKey,
                                                       string endpoint,
                                                       string transactionId,
                                                       string method,
                                                       int created)
        {
            var signature = ComputeSignature(digest, secretKey, endpoint, created, transactionId, method);
            var signatureHeader = new[] {
                    $"keyId=\"{keyId}\"",
                    $"algorithm=\"{algorithm}\"",
                    $"created={created}",
                    $"headers=\"{headers}\"",
                    $"signature=\"{signature}\""
                };
            var signatureLatest = string.Join(", ", signatureHeader);
            return signatureLatest;
        }

        public static string ComputeSignature(string digest, string secretKey, string endpoint, Int32 created, string transactionId, string method)
        {
            string result = "";
            try
            {
                var digestCompute = new[] {
                    $"(request-target): {method} {endpoint}",
                    $"(created): {created}",
                    $"digest: {digest}",
                    $"client-transaction-id: {transactionId}"};
                var message = string.Join("\n", digestCompute);
                byte[] sigBytes = Encoding.UTF8.GetBytes(message);
                byte[] decodedSecret = Encoding.UTF8.GetBytes(secretKey);
                var hmacSha256 = new HMACSHA256(decodedSecret);
                var messageHash = hmacSha256.ComputeHash(sigBytes);
                result = Convert.ToBase64String(messageHash);
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }
        public class MsgLog
        {
            public string internal_ref { get; set; }
            public string app_id { get; set; }
            public string source { get; set; }
            public string action_type { get; set; }
            public object request_data { get; set; }
            public object response_data { get; set; }
            public object header_data { get; set; }
            public MsgLog() { }
            public MsgLog(string _internal_ref,string _app_id, string _source, string _action_type, object _request_data, object _response_data, object _header_data)
            {
                internal_ref = _internal_ref;
                app_id = _app_id;
                source = _source;
                action_type = _action_type;
                response_data = _response_data;
                response_data = _response_data;
                header_data = _header_data;
            }
            public void Insert()
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = Connection.ConnectionString("ENTITY2");
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                string status = "";
                string message = "";
                try
                {
                    cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_LOG_URL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_int_ref", OracleDbType.Varchar2).Value = internal_ref;
                    cmd.Parameters.Add("p_app_id", OracleDbType.Varchar2).Value = app_id;
                    cmd.Parameters.Add("p_source", OracleDbType.Varchar2).Value = source;
                    cmd.Parameters.Add("p_action_type", OracleDbType.Varchar2).Value = action_type;
                    cmd.Parameters.Add("p_header", OracleDbType.Varchar2).Value = JsonConvert.SerializeObject(header_data, Formatting.Indented);
                    cmd.Parameters.Add("p_request", OracleDbType.Varchar2).Value = JsonConvert.SerializeObject(request_data, Formatting.Indented);
                    cmd.Parameters.Add("p_response", OracleDbType.Varchar2).Value = JsonConvert.SerializeObject(response_data, Formatting.Indented);
                    cmd.Parameters.Add("op_status", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("op_message", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    status = cmd.Parameters["op_status"].Value.ToString();
                    message = cmd.Parameters["op_message"].Value.ToString();
                    
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

            }
            public void InsertLarge(string dataText = null)
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = Connection.ConnectionString("ENTITY2");
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                try
                {
                    //cmd.CommandText = @"INSERT INTO API_MSG_LOG
	                   //         (INTERNAL_REF,
	                   //          APP_ID,
	                   //          SOURCES,
	                   //          ACTION_TYPE,
	                   //          REQUEST_HEADER,
	                   //          REQUEST_PAYLOAD,
	                   //          RESPONSE_DATA,
                    //             ACTION_DATE)
                    //        VALUES
	                   //         (:V_INTERNAL_REF,
	                   //          :V_APP_ID,
	                   //          :V_SOURCES,
	                   //          :V_ACTION_TYPE,
	                   //          :V_REQUEST_HEADER,
	                   //          :V_REQUEST_PAYLOAD,
	                   //          :V_RESPONSE_DATA,
                    //             SYSDATE)";
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_LOG_URL_1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("V_INTERNAL_REF", OracleDbType.Varchar2).Value = internal_ref;
                    cmd.Parameters.Add("V_APP_ID", OracleDbType.Varchar2).Value = app_id;
                    cmd.Parameters.Add("V_SOURCES", OracleDbType.Varchar2).Value = source;
                    cmd.Parameters.Add("V_ACTION_TYPE", OracleDbType.Varchar2).Value = action_type;
                    cmd.Parameters.Add("V_REQUEST_HEADER", OracleDbType.Varchar2).Value = (dataText == null) ? JsonConvert.SerializeObject(header_data, Formatting.Indented) : header_data;
                    cmd.Parameters.Add("V_REQUEST_PAYLOAD", OracleDbType.Varchar2).Value = (dataText == null) ? JsonConvert.SerializeObject(request_data, Formatting.Indented) : request_data;
                    cmd.Parameters.Add("V_RESPONSE_DATA", OracleDbType.Varchar2).Value = (dataText == null) ? JsonConvert.SerializeObject(response_data, Formatting.Indented) : response_data;
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Core.DebugInfo("Log inserted successfully");
                    }
                    else
                    {
                        Core.DebugInfo("Log inserted failed");
                    };
                }
                catch (Exception ex)
                {
                    Core.DebugInfo("Failed insert log");
                    Core.DebugError(ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                }

            }
            
        }
        public static async Task<ResAPIPUserServiceGet> APIPUserServiceGet()
        {
            ResAPIPUserServiceGet REF = new ResAPIPUserServiceGet();
            List<ExeResAPIPUserServiceGet> all_user_service = new List<ExeResAPIPUserServiceGet>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_GET_USERSERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("ALL_USERSERVICE_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                REF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REF.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (REF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["ALL_USERSERVICE_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPUserServiceGet tmpPL = new ExeResAPIPUserServiceGet();
                        tmpPL.service_id = dr1[0].ToString();
                        tmpPL.service_name = dr1[1].ToString();
                        tmpPL.user_id = dr1[2].ToString();
                        tmpPL.user_name = dr1[3].ToString();
                        tmpPL.record_status = dr1[4].ToString();
                        tmpPL.created_by = dr1[5].ToString();
                        tmpPL.created_date = dr1[6].ToString();
                        tmpPL.modifired_by = dr1[7].ToString();
                        tmpPL.modifired_date = dr1[8].ToString();
                        all_user_service.Add(tmpPL);
                    }

                    REF.data = all_user_service;
                    ds1.Dispose();
                    dt1.Dispose();
                    ad1.Dispose();
                    c1.Dispose();
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
            return await Task.FromResult<ResAPIPUserServiceGet>(REF);
        }
        public static async Task<ResAPIPUserServiceMapCheck> APIPUserServiceMapCheck(ReqAPIPUserServiceMapCheck param)
        {
            ResAPIPUserServiceMapCheck RES = new ResAPIPUserServiceMapCheck();
            List<ExeResAPIPUserServiceMapCheck> User_Service_Map_Check = new List<ExeResAPIPUserServiceMapCheck>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_USER_SERVICE_MAP_CHECK";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_USER_SERVICE_ID", OracleDbType.Varchar2).Value = param.user_view_service.ToString();
                cmd.Parameters.Add("USER_SERVICEMAP_CHECK_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["USER_SERVICEMAP_CHECK_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPUserServiceMapCheck tmpPL = new ExeResAPIPUserServiceMapCheck();
                        tmpPL.user_service_id = dr1[0].ToString();
                        tmpPL.user_service_mapid = dr1[1].ToString();
                        tmpPL.user_service_mapdesc = dr1[2].ToString();
                        User_Service_Map_Check.Add(tmpPL);
                    }

                    RES.data = User_Service_Map_Check;
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
            return await Task.FromResult<ResAPIPUserServiceMapCheck>(RES);
        }
        public static async Task<ResAPIPRegisterUserService> APIPRegisterUserService(ReqAPIPRegisterUserService param)
        {
            ResAPIPRegisterUserService Res = new ResAPIPRegisterUserService();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_REGISTER_USERSERVICE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_USER_SERVICE", OracleDbType.Varchar2).Value = param.user_view_service.ToString();
                cmd.Parameters.Add("P_SERVICE_ID", OracleDbType.Varchar2).Value = param.service_id.ToString();
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
            return await Task.FromResult<ResAPIPRegisterUserService>(Res);
        }
        public static async Task<ResAPIPUserTxnCheck> APIPUserTxnCheck()
        {
            ResAPIPUserTxnCheck RES = new ResAPIPUserTxnCheck();
            List<ExeResAPIPUserTxnCheck> User_Service_Txn = new List<ExeResAPIPUserTxnCheck>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "SAPI_MANAGEMENT.PRO_API_USER_TXN";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("USER_SERVICE_TXN_CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RES.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RES.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RES.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["USER_SERVICE_TXN_CUR"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResAPIPUserTxnCheck tmpPL = new ExeResAPIPUserTxnCheck();
                        tmpPL.user_txn_id = dr1[0].ToString();
                        tmpPL.user_txn_desc = dr1[1].ToString();
                        User_Service_Txn.Add(tmpPL);
                    }

                    RES.data = User_Service_Txn;
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
            return await Task.FromResult<ResAPIPUserTxnCheck>(RES);
        }
    }
    
}
