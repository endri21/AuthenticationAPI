
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoreApiRegister.Data.ValidationConst.CompanyConst;

namespace CoreApiRegister.Data.Models
{
    [Table("Company")]
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(MaxAddressLength)]
        public string Address { get; set; }
        public string UrlImage { get; set; }
        public User user { get; set; }//useri qe eshte i regjistruar mund te shtoje nje universitet ..../ me pas do te behet me rol
     //   public int Invalitated { get; set; }
       // public DateTime CreateAt { get; set; }
        //public DateTime ModifiedAt { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
