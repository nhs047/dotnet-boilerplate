using Localization.Test.API.Helpers;
using Localization.Test.Common.Constants;
using Localization.Test.Common.Helpers;
using Localization.Test.Common.RequestModels;
using Localization.Test.Common.ResponseModels;
using Localization.Test.Infrastructure.Models;
using Localization.Test.Service;
using Localization.Test.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localization.Test.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly LanguageService _languageService;
        private readonly IAuthService _authService;


        public UserController(IUserService userService, LanguageService languageService, IdentityResolver identityResolver, IAuthService authService)
        {
            _userService = userService;
            _languageService = languageService;
            _authService = authService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public ActionResult<ResponseObject<RegistrationResponse>> RegisterUser(RegistrationRequest registrationRequest)
        {
            return ResponseObject<RegistrationResponse>
                .Build()
                .setData(
                    _userService.Register(registrationRequest)
                 );
        }

        [HttpGet()]
        [Authorize("users.read")]
        public ActionResult<ResponseObject<IEnumerable<User>>> GetUsers([FromQuery] SearchQueryRequest searchQueryRequest)
        {
            try
            {
                return ResponseObject<IEnumerable<User>>.Build().setData(_userService.GetUsers(searchQueryRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseObject<IEnumerable<User>>.Build().setMessage(ex.Message).setIsError(true));
            }
        }

        [HttpGet("{id}")]
        [Authorize("users.read")]
        public ActionResult<ResponseObject<User>> GetUser(int id)
        {
            try
            {
                var user = _userService.GetUserWithRole(id);
                if (user != null)
                {
                    return ResponseObject<User>.Build().setData(user);
                }
                else
                {
                    return ResponseObject<User>.Build().setIsError(true).setMessage(_languageService.GetString(MessageCodes.NO_RECORDS_FOUND));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseObject<IEnumerable<User>>.Build().setMessage(ex.Message).setIsError(true));
            }
        }

        [HttpPut()]
        public ActionResult<ResponseObject<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequest userRequest)
        {
            try
            {
                var user = _userService.UpdateUser(userRequest);

                if (user != null)
                {
                    return ResponseObject<UpdateUserResponse>.Build().setData(
                           new UpdateUserResponse
                           {
                               Token = _authService.GetToken(user)
                           }
                       );
                }
                else
                {
                    return ResponseObject<UpdateUserResponse>.Build().setIsError(true).setMessage(_languageService.GetString(MessageCodes.NO_RECORDS_FOUND));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseObject<IEnumerable<UpdateUserResponse>>.Build().setMessage(ex.Message).setIsError(true));
            }
        }
    }
}
