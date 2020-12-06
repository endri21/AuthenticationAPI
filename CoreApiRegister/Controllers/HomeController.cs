
using Microsoft.AspNetCore.Mvc;


namespace CoreApiRegister.Controllers
{
    
    public class HomeController : ApiController
    {
        //[Authorize]
       [Route("[controller]")]
        public IActionResult Get()
        {
            return Ok("this works ");
        }

    }
}
