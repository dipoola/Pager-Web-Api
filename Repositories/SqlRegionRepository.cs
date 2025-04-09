using Microsoft.EntityFrameworkCore;
using Pager.Data;
using Pager.Model.Domain;
using System.Runtime.InteropServices;

namespace Pager.Repositories
{
    public class SqlRegionRepository : IRegionrepository

    {
        private readonly ApplicationDbContext dbContext;

        public SqlRegionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Region> CreateRegionAsync(Region region)
        {
           await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync(); 
            return region;  
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return null;
            }
            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();
            return (regionDomainModel);
        }

        public async Task<List<Region>> GetAllRegionAsync()
        {
           return  await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionsByIdAsync(Guid Id)
        {
          return  await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == Id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(regionDomainModel == null)
            {
                return null;
            }
            regionDomainModel.Name = region.Name;   
            regionDomainModel.Phone = region.Phone; 
            regionDomainModel.Email = region.Email; 

            await dbContext.SaveChangesAsync();  
            return (regionDomainModel); 
        }
    }
}
