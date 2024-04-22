using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOAPP_API.Models
{
    public class BIResCatalogList
    {
        public string site { get; set; }
    }
    public class BIPCatalogList
    {
        public string status { get; set; }
        public string message { get; set; }
        public BIData data { get; set; }

    }
    public class BIData
    {
        public string report_date { get; set; }
        public string backup_source { get; set; }
        public string restore_destination { get; set; }
        public List<BIPCatalogData> to_backup_catalog{ get; set; }
    }
    public class BIPCatalogData
    {
        public string catalog_name { get; set; }
        public string last_modified { get; set; }
        public string last_modifier { get; set; }
        public string owner { get; set; }
    }
    public class BIResBackupCatalog
    {
        public string catalog_name { get; set; }
    }
    public class BIResRestoreCatalog
    {
        public string catalog_name { get; set; }
        public string report_date { get; set; }
    }
    public class BIResBackupDetail
    {
        public string report_date { get; set; }
    }
    public class BIBackupDetail
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<BIDataBackup> data { get; set; }
    }
    public class BIDataBackup
    {
        
        public string catalog_name { get; set; }
        public string backup_server { get; set; }
        public string restore_server { get; set; }
        public string saved_location { get; set; }
        public string backup_stat { get; set; }
        public string restored_stat { get; set; }
        public string backup_by { get; set; }
        public string backup_date { get; set; }
        public string restored_by { get; set; }
        public string restored_date { get; set; }
    }
}
