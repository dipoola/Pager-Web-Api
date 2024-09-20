using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pager.Data;
using Pager.Model;
using Pager.Model.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;

namespace Pager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public DetailsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
     public IActionResult GetAllDetails()
        {
           var alldetails=  dbContext.GetDetails.ToList();
            return Ok(alldetails);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetDetailsById(Guid id) 
        {
           var Details = dbContext.GetDetails.Find(id);
            if(Details is null)
            {
                return NotFound();
            }
           return Ok(Details);
        }

        [HttpPost]
        public IActionResult AddDetails( AddDetailsDto addDetailsDto )

        {
            var DetailsEntity = new Details()
            {
                Email = addDetailsDto.Email,
                Name = addDetailsDto.Name,
                Age = addDetailsDto.Age,
                Phone = addDetailsDto.Phone,
                Salary = addDetailsDto.Salary,
            };

            dbContext .GetDetails.Add(DetailsEntity );
            dbContext.SaveChanges();
            return Ok(DetailsEntity);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateDetails(Guid id, UpdateDetailsDto updateDetailsDto)
        {
            var DetailInfo= dbContext.GetDetails.Find(id);  
            if( DetailInfo is null)
            {
                return NotFound();
            }
            DetailInfo.Name = updateDetailsDto.Name;
            DetailInfo.Age = updateDetailsDto.Age;  
            DetailInfo.Salary = updateDetailsDto.Salary;
            DetailInfo.Email = updateDetailsDto.Email;  
            DetailInfo.Phone = updateDetailsDto.Phone;
            dbContext .SaveChanges();
            return Ok( DetailInfo );
        }

        [HttpDelete]
        public IActionResult DeleteDetails( Guid id )
        {
            var details = dbContext.GetDetails.Find(id);
            if ( details is null)
            {
                return NotFound();  
            }
             dbContext.GetDetails.Remove(details);
             dbContext.SaveChanges();   
            return Ok();    

            
        }
    }
}
