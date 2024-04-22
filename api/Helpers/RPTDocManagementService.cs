using CoreFunction;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Renci.SshNet.Common;
using Renci.SshNet;
using StandardCrypt;
using static ITOAPP_API.Models.RPTDocManagementModel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace ITOAPP_API.Helpers
{
    public class RPTDocManagementService
    {
        public static async Task<ResFirstLoad> DocMGTGetDataFirstLoad()
        {
            ResFirstLoad RFL = new ResFirstLoad();
            DataResFirstLoad data = new DataResFirstLoad();
            List<ExeDocCategory> ListDocCategory = new List<ExeDocCategory>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.DOC_MANAGEMENTS_FIRST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("OP_DATA_DOC_CATEGORY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RFL.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RFL.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RFL.status == "1")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["OP_DATA_DOC_CATEGORY"].Value;
                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ODA1.Fill(ds1, ORC1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeDocCategory lis_category = new ExeDocCategory();
                        lis_category.doc_category_id = dr1[0].ToString();
                        lis_category.doc_category_name = dr1[1].ToString();
                        lis_category.type = dr1[2].ToString();
                        lis_category.sub_category = dr1[3].ToString();
                        lis_category.dep_id = dr1[4].ToString();
                        ListDocCategory.Add(lis_category);
                    }
                    RFL.data = data;
                    RFL.data.ListDocCategory = ListDocCategory;
                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                RFL.status = "-1";
                RFL.message = ex.ToString();
                Core.DebugError(ex);
                RFL.exception = ex.Message;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return await Task.FromResult<ResFirstLoad>(RFL);
        }
        public static async Task<ResDocMGTGetDataTbLising> DocMGTGetDataTableListing()
        {
            ResDocMGTGetDataTbLising res = new ResDocMGTGetDataTbLising();
            DataResGetTbListing data = new DataResGetTbListing();
            List<ExeDocManagement> ListDocManagement = new List<ExeDocManagement>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.DOC_MANAGEMENTS_GET_TABLE_LISTING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("OP_DATA_DOC_LISTING", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                res.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (res.status == "1")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["OP_DATA_DOC_LISTING"].Value;
                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ODA1.Fill(ds1, ORC1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr2 in dt1.Rows)
                    {
                        ExeDocManagement D = new ExeDocManagement();
                        D.doc_management_id = dr2[0].ToString();
                        D.doc_management_name = dr2[1].ToString();
                        D.doc_management_department = dr2[2].ToString();
                        D.doc_management_unit = dr2[3].ToString();
                        D.doc_management_date = dr2[4].ToString();
                        D.doc_management_code = dr2[5].ToString();
                        D.doc_category_name = dr2[6].ToString();
                        D.doc_name = dr2[7].ToString();
                        D.doc_file = dr2[8].ToString();
                        D.file_path = dr2[9].ToString();
                        D.doc_state = dr2[10].ToString();
                        D.last_oper_id = dr2[11].ToString();
                        D.last_oper_date = dr2[12].ToString();
                        D.doc_remark = dr2[13].ToString();
                        D.doc_category_id = dr2[14].ToString();
                        D.doc_id = dr2[15].ToString();
                        D.upload_by_id = dr2[16].ToString();
                        D.upload_date = dr2[17].ToString();
                        ListDocManagement.Add(D);
                    }
                    data.ListDocManagement = ListDocManagement;
                    res.data = data;
                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.ToString();
                Core.DebugError(ex);
                res.exception = ex.Message;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return await Task.FromResult<ResDocMGTGetDataTbLising>(res);
        }
        public static async Task<ResInsertDocManagement> InsertDocManagement(ReqInsertDocManagement param)
        {
            ResInsertDocManagement RIDM = new ResInsertDocManagement();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            var arr_dep = param.doc_management_department.Split("~");
            var arr_unit = param.doc_management_unit.Split("~");
            var arr_category = param.doc_category_id.Split("~");
            OracleClob msgclob;
            string path = Path.Combine(arr_dep[1].ToString(), arr_unit[1].ToString(), arr_category[1].ToString(), param.doc_management_name.ToString(), param.doc_management_date.ToString());
            path = path.Replace("\\", "/");
            var remotePath = Core.GetAppSetting("SFTP_CON_INFO:RemotePath");
            try
            {

                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.INSERT_DOC_MANAGEMENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_DEPARTMENT", OracleDbType.Varchar2).Value = arr_dep[0].ToString();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_UNIT", OracleDbType.Varchar2).Value = arr_unit[0].ToString();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_DATE", OracleDbType.Varchar2).Value = param.doc_management_date.ToString();
                //cmd.Parameters.Add("P_DOC_MANAGEMENT_CODE", OracleDbType.Varchar2).Value = param.doc_management_code.ToString();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_NAME", OracleDbType.Varchar2).Value = param.doc_management_id.ToString();
                cmd.Parameters.Add("P_DOC_CATEGORY_ID", OracleDbType.Varchar2).Value = arr_category[0].ToString();
                cmd.Parameters.Add("P_DOC_TYPE", OracleDbType.Varchar2).Value = param.doc_file.ToString();
                cmd.Parameters.Add("P_DOC_PATH", OracleDbType.Varchar2).Value = Path.Combine(remotePath, path).ToString();
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.doc_remark.ToString();
                cmd.Parameters.Add("P_DOC_NAME", OracleDbType.Varchar2).Value = param.doc_name.ToString();
                cmd.Parameters.Add("P_DOC_ID", OracleDbType.Varchar2).Value = param.doc_id.ToString();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RIDM.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RIDM.message = msgclob.Value.ToString();
                RIDM.file_path = Path.Combine(remotePath, path).Replace("\\", "/");
                RIDM.file_name = param.doc_name;
            }
            catch (Exception ex)
            {
                RIDM.status = "-1";
                RIDM.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResInsertDocManagement>(RIDM);
        }
        public static async Task<ResEditDocManagement> EditDocManagement(ReqEditDocManagement param)
        {
            ResEditDocManagement REDM = new ResEditDocManagement();
            DataResEditManagement data = new DataResEditManagement();
            ExeEditDocManagement ListEditDocManagement = new ExeEditDocManagement();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            var con = GetConnectionToSFTP();
            var fileName = "";
            var filePath = "";
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.EIDT_DOC_MANAGEMENTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_EIDT_DOC_MANAGEMENT_ID", OracleDbType.Varchar2).Value = param.doc_management_id.ToString();
                cmd.Parameters.Add("OP_EIDT_DOC_MANAGEMENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                REDM.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REDM.message = msgclob.Value.ToString();
                msgclob.Dispose();


                if (REDM.status == "1")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["OP_EIDT_DOC_MANAGEMENT"].Value;
                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ODA1.Fill(ds1, ORC1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ListEditDocManagement.doc_management_department = dr1[0].ToString();
                        ListEditDocManagement.doc_category_name = dr1[2].ToString();
                        ListEditDocManagement.doc_management_unit = dr1[1].ToString();
                        //ListEditDocManagement.doc_management_code = dr1[3].ToString();
                        ListEditDocManagement.doc_management_name = dr1[3].ToString();
                        ListEditDocManagement.doc_management_date = dr1[4].ToString();
                        ListEditDocManagement.doc_remark = dr1[5].ToString();
                        ListEditDocManagement.docMGTDocName = dr1[6].ToString();
                        ListEditDocManagement.docMGTDocType = dr1[8].ToString();
                        fileName = dr1[6].ToString();
                        filePath = dr1[7].ToString();
                    }
                    ListEditDocManagement.docMGTDocFile = DownLoadFileFromSFTP(con, fileName, filePath);
                    REDM.data = data;
                    REDM.data.ListEditDocManagement = ListEditDocManagement;

                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                REDM.status = "-1";
                REDM.message = ex.Message;
                Core.DebugError(ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return await Task.FromResult<ResEditDocManagement>(REDM);
        }
        public static async Task<ResDeletDocumentReport> DeleteDocManagement(ReqDeleteDocumentReport param)
        {
            ResDeletDocumentReport RDDM = new ResDeletDocumentReport();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {

                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.DELETE_DOC_MANAGEMENTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_DOC_MGT_ID", OracleDbType.Varchar2).Value = param.document_id.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RDDM.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RDDM.message = msgclob.Value.ToString();
            }
            catch (Exception ex)
            {
                RDDM.status = "-1";
                RDDM.message = ex.Message;
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

            }

            return await Task.FromResult<ResDeletDocumentReport>(RDDM);
        }
        public static async Task<ResInsertDocManagement> UpdateDocManagement(ReqUpdateReport param)
        {
            ResInsertDocManagement RIDM = new ResInsertDocManagement();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            var arr_dep = param.doc_management_department.Split("~");
            var arr_unit = param.doc_management_unit.Split("~");
            var arr_category = param.doc_category_id.Split("~");
            OracleClob msgclob;
            string path = Path.Combine(arr_dep[1].ToString(), arr_unit[1].ToString(), arr_category[1].ToString(), param.doc_management_name.ToString(), param.doc_management_date.ToString());
            path = path.Replace("\\", "/");
            var remotePath = Core.GetAppSetting("SFTP_CON_INFO:RemotePath");
            var file_path = Path.Combine(remotePath, path).Replace("\\", "/");
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.UPDATE_DOC_MANAGEMENTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_DEPARTMENT", OracleDbType.Varchar2).Value = arr_dep[0].ToString();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_UNIT", OracleDbType.Varchar2).Value = arr_unit[0].ToString();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_DATE", OracleDbType.Varchar2).Value = param.doc_management_date.ToString();
                //cmd.Parameters.Add("P_DOC_MANAGEMENT_CODE", OracleDbType.Varchar2).Value = param.doc_management_code.ToString();
                cmd.Parameters.Add("P_DOC_MANAGEMENT_NAME", OracleDbType.Varchar2).Value = param.doc_management_id.ToString();
                cmd.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2).Value = arr_category[0].ToString();
                cmd.Parameters.Add("P_DOC_NAME", OracleDbType.Varchar2).Value = param.doc_name.ToString();
                cmd.Parameters.Add("P_DOC_FILE", OracleDbType.Varchar2).Value = param.doc_file.ToString();
                cmd.Parameters.Add("P_DOC_PATH", OracleDbType.Varchar2).Value = file_path;
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.doc_remark.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RIDM.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RIDM.message = msgclob.Value.ToString();
                RIDM.file_name = param.doc_name.ToString();
                RIDM.file_path = file_path;
            }
            catch (Exception ex)
            {
                RIDM.status = "-1";
                RIDM.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResInsertDocManagement>(RIDM);
        }
        public static ConnectionInfo GetConnectionToSFTP()
        {
            KeyboardInteractiveAuthenticationMethod keybAuth = new KeyboardInteractiveAuthenticationMethod(Core.GetAppSetting("SFTP_CON_INFO:User"));
            keybAuth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
            ConnectionInfo conInfo = new ConnectionInfo(Core.GetAppSetting("SFTP_CON_INFO:HostName"), Convert.ToInt16(Core.GetAppSetting("SFTP_CON_INFO:Port")), Core.GetAppSetting("SFTP_CON_INFO:User"), keybAuth);
            return conInfo;
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
        public static bool UpdateFileToSFTP(ConnectionInfo conInfo, byte[] file, string fileName, string path)
        {
            using (SftpClient sftp = new SftpClient(conInfo))
            {
                try
                {
                    sftp.Connect();

                    var directories = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                    var filePath = Path.Combine(path, fileName).Replace("\\", "/");

                    // Change to each directory in turn, creating it if it doesn't exist
                    foreach (var directory in directories)
                    {
                        if (sftp.Exists(directory))
                        {
                            sftp.ChangeDirectory(directory);
                        }
                        else
                        {
                            sftp.CreateDirectory(directory);
                            sftp.ChangeDirectory(directory);
                        }
                    }

                    bool directoryExists = sftp.Exists(path);

                    if (directoryExists)
                    {
                        sftp.ChangeDirectory(path);
                        // Upload the file to the specified path on the SFTP server
                        using (var stream = new MemoryStream(file))
                        {
                            sftp.UploadFile(stream, filePath);
                        }
                    }
                    else
                    {
                        // Directory does not exist, handle the case here
                        sftp.CreateDirectory(path);
                        sftp.ChangeDirectory(path);
                        using (var stream = new MemoryStream(file))
                        {
                            sftp.UploadFile(stream, filePath);
                        }
                    }

                    return sftp.Exists(filePath) ? true : false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    sftp.Disconnect();
                }
            }
        }
        public static string DownLoadFileFromSFTP(ConnectionInfo conInfo, string fileName, string path)
        {
            using (SftpClient sftp = new SftpClient(conInfo))
            {
                string base64String = "";
                try
                {
                    sftp.Connect();
                    sftp.ChangeDirectory(path);

                    // Download the file to the specified path on the SFTP server
                    using (var stream = new MemoryStream())
                    {
                        sftp.DownloadFile(fileName, stream);
                        stream.Position = 0;
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        base64String = Convert.ToBase64String(buffer);
                    }
                    return base64String;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    sftp.Disconnect();
                }
            }
        }
        public async static Task<ResDocMGTUploadFile> DocMGTUploadDocManagement(ReqDocMGTUploadFile param)
        {
            var res = new ResDocMGTUploadFile();
            var file = Convert.FromBase64String(param.file_data);

            try
            {
                var remotePath = Core.GetAppSetting("SFTP_CON_INFO:RemotePath");
                var con = GetConnectionToSFTP();
                var status = UpdateFileToSFTP(con, file, param.file_name, Path.Combine(remotePath, param.file_path));

                if (status)
                {
                    res.status = "1";
                    res.message = "Document upload sucesssfully.";
                }
                else
                {
                    res.status = "0";
                    res.message = "Document upload failed.";
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.Message;
                Core.DebugError(ex);
            }
            return await Task.FromResult<ResDocMGTUploadFile>(res);
        }
        public async static Task<ResDownloadFile> DownloadDocManagement(ReqDownloadFile param)
        {
            var res = new ResDownloadFile();
            try
            {
                var con = GetConnectionToSFTP();
                res.fileData = DownLoadFileFromSFTP(con, param.file_name, param.file_path);
                res.file_name = param.file_name;
                if (res.fileData.Length > 0)
                {
                    res.status = "1";
                    res.message = "Download complete";
                }
                else
                {
                    res.status = "0";
                    res.message = "Download failed";
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.Message;
                Core.DebugError(ex);
            }
            return await Task.FromResult<ResDownloadFile>(res);
        }
        public static async Task<ResGetCatogory> GetCaegoryReport()
        {
            ResGetCatogory res = new ResGetCatogory();
            DataCategoryReport data = new DataCategoryReport();
            List<CategoryReport> ListDocCategory = new List<CategoryReport>();
            List<CategoryListing> ListCategory = new List<CategoryListing>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.DOC_MANAGEMENTS_GET_REPORT_NAME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("OP_DATA_CAT_REPORT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_DATA_CATEGORY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                res.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (res.status == "1")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["OP_DATA_CAT_REPORT"].Value;
                    OracleRefCursor ORC2 = (OracleRefCursor)cmd.Parameters["OP_DATA_CATEGORY"].Value;

                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    OracleDataAdapter ODA2 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();

                    ODA1.Fill(ds1, ORC1);
                    ODA2.Fill(ds2, ORC2);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        CategoryReport c = new CategoryReport();
                        c.category_id = dr1[0].ToString();
                        c.category_name = dr1[1].ToString();
                        c.doc_id = dr1[2].ToString();
                        c.doc_name = dr1[3].ToString();
                        c.code = dr1[4].ToString();
                        c.unit = dr1[5].ToString();
                        c.unit_code = dr1[6].ToString();
                        c.last_oper = dr1[7].ToString();
                        c.last_oper_date = dr1[8].ToString();
                        ListDocCategory.Add(c);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        CategoryListing c = new CategoryListing();
                        c.category_id = dr2[0].ToString();
                        c.category_name = dr2[1].ToString();
                        c.last_oper = dr2[2].ToString();
                        c.last_oper_date = dr2[3].ToString();
                        c.remark = dr2[4].ToString();
                        c.type = dr2[5].ToString();
                        ListCategory.Add(c);
                    }

                    data.list_cat_report = ListDocCategory;
                    data.list_cat = ListCategory;
                    res.data = data;

                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return await Task.FromResult<ResGetCatogory>(res);
        }
        public static async Task<ResCategoryOperation> CatogoryOperation(ReqCategoryOperation param)
        {
            ResCategoryOperation res = new ResCategoryOperation();
            DataCategoryForUpdate data = new DataCategoryForUpdate();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.CATEGORY_OPERATIONS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_OPERATION", OracleDbType.Varchar2).Value = param.p_operation.ToString();
                cmd.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2).Value = param.p_category_id.ToString();
                cmd.Parameters.Add("P_CATEGORY_NAME", OracleDbType.Varchar2).Value = param.p_category_name.ToString();
                cmd.Parameters.Add("P_NEW_CATEGORY_NAME", OracleDbType.Varchar2).Value = param.p_new_category_name.ToString();
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.p_remark.ToString();
                cmd.Parameters.Add("P_MONTHLY", OracleDbType.Varchar2).Value = param.p_monthly.ToString();
                cmd.Parameters.Add("P_DATA_EDIT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                res.message = msgclob.Value.ToString();
                if (param.p_operation.ToString() == "GETBYID")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["P_DATA_EDIT"].Value;
                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ODA1.Fill(ds1, ORC1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        data.category_name = dr1[0].ToString();
                        data.remark = dr1[1].ToString();
                        data.monthly = dr1[2].ToString();
                    }
                    res.data_category = data;
                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResCategoryOperation>(res);
        }
        public static async Task<ResReportOperation> ReportOperation(ReqReportOperation param)
        {
            ResReportOperation res = new ResReportOperation();
            DataReportForUpdate data = new DataReportForUpdate();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.DOC_REPORT_OPERATIONS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_OPERATION", OracleDbType.Varchar2).Value = param.p_operation.ToString();
                cmd.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2).Value = param.p_category_id.ToString();
                cmd.Parameters.Add("P_REPORT_NAME", OracleDbType.Varchar2).Value = param.p_report_name.ToString();
                cmd.Parameters.Add("P_CODE", OracleDbType.Varchar2).Value = param.p_code.ToString();
                cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Varchar2).Value = param.p_unit.ToString();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.p_report_id.ToString();
                cmd.Parameters.Add("P_GET_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                res.message = msgclob.Value.ToString();
                if (param.p_operation.ToString() == "GETBYID")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["P_GET_DATA"].Value;
                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ODA1.Fill(ds1, ORC1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        data.doc_id = dr1[0].ToString();
                        data.doc_name = dr1[1].ToString();
                        data.code = dr1[2].ToString();
                        data.category_id = dr1[3].ToString();
                        data.unit = dr1[4].ToString();
                    }
                    res.data_report = data;
                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResReportOperation>(res);
        }
        public static async Task<ResGetDocManagement> GetDocManagementByFilter(ReqGetDocManagement param)
        {
            ResGetDocManagement res = new ResGetDocManagement();
            DataDocManagement data = new DataDocManagement();
            List<ExeDocManagement> listDocManagement = new List<ExeDocManagement>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.FILTER_DOC_MANAGEMENTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_dep", OracleDbType.Varchar2).Value = param.dep.ToString();
                cmd.Parameters.Add("p_unit", OracleDbType.Varchar2).Value = param.unit.ToString();
                cmd.Parameters.Add("p_category_id", OracleDbType.Varchar2).Value = param.category.ToString();
                cmd.Parameters.Add("p_date_from", OracleDbType.Varchar2).Value = param.fromDate.ToString();
                cmd.Parameters.Add("p_date_to", OracleDbType.Varchar2).Value = param.toDate.ToString();
                cmd.Parameters.Add("OP_DATA_DOC_MANAGEMENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                res.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (res.status == "1")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["OP_DATA_DOC_MANAGEMENT"].Value;
                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ODA1.Fill(ds1, ORC1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr2 in dt1.Rows)
                    {
                        ExeDocManagement D = new ExeDocManagement();
                        D.doc_management_id = dr2[0].ToString();
                        D.doc_management_name = dr2[1].ToString();
                        D.doc_management_department = dr2[2].ToString();
                        D.doc_management_unit = dr2[3].ToString();
                        D.doc_management_date = dr2[4].ToString();
                        D.doc_management_code = dr2[5].ToString();
                        D.doc_category_name = dr2[6].ToString();
                        D.doc_name = dr2[7].ToString();
                        D.doc_file = dr2[8].ToString();
                        D.file_path = dr2[9].ToString();
                        D.doc_state = dr2[10].ToString();
                        D.last_oper_id = dr2[11].ToString();
                        D.last_oper_date = dr2[12].ToString();
                        D.doc_remark = dr2[13].ToString();
                        D.doc_category_id = dr2[14].ToString();
                        D.doc_id = dr2[15].ToString();
                        D.upload_by_id = dr2[16].ToString();
                        D.upload_date = dr2[17].ToString();
                        listDocManagement.Add(D);
                    }
                    data.list_doc = listDocManagement;
                    res.data = data;

                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return await Task.FromResult<ResGetDocManagement>(res);
        }
        public static async Task<ResGetSummaryReport> GetDocSummaryByYear(ReqGetSummaryReport param)
        {
            ResGetSummaryReport res = new ResGetSummaryReport();
            DataGetSummaryReport data = new DataGetSummaryReport();
            List<DataSummaryReportListing> listSummary = new List<DataSummaryReportListing>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DOC_MANAGEMENTS.DOC_MANAGEMENTS_GET_REPORT_SUMMARY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_date", OracleDbType.Varchar2).Value = param.summaryYear.ToString();
                cmd.Parameters.Add("OP_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                res.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (res.status == "1")
                {
                    OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["OP_DATA"].Value;
                    OracleDataAdapter ODA1 = new OracleDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ODA1.Fill(ds1, ORC1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DataSummaryReportListing a = new DataSummaryReportListing();
                        a.docId = dr1[0].ToString();
                        a.docCode = dr1[1].ToString();
                        a.docName = dr1[2].ToString();
                        a.docFrequency = dr1[3].ToString();
                        a.jan = dr1[4].ToString();
                        a.feb = dr1[5].ToString();
                        a.mar = dr1[6].ToString();
                        a.apr = dr1[7].ToString();
                        a.may = dr1[8].ToString();
                        a.jun = dr1[9].ToString();
                        a.jul = dr1[10].ToString();
                        a.aug = dr1[11].ToString();
                        a.sep = dr1[12].ToString();
                        a.oct = dr1[13].ToString();
                        a.nov = dr1[14].ToString();
                        a.dec = dr1[15].ToString();
                        a.unit = dr1[16].ToString();

                        listSummary.Add(a);
                    }
                    data.listSummary = listSummary;
                    res.data = data;

                    dt1.Dispose();
                    ds1.Dispose();
                    ODA1.Dispose();
                    ORC1.Dispose();
                }
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return await Task.FromResult<ResGetSummaryReport>(res);
        }
        public static async Task<ResGetSummaryReport> GetConnectionString()
        {
            ResGetSummaryReport res = new ResGetSummaryReport();
            //DataGetSummaryReport data = new DataGetSummaryReport();
            //List<DataSummaryReportListing> listSummary = new List<DataSummaryReportListing>();

            //OracleConnection conn = new OracleConnection();
            //conn.ConnectionString = Connection.ConnectionString("ENTITY6");
            //OracleCommand cmd = new OracleCommand();
            //cmd.Connection = conn;
            //OracleClob msgclob;
            try
            {
                //    conn.Open();
                //    cmd.CommandText = "RPT_DOC_MANAGEMENTS.DOC_MANAGEMENTS_GET_REPORT_SUMMARY";
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("p_date", OracleDbType.Varchar2).Value = param.summaryYear.ToString();
                //    cmd.Parameters.Add("OP_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //    cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                //    cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                //    cmd.ExecuteNonQuery();

                //    res.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                //    msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                //    res.message = msgclob.Value.ToString();
                //    msgclob.Dispose();

                //    if (res.status == "1")
                //    {
                //        OracleRefCursor ORC1 = (OracleRefCursor)cmd.Parameters["OP_DATA"].Value;
                //        OracleDataAdapter ODA1 = new OracleDataAdapter();
                //        DataSet ds1 = new DataSet();
                //        DataTable dt1 = new DataTable();
                //        ODA1.Fill(ds1, ORC1);
                //        dt1 = ds1.Tables[0];
                //        foreach (DataRow dr1 in dt1.Rows)
                //        {
                //            DataSummaryReportListing a = new DataSummaryReportListing();
                //            a.docId = dr1[0].ToString();
                //            a.docCode = dr1[1].ToString();
                //            a.docName = dr1[2].ToString();
                //            a.docFrequency = dr1[3].ToString();
                //            a.jan = dr1[4].ToString();
                //            a.feb = dr1[5].ToString();
                //            a.mar = dr1[6].ToString();
                //            a.apr = dr1[7].ToString();
                //            a.may = dr1[8].ToString();
                //            a.jun = dr1[9].ToString();
                //            a.jul = dr1[10].ToString();
                //            a.aug = dr1[11].ToString();
                //            a.sep = dr1[12].ToString();
                //            a.oct = dr1[13].ToString();
                //            a.nov = dr1[14].ToString();
                //            a.dec = dr1[15].ToString();
                //            a.unit = dr1[16].ToString();

                //            listSummary.Add(a);
                //        }
                //        data.listSummary = listSummary;
                //        res.data = data;

                //        dt1.Dispose();
                //        ds1.Dispose();
                //        ODA1.Dispose();
                //        ORC1.Dispose();
                //    }
                string connectionString = Connection.ConnectionString("ENTITY6");
                //SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                //connection.Open();
                //cmd.CommandText = "select * from CIF01 c where c.CIF_NO = '15426796'";
                //using System.Data.SqlClient;

                //string connectionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
                string sql = "select top 10 c.CIF_NO,c.BR_CODE from CIF01 c";
                string cif = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cif += Convert.ToString(reader.GetString(0)) + ":"+ Convert.ToString(reader.GetString(1));
                    }

                    reader.Close();
                }


                res.message = cif;
            }
            catch (Exception ex)
            {
                res.status = "-1";
                res.message = ex.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                //cmd.Dispose();
                //conn.Close();
                //conn.Dispose();
            }
            return await Task.FromResult<ResGetSummaryReport>(res);
        }
    }
}
