using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoreApiRegister.Data.ValidationConst.CompanyConst;

namespace CoreApiRegister.Data.Models
{
    [Table("UserPost")]
    public class UserPost
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(MaxAddressLength)]
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public User user { get; set; }
        public string IdUser { get; set; }
    }
}
