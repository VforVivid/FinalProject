using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.ViewModels
{
    public class UserListVM
    {
        public string? Email { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        [Display(Name = "Number of roles")]
        public int NumberOfRoles { get; set; }
        [Display(Name = "Role names")]
        public string? RoleNames { get; set; } = string.Empty;
    }
}
