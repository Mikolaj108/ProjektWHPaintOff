using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WarhammerPaintCenter.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Nick), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " Nick is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string Nick { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string Password { get; set; }



        // Kolekcja farb dodanych przez użytkownika
        public ICollection<Paint> Paints { get; set; }
    }
}
