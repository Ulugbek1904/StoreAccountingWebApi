using StoreAccountingWebApi.Models.Users;

namespace StoreAccountingWebApi.Services.Account
{
    public partial class AccountService
    {
        private void ValidateUserExist(User user)
        {
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
        }
    }
}
