

using System.ComponentModel.DataAnnotations;
using static CoreApiRegister.Data.ValidationConst.CompanyConst;
namespace CoreApiRegister.Features.Companies
{
    public class CreateCompanyRequestModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
        [MaxLength(MaxAddressLength)]
        public string Address { get; set; }
        public string UrlImage { get; set; }
        public string UserId { get; set; }
    }
}
