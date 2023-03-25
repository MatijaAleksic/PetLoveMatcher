using DtoNetProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetLoveMatcher_Backend.Models;
using System.Reflection.Emit;

namespace PetLoveMatcher_Backend.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder
            //.Entity<School>()
            //.HasMany(p => p.Movies)
            //.WithMany(p => p.Actors)
            //.UsingEntity(j => j.HasData(new
            //{
            //    ActorsId = actor.Id,
            //    MoviesId = movie.Id
            //}));

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<School>().ToTable("Schools");


            base.OnModelCreating(modelBuilder);



            //new DbInitializer(modelBuilder);


        }

    }
}
