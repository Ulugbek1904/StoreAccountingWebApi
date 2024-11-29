using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using StoreAccountingWebApi.Models.Users;
using StoreAccountingWebApi.Services.Account;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountsController : RESTFulController
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> LoginAsync(
            [FromQuery]string username, 
            [FromQuery]string password)
        {
            try
            {
                var token = await this.accountService.LoginAsync(username, password);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
