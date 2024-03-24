﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application;
namespace SchoolApp.Admin.Infrastructure.Identity;

public class AppIdentityDbContextSeed(
    ILogger<AppIdentityDbContextSeed> logger, UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager) : IDbSeeder<AppIdentityDbContext>
{
    public async Task SeedAsync(AppIdentityDbContext context)
    {
        context.Database.OpenConnection();

        if (context.Database.IsSqlServer())
        {
            context.Database.Migrate();
        }

        await roleManager.CreateAsync(new IdentityRole(Constants.Roles.ADMINISTRATORS));

        var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
        await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

        string adminUserName = "admin@microsoft.com";
        var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
        await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
        adminUser = await userManager.FindByNameAsync(adminUserName);
        if (adminUser != null)
        {
            await userManager.AddToRoleAsync(adminUser, Constants.Roles.ADMINISTRATORS);
        }
    }
}
