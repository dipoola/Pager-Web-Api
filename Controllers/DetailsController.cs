using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pager.Data;
using Pager.DTOs;
using Pager.Model;
using Pager.Model.Entities;
using Pager.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;

namespace Pager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class DetailsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDetailsRepository detailsRepository;

        public DetailsController(ApplicationDbContext dbContext, IDetailsRepository detailsRepository)
        {
            this.dbContext = dbContext;
            this.detailsRepository = detailsRepository;
        }
        // GET ALL DETAILS
        [HttpGet]
     public async Task <IActionResult> GetAllDetails()
        {
           
              //get data from database - Domain model
                var detailsDomain = await detailsRepository.GetAllDetailsAsync();

                // Map Domain models to DTO

                var detailsDto = new List<DetailsDTO>();

                foreach (var detailDomain in detailsDomain)
                {
                    detailsDto.Add(new DetailsDTO()
                    {
                        Id = detailDomain.Id,
                        Name = detailDomain.Name,
                        Age = detailDomain.Age,
                        Email = detailDomain.Email,
                        Phone = detailDomain.Phone,
                        Salary = detailDomain.Salary,

                    });

                    
                }
                  // returns DTO
                   return Ok(detailsDto);
            
           
        }

        // GET SINGLE DETAILS(GET DETAILS BY ID)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task <IActionResult> GetDetailsById([FromRoute] Guid id) 
        {
            try
                //Get Details Domain model from Database
            {   var DetailsDomain = await detailsRepository.GetDetailsByIdAsync(id);    
               
                if (DetailsDomain is null)
                {
                    return NotFound();
                }

                //Map/Convert  Details Domain Model To Details DTO

                var detailsDto = new DetailsDTO()
                {
                    Id= DetailsDomain.Id,
                    Name = DetailsDomain.Name,  
                    Age = DetailsDomain.Age,    
                    Email = DetailsDomain.Email,    
                    Phone = DetailsDomain.Phone,
                    Salary= DetailsDomain.Salary,
                };

                //return DTO back to client
                return Ok(detailsDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the Database");
            }
        }
        // POST TO CREATE/ADD NEW DETAILS
        [HttpPost]

        public async Task <IActionResult> CreateDetails([FromBody ] AddDetailsDto addDetailsDto )

        {
            
               if(ModelState.IsValid)
            {

                // Map or convert DTO to Domain Model
                var detailsDomainModel = new Details()
                {
                    Email = addDetailsDto.Email,
                    Name = addDetailsDto.Name,
                    Age = addDetailsDto.Age,
                    Phone = addDetailsDto.Phone,
                    Salary = addDetailsDto.Salary,
                };


                // Use Domain Model to create or add  Details

                detailsDomainModel = await detailsRepository.CreateDetailsAsync(detailsDomainModel);


                // Map domain model back to DTO

                var detailDto = new DetailsDTO
                {
                    Id = detailsDomainModel.Id,
                    Name = detailsDomainModel.Name,
                    Age = detailsDomainModel.Age,
                    Phone = detailsDomainModel.Phone,
                    Email = detailsDomainModel.Email,
                    Salary = detailsDomainModel.Salary,

                };
                return CreatedAtAction(nameof(GetDetailsById), new { id = detailDto.Id }, detailDto);

            }
            else
            {
                return BadRequest();
            }






        }
         // Update Details
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task <IActionResult> UpdateDetailsAsync([FromRoute]Guid id, [FromBody] UpdateDetailsDto updateDetailsDto)
        {
           

               if(ModelState.IsValid)
            
                {   //Map DTO to Domain Model
                    var detailsDomainModel = new Details
                    {
                        Name = updateDetailsDto.Name,
                        Age = updateDetailsDto.Age,
                        Phone = updateDetailsDto.Phone,
                        Email = updateDetailsDto.Email,
                    };

                    // Check if Details exist

                    detailsDomainModel = await detailsRepository.UpdateDetailsAsync(id, detailsDomainModel);

                    if (detailsDomainModel is null)
                    {
                        return NotFound();
                    }
                    //Map Domain Model backt to DTO

                    var detaildto = new DetailsDTO
                    {
                        Id = detailsDomainModel.Id,
                        Name = detailsDomainModel.Name,
                        Age = detailsDomainModel.Age,
                        Salary = detailsDomainModel.Salary,
                        Email = detailsDomainModel.Email,
                        Phone = detailsDomainModel.Phone,

                    };

                    //return DTO
                    return Ok(detaildto);
                }
            else
            {
                return BadRequest(ModelState);
            }
           
            
           
            

               
            
        }

        [HttpDelete]

        [Route("{id:Guid}")]
        public async Task <IActionResult> DeleteDetails( [FromRoute] Guid id )
        {
            try
            {    var detailDomainModel= await detailsRepository.DeleteDetailsAsync(id);
             
                if (detailDomainModel is null)
                {
                    return NotFound();
                }
                

                //return deleted details back 
                //Map Domain Model to DTO
                var detailDto = new DetailsDTO
                {
                    Id = detailDomainModel.Id,
                    Name = detailDomainModel.Name,
                    Age = detailDomainModel.Age,
                    Salary = detailDomainModel.Salary,
                    Email = detailDomainModel.Email,
                    Phone = detailDomainModel.Phone,

                };
                return Ok(detailDto);


            }
            catch (Exception)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting  contact record ");
            }
            
        }
    }
}
