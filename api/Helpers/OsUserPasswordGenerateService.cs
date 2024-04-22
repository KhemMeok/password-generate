using CoreFunction;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using static ITOAPP_API.Models.OsUserPasswordGenerateModel;
using System.IO;
using System.Linq;
using Renci.SshNet;
using StandardCrypt;
using Renci.SshNet.Common;

namespace ITOAPP_API.Helpers
{
    public class OsUserPasswordGenerateService
    {
        public static async Task<ResGetHostNameUser> OSHostNameUserFristLoad()
        {
            ResGetHostNameUser Res = new ResGetHostNameUser();
            DataGetHostNameUser data = new DataGetHostNameUser();
            List<DataGetHostName> all_host_name = new List<DataGetHostName>();
            List<DataGetOSUser> all_OS_user = new List<DataGetOSUser>();
            List<DataSystemType> all_system_type = new List<DataSystemType>();
            List<DataGetENV> all_env = new List<DataGetENV>();
            List<DataListSite> allSite = new List<DataListSite>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.OS_GET_HOSTNAME_AND_USRE_FRIST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_HOSTNAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_OS_USER", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_SYSTEM_TYPE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_ENV", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_SITE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_HOSTNAME"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["P_OS_USER"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["P_SYSTEM_TYPE"].Value;
                    OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["P_ENV"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["P_SITE"].Value;

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
                        DataGetHostName n = new DataGetHostName();
                        n.host_id = dr1[0].ToString();
                        n.host_name = dr1[1].ToString();
                        n.system_type = dr1[2].ToString();
                        n.site = dr1[3].ToString();
                        n.env = dr1[4].ToString();
                        n.os_platform = dr1[5].ToString();
                        n.staff_id = dr1[6].ToString();
                        all_host_name.Add(n);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        DataGetOSUser u = new DataGetOSUser();
                        u.host_id = dr2[0].ToString();
                        u.user_name = dr2[1].ToString();
                        u.host_name = dr2[2].ToString();
                        u.site = dr2[3].ToString();
                        u.env = dr2[4].ToString();
                        u.staff_id = dr2[5].ToString();
                        u.os_platform = dr2[6].ToString();
                        u.role = dr2[7].ToString();
                        all_OS_user.Add(u);
                    }
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        DataSystemType s = new DataSystemType();
                        s.system_id = dr3[0].ToString();
                        s.system_name = dr3[1].ToString();

                        all_system_type.Add(s);
                    }
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        DataGetENV e = new DataGetENV();
                        e.env_name = dr4[0].ToString();
                        e.type = dr4[1].ToString();
                        all_env.Add(e);
                    }
                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        DataListSite s = new DataListSite();
                        s.id = dr5[0].ToString();
                        s.name = dr5[1].ToString();
                        s.type = dr5[2].ToString();
                        allSite.Add(s);
                    }

                    Res.data = data;
                    Res.data.dataGetHostNames = all_host_name;
                    Res.data.dataGetOSUsers = all_OS_user;
                    Res.data.dataSytemType = all_system_type;
                    Res.data.dataGetENVs = all_env;
                    Res.data.dataSite = allSite;


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
            return await Task.FromResult(Res);
        }
        public static async Task<ResInsertRecordUserPassword> InsertRecordUserPassword(ReqInsertRecordUserPassword param)
        {
            ResInsertRecordUserPassword Res = new ResInsertRecordUserPassword();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.INSERT_OS_PWD_GENERATE_TMP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DATE,", OracleDbType.Varchar2).Value = param.record_date.ToString();
                cmd.Parameters.Add("P_OS_USERNAME,", OracleDbType.Varchar2).Value = param.user_name.ToString();
                cmd.Parameters.Add("P_PASSWORD", OracleDbType.Varchar2).Value = param.password.ToString();
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
            return await Task.FromResult<ResInsertRecordUserPassword>(Res);


            /*    try
                {
                    var strem = GetMemoryStream(Core.GetAppSetting("SFTP_CON_INFO:LocalPath"), Core.GetAppSetting("SFTP_CON_INFO:FileName"));

                    CsvRepository repository = new CsvRepository();

                    // get all and add new record
                    List<CsvRecord> listUser = repository.GetAll(strem);

                    var hostNameArr = param.host_name.Split(',');
                    var userNameArr = param.user_name.Split(",");
                    var userDuplicate = 0;
                    foreach (var hostName in hostNameArr)
                    {
                        foreach (var b in userNameArr)
                        {
                            var userName = b.ToString().Split("~");
                            var hostNames = hostName.ToString().Split("~");
                            if (userName[1].ToString().Trim() == hostNames[1].Trim().ToString())
                            {
                                if (listUser.Any(r => r.hostName == hostNames[0].Trim().ToString() && r.userName == userName[0]))
                                {
                                    userDuplicate++;
                                }
                                else
                                {
                                    CsvRecord r = new CsvRecord();
                                    r.date = param.record_date;
                                    r.hostName = hostNames[0];
                                    r.userName = userName[0];
                                    r.password = param.password;
                                    listUser.Add(r);
                                }
                            }
                        }
                    }
                    // write all record and new record
                    repository.Add(listUser, Core.GetAppSetting("SFTP_CON_INFO:LocalPath"), Core.GetAppSetting("SFTP_CON_INFO:FileName"));

                    Res.status = "1";
                    Res.message = "Complete and User Duplicate " + userDuplicate;
                }
                catch (Exception ex)
                {
                    Res.status = "-1";
                    Res.message = ex.Message.ToString();
                    Core.DebugError(ex);
                }
                finally
                {
                    //conn.Close();
                    //conn.Dispose();
                    //cmd.Dispose();

                }
                return await Task.FromResult<ResInsertRecordUserPassword>(Res); */

        }
        public static async Task<ResGetUserPasswordTable> GetDataTable(ReqGetDataTableOSPwd param)
        {
            ResGetUserPasswordTable Res = new ResGetUserPasswordTable();
            ResDataTable data = new ResDataTable();
            List<DataUserPasswordtable> all_data_record = new List<DataUserPasswordtable>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.GET_OS_PWD_GENERATE_TMP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ENV,", OracleDbType.Varchar2).Value = param.env.ToString();
                cmd.Parameters.Add("P_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_DATA"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataUserPasswordtable n = new DataUserPasswordtable();
                        n.record_id = dr1[0].ToString();
                        n.record_date = dr1[1].ToString();
                        n.host_name = dr1[2].ToString();
                        n.user_name = dr1[3].ToString();
                        n.password = param.encrypt.ToString() == "N" ? SCrypt.Decrypt(Core.GetAppSetting("key:standard"), dr1[4].ToString()) : dr1[4].ToString();
                        n.create_by = dr1[5].ToString();
                        n.last_oper_by = dr1[6].ToString();
                        n.last_oper_date = dr1[7].ToString();
                        n.site = dr1[8].ToString();
                        n.environment = dr1[9].ToString();
                        n.host_id = dr1[10].ToString();
                        n.staff_id = dr1[11].ToString();
                        n.os_platform = dr1[12].ToString();
                        n.encrypt = param.encrypt.ToString();
                        all_data_record.Add(n);
                    }
                    Res.data = data;
                    Res.data.AllRecordUserPassword = all_data_record;
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
            return await Task.FromResult(Res);

            /* try
             {
                 var strem = GetMemoryStream(Core.GetAppSetting("SFTP_CON_INFO:LocalPath"), Core.GetAppSetting("SFTP_CON_INFO:FileName"));

                 CsvRepository repository = new CsvRepository();

                 List<CsvRecord> listUser = repository.GetAll(strem);

                 var i = 0;
                 if (listUser.Count > 0)
                 {
                     i = 1;
                     listUser.ForEach(item =>
                     {
                         DataUserPasswordtable d = new DataUserPasswordtable();
                         d.record_id = Convert.ToString(i);
                         d.record_date = item.date;
                         d.host_name = item.hostName;
                         d.user_name = item.userName;
                         d.password = item.password;
                         all_data_record.Add(d);
                         i++;
                     });
                 }
                 data.AllRecordUserPassword = all_data_record;
                 Res.data = data;
                 Res.status = "1";
                 Res.message = "Complete";
             }
             catch (Exception ex)
             {
                 Res.status = "-1";
                 Res.message = ex.Message.ToString();
                 Core.DebugError(ex);
             }
             finally
             {
                 //conn.Close();
                 //conn.Dispose();
                 //cmd.Dispose();

             }
             return await Task.FromResult(Res); */
        }
        public static async Task<ResUpdateRecordUserPassword> UpdateRecordUserPassword(ReqUpdateRecordUserPassword param)
        {
            ResUpdateRecordUserPassword Res = new ResUpdateRecordUserPassword();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.UPDATE_OS_PWD_GENERATE_TMP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DATE,", OracleDbType.Varchar2).Value = param.record_date.ToString();
                cmd.Parameters.Add("P_ID,", OracleDbType.Varchar2).Value = param.ids.ToString();
                cmd.Parameters.Add("P_PASSWORD", OracleDbType.Varchar2).Value = param.password.ToString();
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
            return await Task.FromResult<ResUpdateRecordUserPassword>(Res);

            /* try
             {
                 var strem = GetMemoryStream(Core.GetAppSetting("SFTP_CON_INFO:LocalPath"), Core.GetAppSetting("SFTP_CON_INFO:FileName"));

                 CsvRepository repository = new CsvRepository();

                 // get user pwd for update
                 List<CsvRecord> listUser = new List<CsvRecord>();
                 var arrHostName = param.host_name.Split(",");
                 var arrUserName = param.user_name.Split(",");

                 foreach (var hostName in arrHostName)
                 {
                     foreach (var userNames in arrUserName)
                     {
                         var userName = userNames.ToString().Split("~");
                         var hostNames = hostName.ToString().Split("~");
                         if (userName[1].ToString().Trim() == hostNames[1].Trim().ToString())
                         {
                             if (listUser.Any(r => r.hostName == hostNames[0].Trim().ToString() && r.userName == userName[0]))
                             {
                                 Console.WriteLine(userName.ToString().Split("~")[0]);
                             }
                             else
                             {
                                 CsvRecord record = new CsvRecord();
                                 record.date = param.record_date;
                                 record.hostName = hostNames[0];
                                 record.userName = userName[0];
                                 record.password = param.password;
                                 listUser.Add(record);
                             }
                         }
                     }
                 }

                 repository.Update(strem, listUser, Core.GetAppSetting("SFTP_CON_INFO:LocalPath"), Core.GetAppSetting("SFTP_CON_INFO:FileName"));

                 Res.status = "1";
                 Res.message = "Complete";
             }
             catch (Exception ex)
             {
                 Res.status = "-1";
                 Res.message = ex.Message.ToString();
                 Core.DebugError(ex);
             }
             finally
             {
                 //conn.Close();
                 //conn.Dispose();
                 //cmd.Dispose();

             }
             return await Task.FromResult<ResUpdateRecordUserPassword>(Res); */
        }
        public static async Task<ResUpdateRecordUserPassword> DeleteRecordUserPassword(ReqUpdateRecordUserPassword param)
        {
            ResUpdateRecordUserPassword Res = new ResUpdateRecordUserPassword();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.DELETE_OS_PWD_GENERATE_TMP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_ID,", OracleDbType.Varchar2).Value = param.ids.ToString();
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
            return await Task.FromResult<ResUpdateRecordUserPassword>(Res);

            /* try
             {

                 var strem = GetMemoryStream(Core.GetAppSetting("SFTP_CON_INFO:LocalPath"), Core.GetAppSetting("SFTP_CON_INFO:FileName"));

                 CsvRepository repository = new CsvRepository();

                 //List<CsvRecord> listUser = repository.GetAllTextFromMemoryStream();

                 CsvRecord record = new CsvRecord();
                 record.date = param.record_date;
                 record.hostName = param.host_name;
                 record.userName = param.user_name;
                 record.password = param.password;

                 repository.Delete(strem, record, Core.GetAppSetting("SFTP_CON_INFO:LocalPath"), Core.GetAppSetting("SFTP_CON_INFO:FileName"));

                 Res.status = "1";
                 Res.message = "Delete Complete";
             }
             catch (Exception ex)
             {
                 Res.status = "-1";
                 Res.message = ex.Message.ToString();
                 Core.DebugError(ex);
             }
             finally
             {
                 //conn.Close();
                 //conn.Dispose();
                 //cmd.Dispose();

             }
             return await Task.FromResult<ResUpdateRecordUserPassword>(Res); */
        }
        public static async Task<ResGetDataForUpdate> GetDataForUpdate(ReqGetDataForUpdate param)
        {
            ResGetDataForUpdate Res = new ResGetDataForUpdate();
            List<DataGetDataForUpdate> all_data_record = new List<DataGetDataForUpdate>();
            ResDataTableGernerateCSVFile data = new ResDataTableGernerateCSVFile();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.OS_USER_GET_USER_DATA_FOR_UPDATE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2).Value = param.userName.ToString();
                cmd.Parameters.Add("P_DATA_RECORD", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_DATA_RECORD"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataGetDataForUpdate d = new DataGetDataForUpdate();
                        d.userName = dr1[0].ToString();
                        d.systemName = dr1[1].ToString();
                        d.hostId = dr1[2].ToString();
                        d.hostName = dr1[3].ToString();
                        d.site = dr1[4].ToString();
                        d.enviroment = dr1[5].ToString();
                        d.staff_id = dr1[6].ToString();
                        d.os_platform = dr1[7].ToString();
                        all_data_record.Add(d);
                    }
                    data.dataUserUpdate = all_data_record;
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
            return await Task.FromResult(Res);
        }
        public static async Task<ResExploreDataToServer> ExploreRecord(ReqExploreRecordToCSV param)
        {
            ResExploreDataToServer Res = new ResExploreDataToServer();
            try
            {
                var con = GetConnection();
                List<CsvRecord> listUser = new List<CsvRecord>();
                var dataTable = new ResGetUserPasswordTable();
                var reqDataGetDataTb = new ReqGetDataTableOSPwd();
                reqDataGetDataTb.encrypt = "Y";
                reqDataGetDataTb.env = param.type;
                dataTable = await GetDataTable(reqDataGetDataTb);
                dataTable.data.AllRecordUserPassword.ForEach(x =>
                {
                    CsvRecord csvRecord = new CsvRecord();
                    csvRecord.date = x.record_date;
                    csvRecord.hostName = x.host_name;
                    csvRecord.userName = x.user_name;
                    csvRecord.password = SCrypt.Decrypt(Core.GetAppSetting("Key:Standard"), x.password);
                    listUser.Add(csvRecord);
                });

                UpdateFileToSFTP(con, Core.GetAppSetting("SFTP_CON_INFO:FileName"), listUser, (param.type == "P" ? Core.GetAppSetting("SFTP_CON_INFO:ProdPath") : Core.GetAppSetting("SFTP_CON_INFO:UatPath")));

                Res.status = "1";
                Res.message = "Complete";
            }

            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                //conn.Close();
                //conn.Dispose();
                //cmd.Dispose();

            }
            return await Task.FromResult(Res);
        }
        public class CsvRecord
        {
            public string date { get; set; }
            public string hostName { get; set; }
            public string userName { get; set; }
            public string password { get; set; }

            public static CsvRecord FromCsv(string csvLine)
            {
                string[] values = csvLine.Split('~');
                CsvRecord record = new CsvRecord();
                record.date = values[0];
                record.hostName = values[1];
                record.userName = values[2];
                record.password = values[3];
                return record;
            }
        }
        class CsvRepository
        {

            public CsvRepository()
            {

            }
            public List<CsvRecord> GetAll(Stream stream)
            {
                List<CsvRecord> records = new List<CsvRecord>();

                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    int lineNumber = 0;

                    try
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            lineNumber++;
                            CsvRecord record = CsvRecord.FromCsv(line);
                            records.Add(record);
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine(ex);
                        return new List<CsvRecord>();

                    }
                }

                return records;
            }

            //public void Add(List<CsvRecord> record, string filepath, string fileName)
            //{
            //    var file = filepath + @"\" + fileName;
            //    var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            //    {
            //        HasHeaderRecord = false,
            //        Delimiter = "~"
            //    };

            //    // write to local file path
            //    using (var writer = new StreamWriter(file))
            //    using (var csv = new CsvWriter(writer, configPersons))
            //    {
            //        csv.WriteRecords(record);
            //        csv.Dispose();
            //        writer.Dispose();
            //    }
            //}
            //public void Update(Stream stream, List<CsvRecord> newRecords, string filePath, string fileName)
            //{
            //    List<CsvRecord> records = GetAll(stream);

            //    foreach (CsvRecord newRecord in newRecords)
            //    {
            //        CsvRecord existingRecord = records.FirstOrDefault(r => r.hostName == newRecord.hostName && r.userName == newRecord.userName);

            //        if (existingRecord != null)
            //        {
            //            existingRecord.date = newRecord.date;
            //            existingRecord.password = newRecord.password;
            //        }

            //    }
            //    Add(records, filePath, fileName);
            //}
            //public void Delete(Stream stream, CsvRecord record, string filePath, string fileName)
            //{
            //    List<CsvRecord> records = GetAll(stream);
            //    CsvRecord existingRecord = records.FirstOrDefault(r => r.hostName == record.hostName && r.userName == record.userName);
            //    if (existingRecord != null)
            //    {
            //        records.Remove(existingRecord);
            //        Add(records, filePath, fileName);
            //    }
            //}
        }
        public static ConnectionInfo GetConnection()
        {
            KeyboardInteractiveAuthenticationMethod keybAuth = new KeyboardInteractiveAuthenticationMethod(Core.GetAppSetting("SFTP_CON_INFO:User"));
            keybAuth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
            ConnectionInfo conInfo = new ConnectionInfo(Core.GetAppSetting("SFTP_CON_INFO:HostName"), Convert.ToInt16(Core.GetAppSetting("SFTP_CON_INFO:Port")), Core.GetAppSetting("SFTP_CON_INFO:User"), keybAuth);
            return conInfo;

        }
        public static MemoryStream DownloadFileFromSFTP(ConnectionInfo conInfo, string path, string fileName)
        {
            var stream = new MemoryStream();
            try
            {

                using (SftpClient sftp = new SftpClient(conInfo))
                {
                    sftp.Connect();
                    sftp.ChangeDirectory(path);
                    var sftpStream = sftp.OpenRead(Path.Combine(path, fileName));
                    sftpStream.CopyTo(stream);
                    sftp.Disconnect();
                    return stream;
                }
            }
            catch (ArgumentException e)
            {
                Core.DebugError(e);
                return null;
            }
        }
        private static void HandleKeyEvent(object sender, AuthenticationPromptEventArgs e)
        {
            var sftp_pwd = SCrypt.Decrypt(Core.GetAppSetting("Key:Standard"), Core.GetAppSetting("SFTP_CON_INFO:Password"));
            foreach (AuthenticationPrompt prompt in e.Prompts)
            {
                if (prompt.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    prompt.Response = sftp_pwd;
                }
            }
        }
        public static void UpdateFileToSFTP(ConnectionInfo conInfo, string filename, List<CsvRecord> newRecords, string path)
        {
            var strem = DownloadFileFromSFTP(conInfo, path, filename);

            CsvRepository csvRepository = new CsvRepository();

            List<CsvRecord> existingList = csvRepository.GetAll(strem);

            existingList.AddRange(newRecords.Where(nr => !existingList.Contains(nr)));

            // Upload the updated file
            using (var updatedStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(updatedStream))
                {
                    existingList.ForEach(record =>
                    {
                        writer.WriteLine(record.date + "~" + record.hostName + "~" + record.userName + "~" + record.password);
                    });
                    writer.Flush();
                    updatedStream.Position = 0;
                    using (SftpClient sftp = new SftpClient(conInfo))
                    {
                        sftp.Connect();
                        sftp.ChangeDirectory(path);
                        sftp.UploadFile(updatedStream, filename);
                        sftp.Disconnect();
                    }
                }
            }
        }
        public static async Task<ResGetUserAdmin> GetAdminUser()
        {
            ResGetUserAdmin Res = new ResGetUserAdmin();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.GET_USER_ADMIN";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_USER"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        Res.user_id = Res.user_id + dr1[0].ToString() + ",";
                    }
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
            return await Task.FromResult(Res);
        }
        public static async Task<ResGetUserPasswordTable> GetDataTableExcludeUser()
        {
            ResGetUserPasswordTable Res = new ResGetUserPasswordTable();
            ResDataTable data = new ResDataTable();
            List<DataUserPasswordtable> all_data_record = new List<DataUserPasswordtable>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.get_exclude_user";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["P_DATA"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataUserPasswordtable n = new DataUserPasswordtable();
                        n.record_id = dr1[0].ToString();
                        n.record_date = dr1[1].ToString();
                        n.host_name = dr1[2].ToString();
                        n.user_name = dr1[3].ToString();
                        n.password = dr1[4].ToString();
                        n.create_by = dr1[5].ToString();
                        n.last_oper_by = dr1[6].ToString();
                        n.last_oper_date = dr1[7].ToString();
                        n.site = dr1[8].ToString();
                        n.environment = dr1[9].ToString();
                        n.host_id = dr1[10].ToString();
                        n.staff_id = dr1[11].ToString();
                        n.os_platform = dr1[12].ToString();
                        all_data_record.Add(n);
                    }
                    Res.data = data;
                    Res.data.AllRecordUserPassword = all_data_record;
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
            return await Task.FromResult(Res);
        }
        public static async Task<ResGetPwdDataTmpById> GetUserPasswordTmpById(ReqGetPwdTmpById param)
        {
            ResGetPwdDataTmpById Res = new ResGetPwdDataTmpById();
            DataUserPasswordtable dataById = new DataUserPasswordtable();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "OS_PASSWORD_CHANGE.get_os_pwd_generate_tmp_by_id";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_id,", OracleDbType.Varchar2).Value = param.id.ToString();
                cmd.Parameters.Add("p_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (Res.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["p_data"].Value;
                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ad1.Fill(ds1, c1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        dataById.record_id = dr1[0].ToString();
                        dataById.record_date = dr1[1].ToString();
                        dataById.host_name = dr1[2].ToString();
                        dataById.user_name = dr1[3].ToString();
                        dataById.password = param.encrypt.ToString() == "N" ? SCrypt.Decrypt(Core.GetAppSetting("key:standard"), dr1[4].ToString()) : dr1[4].ToString();
                        dataById.create_by = dr1[5].ToString();
                        dataById.last_oper_by = dr1[6].ToString();
                        dataById.last_oper_date = dr1[7].ToString();
                        dataById.site = dr1[8].ToString();
                        dataById.environment = dr1[9].ToString();
                        dataById.host_id = dr1[10].ToString();
                        dataById.staff_id = dr1[11].ToString();
                        dataById.os_platform = dr1[12].ToString();
                        dataById.encrypt = param.encrypt.ToString();

                    }
                    Res.data = dataById;
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
            return await Task.FromResult(Res);
        }

    }
}
