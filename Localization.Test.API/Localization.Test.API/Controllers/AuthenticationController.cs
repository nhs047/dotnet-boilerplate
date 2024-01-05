using Localization.Test.API.Helpers;
using Localization.Test.Common.RequestModels;
using Localization.Test.Common.ResponseModels;
using Localization.Test.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localization.Test.API.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<ResponseObject<LoginResponse>> Login(LoginRequest loginRequest)
        {
            try
            {
                return ResponseObject<LoginResponse>.Build().setData(_authService.Login(loginRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseObject<LoginResponse>.Build().setMessage(ex.Message).setIsError(true));
            }
        }

        [HttpPost("Logout")]
        public ActionResult<ResponseObject<object>> Logout()
        {
            try
            {
                return ResponseObject<object>.Build().setData(new { isSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseObject<object>.Build().setMessage(ex.Message).setIsError(true));
            }
        }
    }
}
