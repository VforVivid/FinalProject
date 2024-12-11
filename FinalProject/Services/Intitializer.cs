using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Services
{
    public class Initializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public Initializer(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedUsersAsync()
        {
            _db.Database.EnsureCreated();
            if (!_db.Roles.Any(r => r.Name == "DM"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "DM" });
            }
            if (!_db.Roles.Any(r => r.Name == "Player"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Player" });
            }
            if (!_db.Roles.Any(r => r.Name == "Guest"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Guest" });
            }
            if (!_db.Users.Any(u => u.UserName == "DM@test.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "DM@test.com",
                    UserName = "DM@test.com",
                };
                await _userManager.CreateAsync(user, "Pass123!");
                await _userManager.AddToRoleAsync(user, "DM");
            }
        }

    }
}
