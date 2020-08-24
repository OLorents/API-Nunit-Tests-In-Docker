using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        private CompanyDbContext companyDbContext;

        public CompanyController(CompanyDbContext _companyDbContext)

        {
            companyDbContext = _companyDbContext;
        }

        // GET: api/Company/GetCompanies
        [HttpGet("GetCompanies")]
        public IEnumerable<Company> Get()
        {
            return companyDbContext.Companies;
        }

        // GET: api/Company/GetCompany/5
        [HttpGet("GetCompany/{id}")]
        public IActionResult Get(string id)
        {
            var company = companyDbContext.Companies.SingleOrDefault(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound("No Record Found...");
            }

            return Ok(company);
        }

        // POST: api/Company/CreateCompany
        [HttpPost("CreateCompany")]
        public IActionResult Post([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            companyDbContext.Companies.Add(company);
            try
            {
                companyDbContext.SaveChanges();
            }

            catch (Exception ex)

            {
                if (ex.InnerException.Message.Contains("Violation of PRIMARY KEY constraint"))

                    return BadRequest($"CompanyId '{company.CompanyId}' is inserted before.");
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Company/UpdateCompany/5
        [HttpPut("UpdateCompany/{id}")]
        public IActionResult Put(string id, [FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            try
            {
                companyDbContext.Companies.Update(company);
                companyDbContext.SaveChanges(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound("No Record Found against this Id...");
            }

            companyDbContext.Companies.Update(company);
            companyDbContext.SaveChanges(true);
            return Ok("Product Updated...");
        }

        // DELETE: api/Company/DeleteCompany/5
        [HttpDelete("DeleteCompany/{id}")]
        public IActionResult Delete(string id)
        {
            var product = companyDbContext.Companies.SingleOrDefault(m => m.CompanyId == id);
            if (product == null)
            {
                return NotFound("No Record Found...");
            }

            companyDbContext.Companies.Remove(product);
            companyDbContext.SaveChanges(true);
            return Ok("Product deleted");
        }
    }
}