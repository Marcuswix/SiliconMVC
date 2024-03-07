using Infrastructure.Entities;
using Infrastructure.Model;

namespace Infrastructure.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";

        public UserEntity? User { get; set; } = new UserEntity();

        public AccountDetailsModel? BasicInfo {  get; set; } = new  AccountDetailsModel();

        public AccountDertailsAddressModel? AddressInfo { get; set; } = new AccountDertailsAddressModel();

        public string? SuccessErrorMessage { get; set; }
    }
}
