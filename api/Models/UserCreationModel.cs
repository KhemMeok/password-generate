using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Twilio.TwiML.Voice;
using static ITOAPP_API.Models.UserCreationModel;

namespace ITOAPP_API.Models
{
    public class UserCreationModel
    {
        public class DataUserTemplete
        {
            public string templete_Id { get; set; }
            public string project { get; set; }
        }
        public class MainModule
        {
            public string module_id { get; set; }
            public string module_name { get; set; }
            public string project_name { get; set; }
        }
        public class SubModule
        {
            public string main_module_id { get; set; }
            public string sub_module_id { get; set; }
            public string sub_module_name { get; set; }

        }
        public class DataEndPointAction
        {
            public string action_id { get; set; }
            public string action_name { get; set; }
        }
        public class DataUserTmpModuleSub
        {
            public List<DataUserTemplete> user_templete { get; set; }
            public List<MainModule> modules { get; set; }
            public List<SubModule> submodules { get; set; }
            public List<DataEndPointAction> endPointActions { get; set; }
            public List<DataListModuleEndPoint> modeleEndPoint { get; set; }
        }

        public class DataListModuleEndPoint
        {
            public string module_id { get; set; }
            public string action_name { get; set; }
        }


        public class ResDataUserTemplete
        {
            public string status { get; set; }
            public string message { get; set; }
            public DataUserTmpModuleSub data { get; set; }
        }
        public class DataAPIEndPoint
        {

            public string end_point_id { get; set; }
            public string module_id { get; set; }
            public string module_name { get; set; }
            public string sub_module_id { get; set; }
            public string sub_module_name { get; set; }
            public string project { get; set; }
            public string end_point_url { get; set; }
            public string end_point_url_substring { get; set; }
            public string end_point_action_name { get; set; }
            public string create_date { get; set; }

            public string action_id { get; set; }
        }
        public class dataEndPoint
        {
            public List<DataAPIEndPoint> dataAPIEndPoints { get; set; }
        }
        public class ResDataAPIEndPoint
        {
            public string status { get; set; }
            public string message { get; set; }
            public dataEndPoint data { get; set; }

        }



        //add new user 
        public class ResAllowUserAccessApi
        {
            public string status { get; set; }
            public string message { get; set; }

        }
        public class ReqAllowUserAccessApi
        {

            public string new_user_id { get; set; }
            public string fullname { get; set; }
            public string templete_id { get; set; }
            public string access_api { get; set; }
            public string email { get; set; }
            public string pwd { get; set; }
            public string request_date { get; set; }
            public string end_point_id { get; set; }
            public string enable_ldap { get; set; }

            public string type { get; set; }

        }

        // add new end point 
        public class ResAddNewEndPoint
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqAddNewEndPoint
        {
            public string project { get; set; }
            public string end_point_url { get; set; }
            public string required_encrypt { get; set; }
            public string module { get; set; }
            public string sub_module { get; set; }

            public string action_id { get; set; }

        }
        //GET END POINT FOR EDIT
        public class ResGetEndPointForUpdate
        {
            public string status { get; set; }
            public string message { get; set; }

            public DataGetEndPointForUpdate data { get; set; }
        }
        public class ReqGetEndPointForUpdate
        {
            public string end_point_id { get; set; }

        }
        public class DataGetEndPointForUpdate
        {
            public string end_point_id { get; set; }
            public string project_id { get; set; }
            public string module_id { get; set; }
            public string module_name { get; set; }
            public string sub_module_id { get; set; }
            public string sub_module_name { get; set; }
            public string end_point_url { get; set; }
            public string required_encrypt { get; set; }
            public string action_id { get; set; }

        }


        //update end point
        public class ResUpdateEndPoint
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqUpdateEndPoint
        {
            public string end_point_id { get; set; }
            public string project { get; set; }
            public string end_point_url { get; set; }
            public string required_encrypt { get; set; }
            public string module { get; set; }
            public string sub_module { get; set; }
            public string action_id { get; set; }


        }
        //delete end point 
        public class ResDeleteEndPoint
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqDeleteEndPoint
        {
            public string end_point_id { get; set; }


        }

        // user for update
        public class ResUserForUpdate
        {
            public string status { get; set; }
            public string message { get; set; }

            public DataUserForUpdate data { get; set; }
            public string action_id_and_module { get; set; }
        }
        public class ReqUserForUpdate
        {
            public string user_id { get; set; }
        }
        public class DataUserForUpdate
        {
            public string p_access_api { get; set; }
            public string p_usre_project { get; set; }
            public string p_allow_ldap { get; set; }
            public List<DataUserEndPoint> p_end_point_id { get; set; }

        }
        public class DataUserEndPoint
        {
            public string end_point_id { get; set; }
            public string end_point_url { get; set; }
            public string action_id { get; set; }
            public string sub_module_id { get; set; }

        }


        // filter end point 

        public class ResFilterEndPoint
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<DataFilterEndPoint> data { get; set; }
        }

        public class ReqFilterEndPoint
        {
            public string p_module { get; set; }
            public string p_sub_module { get; set; }
            public string p_start_date { get; set; }
            public string p_end_date { get; set; }

        }

        public class DataFilterEndPoint
        {
            public string end_point_id { get; set; }
            public string module_name { get; set; }
            public string sub_module_name { get; set; }
            public string project { get; set; }
            public string end_point_url { get; set; }
            public string action_id { get; set; }
            public string create_date { get; set; }
        }

        public class ResDeleteUser
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class ReqDeleteUser
        {
            public string user_id { get; set; }
        }

        public class ResGetEndPointSelect
        {
            public string status { get; set; }
            public string message { get; set; }
            public ListOfEndPointSelect data { get; set; }
        }
        public class ListOfEndPointSelect
        {
            public List<DataGetEndPointSelect> listEndPoint { get; set; }
        }
        //public class ReqGetEndPointSelect
        //{
        //    public string sub_module_id { get; set; }
        //}
        public class DataGetEndPointSelect
        {
            public string end_point_id { get; set; }
            public string sub_module_id { get; set; }
        }

    }
}