using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace ITOAPP_API.Models
{
    public class RPTDatabaseModel
    {
        public class ReqInsertReportDatabase
        {
            public string category_report { get; set; }
            public string category_id { get; set; }
            public string database_name { get; set; }
            public string pluggable_database { get; set; }
            public string backup_source { get; set; }
            public string source_name { get; set; }
            public string destination { get; set; }
            public string backup_date {get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; } 
            public string status { get; set; }
            public string restore_issue { get; set; }
            public string restore_solution { get; set; }
            public string db_remark { get; set; }
            public string action { get; set; }
            public string verifier_id { get; set; }
        }
        public class ResInsertReportDatabase
        {
            public string status { get; set; }
            public string message { get; set; }
        }

        public class ReqUploadFile
        {
            public string RestoreID { get; set; }
            public string FileName { get; set; }
            public string UploadFile { get; set; }
        }

    public class FileModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ContentType { get; set; }
            public byte[] Data { get; set; }
        }


        public class ReqCategoryDatabase
        {
            public string category_database { get; set; }
        }

        public class ResGetDatabaseName
        {
            public string current_date { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public DataResGetDatabaseName data { get; set; }
        }
        public class DataResGetDatabaseName
        {
            public List<ExeResDatabaseReport> db_report_listing { get; set; }
            public List<ExeResDatabaseCategory> db_category_listing { get; set; }
            public List<ExeResGetDatabaseName> db_name_listing { get; set; }
            public List<ExeResGetPluggable_Database> pluggable_db_listing { get; set; }
            public List<ExeResGetHostName> host_name_listing { get; set; }
            public List<ExeResGetChecker> db_checker_listing{ get; set; }
            public List<ExeResGetResult> get_result_listing { get; set; }


        }
        public class ExeResGetResult
        {
            public string report_id { get; set; }
            public string report_files { get; set; }
        }

        public class ExeResDatabaseReport
        {
            public string database_report_id { get; set; }
            public string database_report_name { get; set; }
        }
        public class ExeResDatabaseCategory
        {
            public string database_category_id { get; set; }
            public string database_category_name { get; set; }
        }

        public class ExeResGetDatabaseName
        {
            public string database_id { get; set; } 
            public string database_name { get; set; }
        }  
        public class ExeResGetPluggable_Database
        {
            public string pluggable_id { get; set; }
            public string pluggable_name { get; set; }
        }
        public class ExeResGetHostName
        {
            public string host_id { get; set; }
            public string host_name { get; set; }
        }

        public class ExeResGetChecker
        {
            public string checker_id { get; set; }
            public string checker_name { get; set; }
        }

        public class ReqGetReportName
        {
            public string report_name { get; set; }
            public string report_monthly { get; set; }
        }
        public class ResGetReportName
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataExeReportName data { get; set; }

        }
        public class DataExeReportName
        {
            public List<ExeResGetRestoration> get_report_restoration { get; set; }
            public List<ExeResGetSyncFailure> get_report_sync_failure { get; set; }
            public List<ExeResGetBackupFailure> get_report_backup_failure { get; set; }

        }
        public class ReqDatabaseName
        {
            public string database_name { get; set; }
        }
        public class ReqGetFilesResult
        {
            public string report_id { get; set; }
        }

        public class ExeResGetRestoration
        { 
            public string db_id { get; set; }
            public string mode_no { get; set; }
            public string db_name { get; set; }
            public string backup_source { get; set; }
            public string source_name { get; set; }
            public string destination { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string duration { get; set; }
            public string status { get; set; }
            public string remark { get; set; }
            public string issue { get; set; }
            public string solution { get; set; }  
            public string attach_file { get; set; }             
            public string create_date { get; set; }
            public string maker_id { get; set; }
            public string checker_id { get; set; }
            public string last_oper_date { get; set; }
            public string last_oper_id { get; set; }
        }
        public class ExeResGetSyncFailure
        {
            public string db_id { get; set; }
            public string mode_no { get; set; }
            public string db_name { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string duration { get; set; }
            public string failed { get; set; }
            public string sync { get; set; }
            public string remark { get; set; }
            public string create_date { get; set; }
            public string maker_id { get; set; }
            public string last_oper_date { get; set; }
            public string last_oper_id { get; set; }
        }
        public class ExeResGetBackupFailure
        {
            public string db_id { get; set; }
            public string mode_no { get; set; }
            public string db_name { get; set; }
            public string pluggable_db { get; set; }
            public string backup_date { get; set; }
            public string reason { get; set; }
            public string action { get; set; }           
            public string verified_by { get; set; }
            public string create_date { get; set; }
            public string maker_id { get; set; }
            public string last_oper_date { get; set; }
            public string last_oper_id { get; set; }
        }
        public class ReqDeleteDatabaseReport
        {
            public string report_id { get; set; }
            public string category_report { get; set; }
            
        }
        public class ResDeleteDatabaseReport
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqEditDatabaseReport
        {
            public string report_id { get; set; }
            public string category_report { get; set; }            
        }
        public class ResEditDatabaseReport
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataEditDatabaseReport data { get; set; }
        }
        public class DataEditDatabaseReport
        {
            public List<ExeDatabaseRestore> edit_restore { get; set; }  
            public List<ExeDatabaseSyncFailure> edit_sync_failure { get; set; }  
            public List<ExeDatabaseBackupFailure> edit_backup_failure { get; set; } 
            public List<ExeResGetDatabaseName> edit_database_listing { get; set; }
            public List<ExeResGetPluggable_Database> edit_pluggable_listing { get; set; }

        }
        public class ExeDatabaseRestore
        {
            public string report_id { get; set; }
            public string category_id { get; set; }
            public string db_name { get; set; }
            public string backup_source { get; set; }
            public string source_name { get; set; }
            public string destination { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string status { get; set; }
            public string verify_id { get; set; }
            public string db_remark { get; set; }
            public string restore_issue { get; set; }
            public string restore_solution { get; set; }           
        }
        public class ExeDatabaseSyncFailure
        {
            public string report_id { get; set; }
            public string category_id { get; set; }
            public string db_name { get;set; }
            public string pluggable_db { get; set; }
            public string start_time { get;set;}
            public string end_time { get;set; }
            public string db_remark { get;set;} 
        }
        public class ExeDatabaseBackupFailure
        {
            public string report_id { get; set; }
            public string category_id { get; set; }
            public string db_name { get; set; }
            public string pluggable_db { get; set; }
            public string backup_date { get; set; }
            public string verify_id { get; set; }
            public string actions { get; set; }
            public string db_remark { get; set; }

        }
        public class ReqUpdateReportDatabaseRestore
        {
            public string category_report_id { get; set; }
            public string report_id { get; set; }
            public string category_database_id { get; set; }
            public string database_name { get; set; }
            public string backup_source { get; set; }
            public string source_name { get; set; }
            public string destination { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string status { get; set; }
            public string verify_id { get; set; }
            public string remarks { get; set; }
            public string restore_issue { get; set; }
            public string restore_solution { get; set; }
        }       
        public class ReqUpdateReportSynchronizeFailed
        {
            public string category_report_id { get; set; }
            public string report_id { get; set; }
            public string category_database_id { get; set; }
            public string database_name { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string remark_status { get; set; }
        }
        public class ReqUpdateReportBackupFailed
        {
            public string category_report_id { get; set; }
            public string report_id { get; set; }

            public string category_database_id { get; set; }
            public string database_name { get; set; }
            public string pluggable_database { get; set; }
            public string backup_date { get; set; }
            public string remark_status { get; set; }
            public string actions { get; set; }
            public string verifier_id { get; set; }
        }
        public class ResUpdateReportDatabase
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqDeleteRestorationResult
        {
            public string doc_id { get; set; }
        }
        public class ResDeleteRestorationResult
        {
            public string status { get; set; }
            public string message { get; set; }
        }
    }
}
