using System.Threading.Tasks;
using DrShop2City.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrShop2City.WebApi.Controllers
{

    public class UsersController : SiteBaseController
    {
        #region constructor

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region users list

        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            return new ObjectResult(await _userService.GetAllUsers());
        }

        #endregion
    }
}