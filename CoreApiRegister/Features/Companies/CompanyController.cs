using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreApiRegister.Infrastructure.Extensions;
using CoreApiRegister.Controllers;
using System;
using CoreApiRegister.Features.Companies.Models;

namespace CoreApiRegister.Features.Companies
{
    [Authorize]
    public class CompanyController : ApiController
    {


        private readonly ICompaniesService companies;

        public CompanyController(ICompaniesService companies)
        => this.companies = companies;



        [HttpGet]
        [Route("/MineCreated")]
        public async Task<IActionResult> MineCreated()
        {
            try
            {
                var userId = this.User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await this.companies.GetCompanyByUserId(userId);
                // return Ok(result.OrNotFound()); nese perdoret objectextensions
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("/GetCompanyDetails/{id}")]
        public async Task<IActionResult> GetCompanyDetails(int id)
        {
            try
            {
                var userId = this.User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await companies.GetDetailsById(id, userId);
             
                return Ok(result);

            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        public async Task<ActionResult> Create(CreateCompanyResponseModel vm)
        {
            try
            {
                vm.UserId = this.User.GetId();
                if (vm.UserId == "")
                {
                    return Unauthorized();
                }
                var result = await companies.Create(vm);
             
                return Created(nameof(this.Create), result);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
