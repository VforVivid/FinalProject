using FinalProject.Models.Entities;

namespace FinalProject.Services
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> ReadByUsernameAsync(string username);
        Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);
        Task AssignUserToRoleAsync(string userName, string rolename);
        Task<IQueryable<ApplicationUser>> ReadAllAsync();
    }
}
