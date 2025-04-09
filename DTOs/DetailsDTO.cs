using System.ComponentModel.DataAnnotations;

namespace Pager.DTOs
{
    public class DetailsDTO
    {
        public Guid Id { get; set; }
        [MinLength(5,ErrorMessage = "Name must be minimum of 5 characters")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters")]
        public required string Name { get; set; }

        public required int Age { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }


    }
}
