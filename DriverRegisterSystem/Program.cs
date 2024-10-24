using DriverRegisterSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DriverRegisterSystem.Models;
using DriverRegisterSystem.Services;
using DotNetEnv;

namespace DriverRegisterSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Env.Load();

            builder.Services.AddDbContext<AppDbContext>(options =>
                           options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));

            builder.Services.AddIdentity<Employee, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            })
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<IDriverRegisterRepository<Driver>, DriverRepository>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();

            builder.Services.AddRazorPages();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication();

            var app = builder.Build();

            //Seed roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<Employee>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await SeedData.Initialize(userManager, roleManager);
            }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
