using StoreAccountingWebApi.Models.Users;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Brokers.Tokens
{
    public interface ITokenBroker
    {
        public ValueTask<string> GenerateTokenAsync(User user);
    }
}