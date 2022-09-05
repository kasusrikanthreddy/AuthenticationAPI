using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationManagement.API
{
    public class DataSeeder
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly AuthenticationManagementContext context;

        public DataSeeder(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AuthenticationManagementContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task SeedRoles()
        {
            var role1 = new IdentityRole { Name = "Customer" };
            var role2 = new IdentityRole { Name = "Executive" };
            var role3 = new IdentityRole { Name = "Admin" };
            if (!await roleManager.RoleExistsAsync("Customer"))
                await roleManager.CreateAsync(role1);
            if (!await roleManager.RoleExistsAsync("Executive"))
                await roleManager.CreateAsync(role2);
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(role3);
        }

        public async Task SeedAdmin()
        {
            var Admin = new AppUser
            {
                FirstName = "Srikanth",
                LastName = "Kasu",
                Gender = "Male",
                Age = "22",
                DOB = "11/01/2001",
                Email = "Srikanth@gmail.com",
                PhoneNumber = "1234567890",
                Address = "Hyderabad",
                UserName = "Admin",
            };
            if (await userManager.FindByNameAsync("Admin") == null)
            {
                await userManager.CreateAsync(Admin, "Admin@123");
                await userManager.AddToRoleAsync(Admin, "Admin");
                var admindetails = new Admin() { AppUser = Admin, Location = "Hyderabad", Id=Admin.Id };
                context.Admins.Add(admindetails);
                await context.SaveChangesAsync();
            }
        }
        public async Task SeedUsers()
        {
            
            var Executive = new AppUser
            {
                FirstName = "Bhavya",
                LastName = "T",
                Gender = "Female",
                Age = "22",
                DOB = "28/11/2000",
                Email = "bhavya@gmail.com",
                PhoneNumber = "9876543210",
                Address = "Hyderabad",
                UserName = "bhavya@outlook.com",

            };

            var Customer = new AppUser
            {
                FirstName = "Gayatri",
                LastName = "D",
                Gender = "Female",
                Age = "21",
                DOB = "15/09/2001",
                Email = "gayatri@gmail.com",
                PhoneNumber = "7412589630",
                Address = "Hyderabad",
                UserName = "gayatri@outlook.com",

            };

            if (await userManager.FindByNameAsync("bhavya@outlook.com") == null)
            {
                await userManager.CreateAsync(Executive, "Bhavya@123");
                await userManager.AddToRoleAsync(Executive, "Executive");
                var executivedetails = new Executive() { AppUser = Executive, Experience = "2" };
                context.Executives.Add(executivedetails);
                await context.SaveChangesAsync();
            }

            if (await userManager.FindByNameAsync("gayatri@outlook.com") == null)
            {
                await userManager.CreateAsync(Customer, "Gayatri@1234");
                await userManager.AddToRoleAsync(Customer, "Customer");
                await context.SaveChangesAsync();
            }
        }
        }
    }
