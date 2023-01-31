using System;
using System.Threading.Tasks;
using MfcUfmt.Areas.Identity.Data;
using MfcUfmt.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(MfcUfmt.Areas.Identity.IdentityHostingStartup))]
namespace MfcUfmt.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("IdentityDbContextConnection"), buildr =>
                     buildr.MigrationsAssembly("MfcUfmt")));

                services.AddIdentity<ApplicationUser, IdentityRole>(options => options.Stores.MaxLengthForKeys = 128)
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            });

            

        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
             var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            // string[] roleNames = { "Admin", "User", "HR" };
            //  IdentityResult roleResult;

            /*foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }*/

            ApplicationUser user = await UserManager.FindByEmailAsync("xandelucas@gmail.com");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    Nome = "Alexandre Lucas",
                    UserName = "Alexandre Lucas",
                    Email = "xandelucas@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }
              await UserManager.AddToRoleAsync(user, "Admin");


            ApplicationUser user1 = await UserManager.FindByEmailAsync("teste@gmail.com");

            if (user1 == null)
            {
                user1 = new ApplicationUser()
                {
                    Nome = "Teste2",
                    UserName = "Teste",
                    Email = "teste2@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123");
            }
             await UserManager.AddToRoleAsync(user1, "User");

            ApplicationUser user2 = await UserManager.FindByEmailAsync("teste3@gmail.com");

            if (user2 == null)
            {
                user2 = new ApplicationUser()
                {
                    Nome = "Teste3",
                    UserName = "Teste",
                    Email = "teste3@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123");
            }
             await UserManager.AddToRoleAsync(user1, "HR");


        }
    }


}