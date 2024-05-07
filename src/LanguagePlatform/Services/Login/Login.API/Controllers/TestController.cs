using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TestController: ControllerBase
    {

        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> Test()
        {
            return Ok(true);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Test2()
        {
            return Ok(true);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<bool>> Test3()
        {
            return Ok(true);
        }
    }
}
