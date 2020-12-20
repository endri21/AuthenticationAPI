

using CoreApiRegister.Features.UserPost.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.UserPost
{
    public interface IPostsService
    {
        Task<List<PostResponseModel>> GetAllPostsByUserId(string userId);
        Task<PostResponseModel> CreatePost(PostRequestModel vm);
    }
}
