using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Utils
{
    public class UserSeedData : ISeedData 
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public UserSeedData(UserManager<ApplicationUser> userManager , ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task DataSeed()
        {
            if (!await userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "Admin@Gmail.com",
                    FullName = "Admin",
                    EmailConfirmed = true,
                    Gender = Gender.Male
                };

                var user2 = new ApplicationUser
                {
                    UserName = "Basheer",
                    Email = "Basheer@Gmail.com",
                    FullName = "Basheer Daoud",
                    EmailConfirmed = true,
                    Gender = Gender.Male
                };
                var user3 = new ApplicationUser
                {
                    UserName = "Ali",
                    Email = "Ali@Gmail.com",
                    FullName = "Ali Daoud",
                    EmailConfirmed = true,
                    Gender = Gender.Male
                };

                var result1 = await userManager.CreateAsync(user1, "Admin@123");
                if (! result1.Succeeded)
                {
                    throw new Exception(string.Join(", ",
                result1.Errors.Select(e => e.Description)));
                }
                var result2= await userManager.CreateAsync(user2, "Basheer@123");
                if(!result2.Succeeded)
                {
                    throw new Exception(string.Join(", ",
                result2.Errors.Select(e => e.Description)));
                }
                var result3= await userManager.CreateAsync(user3, "Ali@1234");
                if (!result3.Succeeded)
                {
                    throw new Exception(string.Join(", ",
                result3.Errors.Select(e => e.Description)));
                }

                var doctor = new Doctor
                {
                    Specialization = "General",
                    UserId = user2.Id,
                    Certificate="MBBS"
                };
                
                var patient = new Patient
                {
                    UserId = user3.Id,
                    Age = 22,
                    Height = 180,
                    Weight = 75,
                };
                await context.Doctors.AddAsync(doctor);
                await context.Patients.AddAsync(patient);
                await context.SaveChangesAsync();

                await userManager.AddToRoleAsync(user1, "Admin");
                await userManager.AddToRoleAsync(user2, "Doctor");
                await userManager.AddToRoleAsync(user3, "Patient");
            }
        }
    }
}
