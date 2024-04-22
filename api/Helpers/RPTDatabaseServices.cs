using CoreFunction;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using static ITOAPP_API.Models.RPTDatabaseModel;
namespace ITOAPP_API.Helpers
{
    public class RPTDatabaseServices
    {
        public static async Task<ResGetDatabaseName> GetDatabaseFristLoad()
        {
            ResGetDatabaseName RGDN = new ResGetDatabaseName();
            DataResGetDatabaseName data = new DataResGetDatabaseName();
            List<ExeResDatabaseReport> db_report_listing = new List<ExeResDatabaseReport>();
            List<ExeResDatabaseCategory> db_category_listing = new List<ExeResDatabaseCategory>();
            //List<ExeResGetDatabaseName> db_name_listing=new List<ExeResGetDatabaseName>();
            // List<ExeResGetPluggable_Database> pluggable_db_listing=new List<ExeResGetPluggable_Database>();
            List<ExeResGetHostName> host_name_listing = new List<ExeResGetHostName>();
            List<ExeResGetChecker> db_checker_listing = new List<ExeResGetChecker>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.GET_DATABASE_FIRST_LOAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("OP_GET_CURRENT_DATE", OracleDbType.Varchar2,50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_DATABASE_REPORT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_DATABASE_CATEGORY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("OP_GET_DATABASE_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("OP_GET_PLUGGABLE_DATABASE",OracleDbType.RefCursor).Direction= ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_HOST", OracleDbType.RefCursor).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("OP_GET_CHECKER", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                RGDN.current_date = cmd.Parameters["OP_GET_CURRENT_DATE"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RGDN.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RGDN.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RGDN.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_GET_DATABASE_REPORT"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_GET_DATABASE_CATEGORY"].Value;
                    //OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_GET_DATABASE_NAME"].Value;
                    //OracleRefCursor c4 = (OracleRefCursor)cmd.Parameters["OP_GET_PLUGGABLE_DATABASE"].Value;
                    OracleRefCursor c5 = (OracleRefCursor)cmd.Parameters["OP_GET_HOST"].Value;
                    OracleRefCursor c6 = (OracleRefCursor)cmd.Parameters["OP_GET_CHECKER"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    //OracleDataAdapter ad3 = new OracleDataAdapter();
                    // OracleDataAdapter ad4 = new OracleDataAdapter();
                    OracleDataAdapter ad5 = new OracleDataAdapter();
                    OracleDataAdapter ad6 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    //DataSet ds3 = new DataSet();
                    // DataSet ds4 = new DataSet();
                    DataSet ds5 = new DataSet();
                    DataSet ds6 = new DataSet();

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    // DataTable dt3 = new DataTable();
                    // DataTable dt4 = new DataTable();
                    DataTable dt5 = new DataTable();
                    DataTable dt6 = new DataTable();

                    ad1.Fill(ds1, c1);
                    ad2.Fill(ds2, c2);
                    //ad3.Fill(ds3, c3);
                    //ad4.Fill(ds4, c4);
                    ad5.Fill(ds5, c5);
                    ad6.Fill(ds6, c6);

                    dt1 = ds1.Tables[0];
                    dt2 = ds2.Tables[0];
                    // dt3 = ds3.Tables[0];
                    // dt4 = ds4.Tables[0];
                    dt5 = ds5.Tables[0];
                    dt6 = ds6.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResDatabaseReport tmpDBCategory = new ExeResDatabaseReport();
                        tmpDBCategory.database_report_id = dr1[0].ToString();
                        tmpDBCategory.database_report_name = dr1[1].ToString();
                        db_report_listing.Add(tmpDBCategory);
                    }
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResDatabaseCategory tmpDBCategory = new ExeResDatabaseCategory();
                        tmpDBCategory.database_category_id = dr2[0].ToString();
                        tmpDBCategory.database_category_name = dr2[1].ToString();
                        db_category_listing.Add(tmpDBCategory);
                    }
                    //foreach(DataRow dr3 in dt3.Rows)
                    //{
                    //    ExeResGetDatabaseName tmpDatabaseName =new ExeResGetDatabaseName();
                    //    tmpDatabaseName.database_id = dr3[0].ToString();
                    //   tmpDatabaseName.database_name=dr3[1].ToString();
                    //    db_name_listing.Add(tmpDatabaseName);
                    //}
                    //foreach (DataRow dr4 in dt4.Rows)
                    //{
                    //    ExeResGetPluggable_Database tmpPluggable = new ExeResGetPluggable_Database();
                    //    tmpPluggable.pluggable_id = dr4[0].ToString();
                    //    tmpPluggable.pluggable_name = dr4[1].ToString();
                    //    pluggable_db_listing.Add(tmpPluggable);
                    //}
                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        ExeResGetHostName tmpHostName = new ExeResGetHostName();
                        tmpHostName.host_id = dr5[0].ToString();
                        tmpHostName.host_name = dr5[1].ToString();
                        host_name_listing.Add(tmpHostName);
                    }
                    foreach (DataRow dr6 in dt6.Rows)
                    {
                        ExeResGetChecker tmpChecker = new ExeResGetChecker();
                        tmpChecker.checker_id = dr6[0].ToString();
                        tmpChecker.checker_name = dr6[1].ToString();
                        db_checker_listing.Add(tmpChecker);
                    }
                    RGDN.data = data;
                    RGDN.data.db_report_listing = db_report_listing;
                    RGDN.data.db_category_listing = db_category_listing;
                    //RGDN.data.db_name_listing = db_name_listing;
                    //RGDN.data.pluggable_db_listing = pluggable_db_listing;
                    RGDN.data.host_name_listing = host_name_listing;
                    RGDN.data.db_checker_listing = db_checker_listing;

                    ds1.Dispose();
                    ds2.Dispose();
                    //ds3.Dispose();
                    //ds4.Dispose();
                    ds5.Dispose();
                    ds6.Dispose();

                    dt1.Dispose();
                    dt2.Dispose();
                    //dt3.Dispose();
                    // dt4.Dispose();
                    dt5.Dispose();
                    dt6.Dispose();

                    ad1.Dispose();
                    ad2.Dispose();
                    //ad3.Dispose();
                    // ad4.Dispose();
                    ad5.Dispose();
                    ad6.Dispose();

                    c1.Dispose();
                    c2.Dispose();
                    //c3.Dispose();
                    // c4.Dispose();
                    c5.Dispose();
                    c6.Dispose();
                }

            }
            catch (Exception ex)
            {
                RGDN.status = "-1";
                RGDN.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetDatabaseName>(RGDN);
        }
        public static async Task<ResGetDatabaseName> GetDatabaseName(ReqCategoryDatabase param)
        {
            ResGetDatabaseName RGDN = new ResGetDatabaseName();
            DataResGetDatabaseName data = new DataResGetDatabaseName();
            List<ExeResGetDatabaseName> db_name_listing = new List<ExeResGetDatabaseName>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.GET_DATABASE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_DATABASE_CATEGORY", OracleDbType.Varchar2).Value = param.category_database.ToString();
                cmd.Parameters.Add("OP_GET_DATABASE_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RGDN.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RGDN.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RGDN.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_GET_DATABASE_NAME"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResGetDatabaseName tmpDBname = new ExeResGetDatabaseName();
                        tmpDBname.database_id = dr1[0].ToString();
                        tmpDBname.database_name = dr1[1].ToString();
                        db_name_listing.Add(tmpDBname);
                    }

                    RGDN.data = data;
                    RGDN.data.db_name_listing = db_name_listing;

                    ds1.Dispose();

                    dt1.Dispose();

                    ad1.Dispose();

                    c1.Dispose();
                }

            }
            catch (Exception ex)
            {
                RGDN.status = "-1";
                RGDN.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetDatabaseName>(RGDN);
        }

        public static async Task<ResGetDatabaseName> GetPuggableDatabase(ReqDatabaseName param)
        {
            ResGetDatabaseName RGPDB = new ResGetDatabaseName();
            DataResGetDatabaseName data = new DataResGetDatabaseName();
            List<ExeResGetPluggable_Database> pluggable_db_listing = new List<ExeResGetPluggable_Database>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.GET_PLUGGABLE_DATABASE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_DATABASE_NAME", OracleDbType.Varchar2).Value = param.database_name.ToString();
                cmd.Parameters.Add("OP_GET_PLUGGABLE_DATABASE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RGPDB.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RGPDB.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RGPDB.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_GET_PLUGGABLE_DATABASE"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResGetPluggable_Database tmpPluggable_DB = new ExeResGetPluggable_Database();
                        tmpPluggable_DB.pluggable_id = dr1[0].ToString();
                        tmpPluggable_DB.pluggable_name = dr1[1].ToString();
                        pluggable_db_listing.Add(tmpPluggable_DB);
                    };

                    RGPDB.data = data;
                    RGPDB.data.pluggable_db_listing = pluggable_db_listing;

                    ds1.Dispose();

                    dt1.Dispose();

                    ad1.Dispose();

                    c1.Dispose();
                }

            }
            catch (Exception ex)
            {
                RGPDB.status = "-1";
                RGPDB.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetDatabaseName>(RGPDB);
        }

        public static async Task<ResInsertReportDatabase> InsertReportDatabase(ReqInsertReportDatabase param)
        {
            ResInsertReportDatabase RIDF = new ResInsertReportDatabase();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            // Byte[] doc_file = Convert.FromBase64String(param.doc_file);
           // String[] strArray2 = doc_file;

             OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.INSERT_DATABASE_REPORT_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report.ToString();
                cmd.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2).Value = param.category_id.ToString();
                cmd.Parameters.Add("P_DATABASE_NAME", OracleDbType.Varchar2).Value = param.database_name.ToString();
                cmd.Parameters.Add("P_PLUGGABLE_DATABASE", OracleDbType.Varchar2).Value = param.pluggable_database.ToString();
                cmd.Parameters.Add("P_BACKUP_SOURCE", OracleDbType.Varchar2).Value = param.backup_source.ToString();
                cmd.Parameters.Add("P_SOURCE_NAME", OracleDbType.Varchar2).Value = param.source_name.ToString();
                cmd.Parameters.Add("P_DESTINATION", OracleDbType.Varchar2).Value = param.destination.ToString();
                cmd.Parameters.Add("P_BACKUP_DATE", OracleDbType.Varchar2).Value = param.backup_date.ToString();
                cmd.Parameters.Add("P_START_TIME", OracleDbType.Varchar2).Value = param.start_time.ToString();
                cmd.Parameters.Add("P_END_TIME", OracleDbType.Varchar2).Value = param.end_time.ToString();
                cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = param.status.ToString();
                cmd.Parameters.Add("P_ISSUE", OracleDbType.Varchar2).Value = param.restore_issue.ToString();
                cmd.Parameters.Add("P_SOLUTION", OracleDbType.Varchar2).Value = param.restore_solution.ToString();
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.db_remark.ToString();
                cmd.Parameters.Add("P_ACTION", OracleDbType.Varchar2).Value = param.action.ToString();
                cmd.Parameters.Add("P_VERIFIER_ID", OracleDbType.Varchar2).Value = param.verifier_id.ToString();

                //cmd.Parameters.Add("P_DOC_NAME", OracleDbType.Varchar2).Value = param.doc_name.ToString();
                //cmd.Parameters.Add("P_DOC_FILE", OracleDbType.Clob).Value = param.doc_file.ToString();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;


                cmd.ExecuteNonQuery();
                RIDF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RIDF.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                RIDF.status = "-1";
                RIDF.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            };

            return await Task.FromResult<ResInsertReportDatabase>(RIDF);
        }

        public static async Task<ResInsertReportDatabase> ReqUploadFile(ReqUploadFile param)
        {
            ResInsertReportDatabase RIDF = new ResInsertReportDatabase();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            //Byte[] doc_file = Convert.FromBase64String(param.doc_file);
            //String[] strArray2 = doc_file;

            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.INSERT_TESTING_RESULTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();

                cmd.Parameters.Add("P_RESTORE_ID", OracleDbType.Varchar2).Value = param.RestoreID.ToString();
                cmd.Parameters.Add("P_DOC_NAME", OracleDbType.Varchar2).Value = param.FileName.ToString();
                cmd.Parameters.Add("P_DOC_FILE", OracleDbType.Clob).Value = param.UploadFile.ToString();
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                RIDF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RIDF.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                RIDF.status = "-1";
                RIDF.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            };

            return await Task.FromResult<ResInsertReportDatabase>(RIDF);
        }
        public static async Task<ResGetDatabaseName> GetUploadFiles(ReqGetFilesResult param)
        {
            ResGetDatabaseName RGDN = new ResGetDatabaseName();
            DataResGetDatabaseName data = new DataResGetDatabaseName();
            List<ExeResGetResult> get_result_listing = new List<ExeResGetResult>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.GET_RESTORATION_RESULTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("OP_GET_RESULTS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RGDN.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                RGDN.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (RGDN.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_GET_RESULTS"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResGetResult tmpResult = new ExeResGetResult();
                        tmpResult.report_id = dr1[0].ToString();
                        tmpResult.report_files = dr1[1].ToString();
                        get_result_listing.Add(tmpResult);
                    }

                    RGDN.data = data;
                    RGDN.data.get_result_listing = get_result_listing;

                    ds1.Dispose();

                    dt1.Dispose();

                    ad1.Dispose();

                    c1.Dispose();
                }

            }
            catch (Exception ex)
            {
                RGDN.status = "-1";
                RGDN.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetDatabaseName>(RGDN);
        }

        public static async Task<ResGetReportName> GetReportRestoration(ReqGetReportName param)
        {
            ResGetReportName DRGR = new ResGetReportName();
            DataExeReportName data = new DataExeReportName();
            List<ExeResGetRestoration> get_report_restoration = new List<ExeResGetRestoration>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.GET_REPORT_RESTORATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.report_name.ToString();
                cmd.Parameters.Add("P_MONTHLY_REPORT", OracleDbType.Varchar2).Value = param.report_monthly.ToString();

                cmd.Parameters.Add("OP_GET_REPORT_RESTORATION", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                DRGR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                DRGR.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (DRGR.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_GET_REPORT_RESTORATION"].Value;

                    OracleDataAdapter ad1 = new OracleDataAdapter();

                    DataSet ds1 = new DataSet();

                    DataTable dt1 = new DataTable();

                    ad1.Fill(ds1, c1);

                    dt1 = ds1.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        ExeResGetRestoration tmpRestoration = new ExeResGetRestoration();
                        tmpRestoration.db_id = dr1[0].ToString();
                        tmpRestoration.mode_no = dr1[1].ToString();
                        tmpRestoration.db_name = dr1[2].ToString();
                        tmpRestoration.backup_source = dr1[3].ToString();
                        tmpRestoration.source_name = dr1[4].ToString();
                        tmpRestoration.destination = dr1[5].ToString();
                        tmpRestoration.start_time = dr1[6].ToString();
                        tmpRestoration.end_time = dr1[7].ToString();
                        tmpRestoration.duration = dr1[8].ToString();
                        tmpRestoration.status = dr1[9].ToString();
                        tmpRestoration.remark= dr1[10].ToString();
                        tmpRestoration.issue = dr1[11].ToString();
                        tmpRestoration.solution = dr1[12].ToString();
                        tmpRestoration.checker_id = dr1[13].ToString();
                        tmpRestoration.attach_file = dr1[14].ToString();
                        tmpRestoration.create_date = dr1[15].ToString();
                        tmpRestoration.maker_id= dr1[16].ToString();
                        tmpRestoration.last_oper_date= dr1[17].ToString();
                        tmpRestoration.last_oper_id= dr1[18].ToString();
                        get_report_restoration.Add(tmpRestoration);
                    };
                   

                    DRGR.data = data;
                    DRGR.data.get_report_restoration = get_report_restoration;
                    ds1.Dispose();

                    dt1.Dispose();

                    ad1.Dispose();

                    c1.Dispose();
                }

            }
            catch (Exception ex)
            {
                DRGR.status = "-1";
                DRGR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetReportName>(DRGR);
        }
        public static async Task<ResGetReportName> GetReportSyncFailure(ReqGetReportName param)
        {
            ResGetReportName DRGR = new ResGetReportName();
            DataExeReportName data = new DataExeReportName();
            List<ExeResGetSyncFailure> get_report_sync_failure = new List<ExeResGetSyncFailure>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.GET_REPORT_SYNC_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.report_name.ToString();
                cmd.Parameters.Add("P_MONTHLY_REPORT", OracleDbType.Varchar2).Value = param.report_monthly.ToString();

                cmd.Parameters.Add("OP_GET_REPORT_SYNC_FAILURE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                DRGR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                DRGR.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (DRGR.status == "1")
                {
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_GET_REPORT_SYNC_FAILURE"].Value;
                   
                    OracleDataAdapter ad2 = new OracleDataAdapter();
                    
                    DataSet ds2 = new DataSet();

                    DataTable dt2 = new DataTable();

                    ad2.Fill(ds2, c2);

                    dt2 = ds2.Tables[0];

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResGetSyncFailure tmpSyncFailure = new ExeResGetSyncFailure();
                        tmpSyncFailure.db_id = dr2[0].ToString();
                        tmpSyncFailure.mode_no = dr2[1].ToString();
                        tmpSyncFailure.db_name = dr2[2].ToString();
                        tmpSyncFailure.start_time = dr2[3].ToString();
                        tmpSyncFailure.end_time = dr2[4].ToString();
                        tmpSyncFailure.duration = dr2[5].ToString();
                        tmpSyncFailure.failed = dr2[6].ToString();
                        tmpSyncFailure.sync = dr2[7].ToString();
                        tmpSyncFailure.remark = dr2[8].ToString();
                        tmpSyncFailure.create_date = dr2[9].ToString();
                        tmpSyncFailure.maker_id = dr2[10].ToString();
                        tmpSyncFailure.last_oper_date = dr2[11].ToString();
                        tmpSyncFailure.last_oper_id = dr2[12].ToString();
                        get_report_sync_failure.Add(tmpSyncFailure);

                    };

                    DRGR.data = data;
                    DRGR.data.get_report_sync_failure = get_report_sync_failure;
                  
                    ds2.Dispose();

                    dt2.Dispose();

                    ad2.Dispose();

                    c2.Dispose();
                }

            }
            catch (Exception ex)
            {
                DRGR.status = "-1";
                DRGR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetReportName>(DRGR);
        }
        public static async Task<ResGetReportName> GetReportBackupFailure(ReqGetReportName param)
        {
            ResGetReportName DRGR = new ResGetReportName();
            DataExeReportName data = new DataExeReportName();
            List<ExeResGetBackupFailure> get_report_backup_failure = new List<ExeResGetBackupFailure>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.GET_REPORT_BACKUP_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.report_name.ToString();
                cmd.Parameters.Add("P_MONTHLY_REPORT", OracleDbType.Varchar2).Value = param.report_monthly.ToString();

                cmd.Parameters.Add("OP_GET_REPORT_BACKUP_FAILURE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                DRGR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                DRGR.message = msgclob.Value.ToString();
                msgclob.Dispose();
                if (DRGR.status == "1")
                {
                   
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_GET_REPORT_BACKUP_FAILURE"].Value;
                    
                    OracleDataAdapter ad3 = new OracleDataAdapter();
                   
                    DataSet ds3 = new DataSet();
                  
                    DataTable dt3 = new DataTable();
                  
                    ad3.Fill(ds3, c3);

                    dt3 = ds3.Tables[0];

                    
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResGetBackupFailure tmpBackupFailure = new ExeResGetBackupFailure();
                        tmpBackupFailure.db_id = dr3[0].ToString();
                        tmpBackupFailure.mode_no = dr3[1].ToString();
                        tmpBackupFailure.db_name = dr3[2].ToString();
                        tmpBackupFailure.pluggable_db = dr3[3].ToString();
                        tmpBackupFailure.backup_date = dr3[4].ToString();
                        tmpBackupFailure.reason = dr3[5].ToString();
                        tmpBackupFailure.action = dr3[6].ToString();
                        tmpBackupFailure.verified_by = dr3[7].ToString();
                        tmpBackupFailure.create_date = dr3[8].ToString();
                        tmpBackupFailure.maker_id = dr3[9].ToString();
                        tmpBackupFailure.last_oper_date = dr3[10].ToString();
                        tmpBackupFailure.last_oper_id = dr3[11].ToString();
                        get_report_backup_failure.Add(tmpBackupFailure);
                    };

                    DRGR.data = data;
                    DRGR.data.get_report_backup_failure = get_report_backup_failure;

                  
                    ds3.Dispose();

                    dt3.Dispose();

                  
                    ad3.Dispose();

                    c3.Dispose();
                }

            }
            catch (Exception ex)
            {
                DRGR.status = "-1";
                DRGR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResGetReportName>(DRGR);
        }


        public static async Task<ResDeleteRestorationResult> DeleteUploadFiles(ReqDeleteRestorationResult param)
        {
            ArrayList report_ids = new ArrayList();
            report_ids = Core.ToArrayList(param.doc_id);

            ResDeleteRestorationResult RDRR = new ResDeleteRestorationResult();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            int I = 0;
            try
            {
                conn.Open();
                foreach (string report_id in report_ids)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "RPT_DATABASE.DELETE_RESTORATION_RESULTS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                    cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                    cmd.Parameters.Add("P_DOC_ID", OracleDbType.Varchar2).Value = param.doc_id.ToString();

                    cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                    RDRR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                    RDRR.message = msgclob.Value.ToString();
                    msgclob.Dispose();
                    if (RDRR.status == "-1")
                    {
                        I += 1;
                    }
                }

                if (I > 0 && report_ids.Count > 1)
                {
                    RDRR.status = "-1";
                    RDRR.message = "Some report id are failed to delete";
                }
                if (I == 0 && report_ids.Count > 1)
                {
                    RDRR.message = "Report id " + param.doc_id + " successfully deleted";
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RDRR.status = "-1";
                RDRR.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                report_ids.Clear();

            }
            return await Task.FromResult<ResDeleteRestorationResult>(RDRR);
        }

        public static async Task<ResDeleteDatabaseReport> DeleteDatabaseReport(ReqDeleteDatabaseReport param)
        {
            ArrayList report_ids = new ArrayList();
            report_ids = Core.ToArrayList(param.report_id);

            ResDeleteDatabaseReport RDDR = new ResDeleteDatabaseReport();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            int I = 0;
            try
            {
                conn.Open();
                foreach (string report_id in report_ids)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "RPT_DATABASE.DELETE_DATABASE_REPORT_ALL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                    cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                    cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report.ToString();
                    cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();

                    cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                    RDDR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                    RDDR.message = msgclob.Value.ToString();
                    msgclob.Dispose();
                    if (RDDR.status == "-1")
                    {
                        I += 1;
                    }
                }

                if (I > 0 && report_ids.Count > 1)
                {
                    RDDR.status = "-1";
                    RDDR.message = "Some report id are failed to delete";
                }
                if (I == 0 && report_ids.Count > 1)
                {
                    RDDR.message = "Report id " + param.report_id + " successfully deleted";
                }
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
                RDDR.status = "-1";
                RDDR.message = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                report_ids.Clear();

            }
            return await Task.FromResult<ResDeleteDatabaseReport>(RDDR);
        }


        public static async Task<ResEditDatabaseReport> EditDatabaseRestore(ReqEditDatabaseReport param)
        {
            ResEditDatabaseReport REDR = new ResEditDatabaseReport();
            DataEditDatabaseReport data = new DataEditDatabaseReport();
            List<ExeDatabaseRestore> edit_restore_listing = new List<ExeDatabaseRestore>();
            List<ExeResGetDatabaseName> edit_database_listing = new List<ExeResGetDatabaseName>();
            List<ExeResGetPluggable_Database> edit_pluggable_listing = new List<ExeResGetPluggable_Database>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.EDIT_DATABASE_RESTORE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report.ToString();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("OP_EIDT_RESTORE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_DATABASE_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_PLUGGABLE_DATABASE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REDR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                REDR.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (REDR.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_EIDT_RESTORE"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_GET_DATABASE_NAME"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_GET_PLUGGABLE_DATABASE"].Value;

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
                        ExeDatabaseRestore tmp_edit_restore = new ExeDatabaseRestore();
                        tmp_edit_restore.report_id = dr1[0].ToString();
                        tmp_edit_restore.category_id = dr1[1].ToString();
                        tmp_edit_restore.db_name = dr1[2].ToString();
                        tmp_edit_restore.backup_source = dr1[3].ToString();
                        tmp_edit_restore.source_name = dr1[4].ToString();
                        tmp_edit_restore.destination = dr1[5].ToString();
                        tmp_edit_restore.start_time = dr1[6].ToString();
                        tmp_edit_restore.end_time = dr1[7].ToString();
                        tmp_edit_restore.status = dr1[8].ToString();
                        tmp_edit_restore.verify_id = dr1[9].ToString();
                        tmp_edit_restore.db_remark = dr1[10].ToString();
                        tmp_edit_restore.restore_issue = dr1[11].ToString();
                        tmp_edit_restore.restore_solution = dr1[12].ToString();                        
                        
                        edit_restore_listing.Add(tmp_edit_restore);
                    };
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResGetDatabaseName tmp_database_name = new ExeResGetDatabaseName();
                        tmp_database_name.database_id = dr2[0].ToString();
                        tmp_database_name.database_name = dr2[1].ToString();
                        edit_database_listing.Add(tmp_database_name);
                    };
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResGetPluggable_Database tmp_pluggable = new ExeResGetPluggable_Database();
                        tmp_pluggable.pluggable_id = dr3[0].ToString();
                        tmp_pluggable.pluggable_name = dr3[1].ToString();
                        edit_pluggable_listing.Add(tmp_pluggable);
                    };

                    REDR.data = data;
                    REDR.data.edit_restore = edit_restore_listing;
                    REDR.data.edit_database_listing = edit_database_listing;
                    REDR.data.edit_pluggable_listing = edit_pluggable_listing;

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
                REDR.status = "-1";
                REDR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResEditDatabaseReport>(REDR);
        }
        public static async Task<ResEditDatabaseReport> EditDatabaseSyncFailure(ReqEditDatabaseReport param)
        {
            ResEditDatabaseReport REDSF = new ResEditDatabaseReport();
            DataEditDatabaseReport data = new DataEditDatabaseReport();
            List<ExeDatabaseSyncFailure> edit_sync_failure_listing = new List<ExeDatabaseSyncFailure>();
            List<ExeResGetDatabaseName> edit_database_listing = new List<ExeResGetDatabaseName>();
            List<ExeResGetPluggable_Database> edit_pluggable_listing = new List<ExeResGetPluggable_Database>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.EDIT_DATABASE_SYNC_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report.ToString();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("OP_EIDT_SYNC_FAILURE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_DATABASE_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_PLUGGABLE_DATABASE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REDSF.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                REDSF.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (REDSF.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_EIDT_SYNC_FAILURE"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_GET_DATABASE_NAME"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_GET_PLUGGABLE_DATABASE"].Value;

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
                        ExeDatabaseSyncFailure tmpsyncfailure = new ExeDatabaseSyncFailure();
                        tmpsyncfailure.report_id = dr1[0].ToString();
                        tmpsyncfailure.category_id = dr1[1].ToString();
                        tmpsyncfailure.db_name = dr1[2].ToString();
                        tmpsyncfailure.start_time = dr1[3].ToString();
                        tmpsyncfailure.end_time = dr1[4].ToString();
                        tmpsyncfailure.db_remark = dr1[5].ToString();
                        edit_sync_failure_listing.Add(tmpsyncfailure);
                    };
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResGetDatabaseName tmp_database_name = new ExeResGetDatabaseName();
                        tmp_database_name.database_id = dr2[0].ToString();
                        tmp_database_name.database_name = dr2[1].ToString();
                        edit_database_listing.Add(tmp_database_name);
                    };
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResGetPluggable_Database tmp_pluggable = new ExeResGetPluggable_Database();
                        tmp_pluggable.pluggable_id = dr3[0].ToString();
                        tmp_pluggable.pluggable_name = dr3[1].ToString();
                        edit_pluggable_listing.Add(tmp_pluggable);
                    };

                    REDSF.data = data;
                    REDSF.data.edit_sync_failure = edit_sync_failure_listing;
                    REDSF.data.edit_database_listing = edit_database_listing;
                    REDSF.data.edit_pluggable_listing = edit_pluggable_listing;

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
                REDSF.status = "-1";
                REDSF.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResEditDatabaseReport>(REDSF);
        }
        public static async Task<ResEditDatabaseReport> EditDatabaseBackUpFailure(ReqEditDatabaseReport param)
        {
            ResEditDatabaseReport REDR = new ResEditDatabaseReport();
            DataEditDatabaseReport data = new DataEditDatabaseReport();
            List<ExeDatabaseBackupFailure> backup_failure_listing = new List<ExeDatabaseBackupFailure>();
            List<ExeResGetDatabaseName> edit_database_listing = new List<ExeResGetDatabaseName>();
            List<ExeResGetPluggable_Database> edit_pluggable_listing = new List<ExeResGetPluggable_Database>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.EDIT_DATABASE_BACKUP_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report.ToString();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("OP_EIDT_BACKUP_FAILURE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_DATABASE_NAME", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_GET_PLUGGABLE_DATABASE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                REDR.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                REDR.message = msgclob.Value.ToString();
                msgclob.Dispose();

                if (REDR.status == "1")
                {
                    OracleRefCursor c1 = (OracleRefCursor)cmd.Parameters["OP_EIDT_BACKUP_FAILURE"].Value;
                    OracleRefCursor c2 = (OracleRefCursor)cmd.Parameters["OP_GET_DATABASE_NAME"].Value;
                    OracleRefCursor c3 = (OracleRefCursor)cmd.Parameters["OP_GET_PLUGGABLE_DATABASE"].Value;

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
                        ExeDatabaseBackupFailure tmpbackupfailure = new ExeDatabaseBackupFailure();
                        tmpbackupfailure.report_id = dr1[0].ToString();
                        tmpbackupfailure.category_id = dr1[1].ToString();
                        tmpbackupfailure.db_name = dr1[2].ToString();
                        tmpbackupfailure.pluggable_db = dr1[3].ToString();
                        tmpbackupfailure.backup_date = dr1[4].ToString();
                        tmpbackupfailure.verify_id = dr1[5].ToString();
                        tmpbackupfailure.actions = dr1[6].ToString();
                        tmpbackupfailure.db_remark = dr1[7].ToString();
                        backup_failure_listing.Add(tmpbackupfailure);
                    };
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        ExeResGetDatabaseName tmp_database_name = new ExeResGetDatabaseName();
                        tmp_database_name.database_id = dr2[0].ToString();
                        tmp_database_name.database_name = dr2[1].ToString();
                        edit_database_listing.Add(tmp_database_name);
                    };
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        ExeResGetPluggable_Database tmp_pluggable = new ExeResGetPluggable_Database();
                        tmp_pluggable.pluggable_id = dr3[0].ToString();
                        tmp_pluggable.pluggable_name = dr3[1].ToString();
                        edit_pluggable_listing.Add(tmp_pluggable);
                    };

                    REDR.data = data;
                    REDR.data.edit_backup_failure = backup_failure_listing;
                    REDR.data.edit_database_listing = edit_database_listing;
                    REDR.data.edit_pluggable_listing = edit_pluggable_listing;


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
                REDR.status = "-1";
                REDR.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResEditDatabaseReport>(REDR);
        }

        public static async Task<ResUpdateReportDatabase> UpdateReportDatabaseRestore(ReqUpdateReportDatabaseRestore param)
        {
            ResUpdateReportDatabase RURD = new ResUpdateReportDatabase();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.UPDATE_DATABASE_RESTORE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();
                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report_id.ToString();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();

                cmd.Parameters.Add("P_CATEGORY_DATABASE_ID", OracleDbType.Varchar2).Value = param.category_database_id.ToString();
                cmd.Parameters.Add("P_DATABASE_NAME", OracleDbType.Varchar2).Value = param.database_name.ToString();
                cmd.Parameters.Add("P_BACKUP_SOURCE", OracleDbType.Varchar2).Value = param.backup_source.ToString();
                cmd.Parameters.Add("P_SOURCE_NAME", OracleDbType.Varchar2).Value = param.source_name.ToString();
                cmd.Parameters.Add("P_DESTINATION", OracleDbType.Varchar2).Value = param.destination.ToString();
                cmd.Parameters.Add("P_START_TIME", OracleDbType.Varchar2).Value = param.start_time.ToString();
                cmd.Parameters.Add("P_END_TIME", OracleDbType.Varchar2).Value = param.end_time.ToString();
                cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = param.status.ToString();

                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.remarks.ToString();
                cmd.Parameters.Add("P_ISSUE", OracleDbType.Varchar2).Value = param.restore_issue.ToString();
                cmd.Parameters.Add("P_SOLUTION", OracleDbType.Varchar2).Value = param.restore_solution.ToString();                                  
                cmd.Parameters.Add("P_VERIFIER_ID", OracleDbType.Varchar2).Value = param.verify_id.ToString();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RURD.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RURD.message = msgclob.Value.ToString();
                msgclob.Dispose();

            }

            catch (Exception ex)
            {
                RURD.status = "-1";
                RURD.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResUpdateReportDatabase>(RURD);
        }
        public static async Task<ResUpdateReportDatabase> UpdateReportSynchronizeFailed(ReqUpdateReportSynchronizeFailed param)
        {
            ResUpdateReportDatabase RURD = new ResUpdateReportDatabase();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.UPDATE_DATABASE_SYNC_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();

                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report_id.ToString();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();
                cmd.Parameters.Add("P_CATEGORY_DATABASE_ID", OracleDbType.Varchar2).Value = param.category_database_id.ToString();
                cmd.Parameters.Add("P_DATABASE_NAME", OracleDbType.Varchar2).Value = param.database_name.ToString();
                cmd.Parameters.Add("P_START_TIME", OracleDbType.Varchar2).Value = param.start_time.ToString();
                cmd.Parameters.Add("P_END_TIME", OracleDbType.Varchar2).Value = param.end_time.ToString();
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.remark_status.ToString();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RURD.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RURD.message = msgclob.Value.ToString();
                msgclob.Dispose();
            }
            catch (Exception ex)
            {
                RURD.status = "-1";
                RURD.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResUpdateReportDatabase>(RURD);
        }
        public static async Task<ResUpdateReportDatabase> UpdateReportBackupFailed(ReqUpdateReportBackupFailed param)
        {
            ResUpdateReportDatabase RURD = new ResUpdateReportDatabase();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connection.ConnectionString("ENTITY2");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            OracleClob msgclob;
            try
            {
                conn.Open();
                cmd.CommandText = "RPT_DATABASE.UPDATE_DATABASE_BACKUP_FAILURE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = Core.GetContextValue("user_id");
                cmd.Parameters.Add("P_DEBUG", OracleDbType.Varchar2).Value = Core.DebugStat();

                cmd.Parameters.Add("P_CATEGORY_REPORT", OracleDbType.Varchar2).Value = param.category_report_id.ToString();
                cmd.Parameters.Add("P_REPORT_ID", OracleDbType.Varchar2).Value = param.report_id.ToString();

                cmd.Parameters.Add("P_CATEGORY_DATABASE_ID", OracleDbType.Varchar2).Value = param.category_database_id.ToString();
                cmd.Parameters.Add("P_DATABASE_NAME", OracleDbType.Varchar2).Value = param.database_name.ToString();
                cmd.Parameters.Add("P_PLUGGABLE_DATABASE", OracleDbType.Varchar2).Value = param.pluggable_database.ToString();


                cmd.Parameters.Add("P_BACKUP_DATE", OracleDbType.Varchar2).Value = param.backup_date.ToString();
                cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2).Value = param.remark_status.ToString();
                cmd.Parameters.Add("P_ACTION", OracleDbType.Varchar2).Value = param.actions.ToString();
                cmd.Parameters.Add("P_VERIFIER_ID", OracleDbType.Varchar2).Value = param.verifier_id.ToString();

                cmd.Parameters.Add("OP_STATUS", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OP_MESSAGE", OracleDbType.Clob).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                RURD.status = cmd.Parameters["OP_STATUS"].Value.ToString();
                msgclob = (OracleClob)cmd.Parameters["OP_MESSAGE"].Value;
                RURD.message = msgclob.Value.ToString();
                msgclob.Dispose();

            }

            catch (Exception ex)
            {
                RURD.status = "-1";
                RURD.message = ex.Message.ToString();
                Core.DebugError(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return await Task.FromResult<ResUpdateReportDatabase>(RURD);
        }




    }
}
