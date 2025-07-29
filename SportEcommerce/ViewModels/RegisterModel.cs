using System.ComponentModel.DataAnnotations;

namespace SportEcommerce.ViewModels
{
    public class RegisterModel
    {
        // Name
        [Required(ErrorMessage = "Firts name is required.")]
        [StringLength(40, ErrorMessage = "First name cannot exceed 40 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(40, ErrorMessage = "Last name cannot exceed 40 characters.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "First surname is required")]
        [StringLength(40, ErrorMessage = "First surname cannot exceed 40 characters.")]
        [Display(Name = "First Surname")]
        public required string FirstSurname { get; set; } = string.Empty;

        [StringLength(40, ErrorMessage = "Second surname cannot exceed 40 characters.")]
        [Display(Name = "Second Surname")]
        public string? SecondSurname { get; set; }


        // Email
        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public required string Email { get; set; } = string.Empty;

        // Password
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(maximumLength: 30, MinimumLength = 8, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long..")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmation password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password must match.")]
        public required string ConfirmPassword { get; set; } = string.Empty;
    }
}
