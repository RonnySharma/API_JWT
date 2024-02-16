using API_JWT.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_JWT.Identity
{
    public class ApplicationDbContext: IdentityDbContext<Applicationuser>
    {
        public ApplicationDbContext
      (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
