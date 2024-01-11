using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodApplication.Models;

namespace FoodApplication.ContextDBConfig
{
    public class FoodDBContext : IdentityDbContext<ApplicationUser>
    {
        public FoodDBContext(DbContextOptions<FoodDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Order> orders { get; set; }
    }
}
