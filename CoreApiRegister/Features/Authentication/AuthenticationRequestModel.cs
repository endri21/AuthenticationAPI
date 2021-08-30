using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.Authentication
{
    public class AuthenticationRequestModel
    {
        [Required(ErrorMessage ="")]
        public string username { get; set; }
        [Required(ErrorMessage ="")]
        public string password { get; set; }
    }
}
