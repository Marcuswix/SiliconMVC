using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class SubscribeModel
    {
        [Required(ErrorMessage = "A valid email is required")]
        [EmailAddress]
        [Display(Name = "email", Prompt = "Your Email", Order = 0)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;
    }
}
