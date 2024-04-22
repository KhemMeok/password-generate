using ITOAPP_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using CoreFunction;
using Newtonsoft.Json;
using Renci.SshNet;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using StandardCrypt;
using System.Runtime.CompilerServices;

namespace ITOAPP_API.Helpers
{
    public class WeblogicServices
    {
        public static async Task<BIPCatalogList> GetBICatalogList(BIResCatalogList param)
        {
            BIPCatalogList Res = new BIPCatalogList();
            BIData data = new BIData();
            data.report_date = await RPTEoCServices.CurrentReportDate();
            data.backup_source = "http://" + Core.GetAppSetting("BI:DC_IP") + ":" + Core.GetAppSetting("BIPWebservice:DC_HTTP_PORT") + "/analytics";
            data.restore_destination = "http://" + Core.GetAppSetting("BI:DR_IP") + ":" + Core.GetAppSetting("BIPWebservice:DR_HTTP_PORT") + "/analytics";
            List<BIPCatalogData> to_backup_catalog = new List<BIPCatalogData>();
            try
            {
                string end_point = "";
                if (param.site == "DC")
                {
                    end_point= "http://" + Core.GetAppSetting("BI:DC_IP") + ":" + Core.GetAppSetting("BIPWebservice:DC_HTTP_PORT") + "/xmlpserver/services/v2/CatalogService";
                }
                else
                {
                    end_point = "http://" + Core.GetAppSetting("BI:DR_IP") + ":" + Core.GetAppSetting("BIPWebservice:DR_HTTP_PORT") + "/xmlpserver/services/v2/CatalogService";
                }
                string request_body = File.ReadAllText(Core.InitPath("\\RequestBody\\BIPCatalogs.xml"));
                request_body = request_body.Replace("rep_bi_user", Core.GetAppSetting("BIPWebservice:USER_ID")).Replace("rep_bi_pwd", SCrypt.Decrypt(Core.GetAppSetting("Key:Standard"), Core.GetAppSetting("BIPWebservice:PWD")));
                string soap_resp = SoapServices.SendRequest(end_point, request_body);
                XmlDocument Doc = new XmlDocument();
                Doc.LoadXml(soap_resp);

                int total_elements = Doc.GetElementsByTagName("item").Count;
                for (var i = 0; i < total_elements; i++)
                {
                    XmlNode node = Doc.GetElementsByTagName("item").Item(i);
                    BIPCatalogData Cat = new BIPCatalogData();
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        if (item.Name == "displayName")
                        {
                            try
                            {
                                Cat.catalog_name = ((item).FirstChild).Value.ToString();
                            }
                            catch (Exception)
                            {
                                Cat.catalog_name = string.Empty;
                            }

                        }
                        if (item.Name == "lastModified")
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(((item).FirstChild).Value.ToString());
                                Cat.last_modified = dt.ToString("MM/dd/yyyy HH:mm");

                            }
                            catch (Exception)
                            {
                                Cat.last_modified = string.Empty;
                            }

                        }
                        if (item.Name == "lastModifier")
                        {
                            try
                            {
                                Cat.last_modifier = ((item).FirstChild).Value.ToString();
                            }
                            catch (Exception)
                            {
                                Cat.last_modifier = string.Empty;
                            }

                        }
                        if (item.Name == "owner")
                        {
                            try
                            {
                                Cat.owner = ((item).FirstChild).Value.ToString();
                            }
                            catch (Exception)
                            {
                                Cat.owner = string.Empty;
                            }

                        }
                    }
                    to_backup_catalog.Add(Cat);
                }
                data.to_backup_catalog = to_backup_catalog;

