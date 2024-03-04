using Infrastructure.Model;
namespace SiliconMVC.ViewModels
{
    public class SignInViewModel
    {
        public SignInModel Form { get; set; } = new SignInModel();
        public string? ErrorMessage { get; set; }
    }
}
