using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CoreApiRegister.Data.Models
{
    public class User : IdentityUser
    {

        public IEnumerable<Company> Companies { get;  } = new HashSet<Company>();
        //public int Invalitated { get; set; }
        //public DateTime CreateAt { get; set; }
        
    }
}
