using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.Authentication
{
    public class AuthenticationResponseModel
    {
        public bool success { get; set; }
        public string accessToken { get; set; }
        public string username { get; set; }
        public string errorMessage { get; set; } 
    }
}
