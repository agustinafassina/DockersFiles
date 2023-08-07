using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ApiJobOpportunity.Models
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<JobOpportunity> JobOpportunities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}