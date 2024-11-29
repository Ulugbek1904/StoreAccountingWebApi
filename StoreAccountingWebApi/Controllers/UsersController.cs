using StoreAccountingWebApi.Models.Products;
using StoreAccountingWebApi.Models.Users;
using StoreAccountingWebApi.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using RESTFulSense.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace StoreAccountingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : RESTFulController
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;             
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> GetUsersListAsync()
        {
            IQueryable<User> products = userService.GetAllUsers();

            if (await products.CountAsync() == 0)
                return NoContent();

            return Ok( await products.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> GetUserById(int id)
        {
            try
            {
                var product = await userService.GetUserByIdAsync(id);

                return Ok(product);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async ValueTask<ActionResult<User>> CreateUserAsync(User user)
        {
            var newUser= await userService.AddUserAsync(user);

            return newUser;
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> EditUserAsync(User user)
        {
            try
            {
                User editUser = await
                    userService.UpdateUserAsync(user);

                return Ok(editUser);
            }
            catch (NullProductException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error {ex.Message}");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> DeleteUserAsync(User user)
        {
            try
            {
                User deleteUser= await
                    userService.DeleteUserAsync(user);

                return Ok(deleteUser);
            }
            catch (NullProductException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error {ex.Message}");
            }
        }
    }
}
