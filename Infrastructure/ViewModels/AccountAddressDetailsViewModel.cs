using Infrastructure.Model;

namespace Infrastructure.ViewModels
{
    public class AccountAddressDetailsViewModel
    {
        public AccountDertailsAddressModel? AddressInfo { get; set; } = new AccountDertailsAddressModel();

        public string? SuccessErrorMessage { get; set; }
    }
}
