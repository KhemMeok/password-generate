using StandardCrypt;
using CoreFunction;
using System;
namespace ITOAPP_API.Helpers
{
    public class Connection
    {
        public static string ConnectionString(string connect_name)
        {
            Core.DebugInfo("Fetching connection name: " + connect_name);
            string key = Core.GetAppSetting("Key:Standard");
            string connString="";
            try
            {
                
                connString = SCrypt.Decrypt(key,Core.GetContextValue(connect_name));
            }
            catch (Exception ex)
            {
                Core.DebugInfo("Failed fetching connection name " + connect_name);
                Core.DebugError(ex);
            }
            return connString;
        }
    }
}
