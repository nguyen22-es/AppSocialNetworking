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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace SocialNetworking.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ManagerUser> _userManager;
        private readonly SignInManager<ManagerUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ManageAppDbContext _context;
        public UserController(UserManager<ManagerUser> userManager,
            RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor,
            ManageAppDbContext context, IConfiguration configuration)
        {
            _httpContextAccessor = contextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;


        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
     

           
        
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var uservm = new UserViewModel

                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Birthdate = user.Birthdate,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FistName,
                        LastName = user.LastName,
                        Avatar = user.Avatar,
                        Gender = user.Gender
                    };

                    return Ok(uservm);
                }

            
            



                return NoContent(); 

            

          
            

          



            /*  var users = _userManager.Users;

            var uservms = await users.Select(user => new UserViewModel() // vì muốn xem lên ta dùng UserVm
             {
                 Id = user.Id,
                 Username = user.UserName,
                 Birthdate = user.Birthdate,
                 Email = user.Email,
                 PhoneNumber = user.PhoneNumber,
                 FirstName = user.FistName, // Sửa lỗi chính tả ở đây
                 LastName = user.LastName

             }).ToListAsync();

             return Ok(uservms);*/
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

        private string IdentityName
        {
            get { return User.Identity.Name; }
        }
    }
}
