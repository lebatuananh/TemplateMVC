using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QHomeGroup.Application.Systems.Users;
using QHomeGroup.Application.Systems.Users.Dtos;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Utilities.Constants;
using QHomeGroup.WebApi.Filters;
using QHomeGroup.WebApi.Requests;

namespace QHomeGroup.WebApi.Controllers
{
    [Authorize]
    public class AccountController : V1Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public AccountController(IConfiguration configuration, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAppUserService appUserService)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        [Route("login")]
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
                if (!result.Succeeded)
                    return BadRequest("Mật khẩu không đúng");
                var claims = new[]
                {
                    new Claim("Email", user.Email),
                    new Claim(SystemConstants.UserClaim.Id, user.Id.ToString()),
                    new Claim(SystemConstants.UserClaim.Avatar, user.Avatar ?? string.Empty),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(SystemConstants.UserClaim.FullName, user.FullName??string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                    _configuration["Tokens:Issuer"],
                     claims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return NotFound($"Không tìm thấy tài khoản {request.UserName}");
        }

        [HttpPost]
        [Route("updateToken")]
        public async Task<IActionResult> UpdateToken([FromBody] UpdateTokenRequest request)
        {
            var result = await _appUserService.UpdateToken(request.Token, User?.FindFirst(SystemConstants.UserClaim.Id)?.Value);
            return Ok(result);
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await _appUserService.DeleteToken(User?.FindFirst(SystemConstants.UserClaim.Id)?.Value);
            return Ok();
        }


        [Route("profile")]
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var currentUserId = User?.FindFirst(SystemConstants.UserClaim.Id)?.Value;
            var user = await _appUserService.GetByIdAsync(currentUserId);
            return Ok(user);
        }

        [Route("profile")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUserViewModel request)
        {
            var currentUserId = User?.FindFirst(SystemConstants.UserClaim.Id)?.Value;
            request.Id = Guid.Parse(currentUserId);
            await _appUserService.UpdateAccount(request);
            return Ok();
        }

        [Route("updatePass")]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
        {
            var currentUserId = User?.FindFirst(SystemConstants.UserClaim.Id)?.Value;
            var result = await _appUserService.ChangePassword(currentUserId, request.OldPassword, request.Password);
            return Ok(result);
        }
    }
}