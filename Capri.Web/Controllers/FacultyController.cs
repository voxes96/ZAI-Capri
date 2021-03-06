﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capri.Database.Entities.Identity;
using Capri.Services.Faculties;
using Capri.Web.Controllers.Attributes;
using Capri.Web.ViewModels.Faculty;

namespace Capri.Web.Controllers
{
    [Route("faculties")]
    public class FacultyController : Controller
    {
        private readonly IFacultyCreator _facultyCreator;
        private readonly IFacultyUpdater _facultyUpdater;
        private readonly IFacultyGetter _facultyGetter;
        private readonly IFacultyDeleter _facultyDeleter;

        public FacultyController(
            IFacultyCreator facultyCreator,
            IFacultyUpdater facultyUpdater,
            IFacultyGetter facultyGetter,
            IFacultyDeleter facultyDeleter)
        {
            _facultyCreator = facultyCreator;
            _facultyUpdater = facultyUpdater;
            _facultyGetter = facultyGetter;
            _facultyDeleter = facultyDeleter;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _facultyGetter.Get(id);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _facultyGetter.GetAll();
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [AllowedRoles(RoleType.Dean)]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] FacultyRegistration registration)
        {
            if(registration == null)
            {
                return BadRequest("Faculty registration not given");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("The given faculty registration is invalid");
            }

            var result = await _facultyCreator.Create(registration);
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
            [FromBody] FacultyRegistration registration)
        {
            //if(id == Guid.Empty)
            //{
            //    return NotFound();
            //}
            
            if(registration == null)
            {
                return BadRequest("Faculty registration not given");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("The given faculty registration is invalid");
            }
            
            var result = await _facultyUpdater.Update(id, registration);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }

        [AllowedRoles(RoleType.Dean)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _facultyDeleter.Delete(id);
            if(result.Successful())
            {
                return Ok(result.Body());
            }
            return BadRequest(result.GetAggregatedErrors());
        }
    }
}