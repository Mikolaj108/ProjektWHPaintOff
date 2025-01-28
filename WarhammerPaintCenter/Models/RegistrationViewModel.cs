using System.ComponentModel.DataAnnotations;

namespace WarhammerPaintCenter.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Nick is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string Nick { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid Email")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Enter only 5-20 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
