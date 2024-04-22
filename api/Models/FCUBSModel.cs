using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static ITOAPP_API.Models.RPTDatabaseModel;

namespace ITOAPP_API.Models
{
    public class FCUBSModel
    {
        public class ReqRealDebugState
        {
            public string environment_id { get; set; }
        }
        public class ReqUpdateRealDebug
        {
            public string environment_id { get; set; }
            public string enable { get; set; }
        }
        public class ResRealDebugUpdate
        {
            public string status { get; set; }
            public string message { get; set; }
            public RealDebugStat data { get; set; }
        }
        public class RealDebugStat
        {
            public string real_debug { get; set; }
        }
        public class ReqUpdateUserDebug
        {
            public string environment_id { get; set; }
            public string user_id { get; set; }
            public string enable { get; set; }
        }
        public class ExeResDebugEnv
        {
            public int envir_id { get; set; }
            public string enviroment_name { get; set; }
        }
        public class ReqUpdateDebuglogsdate
        {
            public string Logs_Date { get; set; }
        }
        public class ResUpdateDebuglogs
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResUpdateDebuglogs> data { get; set; }
        }
        public class ExeResUpdateDebuglogs
        {
            public int log_id { get; set; }
            public string enviroment { get; set; }
            public string time_stamp { get; set; }
            public string debug_status { get; set; }
            public string debug_param { get; set; }
            public string completed { get; set; }
            public string user_id { get; set; }
            public string completed_by { get; set; }
        }
        public class ReqUserDebugStat 
        {
            public string environment_id { get; set; }
            public string user_id { get; set; }
        }
        public class ResUserDebugStat
        {
            public string status { get; set; }
            public string message { get; set; }
            public ExeResUserDebugStat data { get; set; }
        }
        public class ExeResUserDebugStat
        {
            public string user_debug { get; set; }
        }
        public class ResFcubParamfirstLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public string current_date { get; set; }
            [JsonProperty(Order = 1)]
            public List<ExeResDebugEnv> all_environment { get; set; }
            [JsonProperty(Order = 2)]
            public List<ExeResUpdateDebuglogs> debug_log { get; set; }
        }

        public class ResHandoffFailedFirstLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public string chk_req { get; set; }
            public DataResHandoffFailedFirstLoad data { get; set; }
        }
        public class DataResHandoffFailedFirstLoad
        {
            public List<ExeResHandoffFailedIssueType> fcub_issue_type { get; set; }
            public List<ExeResLogHandoffList> handoff_entries_log { get; set; }
        }
        public class ExeResHandoffFailedIssueType
        {
            public string issue_id { get; set; }
            public string issue_name { get; set; }
        }
        public class ReqHandoffFailedEntries
        {
            public string trn_ref { get; set; }
            public string type { get; set; }
        }
        public class ResHandoffFailedEntries
        {
            public string status { get; set; }
            public string message { get; set; }
            public string op_type { get; set; }
            public List<ExeResHandofffaliedEntriesList> data { get; set; }
        }
        public class ExeResHandofffaliedEntriesList
        {
            public string trn_ref_no { get; set; }
            public string txn_branch { get; set; }
            public string value_dt { get; set; }
            public string user_id { get; set; }
            public string error_cd { get; set; }
            public string txn_stat { get; set; }
            public string txn_date { get; set; }
            public string source_code { get; set; }
            public string error_message { get; set; }
        }
        public class ReqFixHandoffFailedEntries
        {
            public string trn_ref_no { get; set; }
        }
        public class ReqRejectHandoffFailedEntries
        {
            public string trn_ref_no { get; set; }
        }
        public class ReqLogDateHandoff
        {
            public string log_date { get; set; }
        }
        public class ResLogHandoffLising
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResLogHandoffList> data { get; set; }
        }
        public class ExeResLogHandoffList
        {
            public string log_id { get; set; }
            public string trn_ref_no { get; set; }
            public string txn_branch { get; set; }
            public string value_dt { get; set; }
            public string txn_dt { get; set; }
            public string user_id { get; set; }
            public string error_cd { get; set; }
            public string txn_stat { get; set; }
            public string source_code { get; set; }
            public string request_by { get; set; }
            public string request_date { get; set; }
            public string status { get; set; }
            public string resolve_by { get; set; }
            public string resolve_date { get; set; }
            public string reject_by { get; set; }
            public string reject_date { get; set; }
            public string error_message { get; set; }
        }
        public class ReqError_Code
        {
            public string error_code { get; set; }
        }
        public class ResErrorSMS
        {
            public string status { get; set; }
            public string message { get; set; }
            public ExeResErrorSMS data { get; set; }
        }
        public class ExeResErrorSMS
        {
            public string error_code { get; set; }
            public string sms { get; set; }
            public string type { get; set; }
            public string function { get; set; }
        }
        public class ReqCalculator_EoC_Branch_Group
        {
            public string eoc_type { get; set; }
            public string to_stage { get; set; }           
            public string date_start { get; set; }
            public string date_end { get; set; }
            public string group_number { get; set; }

        }
        public class ResEoC_Branch_Group
        {
            public string status { get; set; }
            public string message { get; set; }
            public string eoc_run_time { get; set; }
            public DataExeEoC_Branch_Group data { get; set; }            
        }
        public class DataExeEoC_Branch_Group
        {
            public List<ExeResEoC_Total_Group> get_eoc_total_group_listing { get; set; }
            public List<ExeResEoC_Branch_Group> get_eoc_branch_group_listing { get; set; }  

        }
        public class ExeResEoC_Total_Group
        {
            public string group_code { get; set; }
            public string total_duration { get; set; }
            public string total_branches { get; set; }
            public string status_pull { get; set; }
            public string date_pull { get; set; }
            public string status { get; set; }
            public string maker_id { get; set; }
        }
        public class ExeResEoC_Branch_Group
        {
            public string group_code { get; set; }
            public string branch_code { get; set; }
            public string duraction_avg { get; set; }
            public string add_date { get; set; }
            public string branch_seq { get; set; }
            public string status { get; set; }
            public string maker_id { get; set; }

        }       
        public class ResCalculator_EoC_Branch_Group_Generate
        {
            public string status { get; set; }
            public string message { get; set; }
        } 
        
        public class ResCalculator_EoC_Branch_Group_Push
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ResCalculator_EoC_Branch_Group_Reject
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqCalculator_EoC_Branch_Group_Refresh
        {
            public string range_date_start { get; set; }
            public string range_date_end { get; set; }
        }

        public class ResCalculator_EoC_Branch_Group_Refresh
        {
            public string option_type { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public DataExeEoC_Branch_Group_Refresh data { get; set; }
        }
        public class DataExeEoC_Branch_Group_Refresh
        {
            public List<ExeResEoC_Total_Group_Refresh> get_eoc_total_group_refresh_listing { get; set; }
            public List<ExeResEoC_Branch_Group_Refresh> get_eoc_branch_group_refresh_listing { get; set; }
            public List<ExeResEoC_Total_Group_his> get_eoc_total_group_his_listing { get; set; }
            public List<ExeResEoC_Branch_Group_his> get_eoc_branch_group_his_listing { get; set; }
            public List<ExeResEoC_Group_Branches> get_eoc_branch_group_brn_listing { get; set; }
            public List<ExeResEoC_Group_Master> get_eoc_branch_group_mas_listing { get; set; }
        }
        public class ExeResEoC_Total_Group_Refresh
        {
            public string group_code { get; set; }
            public string total_duration { get; set; }
            public string total_branches { get; set; }
            public string status_pull { get; set; }
            public string date_pull { get; set; }
            public string status { get; set; }
            public string maker_id { get; set; }
        }
        public class ExeResEoC_Branch_Group_Refresh
        {
            public string group_code { get; set; }
            public string branch_code { get; set; }
            public string duraction_avg { get; set; }
            public string add_date { get; set; }
            public string branch_seq { get; set; }
            public string status { get; set; }
            public string maker_id { get; set; }
        }
        public class ExeResEoC_Total_Group_his
        {
            public string group_code { get; set; }
            public string total_duration { get; set; }
            public string total_branches { get; set; }
            public string status_pull { get; set; }
            public string status { get; set; }
            public string request_date { get; set; }
            public string maker_id { get; set; }
            public string authorize_date { get; set; }
            public string autthorize { get; set; }
        }
        public class ExeResEoC_Branch_Group_his
        {
            public string group_code { get; set; }
            public string branch_code { get; set; }
            public string duraction_avg { get; set; }
            public string branch_seq { get; set; }
            public string status { get; set; }
            public string request_date { get; set; }
            public string maker_id { get; set; }
            public string authorize_date { get; set; }
            public string autthorize { get; set; }

        }
        public class ExeResEoC_Group_Branches
        {
            public string group_code { get; set; }
            public string branch_code { get; set; }
            public string branch_name { get; set; }
            public string branch_seq { get; set; }
        }
        public class ExeResEoC_Group_Master
        {
            public string group_code { get; set; }
            public string group_des { get; set; }
            public string record_stat { get; set; }
            public string auth_stat { get; set; }
            public string maker_id { get; set; }
            public string maker_date { get; set; }
            public string checker_id { get; set; }
            public string checker_date { get; set; }
        }
    }
}
