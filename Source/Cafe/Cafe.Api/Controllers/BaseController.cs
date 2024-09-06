using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }
    }
}
