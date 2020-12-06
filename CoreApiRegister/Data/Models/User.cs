using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

namespace CoreApiRegister.Data.Models
{
    public class User : IdentityUser
    {

        public IEnumerable<Company> Companies { get;  } = new HashSet<Company>();
    }
}
