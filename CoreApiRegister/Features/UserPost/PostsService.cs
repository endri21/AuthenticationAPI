
using CoreApiRegister.Data;
using CoreApiRegister.Features.UserPost.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiRegister.Data.Models;

namespace CoreApiRegister.Features.UserPost
{
    public class PostsService : IPostsService
    {

        private readonly ApplicationDbContext data;

        public PostsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<PostResponseModel> CreatePost(PostRequestModel vm)
        {
            try
            {
                var post = new Data.Models.UserPost
                {
                    Description = vm.Description,
                    CreateAt = vm.CreateAt,
                    IdUser = vm.UserId
                };
                data.UserPost.Add(post);
                await  data.SaveChangesAsync();
                return  new PostResponseModel
                {
                    Description = post.Description,
                    CreateAt = post.CreateAt,
                    UserId = post.IdUser

                };
            }
            catch (System.Exception)
            {

                return new PostResponseModel();
            }
        }

        public async Task<List<PostResponseModel>> GetAllPostsByUserId(string userId)
        {
            try
            {
                return  await data.UserPost.Where(a => a.IdUser == userId)
                    .Select(p => new PostResponseModel
                    {
                        Description = p.Description,
                        CreateAt = p.CreateAt,
                        UserName = p.user.UserName

                    }).ToListAsync();
            }
            catch (System.Exception)
            {

                return new List<PostResponseModel>();
            }
        }
    }
}
