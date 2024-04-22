using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using CoreFunction;
using ITOAPP_API.Helpers;
using ITOAPP_API.Models;
//FOR UPLOADING FILE
using System.IO;

namespace ITOAPP_API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class RPTEoCController : ControllerBase
    {
        [Route("api/EoCReport/v1/EoCReportFirstLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EoCReportFirstLoad()
        {
            ResEoCReportFirstLoad RE = await RPTEoCServices.EoCReportFirstLoad();
            return Ok(RE);
        }

        [Route("api/EoCReport/v1/GetEoCSteps")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetEoCSteps()
        {
            ResGetEoCSteps RE = await RPTEoCServices.GetEoCSteps();
            return Ok(RE);
        }
        [Route("api/EoCReport/v1/InsertDuration")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertEoCDuration(ReqInsertEoCDuration param)
        {
            BasicResponse BR = await RPTEoCServices.InsertEoCDuration(param);
            return Ok(BR);
        }
        [Route("api/EoCReport/v1/EoCDurationData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EoCDurationData(ReqEoCDuationData param)
        {
            ResEoCDurationData RED = new ResEoCDurationData();
            try
            {
                RED = await RPTEoCServices.EoCDurationData(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RED);
        }
        [Route("api/EoCReport/v1/EoCCompletedPct")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EoCCompletedPct(ReqRptEoCCompPct param)
        {
            ResRptEoCCompPct RED = new ResRptEoCCompPct();
            try
            {
                RED = await RPTEoCServices.EoCCompletedPct(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RED);
        }
        [Route("api/EoCReport/v1/GetEoCStepDuration")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetEoCStepDuration(RegRptGetEoCStepDuration param)
        {
            ResRptEoCStepDuration Res = new ResRptEoCStepDuration();
            try
            {
                Res = await RPTEoCServices.GetEoCStepTimesByRptID(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(Res);
        }
        [Route("api/EoCReport/v1/GetEoCStepDurationByStepNo")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetEoCStepDurationByStepNo(RegRptGetEoCStepDurationByStepNo param)
        {
            ResRptEoCStepDuration Res = new ResRptEoCStepDuration();
            try
            {
                Res = await RPTEoCServices.GetEoCStepTimesByStepNo(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(Res);
        }
        [Route("api/EoCReport/v1/UpdateEoCStepDuration")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateEoCStepDuration(ReqRptEoCUpdateStepDuration param)
        {
            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.UpdateEoCStepDuration(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(Res);
        }
        
        [Route("api/EoCReport/v1/InsertPending")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertPending(ReqRptEoCInsertPending param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.InsertPending(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/EoCReportDataFirstLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EoCReportDataFirstLoad(ReqRptEoCData param)
        {

            ResRptEoCDataFirstLoad Res = new ResRptEoCDataFirstLoad();
            try
            {
                Res = await RPTEoCServices.EoCDataFirstLoad(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/GetPendingDataByID")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetPendingDataByID(ReqRptEoCPendingByID param)
        {

            ResEoCPendingByIDData Res = new ResEoCPendingByIDData();
            try
            {
                Res = await RPTEoCServices.GetPendingDataByID(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/UpdatePending")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePending(ResEoCUpdatePending param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.UpdatePending(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/EoCPendingData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EoCPendingData(ReqEoCPendingData param)
        {

            ResEoCPendingData Res = new ResEoCPendingData();
            try
            {
                Res = await RPTEoCServices.EoCPendingData(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/DeleteEoCReports")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteEoCReports(ReqDeleteEoCReports param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.DeleteEoCReports(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/InsertEoCResource")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertEoCResource(ReqInsertEoCResource param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.InsertEoCResource(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/EoCResourceData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>EoCResourceData(ReqEoCRsourceData param)
        {

            ResEoCResourceData Res = new ResEoCResourceData();
            try
            {
                Res = await RPTEoCServices.EoCResourceData(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/RefreshResource")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshResource()
        {

            ResRefEoCResource Res = new ResRefEoCResource();
            try
            {
                Res = await RPTEoCServices.RefreshResource();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/GetResourceByID")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetResourceByID(ReqGetResourceByID param)
        {

            ResResourceByID Res = new ResResourceByID();
            try
            {
                Res = await RPTEoCServices.GetResourceByID(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/UpdateResourceUtl")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateResourceUtl(ReqUpdateResourceUtl param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.UpdateResourceUtl(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/InsertStorageUtl")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertStorageUtl(ReqInsertStorageUtl param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.InsertSorageUtl(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/RefreshStorageUtl")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshStorageUtl()
        {

            ResRefreshStorage Res = new ResRefreshStorage();
            try
            {
                Res = await RPTEoCServices.RefreshStorage();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/RefreshStorageData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshStorageData(ReqStorageUtlData param)
        {

            ResRefreshStorageData Res = new ResRefreshStorageData();
            try
            {
                Res = await RPTEoCServices.RefreshStorageData(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/GetStorageByID")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetStorageByID(ReqGetStorageByID param)
        {

            ResGetStorageByID Res = new ResGetStorageByID();
            try
            {
                Res = await RPTEoCServices.GetStorageByID(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/UpdateStorageUtl")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateStorageUtl(ReqEoCUpdateStorageUtl param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.UpdateStorageUtl(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/InsertEoCFailure")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertEoCFailure(ReqEoCInsertFailure param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.InsertEoCFailure(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/RefreshBranchFailure")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshBranchFailure()
        {

            ResRefreshEoCBranchFailure Res = new ResRefreshEoCBranchFailure();
            try
            {
                Res = await RPTEoCServices.RefreshBranchFailure();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/RefreshEoCFailureData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshEoCFailureData(ReqRefreshEoCFailureData param)
        {

            ResRefreshEoCFailureData Res = new ResRefreshEoCFailureData();
            try
            {
                Res = await RPTEoCServices.RefreshEoCFailureData(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/GetEoCFailureDataByID")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetEoCFailureDataByID(ReqGetEoCFailureDataByID param)
        {

            ResGetEoCFailureDataByID Res = new ResGetEoCFailureDataByID();
            try
            {
                Res = await RPTEoCServices.GetEoCFailureDataByID(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/UpdateEoCFailure")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateEoCFailure(ReqUpdateEoCFailure param)
        {

            BasicResponse Res = new BasicResponse();
            try
            {
                Res = await RPTEoCServices.UpdateEoCFailure(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
        [Route("api/EoCReport/v1/RefreshEoCRestorePointData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshEoCRestorePointData()
        {
            ResRefreshEoCRestorePointData Res = new ResRefreshEoCRestorePointData();
            try
            {
                Res = await RPTEoCServices.RefreshEoCRestorePointData();
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(Res);
        }
    }
    public class ResEoCReportFirstLoad
    {
        [JsonProperty(Order = 1)]
        public string status { get; set; }

        [JsonProperty(Order = 2)]
        public string message { get; set; }
        [JsonProperty(Order = 3)]
        public string eoc_type { get; set; }
        [JsonProperty(Order = 4)]
        public string eoc_report_date { get; set; }
        [JsonProperty(Order = 5)]
        public string eoc_report_comp_pct { get; set; }
        [JsonProperty(Order = 6)]
        public List<ExeResEoCStep> eoc_steps { get; set; }
        [JsonProperty(Order = 7)]
        public List<ExeResBranch> branches { get; set; }
        [JsonProperty(Order = 8)]
        public List<ExeResAllResources> resources { get; set; }
        [JsonProperty(Order = 9)]
        public List<ExeResAllStorages> storages { get; set; }
        [JsonProperty(Order = 10)]
        public List<ExeResBranch> failure_branches { get; set; }
    }
    public class ResGetEoCSteps
    {
        [JsonProperty(Order = 1)]
        public string status { get; set; }

        [JsonProperty(Order = 2)]
        public string message { get; set; }

        [JsonProperty(Order = 3)]
        public List<ExeResEoCStep> data { get; set; }
        
    }
    public class ExeResEoCStep
    {
        public int step_no { get; set; }
        public string step_name { get; set; }
        public string is_auto { get; set; }
    }
    public class ExeResBranch
    {
        public string branch_code { get; set; }
        public string branch_name { get; set; }
    }
    public class ExeResAllResources
    {
        public int resource_id { get; set; }
        public string resource_name { get; set; }
    }
    public class ExeResAllStorages
    {
        public int storage_id { get; set; }
        public string storage_name { get; set; }
    }
    public class ReqInsertEoCDuration
    {
        public string step_no { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string completed_stat { get; set; }
    }
    public class ReqEoCDuationData
    {
        public string report_date { get; set; }
    }
    public class ResEoCDurationData
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResEoCDurationData> data { get; set; }
    }
    public class ExeResEoCDurationData
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("eoc_type")]
        public string eoc_type { get; set; }
        [JsonProperty("step_name")]
        public string step_name { get; set; }
        [JsonProperty("start_time")]
        public string start_time { get; set; }
        [JsonProperty("end_time")]
        public string end_time { get; set; }
        [JsonProperty("elapsed_minutes")]
        public string elapsed_minutes { get; set; }
        [JsonProperty("completed")]
        public string completed { get; set; }
        [JsonProperty("registered_by")]
        public string registered_by { get; set; }
        [JsonProperty("registered_date")]
        public string registered_date { get; set; }
        [JsonProperty("last_modifier")]
        public string last_modifier { get; set; }
        [JsonProperty("last_modified_date")]
        public string last_modified_date { get; set; } 
    }
    public class ReqRptEoCCompPct
    {
        public string report_date { get; set; }
    }
    public class ResRptEoCCompPct
    {
        [JsonProperty(Order = 1)]
        public string status { get; set; }

        [JsonProperty(Order = 2)]
        public string message { get; set; }
        [JsonProperty(Order = 3)]
        public string eoc_report_comp_pct { get; set; }
        [JsonProperty(Order = 4)]
        public string total_br_pulled_gl { get; set; }
    }
    public class RegRptGetEoCStepDuration
    {
        public string report_id { get; set; }
    }
    public class RegRptGetEoCStepDurationByStepNo
    {
        public string step_no { get; set; }
        public string report_date { get; set; }
    }
    public class ResRptEoCStepDuration
    {
        public string status { get; set; }
        public string message { get; set; }
        public ExeResStepDuration data { get; set; }
    }
    public class ExeResStepDuration
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
    }
    public class ReqRptEoCUpdateStepDuration
    {
        public string report_id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string completed { get; set; }
    }
    public class ReqRptEoCDurationDelete
    {
        public string report_id { get; set; }
    }
    public class ReqRptEoCInsertPending
    {
        public string issue_category { get; set; }
        public string branch_code { get; set; }
        public string maker_id { get; set; }
        public string module { get; set; }
        public string function_id { get; set; }
        public string maker_challenge { get; set; }
        public string maker_solution { get; set; }
        public string maker_solution_html { get; set; }
        public string resolved_solution { get; set; }
        public string resolved_detail { get; set; }
        public string resolved_detail_html { get; set; }
        public string resolved_stat { get; set; }
    }
    public class ReqRptEoCData
    {
        public string report_date { get; set; }
    }
    public class ResRptEoCDataFirstLoad
    {
        public string status { get; set; }
        public string message { get; set; }
        public ExeResRptEoCData data { get; set; }
    }
    public class ExeResRptEoCData
    {
        public List<ExeResEoCDurationData> step_duration { get; set; }
        public List<ExeResRptEoCPendingData> pending_trn { get; set; }
        public List<ExeResRptResourceData> resource_utl { get; set; }
        public List<ExeResRptStorageData> storage_utl { get; set; }
        public List<ExeResRptFailureBrData> failure_branches { get; set; }
        public List<ExeResRptRestorePointData> restorepoint { get; set; }
    }
    public class ExeResRptRestorePointData
    {
        public string scn { get; set; }
        public string database_incarnation { get; set; }
        public string guarantee_flashback_database { get; set; }
        public string storage_size { get; set; }
        public string time { get; set; }
        public string restore_point_time { get; set; }
        public string preserved { get; set; }
        public string name { get; set; }
        public string pdb_restore_point { get; set; }
        public string clean_pdb_restore_point { get; set; }
        public string pdb_incarnation { get; set; }
        public string replicated { get; set; }
        public string con_id { get; set; }
    }
    public class ExeResRptEoCPendingData
    {
        public string report_id { get; set; }
        public string issue_category { get; set; }
        public string branch_code { get; set; }
        public string maker_id { get; set; }
        public string module { get; set; }
        public string function_id { get; set; }
        public string resolved_type { get; set; }
        public string resolved_detail { get; set; }
        public string resolved_stat { get; set; }
        public string registered_by { get; set; }
        public string registered_date { get; set; }
        public string last_modifier { get; set; }
        public string last_modified_date { get; set; }
    }
    public class ExeResRptResourceData
    {
        public string report_id { get; set; }
        public string resource_name { get; set; }
        public string min_used_memory { get; set; }
        public string max_used_memory { get; set; }
        public string min_used_cpu { get; set; }
        public string max_used_cpu { get; set; }
        public string record_stat { get; set; }
        public string registered_by { get; set; }
        public string registered_date { get; set; }
        public string last_modifier { get; set; }
        public string last_modified_date { get; set; }
    }
    public class ExeResRptStorageData
    {
        public string report_id { get; set; }
        public string storage_name { get; set; }
        public string used_size { get; set; }
        public string free_size { get; set; }
        public string total_size { get; set; }
        public string record_stat { get; set; }
        public string registered_by { get; set; }
        public string registered_date { get; set; }
        public string last_modifier { get; set; }
        public string last_modified_date { get; set; }
    }
    public class ExeResRptFailureBrData
    {
        public string report_id { get; set; }
        public string branch_code { get; set; }
        public string eoc_ref_no { get; set; }
        public string eod_date { get; set; }
        public string branch_date { get; set; }
        public string curr_stage { get; set; }
        public string target_stage { get; set; }
        public string running_stage { get; set; }
        public string eoc_status { get; set; }
        public string error_code { get; set; }
        public string error_param { get; set; }
        public string error { get; set; }
        public string sr_no { get; set; }
        public string resolved_stat { get; set; }
        public string record_stat { get; set; }
        public string registered_by { get; set; }
        public string registered_date { get; set; }
        public string last_modifier { get; set; }
        public string last_modified_date { get; set; }
    }
    public class ReqRptEoCPendingByID
    {
        public string report_id { get; set; }
    }
    public class ExeResEoCPendingByIDData
    {
        public string branch_code { get; set; }
        public string maker_id { get; set; }
        public string module { get; set; }
        public string function_id { get; set; }
        public string maker_chg { get; set; }
        public string maker_sol_html { get; set; }
        public string resolved_type { get; set; }
        public string resolved_detail { get; set; }
        public string resolved_stat { get; set; }
        public string issue_category { get; set; }
    }
    public class ResEoCPendingByIDData
    {
        public string status { get; set; }
        public string message { get; set; }
        public ExeResEoCPendingByIDData data { get; set; }
    }
    public class ResEoCUpdatePending
    {
        public string report_id { get; set; }
        public string issue_category { get; set; }
        public string branch_code { get; set; }
        public string maker_id { get; set; }
        public string module { get; set; }
        public string function_id { get; set; }
        public string maker_chg { get; set; }
        public string maker_solution { get; set; }
        public string maker_solution_html { get; set; }
        public string resolved_type { get; set; }
        public string resolved_solution { get; set; }
        public string resolved_solution_html { get; set; }
        public string resolved_stat { get; set; }

    }
    public class ReqEoCPendingData
    {
        public string report_date { get; set; }
    }
    public class ResEoCPendingData
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResRptEoCPendingData> data { get; set; }

    }
    public class ReqDeleteEoCReports
    {
        public string report_type { get; set; }
        public string report_id { get; set; }
    }
    public class ReqInsertEoCResource
    {
        public string resource_id { get; set; }
        public string min_mem_used { get; set; }
        public string max_mem_used { get; set; }
        public string min_cpu_used { get; set; }
        public string max_cpu_used { get; set; }
    }
    public class ReqEoCRsourceData
    {
        public string report_date { get; set; }
    }
    public class ResEoCResourceData
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResRptResourceData> data { get; set; }

    }
    public class ResRefEoCResource
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResAllResources> data { get; set; }
    }
    public class ReqGetResourceByID
    {
        public string report_id { get; set; }
    }
    public class ResResourceByID
    {
        public string status { get; set; }
        public string message { get; set; }
        public ExResResourceByID data { get; set; }
    }
    public class ExResResourceByID
    {
        public string min_used_memory { get; set; }
        public string max_used_memory { get; set; }
        public string min_used_cpu { get; set; }
        public string max_used_cpu { get; set; }
    }
    public class ReqUpdateResourceUtl
    {
        public string report_id { get; set; }
        public string min_used_memory { get; set; }
        public string max_used_memory { get; set; }
        public string min_used_cpu { get; set; }
        public string max_used_cpu { get; set; }
    }
    public class ReqInsertStorageUtl
    {
        public string storage_id { get; set; }
        public string total_size { get; set; }
        public string total_size_mesu { get; set; }
        public string used_size { get; set; }
        public string used_size_mesu { get; set; }
    }
    public class ResRefreshStorage
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResAllStorages> data { get; set; }
    }
    public class ReqStorageUtlData
    {
        public string report_date { get; set; }
    }
    public class ResRefreshStorageData
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResRptStorageData> data { get; set; }
    }
    public class ReqGetStorageByID
    {
        public string report_id { get; set; }
    }
    public class ResGetStorageByID
    {
        public string status { get; set; }
        public string message { get; set; }
        public ExeRestGetStorageByID data { get; set; }

    }
    public class ExeRestGetStorageByID
    {
        public string total_size { get; set; }
        public string used_size { get; set; }
    }
    public class ReqEoCUpdateStorageUtl
    {
        public string report_id { get; set; }
        public string total_size { get; set; }
        public string total_size_mesu { get; set; }
        public string used_size { get; set; }
        public string used_size_mesu { get; set; }
    }
    public class ReqEoCInsertFailure
    {
        public string branch_code { get; set; }
        public string eoc_ref_no { get; set; }
        public string resolved_detail { get; set; }
        public string resolved_detail_html { get; set; }
        public string sr_no { get; set; }
        public string root_cause_summary { get; set; }
        public string resolved_stat { get; set; }
    }
    public class ResRefreshEoCBranchFailure
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResBranch> data { get; set; }
    }
    public class ResRefreshEoCRestorePointData
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResRptRestorePointData> data { get; set; }
    }
    
    public class ReqRefreshEoCFailureData
    {
        public string report_date { get; set; }
    }
    public class ResRefreshEoCFailureData
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ExeResRptFailureBrData> data { get; set; }
    }
    public class ReqGetEoCFailureDataByID
    {
        public string report_id { get; set; }
    }
    public class ResGetEoCFailureDataByID
    {
        public string status { get; set; }
        public string message { get; set; }
        public ExeGetEoCFailureDataByID data { get; set; }
    }
    public class ExeGetEoCFailureDataByID
    {
        public string sr_no { get; set; }
        public string root_cause_summary { get; set; }
        public string resolved_stat { get; set; }
        public string resolved_detail_html { get; set; }
    }
    public class ReqUpdateEoCFailure
    {
        public string report_id { get; set; }
        public string sr_no { get; set; }
        public string root_cause_summary { get; set; }
        public string resolved_stat { get; set; }
        public string resolved_detail { get; set; }
        public string resolved_detail_html { get; set; }
    }
}