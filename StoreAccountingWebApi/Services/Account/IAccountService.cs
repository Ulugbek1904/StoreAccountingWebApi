using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Services.Account
{
    public interface IAccountService
    {
        public ValueTask<string> LoginAsync(string username, string password);
    }
}