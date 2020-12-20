

using System;

namespace CoreApiRegister.Features.UserPost.Models
{
    public class PostRequestModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public string UserId { get; set; }
        public string UserName {get;set;}
    }
}
