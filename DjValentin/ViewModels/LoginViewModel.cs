using System.ComponentModel.DataAnnotations;

namespace DjValentin.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "remember me")]
        public bool RememberMe { get; set; }
    }
}
