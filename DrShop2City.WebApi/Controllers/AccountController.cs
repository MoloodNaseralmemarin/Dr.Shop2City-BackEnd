using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DrShop2City.DataLayer.Entities.Account;
using DrShop2City.Infrastructure.DTOs.Account;
using DrShop2City.Infrastructure.Services.Implementations;
using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Common;
using DrShop2City.Infrastructure.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Net.WebRequestMethods;

namespace DrShop2City.WebApi.Controllers
{

    public class AccountController : SiteBaseController
    {
        #region costructor

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Register

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel register)
         
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RegisterUser(register);

                switch (res)
                {
                    case RegisterUserResult.EmailExists:
                        return JsonResponseStatus.Error(new { info = "EmailExist" });
                }
            }

            return JsonResponseStatus.Success();
        }

        #endregion

        #region Login


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel login)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.LoginUser(login);

                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponseStatus.NotFound(new { message = "کاربری با مشخصات وارد شده یافت نشد" });

                    case LoginUserResult.NotActivated:
                        return JsonResponseStatus.Error(new { message = "حساب کاربری شما فعال نشده است" });

                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByEmail(login.Email);
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEshopJwtBearer"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://apidr.shop2city.ir",
                            claims: new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                            },
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: signinCredentials
                        );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                        return JsonResponseStatus.Success(new
                        {
                            token = tokenString,
                            expireTime = 30,
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            userId = user.Id,
                            address = user.Address
                        });
                }
            }

            return JsonResponseStatus.Error();
        }

        #endregion
        #region Check User Authentication

        [HttpPost("check-auth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserByUserId(User.GetUserId());
                return JsonResponseStatus.Success(new
                {
                    userId = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    address = user.Address,
                    email = user.Email,
                });
            }

            return JsonResponseStatus.Error();
        }

        #endregion

        #region edit user account

        [HttpPost("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserViewModel editUser)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userService.EditUserInfo(editUser, User.GetUserId());
                return JsonResponseStatus.Success(new { message = "اطلاعات کاربر با موفقیت ویرایش شد" });
            }

            return JsonResponseStatus.UnAuthorized();
        }

        #endregion
        #region Sign Out


        [HttpGet("sign-out")]
        public async Task<IActionResult> LogOut()
        {

            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.Error();
        }

        #endregion
    }
}