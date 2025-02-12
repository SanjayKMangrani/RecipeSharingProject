using Microsoft.EntityFrameworkCore;
using RecipeSharingBackend.Models;

namespace RecipeSharingBackend.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between User and Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) 
                .WithMany() 
                .HasForeignKey(u => u.RoleId); // Foreign key is RoleId in the Users table

            // Configure the relationship between Recipe and User
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.User) // A Recipe has one User
                .WithMany() // A User can have many Recipes
                .HasForeignKey(r => r.UserId); // Foreign key is UserId in the Recipe table

            // Seed default roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" }
            );
        }

    }
}