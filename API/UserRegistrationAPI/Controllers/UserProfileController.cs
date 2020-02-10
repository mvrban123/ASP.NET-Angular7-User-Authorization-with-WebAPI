using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRegistrationAPI.Models;

namespace UserRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private AuthenticationContext authenticationContext;

        public UserProfileController(UserManager<ApplicationUser> userManager, AuthenticationContext context)
        {
            _userManager = userManager;
            authenticationContext = context;
        }

        [HttpGet]
        [Authorize]
        //Get: /api/UserProfile

        public async Task<Object> GetUserProfile()
        {
            string userID = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userID);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for Admin";
        } 
        
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Route("GetUsers")]
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            //List<ApplicationUserModel> userModels = new List<ApplicationUserModel>();

            var users = await authenticationContext.ApplicationUser.Include(items=>items.RoleType).ToListAsync();

            //var users1 = authenticationContext.ApplicationUser.Where(items => items.Email.Contains("1")).AsQueryable();
            //var users12 = await users1.ToListAsync();

                /*
            for(int i = 0; i<users.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                ApplicationUserModel model = new ApplicationUserModel
                {
                    FullName = users[i].FullName,
                    Email = users[i].Email,
                    UserName = users[i].UserName,
                    Password = users[i].PasswordHash,
                    Role=roles.FirstOrDefault()
                };
                userModels.Add(model);
            }
            
            for(int i = 0; i<users12.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users12[i]);
                ApplicationUserModel model = new ApplicationUserModel
                {
                    FullName = users12[i].FullName,
                    Email = users12[i].Email,
                    UserName = users12[i].UserName,
                    Password = users12[i].PasswordHash,
                    Role=roles.FirstOrDefault()
                };
                userModels.Add(model);
            }
            */
            

            return users;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("ForCustomer")]
        public string GetForCustomer()
        {
            return "Web method for Customer";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [Route("ForAdminOrCustomer")]
        public string GetForAdminOrCustomer()
        {
            return "Web method for Admin or Customer";
        }
    }
}