using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartCloud.Common.Models;
using SmartCloud.Common.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;


namespace SmartCloud.Common.HttpApi.Controllers
{
    [ApiController]
    [Route("connect")]
    public class AccountController : AbpController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserAppService _userAppService;

        public AccountController(
            IConfiguration configuration,
            IUserAppService userAppService
        )
        {
            _configuration = configuration;
            _userAppService = userAppService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<ActionResult<string>> Login(LoginDto login)
        {
            string token = "";
            List<Claim> claims = new ();

            #region 验证用户
            var user = await _userAppService.GetAsync(login.Name, login.Password);
            if (user == null) return BadRequest("用户名或者密码不正确!");

            //增加用户
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, login.Name));
            #endregion

            #region  生成jwt
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SigningKey"])),
                    algorithm: SecurityAlgorithms.HmacSha256)
                );
            #endregion

            //生成token
            token = new JwtSecurityTokenHandler().WriteToken(jwt);

            //返回正确的token
            return Ok(token);
        }
    }
}
