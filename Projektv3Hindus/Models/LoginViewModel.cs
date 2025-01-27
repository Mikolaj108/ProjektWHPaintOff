using System.ComponentModel.DataAnnotations;

namespace WarhammerPaintCenter.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = " Nick is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string Nick { get; set; }

        

        [Required(ErrorMessage = "Password name is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Enter only 5-20 characters")]
        public string Password { get; set; }

     
    }
}
