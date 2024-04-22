using Microsoft.AspNetCore.Mvc;
using static ITOAPP_API.Models.RptPatchManagementModel;
using System.Threading.Tasks;
using ITOAPP_API.Helpers;

namespace ITOAPP_API.Controllers
{
    [ApiController]
    public class RptPatchManagementController : ControllerBase
    {
        [Route("api/v1/RPT_PATCH_GetISAName")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetName()
        {
            ResDataGetTeamName REDATA = await RptPatchManagementService.DataFristLoad();
            return Ok(REDATA);
        }
        [Route("api/v1/RPT_PATCH_INSERT")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertPatch(InsertPatch param)
        {
            ResInsertPatch REDATA = await RptPatchManagementService.InsertNewPatch(param);
            return Ok(REDATA);
        }
        [Route("api/v1/RPT_PATCH_GetDateTable")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetDataTable()
        {
            ResGetDataTable REDATA = await RptPatchManagementService.GetDataTable();
            return Ok(REDATA);
        }
        [Route("api/v1/RPT_PATCH_DeletePatch")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePatch(DataDeletePatch param)
        {
            ResDeletePatch REDATA = await RptPatchManagementService.DeletePatch(param);
            return Ok(REDATA);
        }
        [Route("api/v1/RPT_PATCH_UpdatePatch")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePatch(AllPatchDataTable param)
        {
            ResUpdatePatch REDATA = await RptPatchManagementService.UpdatePatch(param);
            return Ok(REDATA);
        }
        [Route("api/v1/RPT_PATCH_GetPatchForUpdate")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetPatchForUpdate(DataForGetPatchEdit param)
        {
            ResGetPatchEdit REDATA = await RptPatchManagementService.GetPatchEdit(param);
            return Ok(REDATA);

        }
        [Route("api/v1/RPT_PATCH_GetCurrentVersion")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetCurrentVersion(ReqGetCurrentVersion param)
        {
            ResGetCurrentVersion REDATA = await RptPatchManagementService.GetCurrentVerison(param);
            return Ok(REDATA);
        }
        //DataGetTicketFromHDSys
        [Route("api/v1/RPT_PATCH_GetTicketFromHDSys")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetTicketFromHDSys()
        {
            var res = await RptPatchManagementService.DataGetTicketFromHDSys();
            return Ok(res);
        }
    }
}
