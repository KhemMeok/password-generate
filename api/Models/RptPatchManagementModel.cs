using System.Collections.Generic;

namespace ITOAPP_API.Models
{
    public class RptPatchManagementModel
    {
        public class ResDataGetTeamName
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataGetTeamName data { get; set; }
        }
        public class ResGetDataTable
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<AllPatchDataTable> data { get; set; }
        }

        public class ResGetPatchEdit
        {
            public string status { get; set; }
            public string message { get; set; }
            public AllPatchDataTable data { get; set; }
        }
        public class DataForGetPatchEdit
        {
            public string patchId { get; set; }
        }

        public class GetTeamName
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }
        public class DataGetTeamName
        {
            public List<GetTeamName> listgetTeamName { get; set; }

            public List<GetSystemName> listSystemType { get; set; }

            public List<GetHostAndVS> listHostAndVs { get; set; }

            //public List<DataGetTicket> listGetTicket { get; set; }
            public List<DataPatchComponent> listPatchComponent { get; set; }
            public List<DataPatchType> listPatchType { get; set; }
        }
        public class ResGetTicKetFromHDSys
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataGetTicketFromHSSys data { get; set; }
        }
        public class DataGetTicketFromHSSys
        {
            public List<DataGetTicket> listGetTicket { get; set; }
        }
        public class GetSystemName
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }
        public class GetHostAndVS
        {
            public string sysId { get; set; }
            public string sysName { get; set; }
            public string host_name { get; set; }
            public string osVersion { get; set; }
            public string site { get; set; }
            public string hostId { get; set; }
            public string environment { get; set; }
            public string ip { get; set; }

        }
        public class InsertPatch
        {
            public string requestDate { get; set; }
            public string requester { get; set; }
            public string curr_version { get; set; }
            public string upgrade_version { get; set; }
            public string host_id { get; set; }
            public string system_type { get; set; }
            public string priority { get; set; }
            public string uat { get; set; }
            public string impact { get; set; }
            public string objective { get; set; }
            public string descriptions { get; set; }
            public string doc_id { get; set; }
            public string status { get; set; }
            public string applied_by { get; set; }
            public string applied_date { get; set; }
            public string reviewed_by { get; set; }
            public string reviewed_date { get; set; }
            public string approved_by { get; set; }
            public string approved_date { get; set; }
            public string sr_id { get; set; }
            public string patch_type { get; set; }
            public string site { get; set; }
            public string patch_component { get; set; }
            public string criticality { get; set; }
            public string remark { get; set; }
        }
        public class ResInsertPatch
        {
            public string status { get; set; }

            public string message { get; set; }
        }



        public class AllPatchDataTable
        {
            public string patch_mid { get; set; }
            public string requestDate { get; set; }
            public string requester { get; set; }
            public string host_name { get; set; }
            public string system_type { get; set; }
            public string curr_version { get; set; }
            public string patch_type { get; set; }
            public string patch_component { get; set; }
            public string status { get; set; }
            public string upgrade_version { get; set; }
            public string priority { get; set; }
            public string uat { get; set; }
            public string host_id { get; set; }
            public string impact { get; set; }
            public string objective { get; set; }
            public string descriptions { get; set; }
            public string doc_id { get; set; }
            public string applied_by { get; set; }
            public string applied_date { get; set; }
            public string reviewed_by { get; set; }
            public string reviewed_date { get; set; }
            public string approved_by { get; set; }
            public string approved_date { get; set; }
            public string sr_id { get; set; }
            public string site { get; set; }
            public string criticality { get; set; }
            public string remark { get; set; }
            public string env { get; set; }
        }
        public class ResDeletePatch
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class DataDeletePatch
        {
            public string id { get; set; }
        }
        public class ResUpdatePatch
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class ResGetTicket
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<string> data { get; set; }
        }
        public class DataGetTicket
        {
            public string ticketId { get; set; }
            public string request_date { get; set; }
            public string requester { get; set; }
            public string uat { get; set; }
            public string patch_description { get; set; }
            public string service_impact { get; set; }
            public string priority { get; set; }
            public string reviewed_by { get; set; }
            public string reviewed_date { get; set; }
            public string approved_by { get; set; }
            public string approved_date { get; set; }
            public string applied_by { get; set; }
            public string applied_date { get; set; }
            public string category { get; set; }
            public string sub_category { get; set; }
            public string components { get; set; }
            public string criticality { get; set; }
            public string criticality_note { get; set; }
        }
        public class DataPatchComponent
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }
        public class DataPatchType
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        public class ResGetCurrentVersion
        {
            public string status { get; set; }
            public string message { get; set; }
            public string version { get; set; }
        }
        public class ReqGetCurrentVersion
        {
            public string host_id { get; set; }
            public string sys_type { get; set; }
            public string patch_component { get; set; }
        }
    }
}
