using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace ITOAPP_API.Models
{
    public class APIManagementModel
    {

        public class ResAPIMonLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResServiceType data { get; set; }
        }
        public class DataResServiceType
        {
            public List<ExeResAPIMServiceType> all_service_type { get; set; }
        }

        public class ExeResAPIMServiceType
        {
            public string service_name { get; set; }
            public string service_desc { get; set; }
        }
        public class ResAPITonLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataResTraServiceType data { get; set; }
        }
        public class DataResTraServiceType
        {
            public List<ExeResAPITServiceType> all_transaction_service_type { get; set; }
        }

        public class ExeResAPITServiceType
        {
            public string service_name { get; set; }
            public string service_desc { get; set; }
        }

        public class ReqAPIParameter
        {
            public string parameter_id { get; set; }
        }
        public class ReqAPIParamCheck
        {
            public string param_name { get; set; }
        }
        public class ResAPIPQuery
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPType> data { get; set; }
        }
        public class ExeResAPIPType
        {
            public string param_name { get; set; }
            public string param_value { get; set; }
            public string system { get; set; }
        }
        public class ReqAPIParam
        {
            public string type { get; set; }
            public string parameter { get; set; }
            public string param_name { get; set; }
            public string param_value { get; set; }
            public string system { get; set; }
        }
        public class ResAPIPUpdate
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ResAPIPGetEndpoint
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPGetEndpoint> data { get; set; }
        }
        public class ExeResAPIPGetEndpoint
        {
            public string endpoint_id { get; set; }
            public string description { get; set; }
            public string endpoint { get; set; }
            public string status { get; set; }
            public string endpoint_type { get; set; }
            public string key_name { get; set; }
            public string key_value { get; set; }
            public string method { get; set; }
            public string content_type { get; set; }
            public string dc_or_dr { get; set; }
            public string current_service { get; set; }
            public string status_code { get; set; }

        }
        public class ReqAPIPRegisterEndpoint
        {
            public string endpoint_id { get; set; }
            public string endpoint_description { get; set; }
            public string endpoint { get; set; }
            public string endpoint_status { get; set; }
            public string endpoint_type { get; set; }
            public string message_body { get; set; }
            public string key_name { get; set; }
            public string key_value { get; set; }
            public string method { get; set; }
            public string content_type { get; set; }
            public string service_type { get; set; }
        }
        public class ResAPIPRegisterEndpoint
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPIPEditEndpointt
        {
            public string endpoint_type { get; set; }
            public string endpoint_ids { get; set; }
        }
        public class ResAPIPEditEndpoint
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPUpdateEndpoint> data { get; set; }
        }
        public class ReqAPIPUpdateEndpoint
        {
            public string endpoint_id { get; set; }
            public string endpoint_description { get; set; }
            public string endpoint { get; set; }
            public string endpoint_status { get; set; }
            public string endpoint_type { get; set; }
            public string message_body { get; set; }
            public string key_name { get; set; }
            public string key_value { get; set; }
            public string method { get; set; }
            public string content_type { get; set; }
            public string service_type { get; set; }
        }
        public class ResAPIPUpdateEndpoint
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ExeResAPIPUpdateEndpoint
        {
            public string endpoint_id { get; set; }
            public string endpoint_description { get; set; }
            public string endpoint { get; set; }
            public string endpoint_status { get; set; }
            public string endpoint_type { get; set; }
            public string message_body { get; set; }
            public string key_name { get; set; }
            public string key_value { get; set; }
            public string method { get; set; }
            public string content_type { get; set; }
            public string service_type { get; set; }
        }
        public class ReqAPIPDeleteEndpoint
        {
            public string endpoint_type { get; set; }
            public string endpoint_id { get; set; }
        }
        public class ResAPIPDeleteEndpoint
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ResAPIPGetClient
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPGetClient> data { get; set; }
        }
        public class ExeResAPIPGetClient
        {
            public string appid_client { get; set; }
            public string client_id { get; set; }
            public string client_secert { get; set; }
            public string client_name { get; set; }
            public string client_des { get; set; }
            public string grent_type { get; set; }
            public string client_status { get; set; }
            public string create_date { get; set; }
            public string update_date { get; set; }

        }
        public class ReqAPIPRegisterClient
        {
            public string appid_client { get; set; }
            public string client_name { get; set; }
            public string client_des { get; set; }
            public string grent_type { get; set; }
        }
        public class ResAPIPRegisterClient
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPIPDeleteClient
        {
            public string client_type { get; set; }
            public string client_id { get; set; }
        }
        public class ResAPIPDeleteClient
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPIPEditClient
        {
            public string client_type { get; set; }
            public string client_id { get; set; }
        }
        public class ResAPIPEditClient
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPUpdateclient> data { get; set; }
        }
        public class ExeResAPIPUpdateclient
        {
            public string appid_client { get; set; }
            public string client_id { get; set; }
            public string client_secert { get; set; }
            public string client_name { get; set; }
            public string client_des { get; set; }
            public string grent_type { get; set; }
            public string client_status { get; set; }
        }
        public class ReqAPIPUpdateClient
        {
            public string p_client_id { get; set; }
            public string p_client_secert { get; set; }
            public string appid_client { get; set; }
            public string client_id { get; set; }
            public string client_secert { get; set; }
            public string client_name { get; set; }
            public string client_des { get; set; }
            public string grent_type { get; set; }
            public string client_status { get; set; }
        }
        public class ResAPIPUpdateClient
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ResAPIPGetSinature
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPGetSinature> data { get; set; }
        }
        public class ExeResAPIPGetSinature
        {
            public string sinature_id { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string modifired_by { get; set; }
            public string modifired_date { get; set; }
            public string sinature_status { get; set; }
            public string sinature_keyid { get; set; }
            public string sinature_algorithm { get; set; }
            public string sinature_headers { get; set; }
            public string sinature_secretkey { get; set; }
            public string sinature_maxagerequest { get; set; }

        }
        public class ReqAPIPRegisterSinature
        {
            public string sinature_keyid { get; set; }
            public string sinature_algorithm { get; set; }
            public string sinature_headers { get; set; }
            public string sinature_max { get; set; }
        }
        public class ResAPIPRegisterSinature
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPIPDeleteSinature
        {
            public string sinature_type { get; set; }
            public string sinature_id { get; set; }
        }
        public class ResAPIPDeleteSinature
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPIPEditSinature
        {
            public string sinature_type { get; set; }
            public string sinature_id { get; set; }
        }
        public class ResAPIPEditSinature
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPEditSinature> data { get; set; }
        }
        public class ExeResAPIPEditSinature
        {
            public string sinature_id { get; set; }
            public string sinature_status { get; set; }
            public string sinature_keyid { get; set; }
            public string sinature_algorithm { get; set; }
            public string sinature_headers { get; set; }
            public string sinature_secretkey { get; set; }
            public string sinature_max { get; set; }
        }
        public class ReqAPIPUpdateSinature
        {
            public string sinature_id { get; set; }
            public string sinature_status { get; set; }
            public string sinature_keyid { get; set; }
            public string sinature_algorithm { get; set; }
            public string sinature_headers { get; set; }
            public string sinature_secretkey { get; set; }
            public string sinature_max { get; set; }
        }
        public class ResAPIPUpdateSinature
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ResAPIPGetEndpointuser
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPGetEndpointuser> data { get; set; }
        }
        public class ExeResAPIPGetEndpointuser
        {
            public string api_id { get; set; }
            public string endpoint_id { get; set; }
            public string endpoint { get; set; }
            public string method { get; set; }
            public string record_status { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string modifired_by { get; set; }
            public string modifired_date { get; set; }
            public string enabled { get; set; }
            public string debug { get; set; }
            public string debug_name { get; set; }
            public string authorize { get; set; }
            public string validatetrn_id { get; set; }
            public string validate_createtime { get; set; }
            public string validate_agerequest { get; set; }
            public string validate_digest { get; set; }
            public string validate_sinature { get; set; }
            public string sourcesystem_check { get; set; }
            public string allowanonymous { get; set; }
            public string multipart_data { get; set; }
            public string auth_type { get; set; }
            public string api_key { get; set; }

        }
        public class ReqAPIPRegisterEndpointuser
        {
            public string api_id { get; set; }
            public string endpoint { get; set; }
            public string method { get; set; }
            public string enabled { get; set; }
            public string debug { get; set; }
            public string debug_name { get; set; }
            public string authorize { get; set; }
            public string validatetrn_id { get; set; }
            public string validate_createtime { get; set; }
            public string validate_agerequest { get; set; }
            public string validate_digest { get; set; }
            public string validate_sinature { get; set; }
            public string sourcesystem_check { get; set; }
            public string allowanonymous { get; set; }
            public string multipart_data { get; set; }
            public string auth_type { get; set; }
            public string api_key { get; set; }
        }
        public class ResAPIPRegisterEndpointuser
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPIPDeleteEndpointuser
        {
            public string endpoint_type { get; set; }
            public string endpoint_id { get; set; }
        }
        public class ResAPIPDeleteEndpointuser
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPIPEditEndpointuser
        {
            public string endpoint_type { get; set; }
            public string endpoint_id { get; set; }
        }
        public class ResAPIPEditEndpointuser
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPEditEndpointuser> data { get; set; }
        }
        public class ExeResAPIPEditEndpointuser
        {
            public string api_id { get; set; }
            public string endpoint_id { get; set; }
            public string endpoint { get; set; }
            public string method { get; set; }
            public string record_status { get; set; }
            public string enabled { get; set; }
            public string debug { get; set; }
            public string debug_name { get; set; }
            public string authorize { get; set; }
            public string validatetrn_id { get; set; }
            public string validate_createtime { get; set; }
            public string validate_agerequest { get; set; }
            public string validate_digest { get; set; }
            public string validate_sinature { get; set; }
            public string sourcesystem_check { get; set; }
            public string allowanonymous { get; set; }
            public string multipart_data { get; set; }
            public string auth_type { get; set; }
            public string api_key { get; set; }
        }
        public class ReqAPIPUpdateEndpointuser
        {
            public string api_id { get; set; }
            public string endpoint_id { get; set; }
            public string endpoint { get; set; }
            public string method { get; set; }
            public string record_status { get; set; }
            public string enabled { get; set; }
            public string debug { get; set; }
            public string debug_name { get; set; }
            public string authorize { get; set; }
            public string validatetrn_id { get; set; }
            public string validate_createtime { get; set; }
            public string validate_agerequest { get; set; }
            public string validate_digest { get; set; }
            public string validate_sinature { get; set; }
            public string sourcesystem_check { get; set; }
            public string allowanonymous { get; set; }
            public string multipart_data { get; set; }
            public string auth_type { get; set; }
            public string api_key { get; set; }
        }
        public class ResAPIPUpdateEndpointuser
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ResAPIPGetClientEndpoint
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPGetClientEndpoint> data { get; set; }
        }
        public class ExeResAPIPGetClientEndpoint
        {
            public string app_id { get; set; }
            public string client_id { get; set; }
            public string endpoint_id { get; set; }
            public string record_status { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string modifired_by { get; set; }
            public string modifired_date { get; set; }
            public string action_type { get; set; }

        }
        public class ReqAPIPGetClientSinature
        {
            public string client_sinature_appid { get; set; }
            public string client_sinature_id { get; set; }
        }
        public class ResAPIPGetClientSinature
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPGetClientSinature> data { get; set; }
        }
        public class ExeResAPIPGetClientSinature
        {
            public string app_id { get; set; }
            public string client_id { get; set; }
            public string sig_id { get; set; }
            public string record_status { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string modifired_by { get; set; }
            public string modifired_date { get; set; }
            public string action_type { get; set; }

        }
        public class ResAPIPGetMessage
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPGetMessage> data { get; set; }
        }
        public class ExeResAPIPGetMessage
        {
            public string message_id { get; set; }
            public string appid_mes { get; set; }
            public string message_code { get; set; }
            public string message_type { get; set; }
            public string message_language { get; set; }
            public string message_description { get; set; }
            public string record_status { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string modifired_by { get; set; }
            public string modifired_date { get; set; }

        }
        public class ReqAPIPGetMessageCode
        {
            public string appid_mes { get; set; }
            public string message_type { get; set; }
        }
        public class ResAPIPGetMessageCode
        {
            public string status { get; set; }
            public string message { get; set; }
            public string message_code { get; set; }
        }
        public class ReqAPIPRegisterMessage
        {
            public string appid_mes { get; set; }
            public string message_type { get; set; }
            public string message_eng { get; set; }
            public string message_description_eng { get; set; }
            public string message_khr { get; set; }
            public string message_description_khr { get; set; }
            public string message_thb { get; set; }
            public string message_description_thb { get; set; }
        }
        public class ResAPIPRegisterMessage
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class ReqAPIPEditMessage
        {
            public string message_type { get; set; }
            public string message_ids { get; set; }
        }
        public class ResAPIPEditMessage
        {
            public string status { get; set; }
            public string message { get; set; }
            public string message_id { get; set; }
            public string appid_mes { get; set; }
            public string message_code { get; set; }
            public string message_type { get; set; }
            public DataResMessage data { get; set; }

        }
        public class DataResMessage
        {
            public List<ExeResAPIPEditMessageENG> message_eng { get; set; }
            public List<ExeResAPIPEditMessageKHR> message_khr { get; set; }
            public List<ExeResAPIPEditMessageTHB> message_thb { get; set; }
        }
        public class ExeResAPIPEditMessageENG
        {
            public string message_language_eng { get; set; }
            public string message_description_eng { get; set; }
            public string record_status_eng { get; set; }
        }
        public class ExeResAPIPEditMessageKHR
        {
            public string message_language_khr { get; set; }
            public string message_description_khr { get; set; }
            public string record_status_khr { get; set; }
        }
        public class ExeResAPIPEditMessageTHB
        {
            public string message_language_thb { get; set; }
            public string message_description_thb { get; set; }
            public string record_status_thb { get; set; }
        }
        public class ReqAPIPUpdateMessage
        {
            public string message_id { get; set; }
            public string appid_mes { get; set; }
            public string message_code { get; set; }
            public string message_type { get; set; }
            public string message_eng { get; set; }
            public string message_description_eng { get; set; }
            public string message_status_eng { get; set; }
            public string message_khr { get; set; }
            public string message_description_khr { get; set; }
            public string message_status_khr { get; set; }
            public string message_thb { get; set; }
            public string message_description_thb { get; set; }
            public string message_status_thb { get; set; }
        }
        public class ResAPIPUpdateMessage
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ResAPIUGetValue
        {
            public string status { get; set; }
            public string message { get; set; }
            public string client_value { get; set; }
            public string endpoint_value { get; set; }
            public string sinature_value { get; set; }
            public string messages_value { get; set; }
            public List<ExeResAPIPGetAppID> data { get; set; }
        }
        public class ExeResAPIPGetAppID
        {
            public string app_id { get; set; }


        }
        public class ReqAPIPClientEndpointMap
        {
            public string client_endpoint_type { get; set; }
            public string client_endpoint_id { get; set; }
        }
        public class ResAPIPClientEndpointMap
        {
            public string status { get; set; }
            public string message { get; set; }
            public string client_endpoint_appid { get; set; }
            public string client_endpoint_id { get; set; }
            public List<ExeResAPIPClientEndpointMap> data { get; set; }
        }
        public class ExeResAPIPClientEndpointMap
        {
            public string endpoint_id { get; set; }
            public string endpointuser_id { get; set; }
            public string endpointuser_desc { get; set; }

        }

        public class ReqAPIPClientEndpointMapCheck
        {
            public string client_endpoint_appid { get; set; }
            public string client_endpoint_id { get; set; }
        }
        public class ResAPIPClientEndpointMapCheck
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPClientEndpointMapCheck> data { get; set; }
        }
        public class ExeResAPIPClientEndpointMapCheck
        {
            public string endpoint_id { get; set; }
            public string endpointuser_id { get; set; }
            public string endpointuser_desc { get; set; }

        }
        public class ReqAPIPRegisterClientEndpoint
        {
            public string client_endpoint_appid { get; set; }
            public string client_endpoint_id { get; set; }
            public string endpointuser_id { get; set; }
        }
        public class ResAPIPRegisterClientEndpoint
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class ReqAPIPClientSinatureMap
        {
            public string client_sinature_type { get; set; }
            public string client_sinature_id { get; set; }
        }
        public class ResAPIPClientSinatureMap
        {
            public string status { get; set; }
            public string message { get; set; }
            public string client_sinature_id { get; set; }
            public string client_sinature_appid { get; set; }
            public List<ExeResAPIPClientSinatureMap> data { get; set; }
        }
        public class ExeResAPIPClientSinatureMap
        {
            public string client_sinatureuser_id { get; set; }
            public string client_sinatureuser_des { get; set; }
        }
        public class ReqAPIPClientSinatureMapCheck
        {
            public string client_sinature_appid { get; set; }
            public string client_sinature_id { get; set; }
        }
        public class ResAPIPClientSinatureMapCheck
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPClientSinatureMapCheck> data { get; set; }
        }
        public class ExeResAPIPClientSinatureMapCheck
        {
            public string client_sinatureuser_id { get; set; }
            public string client_sinatureuser_appid { get; set; }
            public string client_sinatureuser_des { get; set; }
        }
        public class ReqAPIPRegisterClientSinature
        {
            public string client_sinature_appid { get; set; }
            public string client_sinature_id { get; set; }
            public string sinature_id { get; set; }
        }
        public class ResAPIPRegisterClientSinature
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class ReqAPIPEditClientSinature
        {
            public string client_sinature_type { get; set; }
            public string client_sinature_id { get; set; }
        }
        public class ResAPIPEditClientSinature
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPEditClientSinature> data { get; set; }
        }
        public class ExeResAPIPEditClientSinature
        {
            public string app_id { get; set; }
            public string client_id { get; set; }
            public string sinature_id { get; set; }
            public string sinature_status { get; set; }
        }
        public class ReqAPIPUpdateClientSinature
        {
            public string signature_pra { get; set; }
            public string app_id { get; set; }
            public string client_id { get; set; }
            public string sinature_id { get; set; }
            public string sinature_status { get; set; }
        }
        public class ResAPIPUpdateClientSinature
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAPITransaction
        {
            public string transaction_type { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
        }
        public class ResAPITQuery
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPITransaction> data { get; set; }
        }
        public class ExeResAPITransaction
        {
            public string reference_no { get; set; }
            public string reference_no1 { get; set; }
            public string date { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public string customer { get; set; }
            public string amount { get; set; }
            public string currency { get; set; }
        }
        public class ReqAPITransactionDetail
        {
            public string service_type { get; set; }
            public string ref_no { get; set; }
        }

        public class ResAPITDetail
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPITransactionDetail> data { get; set; }
        }
        public class ExeResAPITransactionDetail
        {
            public string record_no { get; set; }
            public string reference_no { get; set; }
            public string action_type { get; set; }
            public string action_date { get; set; }
            public string request_data { get; set; }
            public string response_data { get; set; }
            public string header_data { get; set; }
        }

        public class ReqAPITGetRequestdata
        {
            public string service_type { get; set; }
            public string record_no { get; set; }
            public string ref_no { get; set; }
        }

        public class ResAPITGetRequestData
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPITGetRequestData> data { get; set; }
        }
        public class ExeResAPITGetRequestData
        {
            public string data1 { get; set; }
        }
        public class ReqAPITGetResponsetdata
        {
            public string service_type { get; set; }
            public string record_no { get; set; }
            public string ref_no { get; set; }
        }

        public class ResAPITGetResponsetData
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPITGetResponseData> data { get; set; }
        }
        public class ExeResAPITGetResponseData
        {
            public string data2 { get; set; }
        }
        public class ReqAPITGetHeaderdata
        {
            public string service_type { get; set; }
            public string record_no { get; set; }
            public string ref_no { get; set; }
        }

        public class ResAPITGetHeaderData
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPITGetHeaderData> data { get; set; }
        }
        public class ExeResAPITGetHeaderData
        {
            public string data3 { get; set; }
        }
        public class ResAPIConLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public ResAPIConnectionType data { get; set; }
        }   
        public class ResAPIConnectionType {
            public List<ExeResAPIConnectionTypeAll> all_service { get; set; }
            public List<ExeResAPIConnectionType> service { get; set; }
        }
       
        public class ExeResAPIConnectionTypeAll
        {
            public string endpoint_ids { get; set; }
            public string descriptions { get; set; }
        }
        public class ExeResAPIConnectionType
        {
            public string endpoint_id { get; set; }
            public string description { get; set; }
        }
        
        public class ReqAPICConnection
        {
            public string endpoint_id { get; set; }
        }
        public class ResAPICCheck
        {
            public string status { get; set; }
            public string message { get; set; }
            public GetAPICCheck data { get; set; }
        }
        public class GetAPICCheck
        {
            public string status_con { get; set; }
            public string statuscode_con { get; set; }
            public string data_responce { get; set; }
        }
        public class ResAPICDowntime
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPICDowntime> data { get; set; }
        }
        public class ExeResAPICDowntime
        {
            public string service_name { get; set; }
            public string location { get; set; }
            public string down_date { get; set; }
            public string up_date { get; set; }
            public string downtime { get; set; }
            public string endpoint { get; set; } 
        }
        public class ResAPICGetAllService
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPICGetAllService> data { get; set; }
        }
        public class ExeResAPICGetAllService
        {
            public string service_name { get; set; }
            public string endpoint_type { get; set; }
            public string location { get; set; }
            public string offline_date { get; set; }
            public string online_date { get; set; }
            public string status { get; set; }
            public string status_code { get; set; }
        }
        
        public class ReqAPICDownTime
        {
            public string connection_type { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
        }
        public class ResAPICDTQuery
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPICDTQuery> data { get; set; }
        }
        public class ExeResAPICDTQuery
        {
            public string service_name { get; set; }
            public string location { get; set; }
            public string down_time { get; set; }
            public string up_time { get; set; }
            public string total_downTime { get; set; }
        }
        public class ReqAPICChartDownTime
        {
            public string[] connection_type { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
        }
        public class AllData
        {
            public string allservice { get; set; }
            public string alldate { get; set; }
        }
        public class ResAPICChartDownService
        {
            public string status { get; set; }
            public string message { get; set; }
            public string format { get; set; }
            public string from_date { get; set; }
            public string to_date { get; set; }
            public List<ExeAPICChartDownService> data { get; set; }
        }
        public class ExeAPICChartDownService
        {
            public string service_id { get; set; }
            public string service_name { get; set; }
            public List<ExeAPICCurrentData> data_detail { get; set; }
            
        }
        public class ExeAPICCurrentData
        {

            public string current_date { get; set; }
            public string total_Time { get; set; }
        }

        public class ReqAPICChartDownTimeDay
        {
            public string service_type { get; set; }
            public string date { get; set; }
        }
        public class ResAPICChartDownTimeService
        {
            public string status { get; set; }
            public string message { get; set; }
            public string service_type { get; set; }
            public string date { get; set; }
            public string format { get; set; }
            public List<ExeAPICChartDownTimeService> data { get; set; }
        }
        public class ExeAPICChartDownTimeService
        {
            public string rang_time { get; set; }
            public string total_time { get; set; }

        }
        public class ResAPIUEndpointonLoad
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIUEndpointonLoad> data { get; set; }

        }
        public class ExeResAPIUEndpointonLoad
        {
            public string endpointuser_id { get; set; }
            public string endpointuser_desc { get; set; }
        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AccountInfo
        {
            public string accountNo { get; set; }
            public string accountCurrency { get; set; }
        }

        public class Body
        {
            public List<CutomerInfo> cutomerInfo { get; set; }
            public string externalRef { get; set; }
            public string externalSystem { get; set; }
        }

        public class CutomerInfo
        {
            public string customerNo { get; set; }
            public PhoneInfo phoneInfo { get; set; }
            public string nationalID { get; set; }
            public AccountInfo accountInfo { get; set; }
        }

        public class PhoneInfo
        {
            public string countryCode { get; set; }
            public string phoneNumber { get; set; }
        }

        public class ReqAPITool
        {
            public string[]? name { get; set; }
            public string[]? value { get; set; }
            public string type_headersig { get; set; }
            public string? endpoint { get; set; }
            public string? secretKey { get; set; }
            public string? algorithm { get; set; }
            public string? header { get; set; }
            public string? keyid { get; set; }
            public string method { get; set; }
            public string url_req { get; set; }
            public dynamic body { get; set; }

        }
        public class ResAPITool
        {
            public string transaction_id { get; set; }
            public int created { get; set; }
            public string digest { get; set; }
            public string signature { get; set; }
            public string data { get; set; }

        }

        //public class CutomerInfo
        //{
        //    public string CustomerNo { get; set; }
        //    public string RiskLeval { get; set; }
        //    public string Remark { get; set; }
        //}

        //public class Body
        //{
        //    public List<CutomerInfo> CutomerInfo { get; set; }
        //    public string externalRef { get; set; }
        //    public string externalSystem { get; set; }
        //}
        public class ReqAPIToolTest
        {
            public string url { get; set; }
            public string token { get; set; }
            public Body body { get; set; }
        }
        public class ResAPIPUserServiceGet
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPUserServiceGet> data { get; set; }
        }
        public class ExeResAPIPUserServiceGet
        {
            public string service_id { get; set; }
            public string service_name { get; set; }
            public string user_id { get; set; }
            public string user_name { get; set; }
            public string record_status { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string modifired_by { get; set; }
            public string modifired_date { get; set; }

        }
        public class ReqAPIPUserServiceMapCheck
        {
            public string user_view_service { get; set; }
        }
        public class ResAPIPUserServiceMapCheck
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPUserServiceMapCheck> data { get; set; }
        }
        public class ExeResAPIPUserServiceMapCheck
        {
            public string user_service_id { get; set; }
            public string user_service_mapid { get; set; }
            public string user_service_mapdesc { get; set; }

        }
        public class ReqAPIPRegisterUserService
        {
            public string user_view_service { get; set; }
            public string service_id { get; set; }
        }
        public class ResAPIPRegisterUserService
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class ResAPIPUserTxnCheck
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<ExeResAPIPUserTxnCheck> data { get; set; }
        }
        public class ExeResAPIPUserTxnCheck
        {
            public string user_txn_id { get; set; }
            public string user_txn_desc { get; set; }

        }

    }
}
