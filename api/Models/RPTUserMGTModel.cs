using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOAPP_API.Models
{
    public class RPTUserMGTModel
    {
        //get information staff
        public class ReqInfoStaff
        {
            public string staff_id { get; set; }
        }
        public class ResInfoStaff
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResInfoStaff> data { get; set; }
        }
        public class ExeResInfoStaff
        {
            public string staff_id { get; set; }
            public string full_name { get; set; }
            public string e_mail { get; set; }
            public string jobtile { get; set; }
            public string branch_id { get; set; }
            public string brn_dep_name { get; set; }
        }
        //get information system
        public class ReqCategory{
            public string category_id { get; set; }
        }
        public class ResSystem
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResSystem_type data { get; set; }
        }
        public class DataResSystem_type
        {
            public List<ExeResSystem_type> list_system { get; set; }
        }
        public class ExeResSystem_type
        {
            public string system_id { get; set; }
            public string system_name { get; set; }
        }
        //get infomation role system
        public class ReqSystem_type
        {
            public string system_type { get; set; }
        }
        public class ResRole_system
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResRole_System data { get; set; }
        }
        public class DataResRole_System
        {
            public List<ExeRole_System> list_role { get; set; }
        }
        public class ExeRole_System
        {
            public string role_system_id { get; set; }
            public string role_system_name { get; set; }
        }
        /// <summary>
        /// Get Host Name
        /// </summary>
        public class ReqRole_System
        {
            public string role_system_id { get; set; }
        }
        public class ResHost_Name
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResHost_Name data { get; set; }
        }
        public class DataResHost_Name
        {
            public List<ExeHost_Name> list_hostname { get; set; }
        }
        public class ExeHost_Name
        {
            public string host_id { get; set; }
            public string host_name { get; set; }
        }
       
        /// Service Run Prepare by sophorn
       
        public class ReqHost_Name
        {
            public string host_id { get; set; }
        }
        public class ResService_Run
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataService_Run data { get; set; }
        }
        public class DataService_Run
        {
            public List<ExeService_Run> list_service_run { get; set; }
        }
        public class ExeService_Run
        {
            public string service_id { get; set; }
            public string service_run { get; set; }
        }        
        /// Get user system        
        public class ReqMaker
        {
            public string maker_id { get; set; }
        }
        public class ResUserSystem
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataExeUserSystem data { get; set; }
        }
        public class DataExeUserSystem
        {
            public List<ExeUserSystem> user_system_listing { get; set; }
        }

        public class ExeUserSystem
        {
            public string system_id { get; set; }
            public string system_user { get; set; }
            public string system_type { get; set; }
            public string system_role { get; set; }
            public string host_id { get; set; }
            public string service_run { get; set; }
            public string system_status { get; set; }
        }
        public class ReqViewRequest
        {
            public string system_type { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }    
        }
        public class ResViewRequest
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeView_Request> data { get; set; }
        }
        public class ExeView_Request
        {
            public string system_id { get; set; }
            public string request_id { get; set; }
            public string request_staff_id { get; set; }
            public string request_name { get; set; }
            public string request_email { get; set; }
            public string request_position { get; set; }
            public string branch_code { get; set; }
            public string branch_name { get; set; }
            public string request_date { get; set; }           
            public string system_user { get; set; }
            public string system_type { get; set; }
            public string system_role { get; set; }
            public string system_host_name { get; set; }
            public string system_service_no { get; set; }
            public string verify_date { get; set; }
            public string effective_date { get; set; }
            public string system_status { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
            public string doc_size { get; set; }
            public string doc_date { get; set; }
            public string request_remark { get; set; }
            public string mod_no { get; set; }
            public string maker_id { get; set; }
            public string record_status { get; set; }
            public string create_date { get; set; }
            public string last_oper_id { get; set; }
            public string last_oper_date { get; set; }

        }
        public class ExeResCategorySystem
        {
            public string category_id { get; set; }
            public string category_name { get; set; }
        }


        //Insert User system pre
        public class ReqUserSystemPre
        {
            public string req_staff_id { get; set; }
            public string system_user_id { get; set; }
            public string system_type { get; set; }
            public string system_role { get; set; }
            public string host_name { get; set; }
            public string service_run { get; set; }
            public string system_status { get; set; }
            public string system_maker { get; set; }
        }
        public class ResUserSystemPre
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        //Delete user system pre
        public class ReqDeleteUserSystemID
        {
            public string system_id { get; set; }
            public string maker_id { get; set; }
        }

        public class ReqDeleteUserSystemMaker
        {
            public string maker_id { get; set; }
        }

        public class ResDeleteUserSystem
        {
            //[JsonProperty(Order = 1)]
            public string status { get; set; }
           // [JsonProperty(Order = 2)]
            public string message { get; set; }
        }      

        //Insert User MGMT System
        public class ReqInsertRecord
        {
            public string request_staff_id { get; set; }
            public string request_name { get; set; }
            public string request_eamil { get; set; }
            public string request_position { get; set; }
            public string branch_code { get; set; }
            public string branch_name { get; set; }
            public string request_date { get; set; }
            public string remarks { get; set; }
            public string maker_id { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }           
        }
        public class ResInsertRecord
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        //delete request
        public class ReqDeleteRequest
        {
            public string request_id { get; set; }
            public string maker_id { get; set; }
        }
        public class ResDeleteRequest
        {
            //[JsonProperty(Order = 1)]
            public string status { get; set; }
            // [JsonProperty(Order = 2)]
            public string message { get; set; }
        }
        //Download document
        public class ReqDownloadDocument
        {
            public string request_id { get; set; }
        }
        public class ResDownloadDocument
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeDownloadDocument> data { get; set; }
        }
        public class ExeDownloadDocument
        {
            public string request_id { get; set; }
            public string doc_id { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
        }

        public class ReqUpdateRequest
        {
            public string request_id { get; set; }
            public string staff_id { get; set; }
            public string staff_name { get; set; }
            public string staff_email { get; set; }
            public string position { get; set; }
            public string brn_code { get; set; }
            public string brn_name { get; set; }
            public string req_date { get; set; }
            public string verify_date { get; set; }
            public string effective_date { get; set; }
            public string req_remark { get; set; }

            public string system_user { get; set; }
            public  string system_type { get; set; }
            public string system_role { get; set; } 
            public string system_host_name { get; set; }
            public string system_service_no { get; set; }  
            public string system_status { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
            public string maker_id { get; set; }
        }
        public class ResUpdateRequest {
            public string status { get; set; }
            public string message { get; set; }
        }
        //Top up data to Editor
        public class ReqDataModify
        {
            public string request_id { get; set; }
        }
        public class ResDataModify
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataModify data { get; set; }
        }
        public class DataModify
        {
            public List<ExeDataModify> modify_request { get; set; }
            public List<ExeRole_System> modify_role { get; set; }
            public List<ExeHost_Name> modify_host_name { get; set; }
            public List<ExeService_Run> modify_service_run { get; set; }

        }
        public class ExeDataModify
        {
            public string request_id { get; set; }
            public string staff_id { get; set; }
            public string staff_name { get; set; }
            public string email { get; set; }
            public string position { get; set; }
            public string branch_id { get; set; }
            public string brn_dep_name { get; set; }
            public string req_date { get; set; }
            public string verify_date { get; set; }
            public string effe_date { get; set; }
            //public string doc_file { get; set; }
            public string user_id { get; set; }
            public string system_type { get; set; }
            public string role_type { get; set; }
            public string host_name { get; set; }
            public string service_run { get; set; }
            public string status { get; set; }
            public string req_remark { get; set; }
        }
        public class ReqEffectiveDate
        {
            public string system_id { get; set; }
            public string verify_date { get; set; }
            public string effective_date { get; set; }  
            public string maker_id { get; set; }
        }
        public class ResEffectiveDate
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        //First_load
        public class ReqFirstLoad
        {
            public string system_type { get; set; }
            public string host_name { get; set; }
        }
        public class ResFirstLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataFirstLoad data { get; set; }
        }
        public class DataFirstLoad
        {
            public List<ExeResCategorySystem> list_category { get; set; }
            public List<ExeResSystem_type> list_system { get; set; }
            public List<ExeView_Request> list_view_request{ get; set; }
        }

    }
}
