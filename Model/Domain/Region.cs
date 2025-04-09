namespace Pager.Model.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
        public string?  Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
    }
}
