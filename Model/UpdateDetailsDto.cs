namespace Pager.Model
{
    public class UpdateDetailsDto
    {
        public required string Name { get; set; }

        public required int Age { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
