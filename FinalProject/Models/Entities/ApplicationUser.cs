using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public ICollection<string> Roles { get; set; } = new List<string>();
        public bool HasRole(string rolename)
        {
            return Roles.Any(r => r == rolename);
        }
    }
}
