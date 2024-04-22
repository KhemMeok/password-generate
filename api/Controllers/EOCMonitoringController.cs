using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITOAPP_API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ITOAPP_API.Models.EoCMonitoringModel;

namespace ITOAPP_API.Controllers
{
    
    [ApiController]
    public class EOCMonitoringController : ControllerBase
    {
        [Route("api/EoCMonitoring/v1/GetParamConfig")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetParamConfig()
        {
            ResParamConfig Res = new ResParamConfig();
            Res = await EoCMonitorServices.GetParamConfig();
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetEoDSummary")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetEoDSummary(ReqEoDSummary param)
        {
            ResEoDSummary Res = new ResEoDSummary();
            Res = await EoCMonitorServices.GetEoDSummary(param);
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetRunAbleBranches")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetRunAbleBranches(ReqRunAbleBranches param)
        {
            ResRunAbleBranches Res = new ResRunAbleBranches();
            Res = await EoCMonitorServices.GetRunAbleBranches(param);
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetFinishEoDMBranches")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetFinishEoDMBranches(ReqFinishEoDM param)
        {
            ResEoDM Res = new ResEoDM();
            Res = await EoCMonitorServices.GetFinishEoDMBranches(param);
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetNotFinishEoDMBranches")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetNotFinishEoDMBranches(ReqNotFinishEoDM param)
        {
            ResEoDM Res = new ResEoDM();
            Res = await EoCMonitorServices.GetNotFinishEoDMBranches(param);
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetFailedEoDMBranches")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetFailedEoDMBranches(ReqFailedEoDM param)
        {
            ResEoDM Res = new ResEoDM();
            Res = await EoCMonitorServices.GetFailedEoDMBranches(param);
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetFinishEoC")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetFinishEoC(ReqFinishEoC param)
        {
            ResFinishEoC Res = new ResFinishEoC();
            Res = await EoCMonitorServices.GetFinishEoC(param);
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetSubmitBranches")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetSubmitBranches()
        {
            ResSubmitBranches Res = new ResSubmitBranches();
            Res = await EoCMonitorServices.GetSubmitBranches();
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetCBSTBS")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetCBSTBS()
        {
            ResCBSTBS Res = new ResCBSTBS();
            Res = await EoCMonitorServices.GetCBSTBS();
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetCBSDBS")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetCBSDBS(ReqCBSDBS param)
        {
            ResCBSDBS Res = new ResCBSDBS();
            Res = await EoCMonitorServices.GetCBSDBS(param);
            return Ok(Res);
        }
        [Route("api/EoCMonitoring/v1/GetCBSDBSize")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetCBSDBSize()
        {
            ResCBSDBSize Res = new ResCBSDBSize();
            Res = await EoCMonitorServices.GetCBSDBSize();
            return Ok(Res);
        }


        [Route("api/EoCMonitoring/v1/GetContact")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetContact()
        {
            ResContact Res = new ResContact();
            Res = await EoCMonitorServices.GetContact();
            return Ok(Res);
        }

        [Route("api/EoCMonitoring/v1/GetPending")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetPending()
        {
            ResPending Res = new ResPending();
            Res = await EoCMonitorServices.GetPending();
            return Ok(Res);
        }

        [Route("api/EoCMonitoring/v1/GetMissmatchBalance")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetMissmatchBalance()
        {
            ResMissmatchBalance Res = new ResMissmatchBalance();
            Res = await EoCMonitorServices.GetMissmatchBalance();
            return Ok(Res);
        }

    }
}
