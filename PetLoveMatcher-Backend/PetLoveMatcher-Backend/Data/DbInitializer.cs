using DtoNetProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveMatcher_Backend.Models;

namespace PetLoveMatcher_Backend.Data
{
    public class DbInitializer
    {

        //private readonly ModelBuilder modelBuilder;

        //public DbInitializer(ModelBuilder modelBuilder)
        //{
        //    this.modelBuilder = modelBuilder;
        //    SeedData();
        //}

        public static async Task SeedData(DbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            User user1 = new User("Pero", "Pero", "pero9", "pero@gmail.com", "062-123-123123");
            User user2 = new User("Milan", "Milanovic", "milan9", "milan@gmail.com", "062-223-1113");

            if (await userManager.FindByNameAsync(user1.UserName) == null)
            {
                var result = await userManager.CreateAsync(user1);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user1, "peropero");
                    await userManager.AddToRoleAsync(user1, "Admin");
                }
            }

            if (await userManager.FindByNameAsync(user2.UserName) == null)
            {
                var result = await userManager.CreateAsync(user2);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user2, "milanmilan");
                    await userManager.AddToRoleAsync(user2, "User");
                }
            }

            School school1 = new School("Skola1");
            School school2 = new School("Skola2");

            school1.Students.Add(user1);
            school2.Students.Add(user2);

            context.Add(school1);
            context.Add(school2);

            context.SaveChanges();


            // Hash the password


            //List<IdentityRole> rolesList = new List<IdentityRole>
            //{
            //    new IdentityRole() { Name  = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
            //    new IdentityRole() { Name  = "User", ConcurrencyStamp = "1", NormalizedName = "User"},
            //};

            //List<IdentityUserRole<string>> userRolesList = new List<IdentityUserRole<string>>
            //{
            //    new IdentityUserRole<string>() { RoleId = rolesList[0].Id, UserId = userList[0].Id },
            //    new IdentityUserRole<string>() { RoleId = rolesList[1].Id, UserId = userList[1].Id },
            //};

            //modelBuilder.Entity<User>().HasData(userList);

            //modelBuilder.Entity<IdentityRole>().HasData(rolesList);
            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRolesList);


        }

    }
}
