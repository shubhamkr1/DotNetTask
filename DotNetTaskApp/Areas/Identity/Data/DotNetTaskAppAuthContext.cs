using DotNetTaskApp.Areas.Identity.Data;
using DotNetTaskApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetTaskApp.Data;

public class DotNetTaskAppAuthContext : IdentityDbContext<ApplicationUser>
{
    public DotNetTaskAppAuthContext(DbContextOptions<DotNetTaskAppAuthContext> options)
        : base(options)
    {
    }

    public DbSet<UserProfile> userProfiles { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
