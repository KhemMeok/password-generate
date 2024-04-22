using System.Collections.Generic;

namespace ITOAPP_API.Models
{
    public class RPTDocManagementModel
    {
        public class ResFirstLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResFirstLoad data { get; set; }
            public string exception { get; set; }
        }
        public class ResDocMGTGetDataTbLising
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResGetTbListing data { get; set; }
            public string exception { get; set; }
        }
        public class DataResGetTbListing
        {
            public List<ExeDocManagement> ListDocManagement { get; set; }
        }
        public class DataResFirstLoad
        {
            public List<ExeDocCategory> ListDocCategory { get; set; }
        }
        public class ExeDocCategory
        {
            public string doc_category_id { get; set; }
            public string doc_category_name { get; set; }
            public string type { get; set; }
            public string sub_category { get; set; }
            public string dep_id { get; set; }
        }
        public class ExeDocManagement
        {
            public string doc_management_id { get; set; }

            public string doc_management_department { get; set; }
            public string doc_management_unit { get; set; }
            public string doc_management_code { get; set; }
            public string doc_management_name { get; set; }
            public string doc_management_date { get; set; }
            public string doc_category_id { get; set; }
            public string doc_category_name { get; set; }
            public string doc_id { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
            public string file_path { get; set; }
            public string doc_state { get; set; }
            public string doc_remark { get; set; }
            public string last_oper_id { get; set; }
            public string last_oper_date { get; set; }
            public string upload_by_id { get; set; }
            public string upload_date { get; set; }
        }
        //End First Load

        //Start Insert
        public class ReqInsertDocManagement
        {
            public string doc_management_department { get; set; }
            public string doc_management_unit { get; set; }
            //public string doc_management_code { get; set; }
            public string doc_management_id { get; set; }
            public string doc_management_date { get; set; }
            public string doc_category_id { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
            //public string file_path { get; set; }
            public string doc_remark { get; set; }
            public string doc_id { get; set; }
            public string doc_management_name { get; set; }
        }
        public class ResInsertDocManagement
        {
            public string status { get; set; }
            public string message { get; set; }
            public string file_path { get; set; }
            public string file_name { get; set; }
        }
        ///Edit Doc Management
        public class ReqEditDocManagement
        {
            public string doc_management_id { get; set; }

        }
        public class ResEditDocManagement
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResEditManagement data { get; set; }
        }
        public class DataResEditManagement
        {
            public ExeEditDocManagement ListEditDocManagement { get; set; }
            ///Call public class ExDocManagement
        }
        public class ExeEditDocManagement
        {
            public string doc_management_department { get; set; }
            public string doc_management_unit { get; set; }
            //public string doc_management_code { get; set; }
            public string doc_management_name { get; set; }
            public string doc_management_date { get; set; }
            public string doc_category_name { get; set; }
            public string doc_remark { get; set; }
            public string category_name { get; set; }
            public string doc_id { get; set; }
            public string docMGTDocFile { get; set; }
            public string docMGTDocType { get; set; }
            public string docMGTDocName { get; set; }

        }
        /// UploadFile

        public class ResDocMGTUploadFile
        {
            public string status { get; set; }
            public string message { get; set; }
        }

        public class ReqDocMGTUploadFile
        {
            public string file_data { get; set; }
            public string file_name { get; set; }
            public string file_path { get; set; }
        }
        /// DownloadFile
        public class ResDownloadFile
        {
            public string status { get; set; }
            public string message { get; set; }
            public string fileData { get; set; }
            public string file_name { get; set; }
        }
        public class ReqDownloadFile
        {
            public string file_name { get; set; }
            public string file_path { get; set; }
        }

        // Update report
        public class ResUpdateReport
        {
            public string status { get; set; }
            public string message { get; set; }
        }

        public class ReqUpdateReport
        {
            public string report_id { get; set; }
            public string doc_management_department { get; set; }
            public string doc_management_unit { get; set; }
            //public string doc_management_code { get; set; }
            public string doc_management_id { get; set; }
            public string doc_management_name { get; set; }
            public string doc_management_date { get; set; }
            public string doc_category_id { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
            public string doc_remark { get; set; }
        }

        // delect document report 
        public class ResDeletDocumentReport
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqDeleteDocumentReport
        {
            public string document_id { get; set; }
        }

        // GET CATEGORY AND REPORT NAME
        public class ResGetCatogory
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataCategoryReport data { get; set; }
        }

        public class DataCategoryReport
        {
            public List<CategoryReport> list_cat_report { get; set; }
            public List<CategoryListing> list_cat { get; set; }
        }

        public class CategoryReport
        {
            public string category_id { get; set; }
            public string category_name { get; set; }
            public string doc_id { get; set; }
            public string doc_name { get; set; }
            public string code { get; set; }
            public string unit { get; set; }
            public string unit_code { get; set; }
            public string last_oper { get; set; }
            public string last_oper_date { get; set; }
        }
        public class CategoryListing
        {
            public string category_id { get; set; }
            public string category_name { get; set; }
            public string last_oper { get; set; }
            public string last_oper_date { get; set; }
            public string remark { get; set; }
            public string type { get; set; }
        }
        // cateogry maintance
        public class ResCategoryOperation
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataCategoryForUpdate data_category { get; set; }
        }
        public class ReqCategoryOperation
        {
            public string p_operation { get; set; }
            public string p_category_id { get; set; }
            public string p_category_name { get; set; }
            public string p_new_category_name { get; set; }
            public string p_remark { get; set; }
            public string p_monthly { get; set; }
        }
        public class DataCategoryForUpdate
        {
            public string category_name { get; set; }
            public string remark { get; set; }
            public string monthly { get; set; }
        }
        // Report Name
        public class ResReportOperation
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataReportForUpdate data_report { get; set; }
        }
        public class ReqReportOperation
        {
            public string p_operation { get; set; }
            public string p_category_id { get; set; }
            public string p_report_name { get; set; }
            public string p_code { get; set; }
            public string p_unit { get; set; }
            public string p_report_id { get; set; }
        }
        public class DataReportForUpdate
        {
            public string doc_id { get; set; }
            public string doc_name { get; set; }
            public string code { get; set; }
            public string category_id { get; set; }
            public string unit { get; set; }
        }
        public class ResGetDocManagement
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataDocManagement data { get; set; }
        }
        public class ReqGetDocManagement
        {
            public string dep { get; set; }
            public string unit { get; set; }
            public string category { get; set; }
            public string fromDate { get; set; }
            public string toDate { get; set; }

        }
        public class DataDocManagement
        {
            public List<ExeDocManagement> list_doc { get; set; }
        }
        public class DataGetDocManagemetFilter
        {
            public string doc_management_id { get; set; }
            public string doc_management_department { get; set; }
            public string doc_management_unit { get; set; }
            public string doc_management_code { get; set; }
            public string doc_management_name { get; set; }
            public string doc_management_date { get; set; }
            public string doc_category_name { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
            public string file_path { get; set; }
            public string doc_state { get; set; }
            public string doc_remark { get; set; }
            public string last_oper_id { get; set; }
            public string last_oper_date { get; set; }
        }
        // get summary report
        public class ResGetSummaryReport
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataGetSummaryReport data { get; set; }
        }
        public class ReqGetSummaryReport
        {
            public string summaryYear { get; set; }
        }
        public class DataGetSummaryReport
        {
            public List<DataSummaryReportListing> listSummary { get; set; }
        }
        public class DataSummaryReportListing
        {
            public string docId { get; set; }
            public string docCode { get; set; }
            public string docName { get; set; }
            public string docFrequency { get; set; }
            public string jan { get; set; }
            public string feb { get; set; }
            public string mar { get; set; }
            public string apr { get; set; }
            public string may { get; set; }
            public string jun { get; set; }
            public string jul { get; set; }
            public string aug { get; set; }
            public string sep { get; set; }
            public string oct { get; set; }
            public string nov { get; set; }
            public string dec { get; set; }
            public string unit { get; set; }
        }
    }
}
