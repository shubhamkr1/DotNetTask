using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetTaskApp.Data;
using DotNetTaskApp.Areas.Identity.Data;
namespace DotNetTaskApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DotNetTaskAppAuthContextConnection") ?? throw new InvalidOperationException("Connection string 'DotNetTaskAppAuthContextConnection' not found.");
           
            builder.Services.AddDbContext<DotNetTaskAppAuthContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<DotNetTaskAppAuthContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //add support for razor pages
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //razor pages for user authentication and authorization
            app.MapRazorPages();

            app.Run();
        }
    }
}