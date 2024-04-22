using ITOAPP_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using static ITOAPP_API.Models.RPTDocManagementModel;
using System.Threading.Tasks;

namespace ITOAPP_API.Controllers
{
    [ApiController]
    public class RPTDocManagementController : ControllerBase
    {
        [Route("api/RPTDocManagement/v1/FirstLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DocMGTFirstLoad()
        {
            ResFirstLoad RFL = await RPTDocManagementService.DocMGTGetDataFirstLoad();
            return Ok(RFL);
        }
        [Route("api/v1/RPTDocManagement/DocMGTGetDataTbListing")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DocMGTGetDateTblisting()
        {
            ResDocMGTGetDataTbLising res = await RPTDocManagementService.DocMGTGetDataTableListing();
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/InsertDocManagement")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertDocManagement(ReqInsertDocManagement param)
        {
            ResInsertDocManagement RIDM = await RPTDocManagementService.InsertDocManagement(param);
            return Ok(RIDM);
        }
        [Route("api/RPTDocManagement/v1/EditDocManagement")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditDocManagement(ReqEditDocManagement param)
        {
            ResEditDocManagement REDM = await RPTDocManagementService.EditDocManagement(param);
            return Ok(REDM);
        }
        [Route("api/RPTDocManagement/v1/UpdateDocManagement")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateDocManagement(ReqUpdateReport param)
        {
            ResInsertDocManagement REDM = await RPTDocManagementService.UpdateDocManagement(param);
            return Ok(REDM);
        }

        [Route("api/RPTDocManagement/v1/DeleteDocManagement")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteDocManagement(ReqDeleteDocumentReport param)
        {
            ResDeletDocumentReport RDDM = await RPTDocManagementService.DeleteDocManagement(param);
            return Ok(RDDM);
        }
        [Route("api/v1/RPTDocManagement/UploadDocManagement")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DocMGTUploadDocManagement(ReqDocMGTUploadFile param)
        {
            ResDocMGTUploadFile res = await RPTDocManagementService.DocMGTUploadDocManagement(param);
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/DownloadDocManagement")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DownloadDocManagement(ReqDownloadFile param)
        {
            ResDownloadFile res = await RPTDocManagementService.DownloadDocManagement(param);
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/GetCategoryReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetCatrgoryReport()
        {
            ResGetCatogory res = await RPTDocManagementService.GetCaegoryReport();
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/InsertCategoryReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CatrgoryOperation(ReqCategoryOperation param)
        {
            ResCategoryOperation res = await RPTDocManagementService.CatogoryOperation(param);
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/OperationReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReportOperation(ReqReportOperation param)
        {
            ResReportOperation res = await RPTDocManagementService.ReportOperation(param);
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/GetSummaryReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetDocSummaryByYear(ReqGetSummaryReport param)
        {
            ResGetSummaryReport res = await RPTDocManagementService.GetDocSummaryByYear(param);
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/GetFilterDocManagement")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetDocMgtByFilter(ReqGetDocManagement param)
        {
            ResGetDocManagement res = await RPTDocManagementService.GetDocManagementByFilter(param);
            return Ok(res);
        }
        [Route("api/RPTDocManagement/v1/GetConnectionString")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetConnectionString()
        {
            ResGetSummaryReport res = await RPTDocManagementService.GetConnectionString();
            return Ok(res);
        }
    }
}
