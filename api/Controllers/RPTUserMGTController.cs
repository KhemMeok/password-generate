using CoreFunction;
using ITOAPP_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ITOAPP_API.Models.RPTUserMGTModel;

namespace ITOAPP_API.Controllers
{
    [ApiController]
    public class RPTUserMGTController : ControllerBase
    {
        [Route("api/RPTUserMGT/v1/InfoStaff")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>GetInfoStaff(ReqInfoStaff param)
        {
            ResInfoStaff RIS = new ResInfoStaff();
            try
            {
                RIS=await RPTUserMGTService.GetInfoStaff(param);
            }
            catch(Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RIS);
        }
        [Route("api/RPTUserMGT/v1/SystemType")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>GetSystem()
        {
            ResSystem RSR = await RPTUserMGTService.GetSystem();
            return Ok(RSR);
        }
        [Route("api/RPTUserMGT/v1/SystemByCategory")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetSystemByCategory(ReqCategory param)
        {
            ResSystem RSR = await RPTUserMGTService.GetSystemByCategory(param);
            return Ok(RSR);
        }

        [Route("api/RPTUserMGT/v1/RoleSystem")]
       [HttpPost]
       [Authorize]
       public async Task<IActionResult> GetRole_system(ReqSystem_type param)
        {
            ResRole_system RRS = new ResRole_system();
            try
            {
                RRS = await RPTUserMGTService.GetRole_system(param);
            }catch(Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RRS);
        }
        [Route("api/RPTUserMGT/v1/HostName")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetHost_Name(ReqRole_System param)
        {
            ResHost_Name RHN = new ResHost_Name();
            try
            {
                RHN = await RPTUserMGTService.GetHost_Name(param);
            }catch(Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RHN);
        }

        [Route("api/RPTUserMGT/v1/ServiceRun")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>GetService_Run(ReqHost_Name param)
        {
            ResService_Run RSR = new ResService_Run();
            try
            {
                RSR = await RPTUserMGTService.GetService_Run(param);
            }catch(Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RSR);
        }

        [Route("api/RPTUserMGT/v1/SystemUser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>GetSystem_User(ReqMaker param)
        {
            ResUserSystem RUS = new ResUserSystem();
            try
            {
                RUS = await RPTUserMGTService.GetSystem_User(param);
            }catch(Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RUS);
        }

        [Route("api/RPTUserMGT/v1/InsertSystemPre")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>InsertSystemPre(ReqUserSystemPre param)
        {
            ResUserSystemPre RUSP = new ResUserSystemPre();
            try
            {
                RUSP = await RPTUserMGTService.InsertSystemPre(param);
            }catch(Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RUSP);
        }
        [Route("api/RPTUserMGT/v1/DeleteUserSystem")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>DeleteUserSystemPre(ReqDeleteUserSystemID param)
        {
            ResDeleteUserSystem RDUS = new ResDeleteUserSystem();
            try
            {
                RDUS= await RPTUserMGTService.DeleteUserSystemPre(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RDUS);
        }
        [Route("api/RPTUserMGT/v1/DeleteUserSystemMaker")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUserSystemMaker(ReqDeleteUserSystemMaker param)
        {
            ResDeleteUserSystem RDUS = new ResDeleteUserSystem();
            try
            {
                RDUS = await RPTUserMGTService.DeleteUserSystemMaker(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RDUS);
        }


        [Route("api/RPTUserMGT/v1/InsertRequest")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertRecord(ReqInsertRecord param)
        {
            ResInsertRecord RIR = new ResInsertRecord();
            try
            {
                RIR = await RPTUserMGTService.InsertRecord(param);
            }
            catch(Exception ex)
            {
                Core.DebugError(ex);
            }
            return Ok(RIR);
        }
        [Route("api/RPTUserMGT/v1/DownloadDocument")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>DownloadDocument(ReqDownloadDocument param)
        {
            ResDownloadDocument RDD = await RPTUserMGTService.DownloadDocument(param);            
            return Ok(RDD);
        }
        [Route("api/RPTUserMGT/v1/DeleteRequest")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteRequest(ReqDeleteRequest param)
        {
            ResDeleteRequest RDR = await RPTUserMGTService.DeleteRequest(param);
            return Ok(RDR);
        }
        [Route("api/RPTUserMGT/v1/ViewRequest")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>ViewRequest(ReqViewRequest param)
        {
            ResViewRequest RVR = new ResViewRequest();
            RVR=await RPTUserMGTService.ViewRequest(param);
            return Ok(RVR);
        }
        [Route("api/RPTUserMGT/v1/RefreshRequest")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshRequest()
        {
            ResViewRequest RVR = new ResViewRequest();
            RVR = await RPTUserMGTService.RefreshRequest();
            return Ok(RVR);
        }
        [Route("api/RPTUserMGT/v1/UpdateRequest")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>UpdateRequest(ReqUpdateRequest param)
        {
            ResUpdateRequest RUR = await RPTUserMGTService.UpdateRequest(param);           
            return Ok(RUR);
        }
        [Route("api/RPTUserMGT/v1/ModifyRequest")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>ModifyRequest(ReqDataModify param)
        {
            ResDataModify RDM=await RPTUserMGTService.ModifyRequest(param); 
            return Ok (RDM);    
        }
        [Route("api/RPTUserMGT/v1/Effective")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>AddEffective(ReqEffectiveDate param)
        {
            ResEffectiveDate RED = await RPTUserMGTService.AddEffective(param);
            return Ok(RED);
        }

        [Route("api/RPTUserMGT/v1/FirstLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>GetFirstLoad()
        {
            ResFirstLoad RFL = await RPTUserMGTService.GetFirstLoad();
            return Ok(RFL);
        }
    }
}