                Res.status = "1";
                Res.message = "Data successfully retrieved";
                Res.data = data;
            }
            catch (Exception ex)
            {
                Res.status = "-1";
                Res.message = ex.Message.ToString();
                Core.DebugError(ex);
            }

            return await Task.FromResult<BIPCatalogList>(Res);
        }
        public static async Task<BasicResponse> BackupBICatalog(BIResBackupCatalog param)
        {
            BasicResponse Res = new BasicResponse();
            string report_date = await RPTEoCServices.CurrentReportDate();

            DateTime rpt_date = Convert.ToDateTime(report_date);
            string year = rpt_date.Year.ToString();
            string month = rpt_date.Month.ToString("00");
            string day = year + month + rpt_date.Day.ToString("00");
            ArrayList catalog_list = new ArrayList();
            catalog_list = Core.ToArrayList(param.catalog_name);

            var ssh_handler = new SshClient(Core.GetAppSetting("BI:DC_IP"), Core.GetAppSetting("BI:USER_ID"), SCrypt.Decrypt(Core.GetAppSetting("Key:Standard"), Core.GetAppSetting("BI:pwd")));
            try
            {
                ssh_handler.Connect();
                // Create folder
                string save_dir = Core.GetAppSetting("BI:ARCHIVE_DIR") + year + "/" + month + "/" + day;
                string create_dir_cmd = "mkdir -p "+ save_dir;
                var cmd_create_dir = ssh_handler.CreateCommand(create_dir_cmd);
                cmd_create_dir.Execute();
                System.Threading.Thread.Sleep(1000);
                //
                string cd_cmd = "cd "+Core.GetAppSetting("BI:DC_BI_TOOL");
                string run_cmd = "";
                
                foreach (string cname in catalog_list)
                {
                    var cmd_count = ssh_handler.CreateCommand("ls '" + save_dir + "/" + cname + ".catalog'| wc -l");
                    var count = cmd_count.Execute();
                    count = count.Replace("\n", "").Replace("\r", "");
                    if (count == "0")
                    {
                        run_cmd = "";
                        run_cmd = cd_cmd;
                        run_cmd = run_cmd + " && ./runcat.sh -cmd archive -online http://" + Core.GetAppSetting("BI:DC_IP") + ":" + Core.GetAppSetting("BIPWebservice:DC_HTTP_PORT") + "/analytics/saw.dll -credentials cred.txt -folder '/shared/" + cname + "' -outputFile '" + save_dir + "/" + cname + ".catalog' -noTimestamps";
                        var command = ssh_handler.CreateCommand(run_cmd);
                        var result = command.Execute();
                        int check = Regex.Matches(result, @"Command 'archive' completed successfully").Count();
                        string com_stat;
                        if (check == 0)
                        {
                            Res.status = "-1";
                            Res.message = result.ToString();
                            Core.DebugInfo("Error: " + result.ToString());
                            break;
                        }
                        else
                        {
                            Res.status = "1";
                            Res.message = "Successfully Archived";
                            com_stat = "1";
                            RegisterBIBackup(cname, Core.GetAppSetting("BI:DC_IP"), save_dir, com_stat, result.ToString());
                        }
                        Core.DebugInfo(result);
                    }
                    else
                    {
                        Res.status = "-1";
                        Res.message = "Catalog "+cname+" is already existed. Archive Failed";
                        break;
                    }
                    System.Threading.Thread.Sleep(1000);
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
                ssh_handler.Disconnect();
                ssh_handler.Dispose();
            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static async Task<BasicResponse> RestoreBICatalog(BIResRestoreCatalog param)
        {
            BasicResponse Res = new BasicResponse();
            string report_date = await RPTEoCServices.CurrentReportDate();

            DateTime rpt_date = Convert.ToDateTime(report_date);
            string year = rpt_date.Year.ToString();
            string month = rpt_date.Month.ToString("00");
            string day = year + month + rpt_date.Day.ToString("00");
            ArrayList catalog_list = new ArrayList();
            catalog_list = Core.ToArrayList(param.catalog_name);

            var ssh_handler = new SshClient(Core.GetAppSetting("BI:DR_IP"), Core.GetAppSetting("BI:USER_ID"), SCrypt.Decrypt(Core.GetAppSetting("Key:Standard"), Core.GetAppSetting("BI:pwd")));
            try
            {
                ssh_handler.Connect();
                // Create folder
                string restore_dir = Core.GetAppSetting("BI:ARCHIVE_DIR") + year + "/" + month + "/" + day;
                //
                string cd_cmd = "cd " + Core.GetAppSetting("BI:DR_BI_TOOL");
                string run_cmd = "";

                foreach (string cname in catalog_list)
                {
                    if (fn_unarchive_chk(param.report_date.ToString(), cname))
                    {
                        var cmd_count = ssh_handler.CreateCommand("ls '" + restore_dir + "/" + cname + ".catalog'| wc -l");
                        var count = cmd_count.Execute();
                        count = count.Replace("\n", "").Replace("\r", "");
                        if (count != "0")
                        {
                            run_cmd = "";
                            run_cmd = cd_cmd;
                            run_cmd = run_cmd + " && ./runcat.sh -cmd unarchive -online http://" + Core.GetAppSetting("BI:DR_IP") + ":" + Core.GetAppSetting("BIPWebservice:DR_HTTP_PORT") + "/analytics/saw.dll -credentials cred.txt -folder '/shared' -inputFile '" + restore_dir + "/" + cname + ".catalog' -acl preserve -overwrite force";
                            var command = ssh_handler.CreateCommand(run_cmd);
                            var result = command.Execute();
                            int check = Regex.Matches(result, @"Command 'unarchive' completed successfully").Count();
                            string com_stat;
                            if (check == 0)
                            {
                                Res.status = "-1";
                                Res.message = result.ToString();
                                Core.DebugInfo("Error: " + result.ToString());
                                break;
                            }
                            else
                            {
                                Res.status = "1";
                                Res.message = "Successfully Unarchived";
                                com_stat = "1";
                                RegisterBIRestore(cname, Core.GetAppSetting("BI:DR_IP"), restore_dir, com_stat, result.ToString());
                            }
                            Core.DebugInfo(result);
                        }
                        else
                        {
                            Res.status = "-1";
                            Res.message = "Catalog " + cname + " is not existed. Unarchive Failed";
                            break;
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        Res.status = "-1";
                        Res.message = "Catalog " + cname + " is already unarchived. Unarchive Failed";
                    }
                    
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
                ssh_handler.Disconnect();
                ssh_handler.Dispose();
            }
            return await Task.FromResult<BasicResponse>(Res);
        }
        public static void RegisterBIBackup(string catalog_name, string backup_server, string save_dir, string comp_stat, string msg_detail)
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_INSERT_BI_BACKUP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CATALOG_NAME", OracleDbType.Varchar2).Value = catalog_name;
                cmd.Parameters.Add("P_BACKUP_SERVER", OracleDbType.Varchar2).Value = backup_server;
                cmd.Parameters.Add("P_SAVE_DIR", OracleDbType.Varchar2).Value = save_dir;
                cmd.Parameters.Add("P_COMP_STAT", OracleDbType.Varchar2).Value = comp_stat;
                cmd.Parameters.Add("P_MSG_DETAIL", OracleDbType.Varchar2).Value = msg_detail;
                cmd.Parameters.Add("P_BACKUPER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Core.DebugInfo("Catalog: " + catalog_name + ": " + msgclob.Value.ToString());
            }
            catch (Exception ex)
            {
                Core.DebugInfo("Register catalog " + catalog_name + " failed....");
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
        public static void RegisterBIRestore(string catalog_name, string restore_server, string source_dir, string comp_stat, string msg_detail)
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_INSERT_BI_RESTORE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CATALOG_NAME", OracleDbType.Varchar2).Value = catalog_name;
                cmd.Parameters.Add("P_RESTORE_SERVER", OracleDbType.Varchar2).Value = restore_server;
                cmd.Parameters.Add("P_SOURCE_DIR", OracleDbType.Varchar2).Value = source_dir;
                cmd.Parameters.Add("P_COMP_STAT", OracleDbType.Varchar2).Value = comp_stat;
                cmd.Parameters.Add("P_MSG_DETAIL", OracleDbType.Varchar2).Value = msg_detail;
                cmd.Parameters.Add("P_RESTORER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                Core.DebugInfo("Catalog: " + catalog_name + ": " + msgclob.Value.ToString());
            }
            catch (Exception ex)
            {
                Core.DebugInfo("Register catalog " + catalog_name + " failed....");
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
        public static async Task<BIBackupDetail> GetBackupDetail(BIResBackupDetail param)
        {
            BIBackupDetail Res = new BIBackupDetail();
            List<BIDataBackup> data = new List<BIDataBackup>();
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
                cmd.CommandText = "RPT_EOC_PKG.PR_API_BI_BACKUP_DATE";
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
                    BIDataBackup e = new BIDataBackup();
                    e.catalog_name = dr["CATALOG_NAME"].ToString();
                    e.backup_server = dr["BACKUP_SERVER"].ToString();
                    e.restore_server = dr["RESTORE_SERVER"].ToString();
                    e.saved_location = dr["SAVED_LOCATION"].ToString();
                    e.backup_stat = dr["BACKUP_STAT"].ToString();
                    e.restored_stat= dr["RESTORED_STAT"].ToString();
                    e.backup_by = dr["BACKUP_BY"].ToString();
                    e.backup_date = dr["BACKUP_DATE"].ToString();
                    e.restored_by= dr["RESTORED_BY"].ToString();
                    e.restored_date= dr["RESTORED_DATE"].ToString();
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
            return await Task.FromResult<BIBackupDetail>(Res);
        }
        public static bool fn_unarchive_chk(string report_date, string catalog_name)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            Boolean status = false;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_EOC_PKG.PR_API_BI_CATALOG_CHK";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_RPT_DATE", OracleDbType.Varchar2).Value = report_date;
                cmd.Parameters.Add("P_CATALOG_NAME", OracleDbType.Varchar2).Value = catalog_name;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                if (cmd.Parameters["OP_STATUS"].Value.ToString() == "1")
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                Core.DebugInfo(msgclob.Value.ToString());
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                status = false;
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                
            }
            return status;
        }
    }
}
