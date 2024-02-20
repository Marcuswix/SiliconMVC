using SiliconMVC.Models;

namespace SiliconMVC.ViewModels
{
    public class SignInViewModel
    {
        public string Titel { get; set; } = "Sign in";
        public SignInModel Form { get; set; } = new SignInModel();
        public string? ErrorMessage { get; set; }
    }
}
