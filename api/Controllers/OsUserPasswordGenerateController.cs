using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ITOAPP_API.Helpers;
using static ITOAPP_API.Models.OsUserPasswordGenerateModel;

namespace ITOAPP_API.Controllers
{
    [ApiController]
    public class OsUserPasswordGenerateController : ControllerBase
    {
        [Route("api/OsPasswordChange/v1/GetHostNameOsUserFristLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetHostNameOsUser()
        {
            ResGetHostNameUser RE = await OsUserPasswordGenerateService.OSHostNameUserFristLoad();
            return Ok(RE);
        }
        [Route("api/OsPasswordChange/v1/InsertRecordUserPassword")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertRecordUserPassword(ReqInsertRecordUserPassword param)
        {
            ResInsertRecordUserPassword RE = await OsUserPasswordGenerateService.InsertRecordUserPassword(param);
            return Ok(RE);
        }
        [Route("api/OsPasswordChange/v1/GetUserPasswordRecordTable")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> GetUserPasswordRecordTable(ReqGetDataTableOSPwd param)
        {
            ResGetUserPasswordTable RE = await OsUserPasswordGenerateService.GetDataTable(param);
            return Ok(RE);
        }

        [Route("api/OsPasswordChange/v1/UpdateRecordInCSVFile")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateRecordCSVFile(ReqUpdateRecordUserPassword param)
        {
            ResUpdateRecordUserPassword RE = await OsUserPasswordGenerateService.UpdateRecordUserPassword(param);
            return Ok(RE);
        }
        [Route("api/OsPasswordChange/v1/DeleteRecordInCSVFile")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteRecordCSVFile(ReqUpdateRecordUserPassword param)
        {
            ResUpdateRecordUserPassword RE = await OsUserPasswordGenerateService.DeleteRecordUserPassword(param);
            return Ok(RE);
        }
        [Route("api/OsPasswordChange/v1/GetDataForUpdate")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetdataForUpdate(ReqGetDataForUpdate param)
        {
            ResGetDataForUpdate RE = await OsUserPasswordGenerateService.GetDataForUpdate(param);
            return Ok(RE);
        }
        //ExploreRecord
        [Route("api/OsPasswordChange/v1/ExploreDataToFileOnServer")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ExploreRecord(ReqExploreRecordToCSV param)
        {
            ResExploreDataToServer RE = await OsUserPasswordGenerateService.ExploreRecord(param);
            return Ok(RE);
        }
        [Route("api/OsPasswordChange/v1/GetUserAdmin")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserAdmin()
        {
            ResGetUserAdmin RE = await OsUserPasswordGenerateService.GetAdminUser();
            return Ok(RE);
        }
        [Route("api/OsPasswordChange/v1/GetDataTableExcludeUser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserExclude()
        {
            ResGetUserPasswordTable RE = await OsUserPasswordGenerateService.GetDataTableExcludeUser();
            return Ok(RE);
        }
        [Route("api/OsPasswordChange/v1/GetUserTmpById")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserPwdTmpById(ReqGetPwdTmpById param)
        {
            ResGetPwdDataTmpById RE = await OsUserPasswordGenerateService.GetUserPasswordTmpById(param);
            return Ok(RE);
        }
    }
}
