using StoreAccountingWebApi.Brokers.Tokens;
using StoreAccountingWebApi.Models.Users;
using StoreAccountingWebApi.Services.UserServices;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Services.Account
{
    public partial class AccountService : IAccountService
    {
        private readonly ITokenBroker tokenBroker;
        private readonly IUserService userService;

        public AccountService(
            ITokenBroker tokenBroker,
            IUserService userService)
        {
            this.tokenBroker = tokenBroker;
            this.userService = userService;
        }
        public async ValueTask<string> LoginAsync(string username, string password)
        {
            IQueryable<User> users = this.userService.GetAllUsers();

            User maybeuser = users.FirstOrDefault(user =>
                user.FirstName == username && user.Password == password);

            ValidateUserExist(maybeuser);

            return await this.tokenBroker.GenerateTokenAsync(maybeuser);
        }
    }
}
