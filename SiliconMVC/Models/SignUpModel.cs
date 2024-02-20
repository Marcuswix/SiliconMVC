using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Models
{
    public class SignUpModel
    {
        //Prompt funkar som en "placeholder" del.
        [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
        [Required(ErrorMessage = "Invalid first name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
        [Required(ErrorMessage = "Invalid last name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email", Prompt = "Enter your email", Order = 2)]
        [EmailAddress]
        [Required(ErrorMessage = "A email is required")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n", ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?\\\":{}|<>])(?=.*[0-9]).{8,}$\r\n", ErrorMessage = "Invalid password")]
        public string Password { get; set; } = null!;

        [Display(Name = "Confrim password", Prompt = "Confirm your password", Order = 4)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Name = "I Agree to the Terms & Conditions.", Order = 5)]
        [Required(ErrorMessage = "To sign up you must agree to our terms & conditions.")]
        public bool TermsAndConditions { get; set; } = false;

        //[Display(Name = "Address", Prompt = "Enter your Address", Order = 5)]
        //[Required(ErrorMessage = "Incorrect password")]
        //public string Address { get; set; } = null!;

        //[Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 6)]
        //[Required(ErrorMessage = "Incorrect password")]
        //public string PostalCode { get; set; } = null!;

        //[Display(Name = "City", Prompt = "Enter your city", Order = 7)]
        //[Required(ErrorMessage = "Invalid city")]
        //public string City { get; set; } = null!;

    }
}
