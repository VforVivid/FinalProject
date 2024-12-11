using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepo;

        public HomeController(IUserRepository userRepo, ILogger<HomeController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> GetUserId()
        {
            if (User.Identity!.IsAuthenticated)
            {
                string username = User.Identity.Name ?? "";
                var user = await _userRepo.ReadByUsernameAsync(username);
                if (user != null)
                {
                    return Content(user.Id);
                }
            }
            return Content("No user");
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetUserName()
        {
            if (User.Identity!.IsAuthenticated)
            {
                string username = User.Identity.Name ?? "";
                return Content(username);
            }
            return Content("No user");
        }

        public async Task<IActionResult> ShowRoles(string userName)
        {
            ApplicationUser? user = await _userRepo.ReadByUsernameAsync(userName);
            StringBuilder builder = new();
            foreach (var rolename in user!.Roles)
            {
                builder.Append(rolename + " ");
            }
            return Content($"UserName: {user.UserName} Roles: {builder}");
        }

        public async Task<IActionResult> HasRole(string userName, string rolename)
        {
            ApplicationUser? user = await _userRepo.ReadByUsernameAsync(userName);
            if (user!.HasRole(rolename))
            {
                return Content($"{userName} has role {rolename}");
            }
            return Content($"{userName} does not have role {rolename}");
        }
    }
}
