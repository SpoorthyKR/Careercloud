using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationController()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);
        }
        [HttpGet]
        [Route("jobeducation/{id}")]
        
        public ActionResult GetCompanyJobEducation(Guid Id)
        {
            CompanyJobEducationPoco poco = _logic.Get(Id);
            if (poco == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(poco);
            }

        }

        [HttpGet]
        [Route("jobeducation/")]
        
        public ActionResult GetAllCompanyJobEducation()
        {
            var Company = _logic.GetAll();
            if (Company == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Company);
            }
        }

        [HttpPost]
        [Route("jobeducation/")]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobeducation/")]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobeducation/")]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}