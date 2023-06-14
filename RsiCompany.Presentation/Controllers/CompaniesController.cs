using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsiCompany.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service) => _service = service;
     
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _service.CompanyService.GetAllCompanies(false);
                return Ok(companies);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

    }
}
