using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capri.Database.Entities.Identity;
using Capri.Services.Students;
using Capri.Web.Controllers.Attributes;
using Capri.Web.ViewModels.Student;

namespace Capri.Web.Controllers
{
    [Route("students")]
    public class StudentController : Controller
    {
        private readonly IStudentCreator _studentCreator;
        private readonly IStudentUpdater _studentUpdater;
        private readonly IStudentGetter _studentGetter;
        private readonly IStudentDeleter _studentDeleter;

        public StudentController(
            IStudentCreator studentCreator,
            IStudentUpdater studentUpdater,
            IStudentGetter studentGetter,
            IStudentDeleter studentDeleter)
        {
            _studentCreator = studentCreator;
            _studentUpdater = studentUpdater;
            _studentGetter = studentGetter;
            _studentDeleter = studentDeleter;
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _studentGetter.Get(id);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [HttpGet("{indexNumber:int}")]
        public async Task<IActionResult> GetByIndex(int indexNumber)
        {
            var result = await _studentGetter.GetByIndex(indexNumber);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _studentGetter.GetAll();
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        //[AllowedRoles(RoleType.Dean)]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] StudentRegistration registration)
        {
            if(registration == null)
            {
                return BadRequest("Student registration not given");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("The given student registration is invalid");
            }

            var result = await _studentCreator.Create(registration);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [AllowedRoles(RoleType.Dean)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] StudentRegistration registration)
        {
            //if(id == Guid.Empty)
            //{
            //    return NotFound();
            //}
            
            if(registration == null)
            {
                return BadRequest("Student registration not given");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("The given student registration is invalid");
            }
            
            var result = await _studentUpdater.Update(id, registration);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [AllowedRoles(RoleType.Dean)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentDeleter.Delete(id);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [AllowedRoles(RoleType.Dean)]
        [HttpDelete("{indexNumber:int}")]
        public async Task<IActionResult> DeleteByIndex(int indexNumber)
        {
            var result = await _studentDeleter.DeleteByIndex(indexNumber);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }
    }
}