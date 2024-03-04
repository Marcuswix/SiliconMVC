using Infrastructure.Model;

namespace SiliconMVC.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";

        public AccountDetailsModel BasicInfo { get; set; } = new AccountDetailsModel();

        public AccountDertailsAddressModel AddressInfo { get; set; } = new AccountDertailsAddressModel();
    }
}
