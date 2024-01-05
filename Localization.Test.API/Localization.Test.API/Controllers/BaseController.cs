using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localization.Test.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
