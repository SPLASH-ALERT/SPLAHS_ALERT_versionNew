using System.ComponentModel.DataAnnotations;

namespace splash_alert.Models.ViewModels
{
    public class VRecovery
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
