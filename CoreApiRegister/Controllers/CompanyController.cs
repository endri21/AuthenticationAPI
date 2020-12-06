

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreApiRegister.Models.Company;
using System.Linq;
using System.Security.Claims;
using CoreApiRegister.Infrastructure.Extensions;
using CoreApiRegister.Data.Models;
using CoreApiRegister.Data;

namespace CoreApiRegister.Controllers
{
    public class CompanyController : ApiController
    {

        private readonly ApplicationDbContext data;

        public CompanyController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateCompanyRequestModel vm)
        {
            try
            {
                var userId = this.User.GetId();
                var company = new Company
                {
                    Address = vm.Address,
                    Name = vm.Name,
                    UrlImage = vm.UrlImage,
                    UserId = userId
                };
                data.Add(company);
                await data.SaveChangesAsync();
                return Created(nameof(this.Create), company.Id);
            
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
    }
}
