using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class DbUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbUserRepository(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> ReadByUsernameAsync(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName ==
            username);
            if (user != null)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            return user;
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
            return user;
        }

        public async Task AssignUserToRoleAsync(string userName, string rolename)
        {
            var roleCheck = await _roleManager.RoleExistsAsync(rolename);
            if (!roleCheck)
            {
                await _roleManager.CreateAsync(new IdentityRole(rolename));
            }
            var user = await ReadByUsernameAsync(userName);
            if (user != null)
            {
                if (!user.HasRole(rolename))
                {
                    await _userManager.AddToRoleAsync(user, rolename);
                }
            }
        }

        public async Task<IQueryable<ApplicationUser>> ReadAllAsync()
        {
            var users = _db.Users;
            // Read the roles for each user in the database
            foreach (var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            return users;
        }

    }
}
