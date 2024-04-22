using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOAPP_API.Models
{
    public class RPT_ServerINVT_Model
    {
        public class ResServerINVTAllServerListing
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResServerINVTHostListing> data { get; set; }
        }
        public class ResServerINVTAllServiceListing
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResServerINVTSERVICEListing> data { get; set; }
        }
        public class ResServerINVTAllCSIListing
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResServerINVTCSIListing> data { get; set; }
        }
        public class ResServerINVTonLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResServerINVT data { get; set; }
        }
        public class DataResServerINVT
        {
            public List<ExeResServerINVTSystemType> all_system_type { get; set; }
            public List<ExeResServerINVTOSPlatform> all_os_platform { get; set; }
            public List<ExeResServerINVTCSI> all_csi { get; set; }
            public List<ExeResServerINVTSYSTEMTYPEService> all_system_type_service { get; set; }
            public List<ExeResServerINVTHostListing> host_listing { get; set; }
            public List<ExeResServerINVTCSIListing> csi_listing { get; set; }
            public List<ExeResServerINVTSERVICEListing> service_listing { get; set; }
            public List<DataResServerINVTGetDRof> all_dc_host { get; set; }
            public List<DataResServerINVTProductType> all_product_type { get; set; }
        }
        public class DataResServerINVTProductType
        {
            public string pd_id { get; set; }
            public string product_name { get; set; }
        }
        public class ExeResServerINVTSERVICEListing
        {
            public string service_id { get; set; }
            public string service_type { get; set; }
            public string service_run { get; set; }
            public string host_id { get; set; }
            public string remark { get; set; }
            public string map_date { get; set; }
            public string map_by { get; set; }
            public string last_oper_id { get; set; }
            public string last_oper_dt { get; set; }
        }
        public class ExeResServerINVTSYSTEMTYPEService
        {
            public string system_id { get; set; }
            public string system_name { get; set; }
        }
        public class ExeResServerINVTSystemType
        {
            public string system_type { get; set; }
            public string pl_name { get; set; }
        }
        public class ExeResServerINVTOSPlatform
        {
            public string os_id { get; set; }
            public string os_name { get; set; }
        }
        public class ExeResServerINVTCSI
        {
            public string no { get; set; }
            public string csi_number { get; set; }
            public string sn { get; set; }
        }
        public class ExeResServerINVTHostListing
        {
            public string host_id { get; set; }
            public string host_name { get; set; }
            public string product_desc { get; set; }
            public string site { get; set; }
            public string dr_of { get; set; }
            public string system_type { get; set; }
            public string os_platform { get; set; }
            public string run_service { get; set; }
            public string csi { get; set; }
            public string ip_mgmt { get; set; }
            public string ip_lan { get; set; }
            public string os_version { get; set; }
            public string environment { get; set; }
            public string remark { get; set; }
            public string record_stat { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
            public string last_oper_dt { get; set; }
        }
        public class ExeResServerINVTCSIListing
        {
            public string no { get; set; }
            public string csi_sla { get; set; }
            public string sn { get; set; }
            public string contract_type { get; set; }
            public string product_type { get; set; }
            public string supporter { get; set; }
            public string contact_person { get; set; }
            public string asr { get; set; }
            public string start_date { get; set; }
            public string expire_date { get; set; }
            public string remark { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
            public string last_oper_id { get; set; }
            public string last_oper_dt { get; set; }
        }
        public class ReqServerINVTRegisterHost
        {
            public string host_name { get; set; }
            public string product_des { get; set; }
            public string site { get; set; }
            public string dr_of { get; set; }
            public string system_type { get; set; }
            public string environment { get; set; }
            public string remark { get; set; }
            public string os_platform { get; set; }
            public string os_version { get; set; }
            public string csi { get; set; }
            public string ip_mgt { get; set; }
            public string ip_lan { get; set; }
        }
        public class ResServerINVTRegisterHost
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class DataResServerINVTGetDRof
        {
            public string host_id { get; set; }
            public string host_name { get; set; }
        }
        public class ReqServerINVTRegisterCSI
        {
            public string csi_sla { get; set; }
            public string sn { get; set; }
            public string contract_type { get; set; }
            public string product_type { get; set; }
            public string supporter { get; set; }
            public string asr { get; set; }
            public string start_date { get; set; }
            public string expire_date { get; set; }
            public string contact_person { get; set; }
            public string remark { get; set; }
            public string doc_support { get; set; }
            public string doc_name { get; set; }
        }
        public class ResServerINVTRegisterCSI
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqServerINVTServiceMapping
        {
            public string service_type { get; set; }
            public string service_run { get; set; }
            public string host_id { get; set; }
            public string remark { get; set; }
        }
        public class ResServerINVTServiceMapping
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqServerINVTDeleteReport
        {
            public string report_type { get; set; }
            public string report_id { get; set; }
        }
        public class ResServerINVTDeleteReport
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqServerINVTEditReport
        {
            public string report_type { get; set; }
            public string report_id { get; set; }
        }
        public class ResServerINVTEditReport
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResServerINVTEditReport data { get; set; }
        }
        public class DataResServerINVTEditReport
        {
            public List<ExeResServerINVTEditServerData> server_data { get; set; }
            public List<ExeResServerINVTEditServiceData> service_data { get; set; }
            public List<ExeResServerINVTEditCSIData> csi_data { get; set; }
        }
        public class ExeResServerINVTEditServerData
        {
            public string host_id { get; set; }
            public string host_name { get; set; }
            public string product_desc { get; set; }
            public string site { get; set; }
            public string dr_of { get; set; }
            public string system_type { get; set; }
            public string enviroment { get; set; }
            public string os_plat { get; set; }
            public string os_version { get; set; }
            public string csi { get; set; }
            public string ip_mgt { get; set; }
            public string ip_lan { get; set; }
            public string remark { get; set; }
        }
        public class ExeResServerINVTEditServiceData
        {
            public string service_id { get; set; }
            public string host_id { get; set; }
            public string service_type { get; set; }
            public string service_run { get; set; }
            public string remark { get; set; }
        }
        public class ExeResServerINVTEditCSIData
        {
            public string csi_id { get; set; }
            public string csi { get; set; }
            public string sn { get; set; }
            public string contract_type { get; set; }
            public string product_type { get; set; }
            public string supporter { get; set; }
            public string asr { get; set; }
            public string start_date { get; set; }
            public string expire_date { get; set; }
            public string contact_person { get; set; }
            public string remark { get; set; }
        }
        public class ReqServerINVTUpdateServerReport
        {
            public string host_id { get; set; }
            public string host_name { get; set; }
            public string product_desc { get; set; }
            public string site { get; set; }
            public string dr_of { get; set; }
            public string system_type { get; set; }
            public string enviroment { get; set; }
            public string os_platform { get; set; }
            public string os_version { get; set; }
            public string csi { get; set; }
            public string ip_mgt { get; set; }
            public string ip_lan { get; set; }
            public string remark { get; set; }
        }
        public class ResServerINVTUpdateServerReport
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqServerINVTUpdateServiceReport
        {
            public string service_id { get; set; }
            public string host_id { get; set; }
            public string service_type { get; set; }
            public string service_run { get; set; }
            public string remark { get; set; }
        }
        public class ResServerINVTUpdateServiceReport
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqServerINVTUpdateCSIReport
        {
            public string csi_no { get; set; }
            public string csi_id { get; set; }
            public string sn { get; set; }
            public string contract_type { get; set; }
            public string product_type { get; set; }
            public string supporter { get; set; }
            public string asr { get; set; }
            public string start_date { get; set; }
            public string expire_date { get; set; }
            public string contact_person { get; set; }
            public string remark { get; set; }
        }
        public class ResServerINVTUpdateCSIReport
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqServerINVTUploadCSIDoc
        {
            public string csi_no { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
        }
        public class ResServerINVTUploadCSIDoc
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqServerINVTGetCSIDoc
        {
            public string csi_no { get; set; }
        }
        public class ResServerINVTServerINVTGetCSIDoc
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeServerINVTServerINVTGetCSIDoc> data { get; set; }
        }
        public class ExeServerINVTServerINVTGetCSIDoc
        {
            public string doc_id { get; set; }
            public string doc_name { get; set; }
            public string doc_size { get; set; }
            public string upload_date { get; set; }
            public string uploader { get; set; }
        }
        public class ReqServerINVTGetDoc4Download
        {
            public string report_type { get; set; }
            public string p_id { get; set; }
        }
        public class ResServerINVTGetDoc4Download
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeServerINVTGetDoc4Download> data { get; set; }
        }
        public class ExeServerINVTGetDoc4Download
        {
            public string csi_no { get; set; }
            public string doc_id { get; set; }
            public string doc_name { get; set; }
            public string doc_file { get; set; }
        }
    }
}
