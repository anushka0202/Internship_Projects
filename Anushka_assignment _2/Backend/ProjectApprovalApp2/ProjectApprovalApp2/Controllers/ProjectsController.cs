using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApprovalRepo.Data;
using ProjectApprovalRepo.Data.Repository;
using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectApprovalApp2.DTOs.Account;

namespace ProjectApprovalApp2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepo _repository;

        public ProjectsController(IProjectsRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _repository.GetAllProjects();
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectWithUserAsync(int id)
        {
            var project = await _repository.GetProjectWithUserAsync(id);

            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }


        [Authorize(Roles = UserRolesDto.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProject(Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            await _repository.CreateProject(project);

            return CreatedAtAction(nameof(GetProjectWithUserAsync), new { id = project.Id }, project);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateProject(id, project);

            return NoContent();
        }


        [Authorize(Roles = UserRolesDto.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var projectToDelete = await _repository.GetProjectWithUserAsync(id);
            if (projectToDelete == null)
            {
                return NotFound();
            }

            await _repository.DeleteProject(projectToDelete);
            return NoContent();
        }
    }

    
}
