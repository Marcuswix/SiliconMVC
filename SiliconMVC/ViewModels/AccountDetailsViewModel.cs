using SiliconMVC.Models;

namespace SiliconMVC.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";

        public AccountDetailsModel BasicInfo { get; set; } = new AccountDetailsModel()
        {
            FirstName = "Hans",
            LastName = "Mattin-Lassie",
            Email = "domaim@soman.se",
        };

        public AccountDertailsAddressModel AddressInfo { get; set; } = new AccountDertailsAddressModel();
    }
}
