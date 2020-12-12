
namespace CoreApiRegister.Features.Companies.Models
{
    public class CompanyDetailsServiceModel : CompanyListingServiceModel
    { 
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
    }
}
