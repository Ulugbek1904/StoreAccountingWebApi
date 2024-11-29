using StoreAccountingWebApi.Brokers.StorageBrokers;
using StoreAccountingWebApi.Models.Products;
using StoreAccountingWebApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        public UserService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<User> AddUserAsync(User user)
        {
            if (user != null)
            {
                await storageBroker.InsertUserAsync(user);

                return user;
            }
            else
                throw new Exception("user does not be null");
        }

        public IQueryable<User> GetAllUsers()
        {
            IQueryable<User> Users =  storageBroker.SelectAllUsers();

            return Users;
        }

        public async ValueTask<User> GetUserByIdAsync(int id)
        {
            var user = await storageBroker.SelectUserByIdAsync(id);
            if (user == null)
                throw new UserNotFoundException("User Not found");

            return user;
        }

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            var existingUser= await storageBroker.SelectUserByIdAsync(user.UserId);

            if (existingUser == null)
            {
                throw new UserNotFoundException("User not found");
            }

            existingUser.LastName = user.LastName;
            existingUser.FirstName = user.FirstName;
            existingUser.Email = user.Email;
            existingUser.phoneNumber = user.phoneNumber;
            existingUser.Role = user.Role;

            await storageBroker.UpdateUserAsync(existingUser);

            return existingUser;
        }

        public async ValueTask<User> DeleteUserAsync(User user)
        {
            var existingUser = await storageBroker.SelectUserByIdAsync(user.UserId);

            if (existingUser == null)
            {
                throw new UserNotFoundException("User not found");
            }

            await storageBroker.DeleteUserAsync(existingUser);

            return existingUser;
        }
    }
}
