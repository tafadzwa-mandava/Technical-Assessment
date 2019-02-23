using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechnicalAssessment.Data.Models;

namespace TechnicalAssessment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<PersonalInformation> PeoplesInformation { get; set; }
        public DbSet<BranchInformation>BranchesInformation { get; set; }

    }
}
