using ITOAPP_API.Helper;
using ITOAPP_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ITOAPP_API.Models.APIManagementModel;

namespace ITOAPP_API.Controllers
{
    [ApiController]
    public class APIManagementController : Controller
    {
        [Route("api/APIManagement/v1/APIMonLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIMonLoad()
        {
            ResAPIMonLoad RE = await APIManagementServices.ResAPIMonLoad();
            return Ok(RE);
        }

        [Route("api/APIManagement/v1/APITonLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APITonLoad()
        {
            ResAPITonLoad RE = await APIManagementServices.ResAPITonLoad();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIParamQuery")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPIPQuery(ReqAPIParameter parameter)
        {
            ResAPIPQuery RE = await APIManagementServices.GetAPIPQuery(parameter);
            return Ok(RE);
        }

        [Route("api/APIManagement/v1/APIParamCheckQuery")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPIPCheckQuery(ReqAPIParamCheck param)
        {
            ResAPIPQuery RE = await APIManagementServices.GetAPIPCheckQuery(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIParamUpdate")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPIPUpdate(ReqAPIParam param)
        {
            ResAPIPUpdate RE = await APIManagementServices.GetAPIPUpdate(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetEndpoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetEndpoint()
        {
            ResAPIPGetEndpoint RE = await APIManagementServices.APIPGetEndpoint();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterEndpoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterEndpoint(ReqAPIPRegisterEndpoint param)
        {
            ResAPIPRegisterEndpoint RE = await APIManagementServices.APIPRegisterEndpoint(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPEditEndpoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPEditEndpoint(ReqAPIPEditEndpointt param)
        {
            ResAPIPEditEndpoint RE = await APIManagementServices.APIPEditEndpoint(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUpdateEndpoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUpdateEndpoint(ReqAPIPUpdateEndpoint param)
        {
            ResAPIPUpdateEndpoint RE = await APIManagementServices.APIPUpdateEndpoint(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPDeleteEndpoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPDeleteEndpoint(ReqAPIPDeleteEndpoint param)
        {
            ResAPIPDeleteEndpoint RE = await APIManagementServices.APIPDeleteEndpoint(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetClient")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetClient()
        {
            ResAPIPGetClient RE = await APIManagementServices.APIPGetClient();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterClient")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterClient(ReqAPIPRegisterClient param)
        {
            ResAPIPRegisterClient RE = await APIManagementServices.APIPRegisterClient(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPDeleteClient")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPDeleteClient(ReqAPIPDeleteClient param)
        {
            ResAPIPDeleteClient RE = await APIManagementServices.APIPDeleteClient(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPEditClient")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPEditClient(ReqAPIPEditClient param)
        {
            ResAPIPEditClient RE = await APIManagementServices.APIPEditClient(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUpdateClient")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUpdateClient(ReqAPIPUpdateClient param, string param_client_id, string param_client_secert)
        {
            ResAPIPUpdateClient RE = await APIManagementServices.APIPUpdateClient(param, param_client_id, param_client_secert);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetSinature()
        {
            ResAPIPGetSinature RE = await APIManagementServices.APIPGetSinature();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterSinature(ReqAPIPRegisterSinature param)
        {
            ResAPIPRegisterSinature RE = await APIManagementServices.APIPRegisterSinature(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPDeleteSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPDeleteSinature(ReqAPIPDeleteSinature param)
        {
            ResAPIPDeleteSinature RE = await APIManagementServices.APIPDeleteSinature(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPEditSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPEditSinature(ReqAPIPEditSinature param)
        {
            ResAPIPEditSinature RE = await APIManagementServices.APIPEditSinature(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUpdateSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUpdateSinature(ReqAPIPUpdateSinature param)
        {
            ResAPIPUpdateSinature RE = await APIManagementServices.APIPUpdateSinature(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetEndpointuser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetEndpointuser()
        {
            ResAPIPGetEndpointuser RE = await APIManagementServices.APIPGetEndpointuser();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterEndpointuser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterEndpointuser(ReqAPIPRegisterEndpointuser param)
        {
            ResAPIPRegisterEndpointuser RE = await APIManagementServices.APIPRegisterEndpointuser(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPDeleteEndpointuser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPDeleteEndpointuser(ReqAPIPDeleteEndpointuser param)
        {
            ResAPIPDeleteEndpointuser RE = await APIManagementServices.APIPDeleteEndpointuser(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPEditEndpointuser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPEditEndpointuser(ReqAPIPEditEndpointuser param)
        {
            ResAPIPEditEndpointuser RE = await APIManagementServices.APIPEditEndpointuser(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUpdateEndpointuser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUpdateEndpointuser(ReqAPIPUpdateEndpointuser param)
        {
            ResAPIPUpdateEndpointuser RE = await APIManagementServices.APIPUpdateEndpointuser(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetClientEndpoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetClientEndpoint()
        {
            ResAPIPGetClientEndpoint RE = await APIManagementServices.APIPGetClientEndpoint();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetClientSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetClientSinature(ReqAPIPGetClientSinature param)
        {
            ResAPIPGetClientSinature RE = await APIManagementServices.APIPGetClientSinature(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetMessage")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetMessage()
        {
            ResAPIPGetMessage RE = await APIManagementServices.APIPGetMessage();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterMessage")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterMessage(ReqAPIPRegisterMessage param)
        {
            ResAPIPRegisterMessage RE = await APIManagementServices.APIPRegisterMessage(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPGetMessageCode")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPGetMessageCode(ReqAPIPGetMessageCode param)
        {
            ResAPIPGetMessageCode RE = await APIManagementServices.APIPGetMessageCode(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPEditMessage")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPEditMessage(ReqAPIPEditMessage param)
        {
            ResAPIPEditMessage RE = await APIManagementServices.APIPEditMessage(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUpdateMessage")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUpdateMessage(ReqAPIPUpdateMessage param)
        {
            ResAPIPUpdateMessage RE = await APIManagementServices.APIPUpdateMessage(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIUGetValue")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIUGetValue()
        {
            ResAPIUGetValue RE = await APIManagementServices.APIUGetValue();
            return Ok(RE);
        }

        [Route("api/APIManagement/v1/APIPClientEndpointMap")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPClientEndpointMap(ReqAPIPClientEndpointMap param)
        {
            ResAPIPClientEndpointMap RE = await APIManagementServices.APIPClientEndpointMap(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPClientEndpointMapCheck")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPClientEndpointMapCheck(ReqAPIPClientEndpointMapCheck param)
        {
            ResAPIPClientEndpointMapCheck RE = await APIManagementServices.APIPClientEndpointMapCheck(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterClientEndpoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterClientEndpoint(ReqAPIPRegisterClientEndpoint param)
        {
            ResAPIPRegisterClientEndpoint RE = await APIManagementServices.APIPRegisterClientEndpoint(param);
            return Ok(RE);
        }

        [Route("api/APIManagement/v1/APIPClientSinatureMap")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPClientSinatureMap(ReqAPIPClientSinatureMap param)
        {
            ResAPIPClientSinatureMap RE = await APIManagementServices.APIPClientSinatureMap(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPClientSinatureMapCheck")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPClientSinatureMapCheck(ReqAPIPClientSinatureMapCheck param)
        {
            ResAPIPClientSinatureMapCheck RE = await APIManagementServices.APIPClientSinatureMapCheck(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterClientSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterClientSinature(ReqAPIPRegisterClientSinature param)
        {
            ResAPIPRegisterClientSinature RE = await APIManagementServices.APIPRegisterClientSinature(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPEditClientSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPEditClientSinature(ReqAPIPEditClientSinature param)
        {
            ResAPIPEditClientSinature RE = await APIManagementServices.APIPEditClientSinature(param);   
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUpdateClientSinature")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUpdateClientSinature(ReqAPIPUpdateClientSinature param)
        {
            ResAPIPUpdateClientSinature RE = await APIManagementServices.APIPUpdateClientSinature(param);
            return Ok(RE);
        }
        
        [Route("api/APIManagement/v1/APITransactionQuery")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPITransactionQuery(ReqAPITransaction transaction)
        {
            ResAPITQuery RE = await APIManagementServices.GetAPITransactionQuery(transaction);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APITransactionDetail")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPITransactionDetail(ReqAPITransactionDetail transactions)
        {
            ResAPITDetail RE = await APIManagementServices.GetAPITransactionDetail(transactions);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APITGetRequestData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPITRequestData(ReqAPITGetRequestdata req_data)
        {
            ResAPITGetRequestData RE = await APIManagementServices.GetAPITRequestData(req_data);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APITGetResponseData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPITResponseData(ReqAPITGetResponsetdata res_data)
        {
            ResAPITGetResponsetData RE = await APIManagementServices.GetAPITResponseData(res_data);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APITGetHeaderData")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPITHeaderData(ReqAPITGetHeaderdata hea_data)
        {
            ResAPITGetHeaderData RE = await APIManagementServices.GetAPITHeaderData(hea_data);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIConnectiononLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPIConLoad()
        {
            ResAPIConLoad RE = await APIManagementServices.GetAPIConLoad();
            return Ok(RE);
        }
        
        [Route("api/APIManagement/v1/APIConnectionCheck")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPIConnectionCheck(ReqAPICConnection req_checkconnection)
        {
            ResAPICCheck RE = await APIManagementServices.GetAPIConnectionCheck(req_checkconnection);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APICDowntimeService")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPICDowntimeQuery(ReqAPICDownTime DT)
        {
            ResAPICDTQuery RE = await APIManagementServices.GetAPICDowntimeQuery(DT);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APICGetAllService")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPICGetAllService()
        {
            ResAPICGetAllService RE = await APIManagementServices.GetAPICGetAllService();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APICGetChartDownService")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPICChartDownService(ReqAPICChartDownTime DTC)
        {
            ResAPICChartDownService RE = await APIManagementServices.GetAPICChartDownService(DTC);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APICGetChartDowntimeService")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPICChartDowntimeService(ReqAPICChartDownTimeDay DTCD)
        {
            ResAPICChartDownTimeService RE = await APIManagementServices.GetAPICChartDownTimeService(DTCD);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIUEndpointonLoad")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIUEndpointonLoad()
        {
            ResAPIUEndpointonLoad RE = await APIManagementServices.ResAPIUEndpointonLoad();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APITool")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APITool(ReqAPITool Param)
        {
            ResAPITool RE = await APIManagementServices.ResAPITool(Param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUserServiceGet")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUserServiceGet()
        {
            ResAPIPUserServiceGet RE = await APIManagementServices.APIPUserServiceGet();
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUserServiceMapCheck")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUserServiceMapCheck(ReqAPIPUserServiceMapCheck param)
        {
            ResAPIPUserServiceMapCheck RE = await APIManagementServices.APIPUserServiceMapCheck(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPRegisterUserService")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPRegisterUserService(ReqAPIPRegisterUserService param)
        {
            ResAPIPRegisterUserService RE = await APIManagementServices.APIPRegisterUserService(param);
            return Ok(RE);
        }
        [Route("api/APIManagement/v1/APIPUserTxnCheck")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> APIPUserTxnCheck()
        {
            ResAPIPUserTxnCheck RE = await APIManagementServices.APIPUserTxnCheck();
            return Ok(RE);
        }
    }
}
