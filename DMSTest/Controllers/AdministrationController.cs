using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMSTest.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdministrationController : ControllerBase
    {
    }
}
