using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pager.Data;
using Pager.DTOs;
using Pager.Model.Domain;
using Pager.Repositories;

namespace Pager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RegionController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRegionrepository regionrepository;

        public RegionController(ApplicationDbContext dbContext, IRegionrepository regionrepository)
        {
            this.dbContext = dbContext;
            this.regionrepository = regionrepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllRegionAsync()
        {
            var regionDomainModel = await regionrepository.GetAllRegionAsync();

            //maping Domain Model to DTO

            var regiondto = new List<RegionDTO>();
            foreach (var regionDomain in regionDomainModel)
            {
                regiondto.Add(new RegionDTO()
                {
                    Name = regionDomain.Name,
                    Email = regionDomain.Email,
                    Phone = regionDomain.Phone,
                });


            }
            //return DTO
            return Ok(regiondto);
        }

        [HttpGet]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> GetRegionByID([FromRoute] Guid id)
        {
            var regionDomainModel = await regionrepository.GetRegionsByIdAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }
            //Map Domain model to DTO

            var regiondto = new RegionDTO()
            {

                Name = regionDomainModel.Name,
                Email = regionDomainModel.Email,
                Phone = regionDomainModel.Phone,
            };

            //RETURN DTO BACK TO CLIENT
            return Ok(regiondto);

        }

        [HttpPost]
        public async Task<IActionResult> CreateRegionAsync([FromBody] AddRegionDto addRegionDto)
        {
            //Map Dto to Domain Model

            var regionDomainModel = new Region()
            {
                Email = addRegionDto.Email,
                Phone = addRegionDto.Phone,
                Name = addRegionDto.Name,

            };
            // Use Domain Model to create or add  Details

            regionDomainModel = await regionrepository.CreateRegionAsync(regionDomainModel);

            // Map domain model back to DTO
            var regiondto = new RegionDTO()
            {
                Name = regionDomainModel.Name,
                Email = regionDomainModel.Email,
                Phone = regionDomainModel.Phone,
                Id = regionDomainModel.Id,

            };
            return CreatedAtAction(nameof(GetRegionByID), new { id = regiondto.Id }, regiondto);


        }

        [HttpPut]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)

        {


            //Map domain model to dto

            var regionDomainModel = new Region()
            {
                Email = updateRegionDto.Email,
                Phone = updateRegionDto.Phone,
                Name = updateRegionDto.Name,

            };

            // Check if Details exist

            var regionDomain = await regionrepository.UpdateRegionAsync(id, regionDomainModel);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain Model backt to DTO

            var regiondto = new RegionDTO()
            {
                Name = regionDomainModel.Name,
                Email = regionDomainModel.Email,
                Phone = regionDomainModel.Phone,
                Id = regionDomainModel.Id,

            };
            return Ok(regiondto);
        }

        
        
           


       
        


           




        
    }
}
