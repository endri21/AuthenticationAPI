

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreApiRegister.Infrastructure.Extensions;
using CoreApiRegister.Controllers;

namespace CoreApiRegister.Features.Companies
{
    public class CompanyController : ApiController
    {

     
        private readonly ICompaniesService companies;

        public CompanyController(ICompaniesService companies)
        => this.companies = companies;
        

        [Authorize]
        [HttpPost]
        [ProducesResponseType(statusCode:201)]
        public async Task<ActionResult> Create(CreateCompanyRequestModel vm)
        {
            try
            {         
                vm.UserId = this.User.GetId();
                if(vm.UserId == "")
                {
                    return BadRequest("Id e perdoruesit nuk eshte e sakte ");
                }
                var result = await companies.Create(vm);
                if(result != null)
                {
                return Created(nameof(this.Create), result);

                }
                return BadRequest();
            
            }
            catch (System.Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
