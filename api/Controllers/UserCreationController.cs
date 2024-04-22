using Microsoft.AspNetCore.Mvc;
using static ITOAPP_API.Models.UserCreationModel;
using System.Threading.Tasks;
using ITOAPP_API.Helpers;

namespace ITOAPP_API.Controllers
{
    [ApiController]
    public class UserCreationController : Controller
    {
        [Route("api/v1/CreateNewUser/Get_Templete")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetName()
        {
            ResDataUserTemplete REDATA = await UserCreationService.GetTemplete();
            return Ok(REDATA);
        }
        [Route("api/v1/CreateNewUser/Get_APIEndPoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAPIEndpoint()
        {
            ResDataAPIEndPoint REDATA = await UserCreationService.GetAPIEndPoint();
            return Ok(REDATA);
        }
        [Route("api/v1/CreateNewUser/EnableUserAccessAPI")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AllowAccessAPI(ReqAllowUserAccessApi param)
        {
            ResAllowUserAccessApi REDATA = await UserCreationService.AcessApi(param);
            return Ok(REDATA);
        }


        [Route("api/v1/CreateNewUser/AddNewEndPoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewEndPoint(ReqAddNewEndPoint param)
        {
            ResAddNewEndPoint REDATA = await UserCreationService.AddNewEndPoint(param);
            return Ok(REDATA);
        }
        [Route("api/v1/CreateNewUser/UpdateEndPoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateEndPoint(ReqUpdateEndPoint param)
        {
            ResUpdateEndPoint REDATA = await UserCreationService.UpdateEndPoint(param);
            return Ok(REDATA);
        }
        [Route("api/v1/CreateNewUser/DeleteEndPoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteEndPoint(ReqDeleteEndPoint param)
        {
            ResDeleteEndPoint REDATA = await UserCreationService.DeleteEndPoint(param);
            return Ok(REDATA);
        }

        [Route("api/v1/CreateNewUser/GetEndPointForUpdate")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetEndPointForUpdate(ReqGetEndPointForUpdate param)
        {
            ResGetEndPointForUpdate REDATA = await UserCreationService.GetEndPointForUpdate(param);
            return Ok(REDATA);
        }
        [Route("api/v1/CreateNewUser/GetUserDataForUpdate")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserForUpdate(ReqUserForUpdate param)
        {
            ResUserForUpdate REDATA = await UserCreationService.GetUserForUpdate(param);
            return Ok(REDATA);
        }

        [Route("api/v1/CreateNewUser/GetFilterEndPoint")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetFilterEndPoint(ReqFilterEndPoint param)
        {
            ResFilterEndPoint REDATA = await UserCreationService.FilterEndPoint(param);
            return Ok(REDATA);
        }

        [Route("api/v1/CreateNewUser/DeleteUser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUser(ReqDeleteUser param)
        {
            ResDeleteUser REDATA = await UserCreationService.DeleteUser(param);
            return Ok(REDATA);
        }
        [Route("api/v1/CreateNewUser/GetEndPointSelect")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetEndPointSelect()
        {
            ResGetEndPointSelect REDATA = await UserCreationService.GetEndPointSelect();
            return Ok(REDATA);
        }

    }
}
