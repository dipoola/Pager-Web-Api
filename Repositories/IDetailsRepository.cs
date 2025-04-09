using Pager.Model.Entities;
using System.Runtime.InteropServices;

namespace Pager.Repositories
{
    public interface IDetailsRepository
    {
       Task<List<Details>> GetAllDetailsAsync();

       Task<Details?> GetDetailsByIdAsync(Guid Id);
       Task<Details> CreateDetailsAsync(Details details);
       Task<Details?> UpdateDetailsAsync (Guid id,  Details details);
       Task<Details?>   DeleteDetailsAsync(Guid Id);
    }
}
