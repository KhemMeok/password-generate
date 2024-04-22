using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITOAPP_API.Helpers;
using ITOAPP_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITOAPP_API.Controllers
{
   
    [ApiController]
    public class WeblogicUtilityController : ControllerBase
    {
        [Route("api/weblogic/v1/ListBICatalogs")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ListBICatalogs(BIResCatalogList param)
        {
            BIPCatalogList Res = await WeblogicServices.GetBICatalogList(param);
            return Ok(Res);
        }
        [Route("api/weblogic/v1/BackupBICatalogs")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BackupBICatalogs(BIResBackupCatalog param)
        {
            BasicResponse Res = await WeblogicServices.BackupBICatalog(param);
            return Ok(Res);
        }
        [Route("api/weblogic/v1/RestoreBICatalogs")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RestoreBICatalogs(BIResRestoreCatalog param)
        {
            BasicResponse Res = await WeblogicServices.RestoreBICatalog(param);
            return Ok(Res);
        }
        [Route("api/weblogic/v1/GetBackupDetail")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetBackupDetail(BIResBackupDetail param)
        {
            BIBackupDetail Res = await WeblogicServices.GetBackupDetail(param);
            return Ok(Res);
        }
    }
    
}
