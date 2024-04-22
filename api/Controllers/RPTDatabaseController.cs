using CoreFunction;
using ITOAPP_API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static ITOAPP_API.Models.RPTDatabaseModel;


namespace ITOAPP_API.Controllers
{
    [ApiController]

    public class RPTDatabaseController:ControllerBase
    {
        [Route("api/DatabaseReport/v1/GetDatabaseFristLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetDatabaseFristLoad()
        {
            ResGetDatabaseName RGDN = await RPTDatabaseServices.GetDatabaseFristLoad();
            return Ok(RGDN);
        }
        [Route("api/DatabaseReport/v1/GetDatabaseName")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetDatabaseName(ReqCategoryDatabase param)
        {
            ResGetDatabaseName RGDN = await RPTDatabaseServices.GetDatabaseName(param);
            return Ok(RGDN);
        }
     
        [Route("api/DatabaseReport/v1/GetReportRestoration")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetReportRestoration(ReqGetReportName param)
        {
            ResGetReportName RGDN = await RPTDatabaseServices.GetReportRestoration(param);
            return Ok(RGDN);
        }
        [Route("api/DatabaseReport/v1/GetReportSyncFailure")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetReportSyncFailure(ReqGetReportName param)
        {
            ResGetReportName RGDN = await RPTDatabaseServices.GetReportSyncFailure(param);
            return Ok(RGDN);
        }
        [Route("api/DatabaseReport/v1/GetReportBackupFailure")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetReportBackupFailure(ReqGetReportName param)
        {
            ResGetReportName RGDN = await RPTDatabaseServices.GetReportBackupFailure(param);
            return Ok(RGDN);
        }
        [Route("api/DatabaseReport/v1/GetPuggableDatabase")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetPuggableDatabase(ReqDatabaseName param)
        {
            ResGetDatabaseName RGPDB = await RPTDatabaseServices.GetPuggableDatabase(param);
            return Ok(RGPDB);
        }
        [Route("api/DatabaseReport/v1/InsertReportDatabase")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertReportDatabase(ReqInsertReportDatabase param)
        {
            ResInsertReportDatabase RIDF = await RPTDatabaseServices.InsertReportDatabase(param);
            return Ok(RIDF);
        }
        [Route("api/DatabaseReport/v1/UploadFiles")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadFiles(ReqUploadFile param)
        {
            ResInsertReportDatabase RIDF = await RPTDatabaseServices.ReqUploadFile(param);
            return Ok(RIDF);
        }
        [Route("api/DatabaseReport/v1/GetUploadFiles")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>GetUploadFiles(ReqGetFilesResult param)
        {
            ResGetDatabaseName RIDF = await RPTDatabaseServices.GetUploadFiles(param);
            return Ok(RIDF);
        }
        [Route("api/DatabaseReport/v1/DeleteUploadFiles")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUploadFiles(ReqDeleteRestorationResult param)
        {
            ResDeleteRestorationResult RIDF = await RPTDatabaseServices.DeleteUploadFiles(param);
            return Ok(RIDF);
        }


        [Route("api/DatabaseReport/v1/DeleteDatabaseReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteDatabaseReport(ReqDeleteDatabaseReport param)
        {           
            ResDeleteDatabaseReport RDDR = new ResDeleteDatabaseReport();
            try
            {
                RDDR = await RPTDatabaseServices.DeleteDatabaseReport(param);
            }
            catch (Exception ex)
            {
                Core.DebugError(ex);
            }

            return Ok(RDDR);
        }

        [Route("api/DatabaseReport/v1/EditDatabaseRestore")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditDatabaseRestore(ReqEditDatabaseReport param)
        {
            ResEditDatabaseReport REDR = await RPTDatabaseServices.EditDatabaseRestore(param);
            return Ok(REDR);
        }
        [Route("api/DatabaseReport/v1/EditDatabaseSyncFailure")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditDatabaseSyncFailure(ReqEditDatabaseReport param)
        {
            ResEditDatabaseReport REDSF = await RPTDatabaseServices.EditDatabaseSyncFailure(param);
            return Ok(REDSF);
        }
        [Route("api/DatabaseReport/v1/EditDatabaseBackUpFailure")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditDatabaseBackUpFailure(ReqEditDatabaseReport param)
        {
            ResEditDatabaseReport REDR = await RPTDatabaseServices.EditDatabaseBackUpFailure(param);
            return Ok(REDR);
        }
        [Route("/api/DatabaseReport/v1/UpdateReportDatabaseRestore")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>UpdateReportDatabaseRestore(ReqUpdateReportDatabaseRestore param)
        {
            ResUpdateReportDatabase RURD = await RPTDatabaseServices.UpdateReportDatabaseRestore(param);
            return Ok(RURD);
        }

        [Route("/api/DatabaseReport/v1/UpdateReportSynchronizeFailed")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateReportSynchronizeFailed(ReqUpdateReportSynchronizeFailed param)
        {
            ResUpdateReportDatabase RURD = await RPTDatabaseServices.UpdateReportSynchronizeFailed(param);
            return Ok(RURD);
        }

        [Route("/api/DatabaseReport/v1/UpdateReportBackupFailed")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateReportBackupFailed(ReqUpdateReportBackupFailed param)
        {
            ResUpdateReportDatabase RURD = await RPTDatabaseServices.UpdateReportBackupFailed(param);
            return Ok(RURD);
        }


    }
}
