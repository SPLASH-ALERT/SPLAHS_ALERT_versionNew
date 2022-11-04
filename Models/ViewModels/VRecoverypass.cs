using System.ComponentModel.DataAnnotations;

namespace splash_alert.Models.ViewModels
{
    public class VRecoverypass
    {
        [Required]

        public string Password { get; set; }
        public string token { get; set; }
        [Compare("Password")]
        [Required]

        public string ConfirmPassword { get; set; }
    }
}
