using Microsoft.EntityFrameworkCore;
using RecipePortalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePortalWebAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<Recipe>()
                .HasOne<User>(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(c => c.UserId)
                .HasPrincipalKey(c => c.id);

            modelbuilder.Entity<Comment>()
                .HasOne<User>(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .HasPrincipalKey(c => c.id)
                .OnDelete(DeleteBehavior.Restrict);


            modelbuilder.Entity<Comment>()
                .HasOne<Recipe>(c => c.Recipe)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.RecipeId)
                .HasPrincipalKey(c => c.id)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
