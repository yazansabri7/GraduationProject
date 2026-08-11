using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Utils
{
    public class RoleSeedData : ISeedData
    {
        public RoleManager<IdentityRole> roleManager { get; }
        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }


        public async Task DataSeed()
        {
            string[] roles = ["Admin", "Patient", "Doctor"];
            if (!await roleManager.Roles.AnyAsync())
            {

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
