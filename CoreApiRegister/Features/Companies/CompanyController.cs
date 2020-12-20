using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreApiRegister.Infrastructure.Extensions;
using CoreApiRegister.Controllers;
using System;
using CoreApiRegister.Features.Companies.Models;
using  CoreApiRegister.Infrastructure;

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
                if(result == null)
                {
                    return BadRequest(result.OrNotFound());
                }
                // return Ok(result.OrNotFound()); nese perdoret objectextensions
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route(WebConstants.RouteId)]
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
                return result != null ? Ok(result) : BadRequest("Nuk ka te dhena !");
            

            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        public async Task<ActionResult> Create(CreateCompanyRequestModel vm)
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
   
        [HttpPut]
        [Route("/UpdateCompany")]
        public async Task<ActionResult> UpdateCompany(UpdateCompanyRequestModel vm)
        {
            var userId = this.User.GetId();
            if(userId == null)
            {
                return Unauthorized();
            }
            vm.UserId = userId;
            var updated = await companies.UpdateCompanyById(vm);
            return Ok(updated);
        }

        [HttpDelete]
        [Route("/DeleteCompany/" + WebConstants.RouteId)]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            var userId = this.User.GetId();
            if (userId == null)
            {
                return Unauthorized();
            }         
            var deleted = await companies.DeleteCompanyById(id,userId);
            return Ok(deleted);
        }
    }
}
