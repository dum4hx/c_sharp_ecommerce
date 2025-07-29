using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SportEcommerce.Models.User
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        public bool IsDefault { get; set; } = false;
    }
}
