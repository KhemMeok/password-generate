using System.Collections.Generic;

namespace ITOAPP_API.Models
{
    public class EoCMonitoringModel
    {
        public class ResParamConfig
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataParamConfig data { get; set; }
        }
        public class DataParamConfig
        {
            public string today_date { get; set; }
            public string nextworking_day_date { get; set; }
            public string next_nextworking_day_date { get; set; }
        }
        public class ReqEoDSummary
        {
            public string today_date { get; set; }
            public string nextworking_day_date { get; set; }
            public string target_stage { get; set; }
        }
        public class DataEoDSummary
        {
            public string total_branches { get; set; }
            public string total_finished_eod { get; set; }
            public string total_finished_eodm { get; set; }
            public string total_not_finished_eodm { get; set; }
            public string total_failed_eodm { get; set; }
            public string total_pulled_gl_bal { get; set; }
            public string mismatch_gl_bal { get; set; }
            public string real_debug_enabled { get; set; }
        }
        public class ResEoDSummary
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataEoDSummary data { get; set; }
        }
        public class ReqRunAbleBranches
        {
            public string target_stage { get; set; }
            public string today_date { get; set; }

        }
        public class DataRunAbleBranches
        {
            public string group_code { get; set; }
            public string branch_code { get; set; }
        }
        public class ResRunAbleBranches
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<DataRunAbleBranches> data { get; set; }
        }
        public class ReqFinishEoDM
        {
            public string today_date { get; set; }
            public string nextworking_day_date { get; set; }
        }
        public class DataEoDM
        {
            public string group_code { get; set; }
            public string branch_code { get; set; }
            public string current_posting_date { get; set; }
            public string next_posting_date { get; set; }
        }
        public class ResEoDM
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<DataEoDM> data { get; set; }
        }
        public class ReqNotFinishEoDM
        {
            public string nextworking_day_date { get; set; }
        }
        public class ReqFailedEoDM
        {
            public string today_date { get; set; }
            public string nextworking_day_date { get; set; }
        }
        public class ReqFinishEoC
        {
            public string today_date { get; set; }
            public string target_stage { get; set; }

        }
        public class DataEocBranches
        {
            public string group_code { get; set; }
            public string eoc_status { get; set; }
            public string branch_code { get; set; }
            public string eod_date { get; set; }
            public string branch_date { get; set; }
            public string target_stage { get; set; }
            public string running_stage { get; set; }
            public string current_stage { get; set; }
            public string eoc_ref_no { get; set; }
            public string error_code { get; set; }
            public string error_param { get; set; }
        }
        public class ResFinishEoC
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<DataEocBranches> data { get; set; }
        }
        public class ResSubmitBranches
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataSubmitBranches data { get; set; }

        }
        public class DataSubmitBranches
        {
            public string running_stage { get; set; }
            public string running_count { get; set; }
            public List<DataEocBranches> running_branch { get; set; }
            public List<DataEocBranches> queue_branch { get; set; }
            public List<DataEocBranches> aborted_branch { get; set; }
        }
        public class DataCBSTBS
        {
            public string tablespace_name { get; set; }
            public string size { get; set; }
            public string used { get; set; }
            public string free { get; set; }
            public string used_pct { get; set; }
            public string free_pct { get; set; }
            public string max_size { get; set; }
            public string used_max_pct { get; set; }
        }
        public class ResCBSTBS
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<DataCBSTBS> data { get; set; }
        }
        public class DataCBSDBS
        {
            public string tablespace_name { get; set; }
            public string file_name { get; set; }
            public string file_id { get; set; }
            public string size { get; set; }
            public string free { get; set; }
            public string max_size { get; set; }
            public string used_max_pct { get; set; }
            public string used_size_pct { get; set; }
            public string auto_extended { get; set; }
            public string status { get; set; }
        }
        public class ResCBSDBS
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<DataCBSDBS> data { get; set; }
        }
        public class ReqCBSDBS
        {
            public string tablespace_name { get; set; }
        }
        public class ResDataCBSDBSize
        {
            public string total_size_gb { get; set; }
            public string used_size_gb { get; set; }
        }
        public class ResCBSDBSize
        {
            public string status { get; set; }
            public string message { get; set; }
            public ResDataCBSDBSize data { get; set; }
        }

        public class ResContact
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ResDataContact> data { get; set; }
        }

        public class ResDataContact
        {
            public string branch_code { get; set; }
            public string staff_id { get; set; }
            public string name { get; set; }
            public string position { get; set; }
            public string telephone { get; set; }
        }

        //pending
        public class ResPending
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ResDataPending> data { get; set; }
        }

        public class ResDataPending
        {
            public string no { get; set; }
            public string pending_type { get; set; }
            public string branch_code { get; set; }
            public string mudule { get; set; }
            public string reference_number { get; set; }
            public string even { get; set; }

            public string marker_id { get; set; }
            public string till_id { get; set; }
            public string function_id { get; set; }
            public string key_id { get; set; }

            public string table_name { get; set; }
            public string record_stat { get; set; }
            public string auth_stat { get; set; }
            public string username { get; set; }

        }

        //MissmatchBalance
        public class ResMissmatchBalance
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ResDataMissmatchBalance> data { get; set; }
        }

        public class ResDataMissmatchBalance
        {
            public string no { get; set; }
            public string branch_code { get; set; }
            public string marker_id { get; set; }
            public string mudule { get; set; }
            public string real_dr { get; set; }
            public string real_cr { get; set; }

            public string dr_minus_cr { get; set; }
            public string cont_dr { get; set; }
            public string cont_cr { get; set; }
            public string memo_dr { get; set; }

            public string memo_cr { get; set; }
            public string posn_dr { get; set; }
            public string posn_cr { get; set; }
            public string financial_cycle { get; set; }
            public string period_code { get; set; }


        }


    }
}
