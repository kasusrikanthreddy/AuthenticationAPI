using AuthenticationManagement.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AuthenticationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AuthenticationManagementContext context;
        private readonly IConfiguration configuration;
        public SignupController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationManagementContext context, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(Signup dto)
        {
            AppUser user = new AppUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Age = dto.Age,
                DOB = dto.DOB,
                UserName = dto.Username,
                Email = dto.Email_Id,
                PhoneNumber = dto.Phone_number,
                Address = dto.Address,
            };

            if (dto.Role == "Executive")
            {
                Executive executive = new Executive
                {
                    
                    Experience = dto.Experience,
                    AppUser = user
                };
                IdentityResult executiveUser = await userManager.CreateAsync(user, dto.Password);
                bool IsRolePresent = await roleManager.RoleExistsAsync("Executive");
                executiveUser = await userManager.AddToRoleAsync(user, "Executive");
                context.Executives.Add(executive);
                await context.SaveChangesAsync();
                if (executiveUser.Succeeded)
                {
                    return StatusCode(201);
                }
            }
            IdentityResult result = await userManager.CreateAsync(user, dto.Password);


            if (result.Succeeded)
            {
                bool IsRolePresent = await roleManager.RoleExistsAsync("Customer");
                result = await userManager.AddToRoleAsync(user, "Customer");
                if (result.Succeeded)
                {
                    return StatusCode(201);
                }
            }
            return BadRequest(result.Errors);

            

        }

    }
}
