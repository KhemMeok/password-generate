using System.Collections.Generic;

namespace ITOAPP_API.Models
{
    public class OsUserPasswordGenerateModel
    {
        //get data frist load 
        public class ResGetHostNameUser
        {
            public string status { get; set; }
            public string message { get; set; }

            public DataGetHostNameUser data { get; set; }
        }
        public class DataGetHostNameUser
        {
            public List<DataGetHostName> dataGetHostNames { get; set; }
            public List<DataGetOSUser> dataGetOSUsers { get; set; }
            public List<DataSystemType> dataSytemType { get; set; }
            public List<DataGetENV> dataGetENVs { get; set; }
            public List<DataListSite> dataSite { get; set; }

        }
        public class DataListSite
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }
        public class DataSystemType
        {
            public string system_id { get; set; }
            public string system_name { get; set; }
        }
        public class DataGetHostName
        {
            public string host_id { get; set; }
            public string host_name { get; set; }
            public string system_type { get; set; }
            public string site { get; set; }
            public string env { get; set; }
            public string os_platform { get; set; }
            public string staff_id { get; set; }
        }
        public class DataGetENV
        {
            public string env_name { get; set; }
            public string type { get; set; }

        }
        public class DataGetOSUser
        {
            public string host_id { get; set; }
            public string user_name { get; set; }
            public string host_name { get; set; }
            public string site { get; set; }
            public string env { get; set; }
            public string staff_id { get; set; }
            public string os_platform { get; set; }
            public string role { get; set; }
        }
        // end frist load

        // insert record 
        public class ResInsertRecordUserPassword
        {
            public string status { get; set; }
            public string message { get; set; }

        }

        public class ReqInsertRecordUserPassword
        {
            public string record_date { get; set; }
            public string host_name { get; set; }
            public string user_name { get; set; }
            public string password { get; set; }
            public string type { get; set; }
        }
        // update record 
        public class ResUpdateRecordUserPassword
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqUpdateRecordUserPassword
        {
            public string record_date { get; set; }
            public string ids { get; set; }
            public string password { get; set; }
        }
        //get data for insert to csv file 
        public class ResGetUserPasswordTable
        {
            public string status { get; set; }
            public string message { get; set; }
            public ResDataTable data { get; set; }
        }
        public class ResDataTable
        {
            public List<DataUserPasswordtable> AllRecordUserPassword { get; set; }
        }
        public class DataUserPasswordtable
        {
            public string record_id { get; set; }
            public string record_date { get; set; }
            public string host_name { get; set; }
            public string user_name { get; set; }
            public string password { get; set; }
            public string create_by { get; set; }
            public string last_oper_by { get; set; }
            public string last_oper_date { get; set; }
            public string site { get; set; }
            public string environment { get; set; }
            public string host_id { get; set; }
            public string staff_id { get; set; }
            public string os_platform { get; set; }
            public string encrypt { get; set; }
        }
        //csv file
        public class ResGetDataForUpdate
        {
            public string status { get; set; }
            public string message { get; set; }
            public ResDataTableGernerateCSVFile data { get; set; }
        }

        public class DataGetDataForUpdate
        {
            public string userName { get; set; }
            public string systemName { get; set; }
            public string hostId { get; set; }
            public string hostName { get; set; }
            public string site { get; set; }
            public string enviroment { get; set; }
            public string staff_id { get; set; }
            public string os_platform { get; set; }
        }
        public class ResDataTableGernerateCSVFile
        {
            public List<DataGetDataForUpdate> dataUserUpdate { get; set; }
        }

        public class ReqGetDataForUpdate
        {
            public string userName { get; set; }
        }

        // explore record to server with sftp
        public class ResExploreDataToServer
        {
            public string status { get; set; }
            public string message { get; set; }
        }

        public class ResGetUserAdmin
        {
            public string status { get; set; }
            public string message { get; set; }
            public string user_id { get; set; }
        }
        public class ReqGetDataTableOSPwd
        {
            public string encrypt { get; set; }
            public string env { get; set; }
        }
        public class ResGetDataFromSQLServer
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class ReqExploreRecordToCSV
        {
            public string type { get; set; }
        }

        // get user pwd tmp by id
        public class ResGetPwdDataTmpById
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataUserPasswordtable data { get; set; }
        }
        public class ReqGetPwdTmpById
        {
            public string id { get; set; }
            public string encrypt { get; set; }
        }
    }
}