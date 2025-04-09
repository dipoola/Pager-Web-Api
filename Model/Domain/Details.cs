using System.ComponentModel.DataAnnotations;

namespace Pager.Model.Entities
{
    public class Details
    {
        
        public Guid Id { get; set; }
        
        public required  string Name { get; set; }

        public   int Age { get; set; }
        public  string Email { get; set; }
        public string? Phone { get; set; }
        public int Salary { get; set; }
    }
}
