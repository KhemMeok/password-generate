using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreFunction;
using ITOAPP_API.Helpers;
using ITOAPP_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ITOAPP_API.Models.RPT_ServerINVT_Model;

namespace ITOAPP_API.Controllers
{
    [ApiController]
    public class RPTServerINVTController : ControllerBase
    {
        [Route("api/ServerINVT/v1/ServerINVTonLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTonLoad()
        {
            ResServerINVTonLoad RE = await RPTServerINVTServices.ServerINVTonLoad();
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTRegisterHost")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTRegisterHost(ReqServerINVTRegisterHost param)
        {
            ResServerINVTRegisterHost RE = await RPTServerINVTServices.ServerINVTRegisterHost(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTRegisterCSI")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTRegisterCSI(ReqServerINVTRegisterCSI param)
        {
            //Prepare list Iform file for copy to FileStream and store in server location (optional)
            //param.doc_support.ForEach(async file =>
            //{
            //    string filePath = "upload_files" + "\\" + Core.GetContextValue("user_id") + "\\";
            //    if (file.Length > 0)
            //    {
            //        if (!Directory.Exists(filePath))
            //        {
            //            Directory.CreateDirectory(filePath);
            //        }
            //        using (var stream = new FileStream(filePath + file.FileName, FileMode.Create))
            //        {
            //            await file.CopyToAsync(stream);
            //        }
            //    }
            //});
            //Uploading file to DB
            ResServerINVTRegisterCSI RE = await RPTServerINVTServices.ServerINVTRegisterCSI(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTServiceMapping")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTServiceMapping(ReqServerINVTServiceMapping param)
        {
            ResServerINVTServiceMapping RE = await RPTServerINVTServices.ServerINVTServiceMapping(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTAllServerListing")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTAllServerListing()
        {
            ResServerINVTAllServerListing RE = await RPTServerINVTServices.ServerINVTAllServerListing();
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTAllServiceListing")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTAllServiceListing()
        {
            ResServerINVTAllServiceListing RE = await RPTServerINVTServices.ServerINVTAllServiceListing();
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTAllCSIListing")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTAllCSIListing()
        {
            ResServerINVTAllCSIListing RE = await RPTServerINVTServices.ServerINVTAllCSIListing();
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTDeleteReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTDeleteReport(ReqServerINVTDeleteReport param)
        {
            ResServerINVTDeleteReport RE = await RPTServerINVTServices.ServerINVTDeleteReport(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTEditReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTEditReport(ReqServerINVTEditReport param)
        {
            ResServerINVTEditReport RE = await RPTServerINVTServices.ServerINVTEditReport(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTUpdateServerReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTUpdateServerReport(ReqServerINVTUpdateServerReport param)
        {
            ResServerINVTUpdateServerReport RE = await RPTServerINVTServices.ServerINVTUpdateServerReport(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTUpdateServiceReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTUpdateServiceReport(ReqServerINVTUpdateServiceReport param)
        {
            ResServerINVTUpdateServiceReport RE = await RPTServerINVTServices.ServerINVTUpdateServiceReport(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTUpdateCSIReport")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTUpdateCSIReport(ReqServerINVTUpdateCSIReport param)
        {
            ResServerINVTUpdateCSIReport RE = await RPTServerINVTServices.ServerINVTUpdateCSIReport(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTUploadCSIDoc")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTUploadCSIDoc(ReqServerINVTUploadCSIDoc param)
        {
            ResServerINVTUploadCSIDoc RE = await RPTServerINVTServices.ServerINVTUploadCSIDoc(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTGetCSIDoc")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTGetCSIDoc(ReqServerINVTGetCSIDoc param)
        {
            ResServerINVTServerINVTGetCSIDoc RE = await RPTServerINVTServices.ServerINVTGetCSIDoc(param);
            return Ok(RE);
        }
        [Route("api/ServerINVT/v1/ServerINVTGetDoc4Download")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ServerINVTGetDoc4Download(ReqServerINVTGetDoc4Download param)
        {
            ResServerINVTGetDoc4Download RE = await RPTServerINVTServices.ServerINVTGetDoc4Download(param);
            return Ok(RE);
        }
    }
}
