using Microsoft.EntityFrameworkCore;
using Pager.Data;
using Pager.Model.Entities;

namespace Pager.Repositories
{
    public class SQLDetailsRepository : IDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SQLDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Details> CreateDetailsAsync(Details details)
        {
            await dbContext.GetDetails.AddAsync(details); 
            await dbContext.SaveChangesAsync(); 
            return details; 

        }

        public async Task<Details?> DeleteDetailsAsync(Guid Id)
        {
            var existingDetails = await dbContext.GetDetails.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingDetails == null)
            {
                return null;
            }

             dbContext.GetDetails.Remove(existingDetails);    
             await dbContext.SaveChangesAsync(); 
              return(existingDetails);
        }

        public async Task<List<Details>> GetAllDetailsAsync()
        {
          return await dbContext.GetDetails.ToListAsync();    
        }

        public async Task<Details?> GetDetailsByIdAsync(Guid Id)
        {
          return  await dbContext.GetDetails.FirstOrDefaultAsync(x => x.Id == Id); 

        }

        public async Task<Details?> UpdateDetailsAsync(Guid id, Details details)
        {
            var DotDetails= await dbContext.GetDetails.FirstOrDefaultAsync(x =>x.Id == id);   
            if (DotDetails == null)
            {
                return null;
            }

            DotDetails.Id= details.Id;
            DotDetails.Name = details.Name; 
            DotDetails.Salary= details.Salary;  
            await dbContext.SaveChangesAsync();
            return DotDetails;
        }
    }
}
