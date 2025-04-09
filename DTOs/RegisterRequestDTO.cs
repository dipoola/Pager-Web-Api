using System.ComponentModel.DataAnnotations;

namespace Pager.DTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public String Username { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public String Password { get; set; }    
        public string[] Roles { get; set; }
    }
}
