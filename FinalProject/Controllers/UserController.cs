using FinalProject.Models.ViewModels;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [Authorize(Roles = "DM")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepo.ReadAllAsync();
            var userList = users
            .Select(u => new UserListVM
            {
                Email = u.Email,
                Username = u.UserName,
                NumberOfRoles = u.Roles.Count,
                RoleNames = string.Join(",", u.Roles.ToArray())
            });
            return View(userList);
        }

    }
}
