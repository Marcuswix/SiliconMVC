using Infrastructure.Entities;

namespace Infrastructure.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";

        public UserEntity? User { get; set; }

        public bool IsExternalAccount { get; set; }
    }
}
