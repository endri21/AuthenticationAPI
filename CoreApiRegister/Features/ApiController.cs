//per te mos e bere ne cdo controller me route krijojme nje klase apstrakte

using Microsoft.AspNetCore.Mvc;

namespace CoreApiRegister.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public abstract class ApiController : ControllerBase
    {
       
    }
}
