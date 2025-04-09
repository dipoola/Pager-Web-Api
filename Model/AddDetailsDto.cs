using System.ComponentModel.DataAnnotations;

namespace Pager.Model
{
    public class AddDetailsDto
    {
        [Required]
        [MinLength(10,ErrorMessage = "Name has to be maximum of 10 characters")]
        [MaxLength(16, ErrorMessage = "Name has to be maximum of 16 characters")] 
        public  string Name { get; set; }

        public required int Age { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public int Salary { get; set; }
    }
}
