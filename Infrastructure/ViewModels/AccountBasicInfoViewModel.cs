using Infrastructure.Model;

namespace Infrastructure.ViewModels
{
    public class AccountBasicInfoViewModel
    {
        public AccountDetailsModel? BasicInfo { get; set; }

        public string? SuccessErrorMessage { get; set; }

        public bool IsExternalAccount { get; set; }
    }
}
