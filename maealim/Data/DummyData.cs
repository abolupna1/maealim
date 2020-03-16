using Microsoft.AspNetCore.Identity;
using maealim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Data
{
    public class DummyData
    {
        
        public static async Task Initialize(ApplicationDbContext context,
                                           UserManager<AppUser> userManager,
                                           RoleManager<AppRole> roleManager)
        {
            context.Database.EnsureCreated();
            int adminID = 0;
            string roleAdmin = "Admin";
            string roleAdminAr = "مدير النظام";
            string ProgramsSupervisor = "ProgramsSupervisor";
            string ProgramsSupervisorAr = "مشرف البرامج";
            string roleGuide = "RoleGuide";
            string roleGuideAr = "مرشد";
            string roleNotable = "RoleNotable";
            string roleNotableAr = "الوجهاء";
            string roleSheikh = "RoleSheikh";
            string roleSheikhAr = "الشيخ";
            string roleMaealim = "RoleMaealim";
            string roleMaealimAr = "المعالم";


            if (await roleManager.FindByNameAsync(roleAdmin) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleAdmin, roleAdminAr));
            }

            if (await roleManager.FindByNameAsync(roleNotable) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleNotable, roleNotableAr));
            }

            if (await roleManager.FindByNameAsync(roleMaealim) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleMaealim, roleMaealimAr));
            }


            if (await roleManager.FindByNameAsync(roleSheikh) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleSheikh, roleSheikhAr));
            }
            if (await roleManager.FindByNameAsync(roleNotable) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleNotable, roleAdminAr));
            }

            if (await roleManager.FindByNameAsync(ProgramsSupervisor) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(ProgramsSupervisor, ProgramsSupervisorAr));
            }

            if (await roleManager.FindByNameAsync(roleGuide) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleGuide, roleGuideAr));
            }

            string password = "2329472589";

            if (await userManager.FindByNameAsync("abolupna1@gmail.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "abolupna1@gmail.com",
                    Email = "abolupna1@gmail.com",
                    FullName = "ايدوم ابراهيم"

                };
            
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, roleAdmin);
                 
                }
                adminID = user.Id;
            }

        }
    }
}
