using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApprovalRepo.Data;
using ProjectApprovalRepo.Data.Repository;
using ProjectApprovalApp2.DTOs;
using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repository;

        public UsersController(IUsersRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserWithProjectsAsync(string id)
        {
            var user = await _repository.GetUserWithProjectsAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
