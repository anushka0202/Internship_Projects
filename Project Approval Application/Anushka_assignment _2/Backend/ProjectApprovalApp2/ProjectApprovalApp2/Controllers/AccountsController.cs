using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectApprovalApp2.DTOs.Account;
using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            //checking is user already exists
            var userExists = await userManager.FindByNameAsync(registerDto.UserName);
            if(userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { Status = "Error", Message = "Username already exixts" });
            }
            
            //if user doesn't exists, we create a new user
            AppUser user = new AppUser()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.UserName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { Status = "Error", Message = "User creation failed" });
            }

            //If Admin Role does not exit then create admin role in the database
            if (!await roleManager.RoleExistsAsync(UserRolesDto.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRolesDto.Admin));
            }

            //If User Role does not exit then create user role in the database
            if (!await roleManager.RoleExistsAsync(UserRolesDto.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRolesDto.User));
            }

            //If User Role already exits, add the current user with role = user
            if (await roleManager.RoleExistsAsync(UserRolesDto.User))
            {
                await userManager.AddToRoleAsync(user, UserRolesDto.User);
            }

            return Ok(new ResponseDto { Status = "Success", Message = "User created successfully" });

        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //Finding the user
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if(user != null && await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginDto.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                //if user is authenticated
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                //defining token
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.UserName
                });
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseDto { Status = "Error", Message = "User not found" });
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            //checking is user already exists
            var userExists = await userManager.FindByNameAsync(registerDto.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { Status = "Error", Message = "Username already exixts" });
            }

            //if user doesn't exists, we create a new user
            AppUser user = new AppUser()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.UserName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { Status = "Error", Message = "User creation failed" });
            }

            //this will create a role inside data table
            //If Admin Role does not exit then create admin role in the database
            if (! await roleManager.RoleExistsAsync(UserRolesDto.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRolesDto.Admin));
            }

            //If User Role does not exit then create user role in the database
            if (!await roleManager.RoleExistsAsync(UserRolesDto.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRolesDto.User));
            }

            //If Admin Role already exits, add the current user with role = admin
            if (await roleManager.RoleExistsAsync(UserRolesDto.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRolesDto.Admin);
            }

            return Ok(new ResponseDto { Status = "Success", Message = "User created successfully" });

        }
    }
}
