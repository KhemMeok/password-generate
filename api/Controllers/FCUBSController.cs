using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITOAPP_API.Helpers;
using ITOAPP_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static ITOAPP_API.Models.FCUBSModel;

namespace ITOAPP_API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class FCUBSController : ControllerBase
    {
        [Route("api/fcubs/v1/CurrentRealDebugStat")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CurrentRealDebugStat(ReqRealDebugState param)
        {
            ResRealDebugUpdate Res = new ResRealDebugUpdate();
            Res = await FCUBSServices.CurrentRealDebugStat(param);
            return Ok(Res);
        }
        [Route("api/fcubs/v1/UpdateRealDebug")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateRealDebug(ReqUpdateRealDebug param)
        {
            ResRealDebugUpdate Res = new ResRealDebugUpdate();
            Res = await FCUBSServices.UpdateRealDebug(param);
            return Ok(Res);
        }
        [Route("api/fcubs/v1/UpdateUserDebug")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUserDebug(ReqUpdateUserDebug param)
        {
            BasicResponse Res = new BasicResponse();
            Res = await FCUBSServices.UpdateUserDebug(param);
            return Ok(Res);
        }
        [Route("api/fcubs/v1/GetUpdateDebugLogs")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUpdateDebugLogs(ReqUpdateDebuglogsdate param)
        {
            ResUpdateDebuglogs Res = new ResUpdateDebuglogs();
            ResUpdateDebuglogs RE = await FCUBSServices.GetUpdateDebugLogs(param);
            return Ok(RE);
        }
        [Route("api/fcubs/v1/CurrentUserDebugStat")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CurrentUserDebugStat(ReqUserDebugStat param)
        {
            ResUserDebugStat Res = new ResUserDebugStat();
            Res = await FCUBSServices.CurrentUserDebugStat(param);
            return Ok(Res);
        }
        [Route("api/fcubs/v1/FcubParamFirstLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FcubParamFirstLoad()
        {
            ResFcubParamfirstLoad Res = new ResFcubParamfirstLoad();
            ResFcubParamfirstLoad RE = await FCUBSServices.FcubParamFirstLoad();
            return Ok(RE);
        }
        [Route("api/fcubs/v1/HandoffFailedEntriesFirstLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HandoffFailedEntriesFirstLoad()
        {
            ResHandoffFailedFirstLoad RE = await FCUBSServices.ResHandoffFailedFirstLoad();
            return Ok(RE);
        }
        [Route("api/fcubs/v1/HandoffFailedEntriesListing")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HandoffFailedEntriesListing(ReqHandoffFailedEntries param)
        {
            ResHandoffFailedEntries RE = await FCUBSServices.HandoffFailedEntriesListing(param);
            return Ok(RE);
        }
        [Route("api/fcubs/v1/InsertRequestFixHandoff")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertRequestFixHandoff(ReqFixHandoffFailedEntries param)
        {
            BasicResponse RE = await FCUBSServices.InsertRequestFixHandoff(param);
            return Ok(RE);
        }
        [Route("api/fcubs/v1/InsertRequestRejectHandoff")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertRequestRejectHandoff(ReqRejectHandoffFailedEntries param)
        {
            BasicResponse RE = await FCUBSServices.RejectRequestFixHandoff(param);
            return Ok(RE);
        }
        [Route("api/fcubs/v1/FixHandoffFailedEntries")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FixHandoffFailedEntries(ReqFixHandoffFailedEntries param)
        {
            BasicResponse RE = await FCUBSServices.FixHandoffFailedEntries(param);
            return Ok(RE);
        }
        [Route("api/fcubs/v1/HandoffLogListing")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HandoffLogListing(ReqLogDateHandoff param)
        {
            ResLogHandoffLising RE = await FCUBSServices.HandoffLogListing(param);
            return Ok(RE);
        }
        [Route("api/fcubs/v1/Get_Fcub_Error_SMS")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Get_Fcub_Error_SMS(ReqError_Code param)
        {
            ResErrorSMS RE = await FCUBSServices.Get_Fcub_Error_SMS(param);
            return Ok(RE);
        }
        [Route("api/fcubs/v1/Calculator_EoC_Branch_Group")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Calculator_EoC_Branch_Group(ReqCalculator_EoC_Branch_Group param)
        {
            ResEoC_Branch_Group REBG = await FCUBSServices.Calculator_EoC_Branch_Group(param);
            return Ok(REBG);
        }
        [Route("api/fcubs/v1/Calculator_EoC_Branch_Group_Push")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Calculator_EoC_Branch_Group_Push()
        {
            ResCalculator_EoC_Branch_Group_Push RCEBGP = await FCUBSServices.Calculator_EoC_Branch_Group_Push();
            return Ok(RCEBGP);
        }
        [Route("api/fcubs/v1/Calculator_EoC_Branch_Group_Generate")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Calculator_EoC_Branch_Group_Generate()
        {
            ResCalculator_EoC_Branch_Group_Generate RCEBGG = await FCUBSServices.Calculator_EoC_Branch_Group_Generate();
            return Ok(RCEBGG);
        }
        [Route("api/fcubs/v1/Calculator_EoC_Branch_Group_Reject")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Calculator_EoC_Branch_Group_Reject()
        {
            ResCalculator_EoC_Branch_Group_Reject RCEBGR = await FCUBSServices.Calculator_EoC_Branch_Group_Reject();
            return Ok(RCEBGR);
        }


        [Route("api/fcubs/v1/Calculator_EoC_Branch_Group_Refresh")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Calculator_EoC_Branch_Group_Refresh(ReqCalculator_EoC_Branch_Group_Refresh param)
        {
            ResCalculator_EoC_Branch_Group_Refresh RCEBGR = await FCUBSServices.Calculator_EoC_Branch_Group_Refresh(param);
            return Ok(RCEBGR);
        }
    }
}