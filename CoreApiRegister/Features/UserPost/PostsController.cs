using CoreApiRegister.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoreApiRegister.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;
using CoreApiRegister.Features.UserPost.Models;

namespace CoreApiRegister.Features.UserPost
{
    [Authorize]
    public class PostsController : ApiController
    {

        private readonly IPostsService service;

        public PostsController(IPostsService service)
            => this.service = service;


        [HttpGet]
        [Route("/Posts")]
        public async Task<IActionResult> Posts()
        {
            var userId = this.User.GetId();
            if(userId == null)
            {
                return Unauthorized();
            }
            try
            {
                var result = await service.GetAllPostsByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpPost]
        [Route("/Posts")]
        public async Task<IActionResult> Posts(PostRequestModel vm)
        {
            var userId = this.User.GetId();

            if (userId == null)
            {
                return Unauthorized();
            }
            try
            {
                vm.UserId = userId;
                var result = await service.CreatePost(vm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
