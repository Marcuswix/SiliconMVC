using Infrastructure.Model;

namespace Infrastructure.ViewModels
{
    public class SignInViewModel
    {
        public SignInModel Form { get; set; } = new SignInModel();
        public string? ErrorMessage { get; set; }
    }
}
