using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetworking.Data;
using SocialNetworking.Models;
using Data.Entities;
using Data;

namespace SocialNetworking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {


        private readonly UserManager<ManagerUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ManageAppDbContext _context;
        public UserController(UserManager<ManagerUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ManageAppDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;


        }



        [HttpGet]
       
        public async Task<IActionResult> GetUsers()
        {
            

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var uservms = user.toUserViewModel();

            return Ok(uservms);
        }

        [HttpPost]
       
        public async Task<IActionResult> Edit(UserViewModel  userViewModel)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"Cannot find  user "));


            user.Avatar = userViewModel.Avatar;
            user.Email = userViewModel.Email;
            user.PhoneNumber = userViewModel.PhoneNumber;
            user.FistName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName ;
            user.Birthdate = userViewModel.Birthdate;
            user.Gender = userViewModel.Gender;
            user.UserName = userViewModel.Username;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                // Handle update failure, return appropriate error response
                return BadRequest(new ApiNotFoundResponse($"Failed to update user: {string.Join(", ", result.Errors)}"));
            }

            return NoContent();
        }


    }
}
