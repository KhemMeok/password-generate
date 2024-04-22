using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ITOAPP_API.Helpers;
using ITOAPP_API.Models;

namespace ITOAPP_API.Controllers
{
    
    [ApiController]
    public class FCUBSBranchUtilityController : ControllerBase
    {
        [Route("api/v1/GetAllBranches")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAllBranches()
        {
            DataTable dt = new DataTable();
            dt = await FCUBBranchUtilityServices.GetallBranches();
            var list = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            return Ok(list);
        }
        [Route("api/v1/GetBranch")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetBranch(ParamGetBranch PB)
        {
            BranchInfoModel BI = await FCUBBranchUtilityServices.GetBranches(PB.branch_code);
            return Ok(BI);
        }
    }
    public class ParamGetBranch
    {
        public string branch_code { get; set; }
    }
}
