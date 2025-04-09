using Pager.Model.Domain;

namespace Pager.Repositories
{
    public interface IRegionrepository
    {
        Task<List<Region>> GetAllRegionAsync();
        Task<Region?> GetRegionsByIdAsync(Guid Id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, Region region);
        Task<Region?> DeleteRegionAsync(Guid id);


    }
}
