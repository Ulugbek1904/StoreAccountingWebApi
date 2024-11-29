using StoreAccountingWebApi.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Brokers.StorageBrokers
{
    public partial class StorageBroker
    {
        DbSet<User> Users { get; set; }

        public ValueTask<User> InsertUserAsync(User user) =>
            InsertAsync(user);

        public IQueryable<User> SelectAllUsers() =>
            SelectAll<User>();

        public ValueTask<User> SelectUserByIdAsync(int id) =>
            SelectByIdAsync<User>(id);

        public ValueTask<User> UpdateUserAsync(User user) =>
            UpdateAsync(user);

        public ValueTask<User> DeleteUserAsync(User user) =>
            DeleteAsync(user);
    }
}
